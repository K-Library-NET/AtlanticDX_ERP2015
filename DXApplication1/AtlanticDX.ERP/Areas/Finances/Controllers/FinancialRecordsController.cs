using AtlanticDX.Model.Areas.Finances.Models;
using UtilityFramework;
using PrivilegeFramework;
using System;
using System.Linq;
using System.Web.Mvc;
using YuShang.ERP.Entities.Finances;

namespace AtlanticDX.Model.Areas.Finances.Controllers
{
    /// <summary>
    /// 财务记录
    /// </summary>
    [ComplexAuthorize]
    public class FinancialRecordsController : PrivilegeFramework.NavigationProps.NavigationLoopSolvedController// Controller
    {
        private AtlanticDXContext db = new AtlanticDXContext();

        // GET: FinancialRecords
        public ActionResult Index()
        {
            //当前用户
            ViewBag.CurrentUser = db.Users.Where(m => m.UserName == HttpContext.User.Identity.Name).SingleOrDefault();
            return View();
        }

        [HttpPost]
        public JsonResult Index(FinancialRecordFilterViewModel filter, int page = 1, int rows = 10)
        {
            var list3 = db.FinancialRecords
                //.GroupJoin(db.FinancialRecordRelations,
                //f => f.AccountsRecordId, rec => rec.AccountsRecordId,
                //(FinancialRecord, Relations) => new { FinancialRecord, Relations })
                .Join(db.Users, A => A.OperatorSysUserName, U => U.UserName,
                (FinancialRecordView, User) => new
                {
                    FinancialRecordView,
                    User,
                    FinancialRecordView.CTIME
                });

            var list1 = list3.OrderByDescending(m => m.CTIME)
                .Skip((page - 1) * rows).Take(rows).ToList();

            var list = from one in list1
                       select new FinancialRecordViewModel(one.FinancialRecordView,
                           one.User);

            return Json(new { total = list3.Count(), rows = list });
        }
       

        [HttpPost]
        public JsonResult Add(AccountsRecord model)
        {
            if (ModelState.IsValid)
            {
                //TODO 添加财务记录
                try
                {
                    model.CTIME = DateTime.Now;
                    model.UTIME = DateTime.Now;
                    model.Year = model.CTIME.Year;
                    model.Month = model.CTIME.Month;
                    model.Day = model.CTIME.Day;
                    model.IsDeleted = false;
                    model.OperatorSysUserName = db.Users.Where(m => m.UserName == HttpContext.User.Identity.Name).SingleOrDefault().UserName;
                    db.FinancialRecords.Add(model);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    LogHelper.Error("添加财务记录", e);
                    ModelState.AddModelError("", e.Message);
                }                
            }
            return Json(this.GetModelStateErrors(ModelState));
        }

        [HttpPost]
        public JsonResult Edit(FinancialRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO 编辑财务记录
            }
            return Json(this.GetModelStateErrors(ModelState));
        }

        [HttpPost]
        public JsonResult Remove(FinancialRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO 删除财务记录
            }
            return Json(this.GetModelStateErrors(ModelState));
        }
    }
}