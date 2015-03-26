using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlanticDX.ERP.Areas.Configs.Models
{

    public class SysUserViewModel
    {
        public SysUserViewModel() { }
        public SysUserViewModel(YuShang.ERP.Entities.Privileges.SysUser m)
        {
            this.Id = m.Id;
            this.UserName = m.UserName;
            this.Email = m.Email;
            this.Photo = m.Photo;
            this.PhoneNumber = m.PhoneNumber;
            this.Address = m.Address;
            this.QQ_or_WeChat = m.QQ_or_WeChat;
            this.Name = m.Name;
            this.CreatorUserName = m.CreatorUserName;
            this.CTIME = m.CTIME;
        }

        public int Id { get; set; }

        [Display(Name = "帐户名")]
        [Required(ErrorMessage="帐户名不能为空")]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email
        {
            get;
            set;
        }

        [Display(Name = "头像")]
        public string Photo
        {
            get;
            set;
        }

        [Display(Name = "手机号码")]
        //[Required]
        [MaxLength(11)]
        public string PhoneNumber
        {
            get;
            set;
        }

        [Display(Name = "地址")]
        public string Address
        {
            get;
            set;
        }

        [MaxLength(100)]
        public string QQ_or_WeChat
        {
            get;
            set;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色")]
        [Required]
        public string RoleName
        {
            get;
            set;
        }

        [Display(Name = "创建时间")]
        public DateTime CTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatorUserName
        {
            get;
            set;
        }
    }

    public class AddSysUserViewModel
    {
        public AddSysUserViewModel() { }
        

        [Display(Name = "帐户名")]
        [Required(ErrorMessage = "帐户名不能为空")]
        [StringLength(20,MinimumLength = 5)]
        public string UserName { get; set; }


        [Display(Name = "密码")]
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email
        {
            get;
            set;
        }

        [Display(Name = "头像")]
        public string Photo
        {
            get;
            set;
        }

        [Display(Name = "手机号码")]
        //[Required]
        [MaxLength(11)]
        public string PhoneNumber
        {
            get;
            set;
        }

        [Display(Name = "地址")]
        public string Address
        {
            get;
            set;
        }

        [MaxLength(100)]
        public string QQ_or_WeChat
        {
            get;
            set;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        //[Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色")]
        [Required]
        public string RoleName
        {
            get;
            set;
        }

        [Display(Name = "创建时间")]
        public DateTime CTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatorUserName
        {
            get;
            set;
        }

       
    }
}