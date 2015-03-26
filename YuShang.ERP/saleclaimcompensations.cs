namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.saleclaimcompensations")]
    public partial class saleclaimcompensations
    {
        public saleclaimcompensations()
        {
            saleclaimcompensationitems = new HashSet<saleclaimcompensationitems>();
            salecontracts = new HashSet<salecontracts>();
        }

        [Key]
        public int SaleClaimCompensationId { get; set; }

        public bool IsCompensationClear { get; set; }

        public bool IsCompensationAbandoned { get; set; }

        public virtual ICollection<saleclaimcompensationitems> saleclaimcompensationitems { get; set; }

        public virtual ICollection<salecontracts> salecontracts { get; set; }
    }
}
