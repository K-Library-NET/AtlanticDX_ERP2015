using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Privileges;

namespace PrivilegeFramework
{
    // 配置要在此应用程序中使用的应用程序登录管理器。
    public class ApplicationSignInManager : SignInManager<SysUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
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
    }
}
