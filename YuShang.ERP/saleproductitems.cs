namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.saleproductitems")]
    public partial class saleproductitems
    {
        public saleproductitems()
        {
            salebargainitems = new HashSet<salebargainitems>();
        }

        [Key]
        public int SaleProductItemId { get; set; }

        public int SaleContractId { get; set; }

        public int? StockItemId { get; set; }

        public int? ProductItemId { get; set; }

        public double Quantity { get; set; }

        public double Weight { get; set; }

        public double UnitPrice { get; set; }

        public string Currency { get; set; }

        public int? SaleClaimCompensationItemId { get; set; }

        public virtual productitems productitems { get; set; }

        public virtual ICollection<salebargainitems> salebargainitems { get; set; }

        public virtual saleclaimcompensationitems saleclaimcompensationitems { get; set; }

        public virtual salecontracts salecontracts { get; set; }

        public virtual stockitems stockitems { get; set; }
    }
}
