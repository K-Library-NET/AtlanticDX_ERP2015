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
    public class MainlandLogisticsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                var list = db.MainlandLogistics.Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.MainlandLogisticsCompanyId).Skip((page - 1) * rows).Take(rows).ToList();

                return Json(new { total = db.MainlandLogistics.Count(), rows = list });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(MainlandLogisticsCompany model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.MainlandLogistics.Add(model);
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_MAINLANDLOGIS_COMPANIES);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("MainlandLogisticsController.Add", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(MainlandLogisticsCompany model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MainlandLogisticsCompany current = db.MainlandLogistics.SingleOrDefault(
                        m => m.MainlandLogisticsCompanyId == model.MainlandLogisticsCompanyId && m.IsDeleted == false);

                    if (current == null)
                    {
                        db.MainlandLogistics.Add(model);
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
                        ResBusinessManager.RESOURCES_MAINLANDLOGIS_COMPANIES);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("MainlandLogisticsController.Edit", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(MainlandLogisticsCompany model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MainlandLogisticsCompany current = db.MainlandLogistics.SingleOrDefault(
                        p => p.MainlandLogisticsCompanyId == model.MainlandLogisticsCompanyId);
                    if (current == null)
                    {
                        ModelState.AddModelError(string.Empty, "内地物流公司不存在");
                    }
                    else
                    {
                        current.IsDeleted = true;
                        db.SaveChanges();

                        //Task.Run(() =>
                        //{
                        ResBusinessManager.Instance.ClearCache(
                            ResBusinessManager.RESOURCES_MAINLANDLOGIS_COMPANIES);
                        //});
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("MainlandLogisticsController.Remove", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }


        // GET: MainlandLogistics
        public ActionResult Index()
        {
            return View(db.MainlandLogistics.ToList());
        }

        // GET: MainlandLogistics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainlandLogisticsCompany mainlandLogisticsCompany = db.MainlandLogistics.Find(id);
            if (mainlandLogisticsCompany == null)
            {
                return HttpNotFound();
            }
            return View(mainlandLogisticsCompany);
        }

        // GET: MainlandLogistics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainlandLogistics/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MainlandLogisticsCompanyId,MainlandLogisticsKey,MainlandLogisticsName")] MainlandLogisticsCompany mainlandLogisticsCompany)
        {
            if (ModelState.IsValid)
            {
                db.MainlandLogistics.Add(mainlandLogisticsCompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mainlandLogisticsCompany);
        }

        // GET: MainlandLogistics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainlandLogisticsCompany mainlandLogisticsCompany = db.MainlandLogistics.Find(id);
            if (mainlandLogisticsCompany == null)
            {
                return HttpNotFound();
            }
            return View(mainlandLogisticsCompany);
        }

        /*
        // POST: MainlandLogistics/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MainlandLogisticsCompanyId,MainlandLogisticsKey,MainlandLogisticsName")] MainlandLogisticsCompany mainlandLogisticsCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mainlandLogisticsCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mainlandLogisticsCompany);
        }*/

        // GET: MainlandLogistics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainlandLogisticsCompany mainlandLogisticsCompany = db.MainlandLogistics.Find(id);
            if (mainlandLogisticsCompany == null)
            {
                return HttpNotFound();
            }
            return View(mainlandLogisticsCompany);
        }

        // POST: MainlandLogistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MainlandLogisticsCompany mainlandLogisticsCompany = db.MainlandLogistics.Find(id);
            db.MainlandLogistics.Remove(mainlandLogisticsCompany);
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
