using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeFramework
{
    public class ProductItemViewModel
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
        /// 合同中的货品项（不管是否销售订单都会有）
        /// </summary>
        public ICollection<ProductItemInfo> ContractItems
        {
            get;
            set;
        }

        /// <summary>
        /// 销售货品项（仅是销售订单才会有，与合同中的货品项一条一条对应）
        /// </summary>
        public ICollection<SaleProductItemInfo> SaleProductItems
        {
            get;
            set;
        }

        #region related items

        /// <summary>
        /// 港口代理（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        public HarborAgentViewModel HarborAgent
        {
            get;
            set;
        }

        /// <summary>
        /// 香港物流（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        public HongkongLogisticsViewModel HongkongLogistics
        {
            get;
            set;
        }

        /// <summary>
        /// 内地物流（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        public MainlandLogisticsViewModel MainlandLogistics
        {
            get;
            set;
        }

        /// <summary>
        /// 库存信息（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        public StockViewModel ProductStock
        {
            get;
            set;
        }

        /// <summary>
        /// 采购索赔（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        public OrderCompensationViewModel OrderCompensation
        {
            get;
            set;
        }

        /// <summary>
        /// 销售索赔（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        public SaleCompensationViewModel SaleCompensation
        {
            get;
            set;
        }

        /// <summary>
        /// 采购财务信息（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        public OrderFinancialsViewModel OrderFinancials
        {
            get;
            set;
        }

        /// <summary>
        /// 销售财务信息（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        public SaleFinancialsViewModel SaleFinancials
        {
            get;
            set;
        }

        #endregion

        #region aggregations

        /// <summary>
        /// 各个汇总项的数据值
        /// </summary>
        public AggregationsViewModel Aggregations
        {
            get;
            set;
        }

        #endregion
    }
}
