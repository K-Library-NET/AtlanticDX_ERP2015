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
    /// 内地物流公司
    /// </summary>
    public class MainlandLogisticsCompany
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Display(Name = "内地物流")]
        public int MainlandLogisticsCompanyId
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
        /// 物流公司编号
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "物流公司编号")]
        public string LogisCompanyCode
        {
            get;
            set;
        }

        [Required]
        [MaxLength(200)]
        [Index(IsUnique = true)]
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
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        [MaxLength(100)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [MaxLength(20)]
        public string MobilePhone
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

        [NotMapped]
        public string QQ_or_WeChat
        {
            get;
            set;
        }
    }
}
