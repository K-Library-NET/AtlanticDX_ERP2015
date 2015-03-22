using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PrivilegeFramework
{
    /// <summary>
    /// 复杂的权限控制Attribute
    /// </summary>
    public class ComplexAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (this.PrivilegeManagerAuthorized == false)
                return false;
            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var userName = filterContext.HttpContext.User.Identity.Name;
            var area = filterContext.RouteData.Values["area"].ToString();
            var controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString();

            bool authorized = PrivilegeManager.Instance.IsAuthorized(
                userName, area, controller, action);

            if ("Public".Equals(area, StringComparison.InvariantCultureIgnoreCase))
            {
                this.PrivilegeManagerAuthorized = true;
            }
            else
                this.PrivilegeManagerAuthorized = authorized;

            base.OnAuthorization(filterContext);
        }

        protected bool PrivilegeManagerAuthorized { get; set; }
    }
}
