using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Privileges
{
    public class SysRole : IdentityRole<int, SysUserRole>
    {
        public SysRole()
            : base()
        {
            this.PrivilegeLevel = DEFAULT_PRIVILEGE_LEVEL;
        }

        public const int DEFAULT_PRIVILEGE_LEVEL = 50;

        /// <summary>
        /// 上级角色的Id，把角色之间的关系视为是树形状的，上级可以继承下级的权限
        /// </summary>
        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        public int ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 角色的权限级别，1-99，数字越大权限越高
        /// </summary>
        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        public int PrivilegeLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 是否系统内置角色，系统内置角色则不允许删除
        /// </summary>
        //[System.ComponentModel.DataAnnotations.ConcurrencyCheck]
        [Display(Name = "是否系统内置角色")]
        [Required]
        public bool IsSystemRole
        {
            get;
            set;
        }
    }
}
