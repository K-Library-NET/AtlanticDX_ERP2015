using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Orders.Controllers
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
    }
}