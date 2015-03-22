namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.accountsreceiverecords")]
    public partial class accountsreceiverecords
    {
        [Key]
        public int AccountsReceiveRecordId { get; set; }

        public int AccountsReceivableId { get; set; }

        public int RecordType { get; set; }

        public DateTime CTIME { get; set; }

        public double Amount { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        [Required]
        [StringLength(128)]
        public string OperatorSysUserId { get; set; }

        [StringLength(256)]
        public string Comments { get; set; }

        public virtual accountsreceivables accountsreceivables { get; set; }
    }
}
