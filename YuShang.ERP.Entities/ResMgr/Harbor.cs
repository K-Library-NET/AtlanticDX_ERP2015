using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.ResMgr
{
    /// <summary>
    /// 港口
    /// </summary>
    public class Harbor
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [Display(Name = "目标港")]
        public int HarborId
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
        /// 国际代码
        /// </summary>
        [Required]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        [MaxLength(100)]
        [Display(Name = "国际代码")]
        public string HarborKey
        {
            get;
            set;
        }

        /// <summary>
        /// 港口名称（中文）
        /// </summary>
        //[Required]
        [MaxLength(100)]
        [Display(Name = "港口名称（中）")]
        public string HarborName
        {
            get;
            set;
        }

        /// <summary>
        /// 港口名称（英）
        /// </summary>
        //[Required]
        [MaxLength(100)]
        [Display(Name = "港口名称（英）")]
        public string HarborNameENG
        {
            get;
            set;
        }
    }
}
