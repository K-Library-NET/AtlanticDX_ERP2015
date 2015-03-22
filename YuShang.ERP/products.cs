namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.products")]
    public partial class products
    {
        public products()
        {
            productitems = new HashSet<productitems>();
        }

        [Key]
        public int ProductId { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductKey { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductNameENG { get; set; }

        [StringLength(100)]
        public string Units { get; set; }

        public int? SupplierId { get; set; }

        [StringLength(100)]
        public string MadeInCountry { get; set; }

        [StringLength(100)]
        public string MadeInFactory { get; set; }

        [StringLength(100)]
        public string Brand { get; set; }

        [StringLength(100)]
        public string Grade { get; set; }

        [StringLength(100)]
        public string Specification { get; set; }

        [StringLength(100)]
        public string Packing { get; set; }

        [StringLength(100)]
        public string UnitsPerMonth { get; set; }

        public virtual ICollection<productitems> productitems { get; set; }
    }
}
