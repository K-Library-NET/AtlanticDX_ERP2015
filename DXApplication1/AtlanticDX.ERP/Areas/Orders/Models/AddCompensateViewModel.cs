using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlanticDX.ERP.Areas.Orders.Models
{
    public class AddCompensateViewModel
    {
        [Required]
        public int ProductItemId
        {
            get;
            set;
        }
       

        /// <summary>
        /// 采购索赔产生的阶段：
        /// 0，未知；1，港口代理；2，香港物流；3，内地物流；4，入仓；……
        /// </summary>
        [Required]
        public int CompensationHappenedType
        {
            get;
            set;
        }

        /// <summary>
        /// 索赔币种
        /// </summary>
        [Required]
        [Display(Name = "索赔币种")]
        public string Currency
        {
            get;
            set;
        }

        /// <summary>
        /// 索赔金额
        /// </summary>
        [Required]
        [Display(Name = "索赔金额")]
        public double Compensation { get; set; }

        /// <summary>
        /// 索赔原因
        /// </summary>
        [Required]
        [Display(Name = "索赔原因")]
        public string CompensationReason { get; set; }
    }
}