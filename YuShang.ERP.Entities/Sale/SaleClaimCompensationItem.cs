using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Sale
{
    public class SaleClaimCompensationItem
    {
        public int SaleClaimCompensationItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售索赔对象ID
        /// used by Navigation Property
        /// </summary>
        public int SaleClaimCompensationId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual SaleClaimCompensation SaleClaimCompensation
        {
            get;
            set;
        }

        /// <summary>
        /// 销售合同的每一个销售货品项ID
        /// used by Navigation Property
        /// </summary>
        public int SaleProductItemId
        {
            get;
            set;
        }

        ///// <summary>
        ///// Navigation Property
        ///// </summary>
        //public virtual SaleProductItem SaleProductItem
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 索赔金额
        /// </summary>
        [Display(Name = "索赔金额")]
        public double Compensation { get; set; }

        /// <summary>
        /// 索赔原因
        /// </summary>
        [Display(Name = "索赔原因")]
        public string CompensationReason { get; set; }
    }
}
