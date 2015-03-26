using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Sales.Controllers
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