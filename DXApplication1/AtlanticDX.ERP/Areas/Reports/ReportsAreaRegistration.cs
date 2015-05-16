using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Reports
{
    public class ReportsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Reports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Reports_default",
                "Reports/{controller}/{action}/{id}",
                new { area = "Reports", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}