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
    public class SuppliersController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                var list = db.Suppliers.Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.SupplierId).Skip((page - 1) * rows).Take(rows).ToList();

                return Json(new { total = context.Suppliers.Count(), rows = list });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(Supplier model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Suppliers.Add(model);
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_SUPPLIERS);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("SuppliersController.Add", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Supplier model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Supplier current = db.Suppliers.SingleOrDefault(
                        m => m.SupplierId == model.SupplierId && m.IsDeleted == false);

                    if (current == null)
                    {
                        db.Suppliers.Add(model);
                    }
                    else
                    {
                        current.Address = model.Address;
                        current.DepositeRates = model.DepositeRates;
                        current.EMail = model.EMail;
                        current.FAX = model.FAX;
                        current.MobilePhone = model.MobilePhone;
                        current.Name = model.Name;
                        current.QQ_or_WeChat = model.QQ_or_WeChat;
                        current.SupplierName = model.SupplierName;
                        current.SupplierPayment = model.SupplierPayment;
                        current.Telephone = model.Telephone;
                    }

                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_SUPPLIERS);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("SuppliersController.Edit", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(Supplier model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Supplier current = db.Suppliers.SingleOrDefault(
                        p => p.SupplierId == model.SupplierId);
                    if (current == null)
                    {
                        ModelState.AddModelError(string.Empty, "供应商不存在");
                    }
                    else
                    {
                        current.IsDeleted = true;
                        db.SaveChanges();

                        //Task.Run(() =>
                        //{
                        ResBusinessManager.Instance.ClearCache(
                            ResBusinessManager.RESOURCES_SUPPLIERS);
                        //});
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("SuppliersController.Remove", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }


        // GET: Suppliers
        public ActionResult Index()
        {
            return View(db.Suppliers.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier Supplier = db.Suppliers.Find(id);
            if (Supplier == null)
            {
                return HttpNotFound();
            }
            return View(Supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,SupplierKey,SupplierName,SupplierPayment,Description")] Supplier Supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(Supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier Supplier = db.Suppliers.Find(id);
            if (Supplier == null)
            {
                return HttpNotFound();
            }
            return View(Supplier);
        }

        /*
        // POST: Suppliers/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierId,SupplierKey,SupplierName,SupplierPayment,Description")] Supplier Supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Supplier);
        }*/

        // GET: Suppliers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier Supplier = db.Suppliers.Find(id);
            if (Supplier == null)
            {
                return HttpNotFound();
            }
            return View(Supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Supplier Supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(Supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Get(int SupplierId)
        {
            Supplier supplier = db.Suppliers.Find(SupplierId);
            return Json(supplier);
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
