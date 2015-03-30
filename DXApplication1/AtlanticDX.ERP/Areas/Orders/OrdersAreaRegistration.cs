using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Orders
{
    public class OrdersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Orders";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Orders_default",
                "Orders/{controller}/{action}/{id}",
                new {area="Orders", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}