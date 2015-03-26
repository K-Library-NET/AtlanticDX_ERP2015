using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 还价
    /// </summary>
    public class Bargain
    {
        public int BargainId
        {
            get;
            set;
        }

        /// <summary>
        /// 关联的采购货品项ID
        /// </summary>
        public int ProductItemId
        {
            get;
            set;
        }

        public string BargainSysUserKey { get; set; }

        //货品	还价吨重	还价件数	//销售员
        //还价单价	
        //	客户	货款小计	已收款	收款日期	尾款	收款日期	操作

        /// <summary>
        /// 还价单价
        /// </summary>
        public double BargainUnitPrice { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string ClientKey { get; set; }

        /// <summary>
        /// 货款小计
        /// </summary>
        public double PaymentSubtotal { get; set; }

        /// <summary>
        /// 已收款
        /// </summary>
        public double ReceivedPayment { get; set; }

        /// <summary>
        /// 收款日期
        /// </summary>
        public DateTime ReceivedDate { get; set; }

        /// <summary>
        /// 尾款
        /// </summary>
        public double BalancePayment { get; set; }

        /// <summary>
        /// 收款日期（尾款）
        /// </summary>
        public DateTime BalancePaymentDate { get; set; }

        /// <summary>
        /// 操作状态（成交/取消还盘）
        /// </summary>
        public int OperationState
        {
            get;
            set;
        }
    }
}
