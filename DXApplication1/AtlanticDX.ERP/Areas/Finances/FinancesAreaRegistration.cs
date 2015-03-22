using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Finances
{
    public class FinancesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Finances";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Finances_default",
                "Finances/{controller}/{action}/{id}",
                new { area = "Finances", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}