using PrivilegeFramework;
using System.Linq;
using System.Web.Mvc;

namespace AtlanticDX.Model.Areas.Configs.Controllers
{
    [ComplexAuthorize]
    public class RoleMgmtController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        AtlanticDXContext db = new AtlanticDXContext();

        // GET: RoleMgmt
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            ApplicationRoleManager manager = new ApplicationRoleManager(db);

            var queryable = manager.Roles.OrderByDescending(m => m.PrivilegeLevel)
                 .Skip((page - 1) * rows).Take(rows);

            return Json(new
            {
                total = manager.Roles.Count(),
                rows = queryable.ToList()
            });
        }

        [HttpPost]
        public JsonResult Add()
        {
            if (ModelState.IsValid)
            {
                ApplicationRoleManager manager = new ApplicationRoleManager(db);


                //TODO 添加角色
            }
            var allErrors = this.GetModelStateErrors(ModelState);
            return Json(allErrors);
        }

        [HttpPost]
        public JsonResult Edit()
        {
            if (ModelState.IsValid)
            {
                ApplicationRoleManager manager = new ApplicationRoleManager(db);
                //TODO 编辑角色
            }
            var allErrors = this.GetModelStateErrors(ModelState);
            return Json(allErrors);
        }


        [HttpPost]
        public JsonResult Remove()
        {
            if (ModelState.IsValid)
            {
                ApplicationRoleManager manager = new ApplicationRoleManager(db);
                //TODO 删除角色
            }
            var allErrors = this.GetModelStateErrors(ModelState);
            return Json(allErrors);
        }
    }
}