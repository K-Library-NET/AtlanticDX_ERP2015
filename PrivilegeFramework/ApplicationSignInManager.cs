using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Privileges;

namespace PrivilegeFramework
{
    // 配置要在此应用程序中使用的应用程序登录管理器。
    public class ApplicationSignInManager : SignInManager<SysUser, int>
    {
        private ExtendedIdentityDbContext _db;

        public ApplicationSignInManager(ApplicationUserManager userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            this._db = userManager.DbContext;
            this._db.Configuration.ProxyCreationEnabled = false;
            this._db.Configuration.LazyLoadingEnabled = false;
        }

        public async override Task<ClaimsIdentity> CreateUserIdentityAsync(SysUser user)
        {
            var manager = (ApplicationUserManager)UserManager;
            var userIdentity = await manager.CreateIdentityAsync(user,
                Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
            //return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public override Task SignInAsync(SysUser user, bool isPersistent, bool rememberBrowser)
        {
            if (this._db != null)
                (this._db as IObjectContextAdapter).ObjectContext.AcceptAllChanges();
            return base.SignInAsync(user, isPersistent, rememberBrowser);
        }

        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (this._db != null)
            {
                this._db.Configuration.ProxyCreationEnabled = false;
                this._db.Configuration.LazyLoadingEnabled = false;

                (this._db as IObjectContextAdapter).ObjectContext.DetectChanges();

                foreach(var v in this._db.ChangeTracker.Entries())
                {
                    System.Diagnostics.Debug.WriteLine(v.Entity.GetType().ToString()
                        + "  " +v.State.ToString());
                }
                
                (this._db as IObjectContextAdapter).ObjectContext.AcceptAllChanges();
            }

            (this._db as IObjectContextAdapter).ObjectContext.DetectChanges();
            var entries = this._db.ChangeTracker.Entries();
            if (entries != null && entries.Count() > 0)
            {
                System.Diagnostics.Debug.WriteLine("test: " + entries.Count());
            }
            return base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }
    }
}
