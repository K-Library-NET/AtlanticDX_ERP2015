using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Public
{
    public class PublicAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Public";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Public_default",
                "Public/{controller}/{action}/{id}",
                new { area = "Public", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}