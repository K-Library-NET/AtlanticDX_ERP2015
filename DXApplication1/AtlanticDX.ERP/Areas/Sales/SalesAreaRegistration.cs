using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Sales
{
    public class SalesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Sales";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Sales_default",
                "Sales/{controller}/{action}/{id}",
                new { area = "Sales", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}