using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlanticDX.ERP.Areas.Orders.Controllers
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