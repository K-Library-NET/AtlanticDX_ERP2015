namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.stockitems")]
    public partial class stockitems
    {
        public stockitems()
        {
            saleproductitems = new HashSet<saleproductitems>();
            stockoutrecords = new HashSet<stockoutrecords>();
        }

        [Key]
        public int StockItemId { get; set; }

        public int ProductItemId { get; set; }

        public int StoreHouseId { get; set; }

        [StringLength(100)]
        public string StoreHouseMountNumber { get; set; }

        public double Quantity { get; set; }

        public double StockWeight { get; set; }

        public DateTime StockInDate { get; set; }

        public bool IsSold { get; set; }

        public virtual productitems productitems { get; set; }

        public virtual ICollection<saleproductitems> saleproductitems { get; set; }

        public virtual storehouses storehouses { get; set; }

        public virtual ICollection<stockoutrecords> stockoutrecords { get; set; }
    }
}
