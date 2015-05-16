using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using YuShang.ERP.Entities.Privileges;

namespace YuShang.ERP
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入电子邮件服务可发送电子邮件。
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入 SMS 服务可发送短信。
            return Task.FromResult(0);
        }
    }

    // 配置此应用程序中使用的应用程序用户管理器。UserManager 在 ASP.NET Identity 中定义，并由此应用程序使用。
    public class ApplicationUserManager : UserManager<SysUser, int>
    {
        public ApplicationUserManager(IUserStore<SysUser, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var db = context.Get<PrivilegeFramework.ExtendedIdentityDbContext>();
            var manager = new ApplicationUserManager(
                new UserStore<SysUser, SysRole,
                    int, SysUserLogin, SysUserRole, SysUserClaim>(
                    db));
            //context.Get<ApplicationDbContext>()));

            // 配置用户名的验证逻辑
            manager.UserValidator = new UserValidator<SysUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // 配置用户锁定默认值
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并将其插入到此处。
            manager.RegisterTwoFactorProvider("电话代码", new PhoneNumberTokenProvider<SysUser, int>
            {
                MessageFormat = "你的安全代码是 {0}"
            });
            manager.RegisterTwoFactorProvider("电子邮件代码", new EmailTokenProvider<SysUser, int>
            {
                Subject = "安全代码",
                BodyFormat = "你的安全代码是 {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<SysUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(SysUser user)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await this.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentity(SysUser user)
        {
            var userIdentity = await this.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    // 配置要在此应用程序中使用的应用程序登录管理器。
    public class ApplicationSignInManager : SignInManager<SysUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public async override Task<ClaimsIdentity> CreateUserIdentityAsync(
            SysUser user)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await this.UserManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
            //return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
