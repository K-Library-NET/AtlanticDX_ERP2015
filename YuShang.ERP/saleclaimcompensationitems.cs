namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.saleclaimcompensationitems")]
    public partial class saleclaimcompensationitems
    {
        public saleclaimcompensationitems()
        {
            saleproductitems = new HashSet<saleproductitems>();
        }

        [Key]
        public int SaleClaimCompensationItemId { get; set; }

        public int SaleClaimCompensationId { get; set; }

        public double Compensation { get; set; }

        public string CompensationReason { get; set; }

        public virtual saleclaimcompensations saleclaimcompensations { get; set; }

        public virtual ICollection<saleproductitems> saleproductitems { get; set; }
    }
}
