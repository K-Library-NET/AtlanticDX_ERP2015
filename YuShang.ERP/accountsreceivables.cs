namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.accountsreceivables")]
    public partial class accountsreceivables
    {
        public accountsreceivables()
        {
            accountsreceiverecords = new HashSet<accountsreceiverecords>();
        }

        [Key]
        public int AccountsReceivableId { get; set; }

        public int? SaleContractId { get; set; }

        public int EventType { get; set; }

        public int PayStatus { get; set; }

        public double Amount { get; set; }

        public DateTime CTIME { get; set; }

        public virtual ICollection<accountsreceiverecords> accountsreceiverecords { get; set; }

        public virtual salecontracts salecontracts { get; set; }
    }
}
