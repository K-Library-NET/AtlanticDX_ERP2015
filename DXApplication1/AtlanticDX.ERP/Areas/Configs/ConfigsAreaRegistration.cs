using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Configs.Controllers
{
    public class ConfigsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Configs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Configs_default",
                "Configs/{controller}/{action}/{id}",
                new { area = "Configs", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}