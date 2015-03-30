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
    public class HongkongLogisticsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                var list = db.HKLogistics.Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.HongkongLogisticsCompanyId).Skip((page - 1) * rows).Take(rows).ToList();

                return Json(new { total = db.HKLogistics.Count(), rows = list });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(HongkongLogisticsCompany model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.HKLogistics.Add(model);
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_HKLOGIS_COMPANIES);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("HongkongLogisticsController.Add", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(HongkongLogisticsCompany model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HongkongLogisticsCompany current = db.HKLogistics.SingleOrDefault(
                        m => m.HongkongLogisticsCompanyId == model.HongkongLogisticsCompanyId && m.IsDeleted == false);

                    if (current == null)
                    {
                        db.HKLogistics.Add(model);
                    }
                    else
                    {
                        current.Address = model.Address;
                        current.CompanyName = model.CompanyName;
                        current.Email = model.Email;
                        current.FAX = model.FAX;
                        current.MobilePhone = model.MobilePhone;
                        current.Name = model.Name;
                        current.QQ_or_WeChat = model.QQ_or_WeChat;
                        current.Telephone = model.Telephone;
                    }
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_HKLOGIS_COMPANIES);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("HongkongLogisticsController.Edit", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(HongkongLogisticsCompany model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HongkongLogisticsCompany current = db.HKLogistics.SingleOrDefault(
                        p => p.HongkongLogisticsCompanyId == model.HongkongLogisticsCompanyId);
                    if (current == null)
                    {
                        ModelState.AddModelError(string.Empty, "香港物流公司不存在");
                    }
                    else
                    {
                        current.IsDeleted = true;
                        db.SaveChanges();

                        //Task.Run(() =>
                        //{
                        ResBusinessManager.Instance.ClearCache(
                            ResBusinessManager.RESOURCES_HKLOGIS_COMPANIES);
                        //});
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("HongkongLogisticsController.Remove", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }


        // GET: HongkongLogistics
        public ActionResult Index()
        {
            return View(db.HKLogistics.ToList());
        }

        // GET: HongkongLogistics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HongkongLogisticsCompany hongkongLogisticsCompany = db.HKLogistics.Find(id);
            if (hongkongLogisticsCompany == null)
            {
                return HttpNotFound();
            }
            return View(hongkongLogisticsCompany);
        }

        // GET: HongkongLogistics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HongkongLogistics/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HongkongLogisticsCompanyId,HongkongLogisticsKey,HongkongLogisticsName")] HongkongLogisticsCompany hongkongLogisticsCompany)
        {
            if (ModelState.IsValid)
            {
                db.HKLogistics.Add(hongkongLogisticsCompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hongkongLogisticsCompany);
        }

        // GET: HongkongLogistics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HongkongLogisticsCompany hongkongLogisticsCompany = db.HKLogistics.Find(id);
            if (hongkongLogisticsCompany == null)
            {
                return HttpNotFound();
            }
            return View(hongkongLogisticsCompany);
        }

        /*
        // POST: HongkongLogistics/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HongkongLogisticsCompanyId,HongkongLogisticsKey,HongkongLogisticsName")] HongkongLogisticsCompany hongkongLogisticsCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hongkongLogisticsCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hongkongLogisticsCompany);
        }*/

        // GET: HongkongLogistics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HongkongLogisticsCompany hongkongLogisticsCompany = db.HKLogistics.Find(id);
            if (hongkongLogisticsCompany == null)
            {
                return HttpNotFound();
            }
            return View(hongkongLogisticsCompany);
        }

        // POST: HongkongLogistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HongkongLogisticsCompany hongkongLogisticsCompany = db.HKLogistics.Find(id);
            db.HKLogistics.Remove(hongkongLogisticsCompany);
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
