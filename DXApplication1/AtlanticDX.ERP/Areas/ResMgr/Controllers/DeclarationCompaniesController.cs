using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using YuShang.ERP.Entities.ResMgr;
using System.Threading.Tasks;
using PrivilegeFramework;

namespace AtlanticDX.Model.Areas.ResMgr.Controllers
{
    public class DeclarationCompaniesController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                var list = db.DeclarationCompanies.Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.DeclarationCompanyId).Skip((page - 1) * rows).Take(rows).ToList();

                return Json(new { total = db.DeclarationCompanies.Count(), rows = list });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(DeclarationCompany model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.DeclarationCompanies.Add(model);
                    db.SaveChanges();
                    
                    //Task.Run(() =>
                    //{
                        ResBusinessManager.Instance.ClearCache(
                            ResBusinessManager.RESOURCES_DECLARATION_COMPANIES);
                    //});
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = this.GetModelStateErrors(ModelState);
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DeclarationCompany model)
        {
            if (ModelState.IsValid)
            {
                DeclarationCompany current = db.DeclarationCompanies.SingleOrDefault(
                    m => m.DeclarationCompanyId == model.DeclarationCompanyId && m.IsDeleted == false);

                if (current == null)
                {
                    db.DeclarationCompanies.Add(model);
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

                try
                {
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                        ResBusinessManager.Instance.ClearCache(
                            ResBusinessManager.RESOURCES_DECLARATION_COMPANIES);
                    //});
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e);
                }
            }
            var allErrors = this.GetModelStateErrors(ModelState);
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(DeclarationCompany model)
        {
            if (ModelState.IsValid)
            {
                DeclarationCompany current = db.DeclarationCompanies.SingleOrDefault(
                    p => p.DeclarationCompanyId == model.DeclarationCompanyId);
                if (current == null)
                {
                    ModelState.AddModelError(string.Empty, "报关公司不存在");
                }
                else
                {
                    try
                    {
                        current.IsDeleted = true;
                        db.SaveChanges();

                        //Task.Run(() =>
                        //{
                            ResBusinessManager.Instance.ClearCache(
                                ResBusinessManager.RESOURCES_DECLARATION_COMPANIES);
                        //});
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError(string.Empty, e);
                    }
                }
            }
            var allErrors = this.GetModelStateErrors(ModelState);
            return Json(allErrors);
        }



        // GET: DeclarationCompanies
        public ActionResult Index()
        {
            return View(db.DeclarationCompanies.ToList());
        }

        // GET: DeclarationCompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeclarationCompany declarationCompany = db.DeclarationCompanies.Find(id);
            if (declarationCompany == null)
            {
                return HttpNotFound();
            }
            return View(declarationCompany);
        }

        // GET: DeclarationCompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeclarationCompanies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeclarationCompanyId,DeclarationCompanyKey,CompanyName")] DeclarationCompany declarationCompany)
        {
            if (ModelState.IsValid)
            {
                db.DeclarationCompanies.Add(declarationCompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(declarationCompany);
        }

        // GET: DeclarationCompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeclarationCompany declarationCompany = db.DeclarationCompanies.Find(id);
            if (declarationCompany == null)
            {
                return HttpNotFound();
            }
            return View(declarationCompany);
        }

        /*
        // POST: DeclarationCompanies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeclarationCompanyId,DeclarationCompanyKey,CompanyName")] DeclarationCompany declarationCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(declarationCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(declarationCompany);
        }*/

        // GET: DeclarationCompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeclarationCompany declarationCompany = db.DeclarationCompanies.Find(id);
            if (declarationCompany == null)
            {
                return HttpNotFound();
            }
            return View(declarationCompany);
        }

        // POST: DeclarationCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeclarationCompany declarationCompany = db.DeclarationCompanies.Find(id);
            db.DeclarationCompanies.Remove(declarationCompany);
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
