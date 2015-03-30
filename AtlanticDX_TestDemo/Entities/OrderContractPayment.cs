using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 采购货款
    /// </summary>
    public class OrderContractPayment
    {
        public int OrderContractPaymentId
        {
            get;
            set;
        }

        /// <summary>
        /// 合同编号（销售）
        /// </summary>
        public string OrderContractKey { get; set; }

        /// <summary>
        /// 订金应付
        /// 参考供应商信息内付款方式 ：
        ///  固定订金 —— 订金比率为0%，尾款100%
        ///  比率订金 —— 按照选定的比率,计算公式（货款共计X订金比率）
        ///  自定义订金 —— 手动录入订金额度
        ///  信用证订金—— 参考与供应商商定的付款方式 
        /// </summary>
        public double DepositEstimated
        {
            get;
            set;
        }

        /// <summary>
        /// 订金已付（手动录入金额,小数点后有效数值1位.四舍五入）
        /// </summary>
        public double DepositePaid
        {
            get;
            set;
        }

        /// <summary>
        /// 订金付款日期
        /// </summary>
        public DateTime DepositePaidTime { get; set; }

        /// <summary>
        /// 订金汇率
        /// </summary>
        public double DepositeExchangeRate { get; set; }

        /// <summary>
        /// 订金人民币
        /// </summary>
        public double DepositeRMB { get; set; }

        /// <summary>
        /// 尾款应付
        /// </summary>
        public double BalancePaymentEstimated
        {
            get;
            set;
        }

        /// <summary>
        /// 尾款已付
        /// </summary>
        public double BalancePaymentPaid { get; set; }

        /// <summary>
        /// 尾款付款日期
        /// </summary>
        public double BalancePaymentPaidTime { get; set; }

        /// <summary>
        /// 尾款汇率
        /// </summary>
        public double BalancePaymentExchangeRate { get; set; }

        /// <summary>
        /// 尾款人民币
        /// </summary>
        public double BalancePaymentRMB { get; set; }
    }
}
