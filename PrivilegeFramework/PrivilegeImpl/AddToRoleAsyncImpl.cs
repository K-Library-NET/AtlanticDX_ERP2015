using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Privileges;

namespace PrivilegeFramework.PrivilegeImpl
{
    class AddToRoleAsyncImpl : AbstractAsyncImpl
    {
        public AddToRoleAsyncImpl(IUserManagerImplWrap userManagerImpl, UserManager<SysUser, int> userManager)
            : base(userManagerImpl, userManager)
        {
        }

        internal override string StringHandler1(object[] paras, DbUpdateConcurrencyException db)
        {
            return string.Format("添加用户ID {0} 到角色 {1} 时出错："
             + db.Message + "\t\t", paras[0], paras[1]);
        }

        internal override string StringHandler2(object[] paras)
        {
            return string.Format(
                "添加用户ID {0} 到角色 {1} 时出错，有可能是无法解决DbUpdateConcurrencyException导致。"
               , paras[0], paras[1]);
        }

        internal override IdentityResult CoreAsync(object[] paras)
        {
            if (paras != null && paras.Length >= 2)
            {
                int userId = Convert.ToInt32(paras[0]);
                string role = paras[1].ToString();

                var tmp = UserManager.IsInRole(userId, role);
                //如果本来已经是在角色之内，那么直接返回True
                if (tmp)
                    return IdentityResult.Success;

                return UserManagerImpl.BaseAddToRole(userId, role);
            }
            return IdentityResult.Failed("AddToRoleCoreAsync参数不正确。");
        }
    }
}
