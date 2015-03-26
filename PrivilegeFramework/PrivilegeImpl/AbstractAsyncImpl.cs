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
    internal abstract class AbstractAsyncImpl : IPrivilegeAsyncImpl
    {
        public AbstractAsyncImpl(IUserManagerImplWrap userManagerImpl, UserManager<SysUser, int> userManager)
        {
            this.UserManager = userManager;
            this.UserManagerImpl = userManagerImpl;
        } 

        public CoreActionEventHandler CoreAction
        {
            get { return this.CoreAsync; }
        }

        public BuildDbUpdateConcurrencyExceptionFormatStringEventHandler DbUpdateConcurrencyExceptionFormatStringBuilder
        {
            get { return this.StringHandler1; }
        }

        public BuildErrorFormatStringEventHandler ErrorFormatStringBuilder
        {
            get { return this.StringHandler2; }
        }

        internal abstract string StringHandler1(object[] paras, DbUpdateConcurrencyException db);

        internal abstract string StringHandler2(object[] paras);

        internal abstract IdentityResult CoreAsync(object[] paras);

        internal UserManager<SysUser, int> UserManager { get; set; }

        internal IUserManagerImplWrap UserManagerImpl { get; set; }
    }
}
