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
    /// 销售客户
    /// </summary>
    public class SaleClient
    {
        /// <summary>
        /// Id
        /// </summary>
        [Display(Name = "客户")]
        public int SaleClientId
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
        /// 客户类别
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "客户类别")]
        public string SaleClientType
        {
            get;
            set;
        }

        [Required]
        [MaxLength(200)]
        [Display(Name = "公司名称")]
        public string CompanyName
        {
            get;
            set;
        }

        /// <summary>
        /// 公司电话
        /// </summary>
        [Display(Name = "公司电话")]
        [MaxLength(20)]
        public string Telephone
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

        [Display(Name = "邮箱")]
        [MaxLength(200)]
        [EmailAddress]
        public string Email
        {
            get;
            set;
        }

        [Display(Name = "手机号码")]
        [MaxLength(20)]
        public string MobilePhone
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
        [Display(Name = "QQ/微信")]
        //[NotMapped]
        public string QQ_or_WeChat
        {
            get;
            set;
        }

        /// <summary>
        /// 销售固定订金
        /// </summary>
        [Display(Name = "销售固定订金")]
        [MaxLength(100)]
        public string SaleDepositeStatic
        {
            get;
            set;
        }

        /// <summary>
        /// 销售订金比率（0-1的小数，显示的时候转换为百分数较好）
        /// </summary>
        [Display(Name = "销售订金比率")]
        [Range(0, 1)]
        public double SaleDepositeRate
        {
            get;
            set;
        }

        /// <summary>
        /// 期初往来余额
        /// </summary>
        [Display(Name = "期初往来余额")]
        [MaxLength(100)]
        public string SaleClientsMoney
        {
            get;
            set;
        }


        /// <summary>
        /// 付款方式
        /// </summary>
        [Display(Name = "付款方式")]
        [MaxLength(100)]
        public string SaleClientPayment
        {
            get;
            set;
        }
    }
}
