using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Orders
{
    /// <summary>
    /// 采购索赔的申请索赔项
    /// </summary>
    public class OrderClaimCompensationItem
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int OrderClaimCompensationItemId
        {
            get;
            set;
        }

        ///// <summary>
        ///// 采购索赔ID
        ///// used by navigation property
        ///// </summary>
        //public int OrderClaimCompensationId
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// Navigation Property
        ///// </summary>
        //public virtual OrderClaimCompensation OrderClaimCompensation
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 索赔对应的货品项编号
        /// used by Navigation Property
        /// </summary> 
        public int ProductItemId
        {
            get;
            set;
        }

        ///// <summary>
        ///// Navigation Property
        ///// </summary>
        //[JsonIgnore]
        //public virtual ProductItem ProductItem
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 采购索赔产生的阶段：
        /// 0，没有索赔；-1，未知阶段
        /// 1，港口代理；2，香港物流；3，内地物流；4，入仓；……
        /// </summary>
        [Display(Name = "采购索赔产生的阶段")]
        public int? CompensationHappenedType
        {
            get;
            set;
        }

        ///// <summary>
        ///// 索赔币种
        ///// </summary>
        //[Display(Name = "索赔币种")]
        //[MaxLength(100)]
        //public string Currency
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 索赔金额
        /// </summary>
        [Display(Name = "索赔金额")]
        public double? Compensation { get; set; }

        /// <summary>
        /// 索赔原因
        /// </summary>
        [Display(Name = "索赔原因")]
        [MaxLength(200)]
        public string CompensationReason { get; set; }
    }
}
