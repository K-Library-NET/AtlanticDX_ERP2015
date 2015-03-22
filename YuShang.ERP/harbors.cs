namespace YuShang.ERP
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.harbors")]
    public partial class harbors
    {
        public harbors()
        {
            ordercontracts = new HashSet<ordercontracts>();
        }

        [Key]
        public int HarborId { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(100)]
        public string HarborKey { get; set; }

        [Required]
        [StringLength(100)]
        public string HarborName { get; set; }

        [Required]
        [StringLength(100)]
        public string HarborNameENG { get; set; }

        [JsonIgnore]
        public virtual ICollection<ordercontracts> ordercontracts { get; set; }
    }
}
