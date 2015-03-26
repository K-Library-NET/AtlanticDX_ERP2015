namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.orderclaimcompensations")]
    public partial class orderclaimcompensations
    {
        public orderclaimcompensations()
        {
            orderclaimcompensationitems = new HashSet<orderclaimcompensationitems>();
            ordercontracts = new HashSet<ordercontracts>();
        }

        [Key]
        public int OrderClaimCompensationId { get; set; }

        public string Currency { get; set; }

        public double Compensation { get; set; }

        public string CompensationReason { get; set; }

        public bool IsCompensationClear { get; set; }

        public bool IsCompensationAbandoned { get; set; }

        public virtual ICollection<orderclaimcompensationitems> orderclaimcompensationitems { get; set; }

        public virtual ICollection<ordercontracts> ordercontracts { get; set; }
    }
}
