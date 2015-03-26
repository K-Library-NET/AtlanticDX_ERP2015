namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.coreconfigs")]
    public partial class coreconfigs
    {
        [Key]
        public int CoreConfigId { get; set; }

        [Required]
        public string ConfigTypeKey { get; set; }

        [Required]
        public string ConfigKey { get; set; }

        [Required]
        public string ConfigName { get; set; }

        [Required]
        public string ConfigValue { get; set; }
    }
}
