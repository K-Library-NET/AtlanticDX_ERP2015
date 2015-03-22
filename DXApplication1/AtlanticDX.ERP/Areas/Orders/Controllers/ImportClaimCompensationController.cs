using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Orders.Controllers
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