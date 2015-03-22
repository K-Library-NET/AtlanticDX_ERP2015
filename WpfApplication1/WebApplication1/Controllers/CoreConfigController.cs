using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WpfApplication1;

namespace WebApplication1.Controllers
{
    public class CoreConfigController : Controller
    {
        // GET: CoreConfig
        public ActionResult Index()
        {
            this.ViewBag.Message = "ViewBag 消息";
            this.ViewBag.Title = "配置设置";

            using (WXPstudioDbContext context = new WXPstudioDbContext())
            {
                var items = context.CoreAccounts.Take(10);
                //throw new Exception("Try to log exceiton");
                return View(items);
            }
        }
    }
}