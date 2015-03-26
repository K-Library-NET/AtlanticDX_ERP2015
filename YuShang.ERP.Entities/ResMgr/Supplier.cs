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
    /// 供应商（资源管理）
    /// </summary>
    public class Supplier : YuShang.ERP.Entities.IContactPersonInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Display(Name = "供应商")]
        public int SupplierId
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
        /// 供应商地区
        /// </summary>
        [Display(Name = "供应商地区")]
        [MaxLength(100)]
        public string SupplierArea
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Display(Name = "公司名称")]
        public string SupplierName
        {
            get;
            set;
        }

        /// <summary>
        /// 公司电话
        /// </summary>
        [Display(Name = "公司电话")]
        [MaxLength(50)]
        public string Telephone
        {
            get;
            set;
        }

        [Display(Name = "联系人")]
        [MaxLength(100)]
        public string Name
        {
            get;
            set;
        }

        [MaxLength(100)]
        [Display(Name = "手机号码")]
        public string MobilePhone
        {
            get;
            set;
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "邮箱")]
        [EmailAddress]
        public string EMail
        {
            get;
            set;
        }

        [Display(Name = "QQ/微信")]
        [MaxLength(100)]
        public string QQ_or_WeChat
        {
            get;
            set;
        }

        /// <summary>
        /// 采购订金比率（0-1的小数，显示的时候转换为百分数较好）
        /// </summary>
        /// <summary>
        /// 采购订金比率
        /// </summary>
        [Display(Name = "采购订金比率")]
        [Range(0, 1)]
        public double DepositeRates
        {
            get;
            set;
        }

        /// <summary>
        /// 采购固定订金
        /// </summary>
        [Display(Name = "采购固定订金")]
        [MaxLength(100)]
        public string SaleDepositeStatic
        {
            get;
            set;
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        [Display(Name = "付款方式")]
        [MaxLength(100)]
        public string SupplierPayment
        {
            get;
            set;
        }

        /// <summary>
        /// 期初往来余额
        /// </summary>
        [Display(Name = "期初往来余额")]
        [MaxLength(100)]
        public string SupplierMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 传真
        /// </summary>
        [Display(Name = "传真")]
        [MaxLength(100)]
        public string FAX
        {
            get;
            set;
        }

        [Display(Name = "公司地址")]
        [MaxLength(200)]
        public string Address
        {
            get;
            set;
        }
    }
}
