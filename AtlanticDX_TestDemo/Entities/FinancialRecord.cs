using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    public class FinancialRecord
    {
        public int FinancialRecordId
        {
            get;
            set;
        }

        public DateTime CreateDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 关联合同号
        /// </summary>
        public string OrderContractKey
        {
            get;
            set;
        }

        /// <summary>
        /// 支出
        /// </summary>
        public double Expenses
        {
            get;
            set;
        }

        /// <summary>
        /// 入账
        /// </summary>
        public double Income
        {
            get;
            set;
        }

        public double Amount
        {
            get;
            set;
        }

        public FinancialRecordType RecordType
        {
            get;
            set;
        }

        /// <summary>
        /// 汇率
        /// </summary>
        public double ExchangeRate { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
    }

    public enum FinancialRecordType
    {
        /// <summary>
        /// 其他分类
        /// </summary>
        Others = 0,
        /// <summary>
        /// 采购订金
        /// </summary>
        ImportDeposite,
        /// <summary>
        /// 采购固定订金
        /// </summary>
        ImportDepositeStatic,
        /// <summary>
        /// 采购尾款
        /// </summary>
        ImportBalancedPayment,
        /// <summary>
        /// 港口代理费
        /// </summary>
        HarborAgentFee,
        /// <summary>
        /// 香港物流费
        /// </summary>
        HKLogisticsFee,
        /// <summary>
        /// 内地物流费
        /// </summary>
        MainlandLogisticsFee,
        /// <summary>
        /// 仓租
        /// </summary>
        InventoriesRentingFee,
        /// <summary>
        /// 销售订金
        /// </summary>
        SalesDeposite,
        /// <summary>
        /// 销售尾款
        /// </summary>
        SalesBalancedPayment,
        /// <summary>
        /// 员工工资
        /// </summary>
        EmploymentSalary,

    }
}
