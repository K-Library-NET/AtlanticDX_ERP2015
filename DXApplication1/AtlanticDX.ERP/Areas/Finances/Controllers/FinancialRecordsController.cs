using AtlanticDX.ERP.Areas.Finances.Models;
using UtilityFramework;
using PrivilegeFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuShang.ERP.Entities.Finances;

namespace AtlanticDX.ERP.Areas.Finances.Controllers
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
            return View();
        }

        [HttpPost]
        public JsonResult Index(FinancialRecordFilterViewModel filter, int page = 1, int rows = 10)
        {
            var list3 = db.FinancialRecords.GroupJoin(db.FinancialRecordRelations,
                f => f.AccountsRecordId, rec => rec.AccountsRecordId,
                (FinancialRecord, Relations) => new { FinancialRecord, Relations })
                .Join(db.Users, A => A.FinancialRecord.OperatorSysUserName, U => U.UserName,
                (FinancialRecordView, User) => new
                {
                    FinancialRecordView.FinancialRecord,
                    FinancialRecordView.Relations,
                    User,
                    FinancialRecordView.FinancialRecord.CTIME
                });

            var list1 = list3.OrderByDescending(m => m.CTIME)
                .Skip((page - 1) * rows).Take(rows).ToList();

            var list = from one in list1
                       select new FinancialRecordViewModel(one.FinancialRecord, one.Relations, one.User, db);

            return Json(new { total = list3.Count(), rows = list });
        }

        [HttpPost]
        public JsonResult Add(FinancialRecordViewModel model){
            if(ModelState.IsValid){
                    //TODO 添加财务记录
            }
            return Json(ModelState.GetModelStateErrors());
        }

        [HttpPost]
        public JsonResult Edit(FinancialRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO 编辑财务记录
            }
            return Json(ModelState.GetModelStateErrors());
        }

        [HttpPost]
        public JsonResult Remove(FinancialRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO 删除财务记录
            }
            return Json(ModelState.GetModelStateErrors());
        }
    }
}