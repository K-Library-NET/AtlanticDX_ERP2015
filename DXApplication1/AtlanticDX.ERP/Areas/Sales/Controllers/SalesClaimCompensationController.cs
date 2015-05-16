using System.Web.Mvc;
using PrivilegeFramework;
namespace AtlanticDX.Model.Areas.Sales.Controllers
{
    [ComplexAuthorize]
    public class SalesClaimCompensationController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: SalesClaimCompensation
        public ActionResult Index()
        {
            return View();
        }
    }
}