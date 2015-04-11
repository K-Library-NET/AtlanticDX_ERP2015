using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Sale
{
    /// <summary>
    /// 每次销售员添加或修改还价的记录
    /// </summary>
    [Obsolete("不再记录整个大的订单对象")]
    public class SaleBargainChangeRecord
    {
        public int SaleBargainChangeRecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 对应的还价记录
        /// used by Navigation Property
        /// </summary>
        public int SaleBargainId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual SaleBargain SaleBargin
        {
            get;
            set;
        }

        /// <summary>
        /// 记录产生日期
        /// </summary>
        [Display(Name = "记录产生日期")]
        public DateTime CTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 之前还价
        /// </summary>
        [Display(Name = "之前还价")]
        public double? PrevTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 目前还价
        /// </summary>
        [Display(Name = "目前还价")]
        public double? CurrentTotal
        {
            get;
            set;
        }

        [Display(Name = "备注")]
        public string Comments
        {
            get;
            set;
        }
    }
}
