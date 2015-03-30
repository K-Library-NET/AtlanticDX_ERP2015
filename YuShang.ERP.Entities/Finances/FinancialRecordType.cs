using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Finances
{
    /// <summary>
    /// 记录类型，指代某一条财务记录是实际发生付款还是收款
    /// </summary>
    public enum FinancialRecordType
    {
        /// <summary>
        /// 实付账款
        /// </summary>
        [Display(Name = "实付账款")]
        AccountsPayable = 0,
        /// <summary>
        /// 实收账款
        /// </summary>
        [Display(Name = "实收账款")]
        AccountsReceivable = 1,
    }
}
