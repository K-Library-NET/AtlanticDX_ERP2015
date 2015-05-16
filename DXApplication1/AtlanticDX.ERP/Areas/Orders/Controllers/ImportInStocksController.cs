using PrivilegeFramework;
using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Orders.Controllers
{
    [ComplexAuthorize]
    public class ImportInStocksController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: ImportInStocks
        public ActionResult Index()
        {
            return View();
        }
    }
}