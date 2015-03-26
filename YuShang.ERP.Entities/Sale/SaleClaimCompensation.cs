using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Sale
{
    /// <summary>
    /// 销售索赔
    /// </summary>
    public class SaleClaimCompensation
    {
        public int SaleClaimCompensationId
        {
            get;
            set;
        }

        ///// <summary>
        ///// 关联的销售合同ID
        ///// </summary>
        //public int? SaleContractId
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// Navigation Property
        ///// </summary>
        //public virtual SaleContract SaleContract
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 关联的销售合同编号
        ///// </summary>
        //[NotMapped]
        //[Display(Name = "关联的销售合同编号")]
        //public string SaleContractKey
        //{
        //    get
        //    {
        //        if (this.SaleContract != null)
        //            return SaleContract.SaleContractKey;
        //        return string.Empty;
        //    }
        //}

        public virtual ICollection<SaleClaimCompensationItem> SaleClaimCompensationItems
        {
            get;
            set;
        }

        /// <summary>
        /// 销售索赔状态：True,已结清；False,未结清；
        /// </summary>
        [Display(Name = "销售索赔状态")]
        public bool IsCompensationClear
        {
            get;
            set;
        }

        /// <summary>
        /// 销售索赔烂账指令：true,取消索赔；False,索赔中
        /// </summary>
        [Display(Name = "销售索赔烂账指令")]
        public bool IsCompensationAbandoned
        {
            get;
            set;
        }
    }
}
