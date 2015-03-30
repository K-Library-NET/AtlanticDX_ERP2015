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
    public class SaleClientsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                var list = db.SaleClients.Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.SaleClientId).Skip((page - 1) * rows).Take(rows).ToList();

                return Json(new { total = db.SaleClients.Count(), rows = list });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(SaleClient model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SaleClients.Add(model);
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_SALECLIENTS);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("SaleClientsController.Add", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SaleClient model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SaleClient current = db.SaleClients.SingleOrDefault(
                        m => m.SaleClientId == model.SaleClientId && m.IsDeleted == false);

                    if (current == null)
                    {
                        db.SaleClients.Add(model);
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
                        ResBusinessManager.RESOURCES_SALECLIENTS);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("SaleClientsController.Edit", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(SaleClient model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SaleClient current = db.SaleClients.SingleOrDefault(
                        p => p.SaleClientId == model.SaleClientId);
                    if (current == null)
                    {
                        ModelState.AddModelError(string.Empty, "销售客户不存在");
                    }
                    else
                    {
                        current.IsDeleted = true;
                        db.SaveChanges();

                        //Task.Run(() =>
                        //{
                        ResBusinessManager.Instance.ClearCache(
                            ResBusinessManager.RESOURCES_SALECLIENTS);
                        //});
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("SaleClientsController.Remove", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    //ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        // GET: SaleClients
        public ActionResult Index()
        {
            return View(db.SaleClients.ToList());
        }

        // GET: SaleClients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleClient saleClient = db.SaleClients.Find(id);
            if (saleClient == null)
            {
                return HttpNotFound();
            }
            return View(saleClient);
        }

        // GET: SaleClients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleClients/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleClientId,SaleClientKey,SaleClientName")] SaleClient saleClient)
        {
            if (ModelState.IsValid)
            {
                db.SaleClients.Add(saleClient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saleClient);
        }

        // GET: SaleClients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleClient saleClient = db.SaleClients.Find(id);
            if (saleClient == null)
            {
                return HttpNotFound();
            }
            return View(saleClient);
        }

        /*
        // POST: SaleClients/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleClientId,SaleClientKey,SaleClientName")] SaleClient saleClient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saleClient);
        }*/

        // GET: SaleClients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleClient saleClient = db.SaleClients.Find(id);
            if (saleClient == null)
            {
                return HttpNotFound();
            }
            return View(saleClient);
        }

        // POST: SaleClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleClient saleClient = db.SaleClients.Find(id);
            db.SaleClients.Remove(saleClient);
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
