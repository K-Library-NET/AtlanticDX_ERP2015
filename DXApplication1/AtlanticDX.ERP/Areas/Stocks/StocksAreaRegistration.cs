using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Stocks
{
    public class StocksAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Stocks";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Stocks_default",
                "Stocks/{controller}/{action}/{id}",
                new { area = "Stocks", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}