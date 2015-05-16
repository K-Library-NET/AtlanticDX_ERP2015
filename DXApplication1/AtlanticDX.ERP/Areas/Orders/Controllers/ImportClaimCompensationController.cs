using PrivilegeFramework;
using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Orders.Controllers
{
    [ComplexAuthorize]
    public class ImportClaimCompensationController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        // GET: ImportClaimCompensation
        public ActionResult Index()
        {
            return View();
        }
    }
}