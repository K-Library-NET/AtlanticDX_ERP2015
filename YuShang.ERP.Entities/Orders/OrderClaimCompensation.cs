using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Orders
{
    /// <summary>
    /// 采购索赔
    /// </summary>
    public class OrderClaimCompensation
    {
        public int OrderClaimCompensationId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<OrderClaimCompensationItem> Items
        {
            get;
            set;
        }

        ///// <summary>
        ///// 采购合同编号
        ///// </summary>
        //[Display(Name = "采购合同编号")]
        //[NotMapped]
        //public string OrderContractKey
        //{
        //    get
        //    {
        //        if (this.OrderContract != null)
        //            return this.OrderContract.OrderContractKey;
        //        return string.Empty;
        //    }
        //}

        ///// <summary>
        ///// 采购订单合同Id
        ///// used by Navigation Property
        ///// </summary>
        ////[ForeignKey("IX_OrderContract_ClaimCompensation")]
        //public int OrderContractId
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// Navigation Property
        ///// </summary>
        //[JsonIgnore]
        //public virtual OrderContract OrderContract
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 索赔币种
        ///// </summary>
        //[Display(Name = "索赔币种")]
        //public string Currency
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 索赔金额（采购货款时发生）
        /// </summary>
        [Display(Name = "索赔总金额")]
        [NotMapped]
        public double Compensation
        {
            get
            {
                //DEBUG 先不管货币
                if (this.Items != null && this.Items.Count > 0)
                {
                    this.Items.Sum(m => m.Compensation);
                }
                return 0;
            }
        }

        ///// <summary>
        ///// 索赔原因（采购货款时发生）
        ///// </summary>
        //[Display(Name = "索赔原因")]
        //public string CompensationReason { get; set; }

        /// <summary>
        /// 采购索赔状态：True,已结清；False,未结清；
        /// </summary>
        [Display(Name = "采购索赔状态")]
        public bool IsCompensationClear
        {
            get;
            set;
        }

        /// <summary>
        /// 采购索赔烂账指令：true,取消索赔；False,索赔中
        /// </summary>
        [Display(Name = "采购索赔烂账指令")]
        public bool IsCompensationAbandoned
        {
            get;
            set;
        }
    }
}
