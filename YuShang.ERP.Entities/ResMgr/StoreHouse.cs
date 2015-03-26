using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.ResMgr
{
    /// <summary>
    /// 仓库（资源管理）
    /// </summary>
    public class StoreHouse
    {
        [Display(Name = "仓库")]
        public int StoreHouseId
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
        /// 仓储容量
        /// </summary>
        [Display(Name = "仓储容量")]
        public string StorageVolume
        {
            get;
            set;
        }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Display(Name = "仓库名称")]
        public string StoreHouseName
        {
            get;
            set;
        }

        /// <summary>
        /// 公司电话
        /// </summary>
        [Display(Name = "电话")]
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
        [MaxLength(20)]
        public string FAX
        {
            get;
            set;
        }

        [Display(Name = "仓库地址")]
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
    }
}
