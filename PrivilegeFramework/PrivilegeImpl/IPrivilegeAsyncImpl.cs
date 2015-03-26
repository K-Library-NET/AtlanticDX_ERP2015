using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Privileges;

namespace PrivilegeFramework.PrivilegeImpl
{
    internal interface IPrivilegeAsyncImpl
    {
        CoreActionEventHandler CoreAction { get; }

        BuildDbUpdateConcurrencyExceptionFormatStringEventHandler
            DbUpdateConcurrencyExceptionFormatStringBuilder { get; }

        BuildErrorFormatStringEventHandler ErrorFormatStringBuilder { get; }
    }

    internal interface IUserManagerImplWrap
    {
        /// <summary>
        /// 调用基础类的AddToRoleAsync
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        IdentityResult BaseAddToRole(int userId, string roleName);

        /// <summary>
        /// 调用基础类的CreateAsync
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        IdentityResult BaseCreate(SysUser user, string password);
    }
}
