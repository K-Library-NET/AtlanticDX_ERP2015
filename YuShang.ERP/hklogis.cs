namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.hklogis")]
    public partial class hklogis
    {
        public hklogis()
        {
            hklogisitems = new HashSet<hklogisitems>();
            ordercontracts = new HashSet<ordercontracts>();
        }

        public int HKLogisId { get; set; }

        public int HongKongLogisticsCompanyId { get; set; }

        public bool CommitToPayCost { get; set; }

        public virtual hongkonglogisticscompanies hongkonglogisticscompanies { get; set; }

        public virtual ICollection<hklogisitems> hklogisitems { get; set; }

        public virtual ICollection<ordercontracts> ordercontracts { get; set; }
    }
}
