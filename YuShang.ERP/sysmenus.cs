namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.sysmenus")]
    public partial class sysmenus
    {
        [Key]
        public int SysMenuId { get; set; }

        [Required]
        [StringLength(100)]
        public string MenuName { get; set; }

        public int ParentId { get; set; }

        public bool IsShowInNavTree { get; set; }

        [StringLength(100)]
        public string Area { get; set; }

        [StringLength(100)]
        public string Controller { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        [StringLength(200)]
        public string StyleClass { get; set; }
    }
}
