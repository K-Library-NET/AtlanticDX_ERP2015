using PrivilegeFramework;
using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Sales.Controllers
{
    [ComplexAuthorize]
    public class SalesReturnsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: SalesReturns
        public ActionResult Index()
        {
            return View();
        }
    }
}