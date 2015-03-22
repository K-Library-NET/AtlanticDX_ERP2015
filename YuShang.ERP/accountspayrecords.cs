namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.accountspayrecords")]
    public partial class accountspayrecords
    {
        [Key]
        public int AccountsPayRecordId { get; set; }

        public int AccountsPayableId { get; set; }

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

        public virtual accountspayables accountspayables { get; set; }
    }
}
