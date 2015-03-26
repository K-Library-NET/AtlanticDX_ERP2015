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
    /// 报关公司
    /// </summary>
    public class DeclarationCompany : YuShang.ERP.Entities.IContactPersonInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Display(Name = "港口代理")]
        public int DeclarationCompanyId
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
        /// 报关区域
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "报关区域")]
        public string DeclarationArea
        {
            get;
            set;
        }

        /// <summary>
        /// 报关公司名称
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Index(IsUnique = true)]
        [Display(Name = "报关公司名称")]
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
        /// 报关编号
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "报关编号")]
        public string DeclarationCode
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

        [Display(Name = "QQ/微信")]
        [MaxLength(100)]
        public string QQ_or_WeChat
        {
            get;
            set;
        }

        /// <summary>
        /// 传真
        /// </summary>
        [Display(Name = "传真")]
        [MaxLength(50)]
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
