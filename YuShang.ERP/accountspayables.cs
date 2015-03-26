namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.accountspayables")]
    public partial class accountspayables
    {
        public accountspayables()
        {
            accountspayrecords = new HashSet<accountspayrecords>();
        }

        [Key]
        public int AccountsPayableId { get; set; }

        public int? OrderContractId { get; set; }

        public int EventType { get; set; }

        public int PayStatus { get; set; }

        public double Amount { get; set; }

        public DateTime CTIME { get; set; }

        public virtual ordercontracts ordercontracts { get; set; }

        public virtual ICollection<accountspayrecords> accountspayrecords { get; set; }
    }
}
