using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Privileges;
using YuShang.ERP.Entities.Sale;

namespace PrivilegeFramework.PrivilegeFilters
{
    internal class BasicSaleContractEntityPrivilegeStrategy
    {
        /// <summary>
        /// 简单一点，只对权限数据加入数据Level的限制
        /// </summary>
        /// <param name="db"></param>
        /// <param name="entityControlType"></param>
        /// <param name="tempDbQuery"></param>
        /// <param name="userName"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        internal IQueryable<SaleContract> AddEntityControlCondition(
            ExtendedIdentityDbContext db, EntityControlType entityControlType,
            IQueryable<SaleContract> tempDbQuery, string userName, IList<string> roles)
        {
            //int level = PrivilegeLevelByEntityControlType.GetDefaultPrivilegeLevelByEntityControlType(entityControlType);
            int selfLevel = PrivilegeManager.GetSelfPrivilegeLevelByEntityControlType(
                entityControlType, userName, roles, db);
            tempDbQuery = tempDbQuery.Where(m => ((m.OperatorSysUser == userName && m.EntityPrivLevRequired <= selfLevel)
                 || m.EntityPrivLevRequired < selfLevel));
            //简单的规则就是：一般的用户只能看到小于自己数据权限级别的数据；
            //或者自己录入的数据但是权限小于等于自己的

            return tempDbQuery;
        }
    }
}
