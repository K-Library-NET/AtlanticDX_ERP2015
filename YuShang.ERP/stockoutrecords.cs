namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.stockoutrecords")]
    public partial class stockoutrecords
    {
        [Key]
        public int StockOutRecordId { get; set; }

        public int StockItemId { get; set; }

        public int SaleContractId { get; set; }

        public double Quantity { get; set; }

        public double StockWeight { get; set; }

        public double InventoriesFeeSubTotal { get; set; }

        public double RemainderQuantity { get; set; }

        public double RemainderStockWeight { get; set; }

        public DateTime StockOutDate { get; set; }

        [Required]
        [StringLength(128)]
        public string OperatorSysUserId { get; set; }

        [StringLength(256)]
        public string Comments { get; set; }

        public virtual salecontracts salecontracts { get; set; }

        public virtual stockitems stockitems { get; set; }
    }
}
