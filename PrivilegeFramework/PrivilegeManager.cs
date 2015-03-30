using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using PrivilegeFramework.PrivilegeFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.Privileges;
using YuShang.ERP.Entities.Sale;

namespace PrivilegeFramework
{
    public class PrivilegeManager
    {
        private ExtendedIdentityDbContext m_dbContext = null;
        //ExtendedIdentityDbContext.Create();                                                                                                             
        private ApplicationUserManager m_userManager = null;

        protected PrivilegeManager()
        {
            m_dbContext = ExtendedIdentityDbContext.Create();

            //this.m_userManager = new PrivilegeFramework.ApplicationUserManager(
            //    new UserStore<YuShang.ERP.Entities.Privileges.SysUser, SysRole,
            //        int, SysUserLogin, SysUserRole, SysUserClaim>(
            //        m_dbContext), m_dbContext);
        }

        private static PrivilegeManager m_instance = null;

        public static PrivilegeManager Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new PrivilegeManager();

                return m_instance;
            }
        }

        public bool IsAuthorized(string userName, string area,
            string controller, string action)
        {//debug
            return true;//以防万一，必须先设定，不然登录不了
            IQueryable<SysRole> roleObjsQuery = this.GetRoleByUserName(userName);

            if (roleObjsQuery != null && roleObjsQuery.Count() > 0)
            {
                IQueryable<InheritedPrivilegeLevelRelation> relationlist
                    = this.GetFunctionRelations(area, controller, action);

                if (relationlist != null && relationlist.Count() > 0)
                {
                    var roleList = roleObjsQuery.ToList();
                    int maxLevel = roleList.Max(m => m.PrivilegeLevel);
                    var tempList = relationlist.ToArray();

                    foreach (var relation in tempList)
                    {//同一个功能配置了多条权限路径，任意找到一条就可以
                        if (relation.RoleId.HasValue)
                        {//指定了最低的角色
                            //查看哪个角色等于或者属于这个最低角色的祖先（上级节点），如果有则认为是true
                            bool selfOrAncestor = this.IsSelfOrAncestorOf(relation.RoleId.Value, roleList);
                            if (selfOrAncestor)
                                return true;
                        }
                        else
                        {//不指定角色的 
                            int levelRequired = relation.LevelRequired;
                            //只要某个角色达到最低等级要求，那就可以了
                            if (maxLevel >= levelRequired)
                                return true;
                        }
                    }
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断列表中的角色任意一个的Id是否传入的relationRoleId相等，
        /// 或者列表中的角色是否属于relationRoleId对应角色的上级
        /// </summary>
        /// <param name="relationRoleId"></param>
        /// <param name="roleList"></param>
        /// <returns></returns>
        private bool IsSelfOrAncestorOf(int relationRoleId, List<SysRole> roleList)
        {
            if (roleList != null && roleList.Count > 0)
            {
                if (roleList.Any(m => m.Id == relationRoleId))
                    return true;//相等的情况下

                //取得指定的SysRole的所有上级对象
                List<SysRole> ancestors = this.AncestorOf(relationRoleId);
                if (ancestors != null && ancestors.Count > 0)
                {
                    //对比roleList中是否任何一个满足条件
                    var idlist = ancestors.Select(m => m.Id);
                    if (roleList.Any(role => idlist.Contains(role.Id)))
                        return true;
                }
            }
            return false;
        }

        private List<SysRole> AncestorOf(int relationRoleId)
        {
            List<SysRole> roles = new List<SysRole>();

            SysRole role = this.m_dbContext.Roles.Find(relationRoleId);
            while (role != null)
            {
                roles.Add(role);
                role = this.m_dbContext.Roles.Find(role.ParentId);
            }

            return roles;
        }

        private IQueryable<InheritedPrivilegeLevelRelation> GetFunctionRelations(
            string area, string controller, string action)
        {
            return m_dbContext.InherPrivLevRelations.AsNoTracking().Where(
                m => m.Area == area && m.Controller == controller
                    && m.Action == action);
        }

        private IQueryable<SysRole> GetRoleByUserName(string userName)
        {
            var userTask = m_userManager.FindByNameAsync(userName);
            userTask.Wait();
            SysUser user = userTask.Result;
            if (user != null)
            {
                var taskRoleList = m_userManager.GetRolesAsync(user.Id);
                taskRoleList.Wait();

                var roleList = taskRoleList.Result;

                if (roleList != null && roleList.Count > 0)
                {
                    IQueryable<SysRole> roles = m_dbContext.Roles.Where(
                        m => roleList.Contains(m.Name));
                    //根据用户寻找角色，这些角色是有Parent的，应该找到所有没有Parent的角色，或者干脆这里不判断
                    return roles;
                }
            }

            return null;
        }

        //public PrivilegeManager(Microsoft.Owin.IOwinContext owinContext)
        //{
        //    this._owinContext = owinContext;
        //}

        //private ApplicationSignInManager _signInManager;
        //private Microsoft.Owin.IOwinContext _owinContext;

        //internal ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager;
        //        // ?? _owinContext.Get<ApplicationSignInManager>();
        //        //.Get<ApplicationSignInManager>();
        //    }
        //    set { _signInManager = value; }
        //}

        public bool IsHighLevelPrivilege(IOwinContext owinContext, string userName,
            YuShang.ERP.Entities.Privileges.EntityControlType type, out IList<string> roles)
        {//FIXED: 判断数据权限
            bool result = false;
            IList<string> userRoles = null;

            IList<string> highLevelRoleNames = this.GetHighLevelRoleNameByConfig(type);

            if (owinContext != null)
            {
                var ttmp = owinContext.GetUserManager<ApplicationUserManager>();
                if (ttmp != null)
                    m_userManager = ttmp;
            }

            // m_userManager = owinContext.GetUserManager<ApplicationUserManager>();
            var taskUserName = m_userManager.FindByNameAsync(userName);
            taskUserName.Wait();
            var user = taskUserName.Result;

            if (user != null)
            {
                var task = m_userManager.GetRolesAsync(user.Id);
                task.Wait();
                if (task.Result != null)
                {
                    userRoles = task.Result;
                    if (userRoles.Intersect(highLevelRoleNames).Count() > 0)
                    {//当用户角色与高级别角色之间交集大于0个，则说明是高权限角色
                        roles = userRoles;
                        return true;
                    }
                }
                //m_userManager.GetRolesAsync().Result.Intersect(highLevelRoleNames).Count() > 0;
                //int userid = user.Id;
                //foreach (var hi in highLevelRoleNames)
                //{
                //    var ttask = m_userManager.IsInRoleAsync(userid, hi);
                //    ttask.Wait();
                //    if (ttask.Result)
                //    {
                //        result = true;
                //        break;
                //    }
                //}

                //Parallel.ForEach(highLevelRoleNames, (s =>
                //{
                //    var tempTask = m_userManager.IsInRoleAsync(userid, s);
                //    tempTask.Wait();
                //    if (tempTask.Result)
                //    {
                //        result = true;
                //    }
                //}));

                //var task = m_userManager.GetRolesAsync(userid);
                //task.Wait();
                //if (task.Result != null)
                //    userRoles = task.Result;
            }

            roles = userRoles;
            return result;
        }

        private IList<string> GetHighLevelRoleNameByConfig(EntityControlType type)
        {
            return new List<string>(new string[] { "系统管理员", "Boss" });
        }

        public IQueryable//<YuShang.ERP.Entities.Orders.OrderContract>
            BuildQueryWithEntityControl(IOwinContext owinContext,
            ExtendedIdentityDbContext db, IQueryable tempDbQuery,
            //IQueryable<YuShang.ERP.Entities.Orders.OrderContract> tempDbQuery,
            EntityControlType entityControlType, string userName)
        {
            IList<string> roles = null;
            bool highLevelPeople = PrivilegeManager.Instance.IsHighLevelPrivilege(
              owinContext, userName, entityControlType, out roles);

            if (highLevelPeople == false && roles != null && roles.Count > 0)
            {//需要加入条件，控制访问者只能看到其下级的数据 
                if (entityControlType == EntityControlType.OrderContract)
                {
                    BasicOrderContractEntityPrivilegeStrategy strategy
                        = this.GetOrderContractEntityTypeStrategy();

                    return strategy.AddEntityControlCondition(db, entityControlType,
                        tempDbQuery as IQueryable<OrderContract>, userName, roles);
                }
                else if (entityControlType == EntityControlType.SaleContract)
                {
                    BasicSaleContractEntityPrivilegeStrategy strategy
                        = this.GetSaleContractEntityTypeStrategy();

                    return strategy.AddEntityControlCondition(db, entityControlType,
                        tempDbQuery as IQueryable<SaleContract>, userName, roles);
                }
            }

            return tempDbQuery;
        }

        private BasicSaleContractEntityPrivilegeStrategy GetSaleContractEntityTypeStrategy()
        {
            return new BasicSaleContractEntityPrivilegeStrategy();
        }

        private BasicOrderContractEntityPrivilegeStrategy
            GetOrderContractEntityTypeStrategy()
        {
            return new BasicOrderContractEntityPrivilegeStrategy();
        }

        public int GetEntityPrivilegeLevel(string userName)
        {
            var user = m_dbContext.Users.FirstOrDefault(m => m.UserName == userName);
            if (user != null)
            {
                var roleList = user.Roles.Select(m => m.RoleId);
                if (roleList != null && roleList.Count() > 0)
                {
                    IQueryable<SysRole> roles = m_dbContext.Roles.Where(
                        m => roleList.Contains(m.Id));
                    if (roles != null && roles.Count() > 0)
                        return roles.Max(m => m.PrivilegeLevel);
                }
            }

            return PrivilegeLevelByEntityControlType.DEFAULT_LEVEL_ORDER_CONTRACT;
        }

        internal static int GetSelfPrivilegeLevelByEntityControlType(EntityControlType entityControlType,
            string userName, IList<string> roles, ExtendedIdentityDbContext db)
        {
            return db.Roles.Where(m => roles.Contains(m.Name)).Max(m => m.PrivilegeLevel);

            //return PrivilegeLevelByEntityControlType.GetDefaultPrivilegeLevelByEntityControlType(entityControlType);
        }
    }
}
