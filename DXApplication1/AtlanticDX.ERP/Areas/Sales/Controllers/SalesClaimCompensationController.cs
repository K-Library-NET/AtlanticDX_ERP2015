using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrivilegeFramework;
namespace AtlanticDX.ERP.Areas.Sales.Controllers
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