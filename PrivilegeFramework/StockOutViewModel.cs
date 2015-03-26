using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeFramework
{
    public class StockOutViewModel
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

        public ICollection<SaleProductItemInfo> SaleProductItems
        {
            get;
            set;
        }

        public StockViewModel StockViewModel
        {
            get;
            set;
        }

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
