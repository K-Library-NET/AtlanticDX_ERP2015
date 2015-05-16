using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.ResMgr
{
    public class ResMgrAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ResMgr";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ResMgr_default",
                "ResMgr/{controller}/{action}/{id}",
                new { area = "ResMgr", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}