using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Privileges;

namespace PrivilegeFramework.PrivilegeImpl
{
    class CreateUserAsyncImpl : AbstractAsyncImpl
    {
        public CreateUserAsyncImpl(IUserManagerImplWrap userManagerImpl, UserManager<SysUser, int> userManager)
            : base(userManagerImpl, userManager)
        {

        }

        internal override string StringHandler1(object[] paras, System.Data.Entity.Infrastructure.DbUpdateConcurrencyException db)
        {
            return string.Format("添加用户ID {0} 时出错："
             + db.Message + "\t\t", (paras[0] as SysUser).UserName);
        }

        internal override string StringHandler2(object[] paras)
        {
            return string.Format(
                "添加用户 {0} 时出错，有可能是无法解决DbUpdateConcurrencyException导致。"
               , (paras[0] as SysUser).UserName);
        }

        internal override IdentityResult CoreAsync(object[] paras)
        {
            if (paras != null && paras.Length >= 2)
            {
                SysUser user = paras[0] as SysUser;
                string password = paras[1].ToString();

                var tmp = UserManager.FindByName(user.UserName);
                //如果本来已经是在SysUser之内，那么直接返回True
                if (tmp != null)
                    return IdentityResult.Success;

                return UserManagerImpl.BaseCreate(user, password);
            }
            return IdentityResult.Failed("AddToRoleCoreAsync参数不正确。");
        }
    }
}
