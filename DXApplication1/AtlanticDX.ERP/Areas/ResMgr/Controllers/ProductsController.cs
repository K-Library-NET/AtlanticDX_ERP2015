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
    public class ProductsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        // GET: Products
        public ActionResult Index()
        {
            return View();
            //View(db.Products.ToList());
        }

        [HttpPost]
        public JsonResult Index(int page = 1, int rows = 10)
        {
            using (AtlanticDXContext context = new AtlanticDXContext())
            {
                var list = db.Products.Where(m => m.IsDeleted == false)
                    .OrderBy(m => m.ProductId).Skip((page - 1) * rows).Take(rows).ToList();

                return Json(new { total = db.Products.Count(), rows = list });
                //IEnumerable<SysUser> users = users = context.Users.OrderBy(m => m.CTIME).Take(rows).Skip((page - 1) * rows).ToList();
                //IEnumerable<SysUserViewModel> userViewModels = users.Select(m => new SysUserViewModel(m));
                //return Json(userViewModels);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Products.Add(model);
                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_PRODUCTS);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("ProductsController.Add", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                }

                //var product = new Product
                //{
                //    UserName = model.UserName,
                //    Name = model.Name,
                //    PhoneNumber = model.PhoneNumber,
                //    Email = model.Email,
                //    CreatorUserName = HttpContext.User.Identity.Name,
                //    CTIME = DateTime.Now
                //};
                //user.Id = Guid.NewGuid().ToString();
                //var result = await UserManager.CreateAsync(user, model.Password);
                //if (!result.Succeeded)
                //{
                //    foreach (string error in result.Errors)
                //    {
                //        ModelState.AddModelError("", error);
                //    }
                //}
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product currentProduct = db.Products.SingleOrDefault(
                        m => m.ProductId == model.ProductId && m.IsDeleted == false);
                    if (currentProduct == null)
                    {
                        db.Products.Add(model);
                    }
                    else
                    {
                        currentProduct.ProductType = model.ProductType;
                        currentProduct.Brand = model.Brand;
                        currentProduct.Grade = model.Grade;
                        currentProduct.MadeInCountry = model.MadeInCountry;
                        currentProduct.MadeInFactory = model.MadeInFactory;
                        currentProduct.Packing = model.Packing;
                        currentProduct.ProductName = model.ProductName;
                        currentProduct.ProductNameENG = model.ProductNameENG;
                        currentProduct.ProductKey = model.ProductKey;
                        currentProduct.Specification = model.Specification;
                        currentProduct.UnitsPerMonth = model.UnitsPerMonth;
                        currentProduct.Units = model.Units;
                        currentProduct.Comments = model.Comments;
                    }

                    db.SaveChanges();

                    //Task.Run(() =>
                    //{
                    ResBusinessManager.Instance.ClearCache(
                        ResBusinessManager.RESOURCES_PRODUCTS);
                    //});
                }
                catch (Exception e)
                {
                    LogHelper.Error("ProductsController.Edit", e);
                    ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        [HttpPost]
        public async Task<ActionResult> Remove(Product model)
        {
            if (ModelState.IsValid)
            {
                Product currentProduct = db.Products.SingleOrDefault(p => p.ProductId == model.ProductId);
                if (currentProduct == null)
                {
                    ModelState.AddModelError("", "商品不存在");
                }
                else
                {
                    try
                    {
                        currentProduct.IsDeleted = true;
                        //db.Products.Remove(currentProduct);
                        db.SaveChanges();

                        //Task.Run(() =>
                        //{
                        ResBusinessManager.Instance.ClearCache(
                            ResBusinessManager.RESOURCES_PRODUCTS);
                        //});
                    }
                    catch (Exception e)
                    {
                        LogHelper.Error("ProductsController.Remove", e);
                        ModelState.AddModelError(string.Empty, e.Message + "\t\t" + e.StackTrace);
                    }
                }
            }
            var allErrors = ModelState.GetModelStateErrors();
            return Json(allErrors);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductKey,ProductName,MadeInCountry,MadeInFactory,Brand,Specification,Packing,Unit")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        /*
        // POST: Products/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductKey,ProductName,MadeInCountry,MadeInFactory,Brand,Specification,Packing,Unit")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }*/

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public JsonResult GetList(int? page, int? rows)
        {
            int pageValue = (page == null) ? 1 : page.Value;
            int rowsValue = (rows == null) ? 30 : rows.Value;

            int skip = page.Value * rows.Value;

            var products = db.Products.Skip(pageValue).Take(rowsValue);

            var json = new
            {
                total = db.Products.Count(),
                rows = products.ToArray(),
            };

            return Json(json, JsonRequestBehavior.DenyGet);
        }
    }
}
