namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.salebargainitems")]
    public partial class salebargainitems
    {
        [Key]
        public int SaleBargainItemId { get; set; }

        public int SaleProductItemId { get; set; }

        public double BargainUnitPrice { get; set; }

        public int SaleBargainId { get; set; }

        public virtual salebargains salebargains { get; set; }

        public virtual saleproductitems saleproductitems { get; set; }
    }
}
