using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    public class ClaimCompensation
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ClaimCompensationId
        {
            get;
            set;
        }

        //public int OrderContractPaymentResidualId
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 采购索赔金额（手动录入）
        /// </summary>
        public double ClaimNumber { get; set; }

        /// <summary>
        /// 索赔币种（默认与采购币种相同,可自定义修改(币种范围美金、人民币、港币））
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 采购索赔原因（手动录入）
        /// </summary>
        public string ClaimReason { get; set; }

        /// <summary>
        /// 指定结清订单
        /// </summary>
        public string[] CompensateOrderContracts { get; set; }

        /// <summary>
        /// 采购索赔状态（未结清/已结清）
        /// </summary>
        public int CompensationStatus { get; set; }

        /// <summary>
        /// 采购索赔烂账指令（索赔中/取消索赔）
        /// </summary>
        public int CompensationResultStatus { get; set; }
    }
}
