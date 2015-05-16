using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using YuShang.ERP.Entities.Configs;

namespace AtlanticDX.Model.Areas.Configs.Controllers
{
    public class SysConfigsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        // GET: SysConfigs
        public ActionResult Index()
        {
            ViewBag.ConfigTypes = YuShang.ERP.Entities.Configs.ConfigType.ConfigTypes.Select(m => new SelectListItem { Text = m.ConfigTypeValue, Value = m.ConfigTypeKey });
            return View();
        }

        [HttpPost]
        public JsonResult Index(int page=1,int rows=10)
        {
            var list = db.CoreConfigs.OrderBy(m=>m.ConfigTypeKey).Skip((page - 1) * rows).Take(rows).ToList();
            var data = new { total = db.CoreConfigs.Count(), rows = list };
            return Json(data);
        }

        [HttpPost]
        public JsonResult Details(int? id)
        {
            CoreConfig coreConfig = db.CoreConfigs.Find(id);
            return Json(coreConfig);
        }

        [HttpPost]
        public JsonResult Add(CoreConfig coreConfig)
        {
            if (ModelState.IsValid)
            {
                db.CoreConfigs.Add(coreConfig);
                db.SaveChanges();
            }
            var errors = this.GetModelStateErrors(ModelState);
            return Json(errors);
        }

        [HttpPost]
        public JsonResult Edit(CoreConfig coreConfig)
        {
            if (ModelState.IsValid)
            { 
                    db.Entry(coreConfig).State = EntityState.Modified;
                    db.SaveChanges();
            }
            var errors = this.GetModelStateErrors(ModelState);
            return Json(errors);
        }
        
        [HttpPost]
        public JsonResult Remove(CoreConfig model)
        {
            if (model != null && model.CoreConfigId > 0)
            {
                CoreConfig coreConfig = db.CoreConfigs.Find(model.CoreConfigId);
                db.CoreConfigs.Remove(coreConfig);
                db.SaveChanges();
            }
            else {
                ModelState.AddModelError("", "");
            }
            var errors = this.GetModelStateErrors(ModelState);
            return Json(errors);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
