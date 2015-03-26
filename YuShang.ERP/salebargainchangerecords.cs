namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.salebargainchangerecords")]
    public partial class salebargainchangerecords
    {
        [Key]
        public int SaleBargainChangeRecordId { get; set; }

        public int SaleBargainId { get; set; }

        public DateTime CTIME { get; set; }

        public string Comments { get; set; }

        public virtual salebargains salebargains { get; set; }
    }
}
