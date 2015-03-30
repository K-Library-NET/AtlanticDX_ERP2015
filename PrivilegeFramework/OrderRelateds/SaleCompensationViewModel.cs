using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeFramework
{
    public class SaleCompensationViewModel : PrivilegeFramework.OrderRelateds.IContractRelatedViewModel
    {
        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是没有这一附加项
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        //[Required]
        public bool? IsEnable
        {
            get;
            set;
        }

        [Display(Name = "错误信息")]
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 销售索赔状态：True,已结清；False,未结清；
        /// </summary>
        [Display(Name = "销售索赔状态")]
        public bool? IsCompensationClear
        {
            get;
            set;
        }

        /// <summary>
        /// 销售索赔烂账指令：true,取消索赔；False,索赔中
        /// </summary>
        [Display(Name = "销售索赔烂账指令")]
        public bool? IsCompensationAbandoned
        {
            get;
            set;
        }

        public ICollection<SaleCompensationInfoItem> SaleCompensationItems
        {
            get;
            set;
        }
    }

    public class SaleCompensationInfoItem
    {
        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是没有这一附加项
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        //[Required]
        public bool? IsEnable
        {
            get;
            set;
        }

        [Display(Name = "销售货品")]
        public int? SaleProductItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 索赔金额
        /// </summary>
        [Display(Name = "索赔金额")]
        public double? Compensation { get; set; }

        /// <summary>
        /// 索赔原因
        /// </summary>
        [Display(Name = "索赔原因")]
        public string CompensationReason { get; set; }
    }
}
