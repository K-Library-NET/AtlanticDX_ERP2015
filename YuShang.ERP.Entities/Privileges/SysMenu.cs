using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Privileges
{
    /// <summary>
    /// 根据Controller、Action控制权限
    /// ViewModel展示的时候需要根据Key的“/”分割成树层次
    /// </summary>
    public class SysMenu
    {
        [Display(Name = "菜单id")]
        public int SysMenuId
        {
            get;
            set;
        }

        [Display(Name = "菜单名称")]
        [Required]
        [MaxLength(100)]
        public string MenuName
        {
            get;
            set;
        }

        [Display(Name = "父级id(为0就是顶级菜单)")]
        public int ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 小于等于0就是不展示
        /// </summary>
        [Display(Name = "是否显示在树形结构中")]
        public int IsShowInNavTree
        {
            get;
            set;
        }

        [Display(Name = "Area")]
        [MaxLength(100)]
        [Index("IX_Menu_CompFunction", 1)]
        public string Area
        {
            get;
            set;
        }

        [Display(Name = "Controller")]
        [MaxLength(100)]
        [Index("IX_Menu_CompFunction", 2)]
        public string Controller
        {
            get;
            set;
        }

        [Display(Name = "Action")]
        [MaxLength(100)]
        [Index("IX_Menu_CompFunction", 3)]
        public string Action
        {
            get;
            set;
        }

        [Display(Name = "StyleClass")]
        [MaxLength(200)]
        public string StyleClass
        {
            get;
            set;
        }

        //菜单id,菜单名称，父级id(为0就是顶级菜单)，是否显示在树形结构中,area,controller,action
        [NotMapped]
        public string SysControllerActionKey
        {
            get
            {
                return string.Format("{0}/{1}/{2}",
                    this.Area, this.Controller, this.Action);
            }
        }
    }
}
