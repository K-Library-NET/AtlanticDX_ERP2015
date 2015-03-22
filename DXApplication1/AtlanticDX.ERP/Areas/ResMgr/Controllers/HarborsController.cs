using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AtlanticDX.ERP;
using YuShang.ERP.Entities.ResMgr;
using System.Threading.Tasks;
using PrivilegeFramework;
using UtilityFramework;

namespace AtlanticDX.ERP.Areas.ResMgr.Controllers
{
    public class HarborsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                var list = db.Harbors.Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.HarborId).Skip((page - 1) * rows).Take(rows).ToList();

                return Json(new { total = db.Harbors.Count(), rows = list });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(Harbor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Harbors.Add(model);
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_HARBORS);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("HarborsController.Add", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Harbor model)
        {
            if (ModelState.IsValid)
            {
                Harbor current = db.Harbors.SingleOrDefault(
                    m => m.HarborId == model.HarborId && m.IsDeleted == false);

                if (current == null)
                {
                    db.Harbors.Add(model);
                }
                else
                {
                    current.HarborKey = model.HarborKey;
                    current.HarborName = model.HarborName;
                    current.HarborNameENG = model.HarborNameENG;
                }

                try
                {
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_HARBORS);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("HarborsController.Edit", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(Harbor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Harbor current = db.Harbors.SingleOrDefault(
                        p => p.HarborId == model.HarborId);
                    if (current == null)
                    {
                        ModelState.AddModelError(string.Empty, "港口不存在");
                    }
                    else
                    {
                        current.IsDeleted = true;
                        db.SaveChanges();

                        //Task.Run(() =>
                        //{
                        ResBusinessManager.Instance.ClearCache(
                            ResBusinessManager.RESOURCES_HARBORS);
                        //});
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("HarborsController.Remove", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        // GET: Harbors
        public ActionResult Index()
        {
            return View(db.Harbors.ToList());
        }

        // GET: Harbors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Harbor harbor = db.Harbors.Find(id);
            if (harbor == null)
            {
                return HttpNotFound();
            }
            return View(harbor);
        }

        // GET: Harbors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Harbors/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HarborId,HarborKey,HarborName")] Harbor harbor)
        {
            if (ModelState.IsValid)
            {
                db.Harbors.Add(harbor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(harbor);
        }

        // GET: Harbors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Harbor harbor = db.Harbors.Find(id);
            if (harbor == null)
            {
                return HttpNotFound();
            }
            return View(harbor);
        }

        /*
        // POST: Harbors/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HarborId,HarborKey,HarborName")] Harbor harbor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(harbor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(harbor);
        }*/

        // GET: Harbors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Harbor harbor = db.Harbors.Find(id);
            if (harbor == null)
            {
                return HttpNotFound();
            }
            return View(harbor);
        }

        // POST: Harbors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Harbor harbor = db.Harbors.Find(id);
            db.Harbors.Remove(harbor);
            db.SaveChanges();
            return RedirectToAction("Index");
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
