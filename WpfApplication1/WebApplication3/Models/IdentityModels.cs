using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Security.Principal;

namespace WebApplication3.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("defaultConnStr", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class TestEntity2DbContext : TestEntity2.TestEntityDbContext
    {

    }

    public class CustomAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        //public string UsersConfigKey { get; set; }
        //public string RolesConfigKey { get; set; }


        //protected virtual CustomPrincipal CurrentUser
        //{
        //    get { return HttpContext.Current.User.Identity. as CustomPrincipal; }
        //}

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (filterContext.HttpContext.User == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                }

                //var authorizedUsers = ConfigurationManager.AppSettings[UsersConfigKey];
                //var authorizedRoles = ConfigurationManager.AppSettings[RolesConfigKey];

                //Users = String.IsNullOrEmpty(Users) ? authorizedUsers : Users;
                //Roles = String.IsNullOrEmpty(Roles) ? authorizedRoles : Roles;

                //if (!String.IsNullOrEmpty(Roles))
                //{
                //    if (!CurrentUser.IsInRole(Roles))
                //    {
                //        filterContext.Result = new RedirectToRouteResult(new
                //        RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));

                //        // base.OnAuthorization(filterContext); //returns to login url
                //    }
                //}

                //if (!string.IsNullOrEmpty(Users))
                //{
                //    if (!Users.Contains(CurrentUser.UserId.ToString()))
                //    {
                //        filterContext.Result = new RedirectToRouteResult(new
                //        RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));

                //        // base.OnAuthorization(filterContext); //returns to login url
                //    }
                //}
            }
        }
    }

    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] roles { get; set; }
    }
}