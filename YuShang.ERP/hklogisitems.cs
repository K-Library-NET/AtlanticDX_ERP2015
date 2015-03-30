namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.hklogisitems")]
    public partial class hklogisitems
    {
        [Key]
        public int HKLogisItemId { get; set; }

        public int HKLogisId { get; set; }

        public int LogisticsProductItemId { get; set; }

        public int ProductItemId { get; set; }

        public double ContractQuantity { get; set; }

        public double ContractWeight { get; set; }

        public double FreightCharges { get; set; }

        public double Insurance { get; set; }

        public DateTime ReceivingTime { get; set; }

        public double ReceivingQuantity { get; set; }

        public double ReceivingWeight { get; set; }

        public virtual hklogis hklogis { get; set; }

        public virtual productitems productitems { get; set; }
    }
}
