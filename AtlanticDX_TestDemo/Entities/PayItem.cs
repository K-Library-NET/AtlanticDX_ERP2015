using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    public class PayItem
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int PayItemId { get; set; }

        /// <summary>
        /// 支付类型（香港物流、内地物流、港口费用等等）
        /// </summary>
        public int PayItemType
        { get; set; }

        /// <summary>
        /// 支付来源ID（香港物流、内地物流、港口费用等ID）
        /// </summary>
        public int PayItemSourceId
        { get; set; }

        /// <summary>
        /// 支付日期
        /// </summary>
        public DateTime PayTime
        { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public double PayCost
        { get; set; }
    }
}
