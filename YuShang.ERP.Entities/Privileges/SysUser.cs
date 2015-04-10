using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Privileges
{
    public class SysUser : IdentityUser<
        int, SysUserLogin, SysUserRole, SysUserClaim>,
        IUser<int>, IContactPersonInfo
    {
        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        [Display(Name = "头像")]
        public string Photo
        {
            get;
            set;
        }

        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        [Display(Name = "手机号")]
        [MaxLength(15)]
        public string MobilePhone
        {
            get;
            set;
        }

        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        [Display(Name = "地址")]
        public string Address
        {
            get;
            set;
        }

        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        [MaxLength(100)]
        public string QQ_or_WeChat
        {
            get;
            set;
        }

        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name
        {
            get;
            set;
        }

        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        [Display(Name = "创建时间")]
        public DateTime CTIME
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人SysUser.Name
        /// </summary>
        [Display(Name = "创建人")]
        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        public string CreatorUserName
        {
            get;
            set;
        } 
    }
}
