using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.ResMgr
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Product
    {
        [Display(Name="货品")]
        public int ProductId
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已删除。系统基础数据的删除应该只是标记删除而不是物理删除，
        /// 不然容易关联不出来
        /// </summary>
        public bool IsDeleted
        {
            get;
            set;
        }

        /// <summary>
        /// 货品编号
        /// </summary>
        [Required]
        [MaxLength(100)]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        [Display(Name = "货品出厂编号")]
        public string ProductKey
        {
            get;
            set;
        }

        /// <summary>
        /// 货品名
        /// </summary>
        //[Required]
        [MaxLength(100)]
        [Display(Name = "货品名（中）")]
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 货品名（英）
        /// </summary>
        //[Required]
        [MaxLength(100)]
        [Display(Name = "货品名（英）")]
        public string ProductNameENG
        {
            get;
            set;
        }

        /// <summary>
        /// 商品类别
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "商品类别")]
        public string ProductType
        {
            get;
            set;
        }

        /// <summary>
        /// 单位：重量单位或计量单位？
        /// </summary>
        [Display(Name = "单位")]
        [MaxLength(100)]
        public string Units
        {
            get;
            set;
        }

        //20141223: 是否该有“单位”？重量单位或者计量单位
        //20141223: 货品的“供应商”是否有意义？是否应该是采购订单中才有供应商的概念？
        ///// <summary>
        ///// 重量单位
        ///// </summary>
        //[MaxLength(50)]
        //[Display(Name = "重量单位")]
        //public string WeightUnit
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 国家/厂号/货品名/品牌
        /// </summary>
        [NotMapped]
        [Display(Name = "国家/厂号/货品名/品牌")]
        public string ProductFullName
        {
            get
            {
                return string.Format("{0} {1} {2} {3}",
                    this.MadeInCountry, this.MadeInFactory,
                    this.ProductName, this.Brand);
            }
        }

        /// <summary>
        /// 国家
        /// </summary>
        [Display(Name = "国家")]
        [MaxLength(100)]
        //[Required]
        public string MadeInCountry { get; set; }

        /// <summary>
        /// 厂号
        /// </summary>
        [Display(Name = "厂号")]
        [MaxLength(100)]
        //[Required]
        public string MadeInFactory { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Display(Name = "品牌")]
        //[Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [Display(Name = "级别")]
        [MaxLength(100)]
        public string Grade { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [Display(Name = "规格")]
        [MaxLength(100)]
        public string Specification
        {
            get;
            set;
        }

        /// <summary>
        /// 包装
        /// </summary>
        [Display(Name = "包装")]
        [MaxLength(100)]
        public string Packing { get; set; }

        /// <summary>
        /// 月生产量（吨）
        /// </summary>
        [Display(Name = "月生产量（吨）")]
        [MaxLength(100)]
        public string UnitsPerMonth { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(200)]
        public string Comments { get; set; }
    }
}
