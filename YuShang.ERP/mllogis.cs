namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.mllogis")]
    public partial class mllogis
    {
        public mllogis()
        {
            mllogisitems = new HashSet<mllogisitems>();
            ordercontracts = new HashSet<ordercontracts>();
        }

        public int MLLogisId { get; set; }

        public int MainlandLogisticsCompanyId { get; set; }

        public bool CommitToPayCost { get; set; }

        public virtual mainlandlogisticscompanies mainlandlogisticscompanies { get; set; }

        public virtual ICollection<mllogisitems> mllogisitems { get; set; }

        public virtual ICollection<ordercontracts> ordercontracts { get; set; }
    }
}
