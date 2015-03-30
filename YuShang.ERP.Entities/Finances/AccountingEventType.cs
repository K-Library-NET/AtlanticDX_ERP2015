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
        ImportDeposite,
        /// <summary>
        /// 采购固定订金
        /// </summary>
        [Display(Name = "采购固定订金")]
        ImportDepositeStatic,
        /// <summary>
        /// 采购尾款
        /// </summary>
        [Display(Name = "采购尾款")]
        ImportBalancedPayment,
        /// <summary>
        /// 港口代理费
        /// </summary>
        [Display(Name = "港口代理费")]
        HarborAgentFee,
        /// <summary>
        /// 香港物流费
        /// </summary>
        [Display(Name = "香港物流费")]
        HKLogisticsFee,
        /// <summary>
        /// 内地物流费
        /// </summary>
        [Display(Name = "内地物流费")]
        MainlandLogisticsFee,
        /// <summary>
        /// 仓租
        /// </summary>
        [Display(Name = "仓租")]
        InventoriesRentingFee,
        /// <summary>
        /// 销售订金
        /// </summary>
        [Display(Name = "销售订金")]
        SalesDeposite,
        /// <summary>
        /// 销售尾款
        /// </summary>
        [Display(Name = "销售尾款")]
        SalesBalancedPayment,
        /// <summary>
        /// 员工工资
        /// </summary>
        [Display(Name = "员工工资")]
        EmploymentSalary,
        /// <summary>
        /// 销售固定订金
        /// </summary>
        [Display(Name = "销售固定订金")]
        SaleDepositeStatic,
    }
}
