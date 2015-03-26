using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Privileges
{
    public class InheritedPrivilegeLevelRelation
    {
        public InheritedPrivilegeLevelRelation()
        {
            this.LevelRequired = SysRole.DEFAULT_PRIVILEGE_LEVEL;
        }

        public int InheritedPrivilegeLevelRelationId
        {
            get;
            set;
        }

        [Display(Name = "需要的最低权限等级")]
        public int LevelRequired
        {
            get;
            set;
        }

        [Display(Name = "角色ID")]
        public int? RoleId
        {
            get;
            set;
        }

        [Display(Name = "Area")]
        [MaxLength(100)]
        [Index("IX_Relation_CompFunction", 1)]
        public string Area
        {
            get;
            set;
        }

        [Display(Name = "Controller")]
        [MaxLength(100)]
        [Index("IX_Relation_CompFunction", 2)]
        public string Controller
        {
            get;
            set;
        }

        [Display(Name = "Action")]
        [MaxLength(100)]
        [Index("IX_Relation_CompFunction", 3)]
        public string Action
        {
            get;
            set;
        }
    }
}
