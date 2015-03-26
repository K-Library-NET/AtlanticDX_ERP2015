namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.suppliers")]
    public partial class suppliers
    {
        public suppliers()
        {
            //ordercontracts = new HashSet<ordercontracts>();
        }

        [Key]
        public int SupplierId { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(200)]
        public string SupplierName { get; set; }

        public string Telephone { get; set; }

        public string FAX { get; set; }

        public string SupplierPayment { get; set; }

        public double DepositeRates { get; set; }

        [StringLength(200)]
        public string EMail { get; set; }

        public string Address { get; set; }

        public string MobilePhone { get; set; }

        public string Name { get; set; }

        //public virtual ICollection<ordercontracts> ordercontracts { get; set; }
    }
}
