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
    public class StoreHousesController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                var list = db.StoreHouses.Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.StoreHouseId).Skip((page - 1) * rows).Take(rows).ToList();

                return Json(new { total = db.StoreHouses.Count(), rows = list });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(StoreHouse model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.StoreHouses.Add(model);
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_STOREHOUSES);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("StoreHousesController.Add", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(StoreHouse model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StoreHouse current = db.StoreHouses.SingleOrDefault(
                        m => m.StoreHouseId == model.StoreHouseId && m.IsDeleted == false);

                    if (current == null)
                    {
                        db.StoreHouses.Add(model);
                    }
                    else
                    {
                        current.Address = model.Address;
                        current.StorageVolume = model.StorageVolume;
                        current.Email = model.Email;
                        current.FAX = model.FAX;
                        current.MobilePhone = model.MobilePhone;
                        current.Name = model.Name;
                        current.StoreHouseName = model.StoreHouseName;
                        current.Telephone = model.Telephone;
                    }

                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_STOREHOUSES);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("StoreHousesController.Edit", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(StoreHouse model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StoreHouse current = db.StoreHouses.SingleOrDefault(
                        p => p.StoreHouseId == model.StoreHouseId);
                    if (current == null)
                    {
                        ModelState.AddModelError(string.Empty, "仓库不存在");
                    }
                    else
                    {
                        current.IsDeleted = true;
                        db.SaveChanges();

                        //Task.Run(() =>
                        //{
                        ResBusinessManager.Instance.ClearCache(
                            ResBusinessManager.RESOURCES_STOREHOUSES);
                        //});
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("StoreHousesController.Remove", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }


        // GET: StoreHouses
        public ActionResult Index()
        {
            return View(db.StoreHouses.ToList());
        }

        // GET: StoreHouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreHouse storeHouse = db.StoreHouses.Find(id);
            if (storeHouse == null)
            {
                return HttpNotFound();
            }
            return View(storeHouse);
        }

        // GET: StoreHouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreHouses/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StoreHouseId,StoreHouseKey,StoreHouseName")] StoreHouse storeHouse)
        {
            if (ModelState.IsValid)
            {
                db.StoreHouses.Add(storeHouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(storeHouse);
        }

        // GET: StoreHouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreHouse storeHouse = db.StoreHouses.Find(id);
            if (storeHouse == null)
            {
                return HttpNotFound();
            }
            return View(storeHouse);
        }

        /*
        // POST: StoreHouses/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StoreHouseId,StoreHouseKey,StoreHouseName")] StoreHouse storeHouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storeHouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storeHouse);
        }*/

        // GET: StoreHouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreHouse storeHouse = db.StoreHouses.Find(id);
            if (storeHouse == null)
            {
                return HttpNotFound();
            }
            return View(storeHouse);
        }

        // POST: StoreHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreHouse storeHouse = db.StoreHouses.Find(id);
            db.StoreHouses.Remove(storeHouse);
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
