using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Finances
{
    /// <summary>
    /// 财务费用产生的类型
    /// </summary>
    public enum AccountingEventType
    {
        /// <summary>
        /// 其他分类
        /// </summary>
        [Display(Name = "其他分类")]
        Others = 0,
        /// <summary>
        /// 采购订金
        /// </summary>
        [Display(Name = "采购订金")]
        ImportDeposite=1,
        /// <summary>
        /// 采购固定订金
        /// </summary>
        [Display(Name = "采购固定订金")]
        ImportDepositeStatic=2,
        /// <summary>
        /// 采购尾款
        /// </summary>
        [Display(Name = "采购尾款")]
        ImportBalancedPayment=3,
        /// <summary>
        /// 港口代理费
        /// </summary>
        [Display(Name = "港口代理费")]
        HarborAgentFee=4,
        /// <summary>
        /// 香港物流费
        /// </summary>
        [Display(Name = "香港物流费")]
        HKLogisticsFee=5,
        /// <summary>
        /// 内地物流费
        /// </summary>
        [Display(Name = "内地物流费")]
        MainlandLogisticsFee=6,
        /// <summary>
        /// 仓租
        /// </summary>
        [Display(Name = "仓租")]
        InventoriesRentingFee=7,
        /// <summary>
        /// 销售订金
        /// </summary>
        [Display(Name = "销售订金")]
        SalesDeposite=8,
        /// <summary>
        /// 销售尾款
        /// </summary>
        [Display(Name = "销售尾款")]
        SalesBalancedPayment=9,
        /// <summary>
        /// 员工工资
        /// </summary>
        [Display(Name = "员工工资")]
        EmploymentSalary=10,
        /// <summary>
        /// 销售固定订金
        /// </summary>
        [Display(Name = "销售固定订金")]
        SaleDepositeStatic=11,
    }
}
