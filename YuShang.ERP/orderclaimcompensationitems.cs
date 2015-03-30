namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.orderclaimcompensationitems")]
    public partial class orderclaimcompensationitems
    {
        [Key]
        public int OrderClaimCompensationItemId { get; set; }

        public int OrderClaimCompensationId { get; set; }

        public int ProductItemId { get; set; }

        public int CompensationHappenedType { get; set; }

        public string Currency { get; set; }

        public double Compensation { get; set; }

        public string CompensationReason { get; set; }

        public virtual productitems productitems { get; set; }

        public virtual orderclaimcompensations orderclaimcompensations { get; set; }
    }
}
