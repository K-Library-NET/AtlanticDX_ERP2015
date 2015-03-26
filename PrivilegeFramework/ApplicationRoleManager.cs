using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Privileges;

namespace PrivilegeFramework
{
    public class ApplicationRoleManager : RoleManager<SysRole, int>
    {
        public ApplicationRoleManager(IRoleStore<SysRole, int> roleStore)
            : base(roleStore)
        {
        } 

        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<SysRole,int,SysUserRole>(
                context.Get<ExtendedIdentityDbContext>()));
        }

        public ApplicationRoleManager(ExtendedIdentityDbContext context)
            : base(new RoleStore<SysRole, int, SysUserRole>(
                context))
        {
            this.DbContext = context;
        }

        public IRoleStore<SysRole, int> RoleStore
        {
            get
            {
                return this.Store;
            }
        }

        public ExtendedIdentityDbContext DbContext { get; set; }
    }
}
