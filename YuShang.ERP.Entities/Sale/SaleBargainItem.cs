﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Privileges;

namespace YuShang.ERP.Entities.Sale
{
    /// <summary>
    /// 销售还价项
    /// </summary>
    public class SaleBargainItem
    {
        public int SaleBargainItemId
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

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual SaleProductItem SaleProductItem
        {
            get;
            set;
        }

        /// <summary>
        /// 货品编号，与Product对象关联
        /// </summary>
        //[Required]
        //[MaxLength(100)]
        //[Index(IsUnique = false)]
        [Display(Name = "货品编号")]
        [NotMapped]
        public string ProductKey
        {
            get
            {
                if (this.SaleProductItem != null)
                    return this.SaleProductItem.ProductKey;
                return string.Empty;
            }
        }

        /// <summary>
        /// 还价单价
        /// </summary>
        [Display(Name = "还价单价")]
        public double BargainUnitPrice { get; set; }

        /// <summary>
        /// 销售人员ID
        /// </summary>
        [Display(Name = "销售人员ID")]
        public int SalesmanId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售人员
        /// </summary>
        [Display(Name = "销售人员")]
        public virtual SysUser Salesman
        {
            get;
            set;
        }

        /// <summary>
        /// 销售还价对象自增ID
        /// 
        /// 这个对象未必有，因为销售员未必被选中出单
        /// 
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
        [JsonIgnore]
        public virtual SaleBargain SaleBargin
        {
            get;
            set;
        }
    }
}
