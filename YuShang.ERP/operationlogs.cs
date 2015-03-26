namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.operationlogs")]
    public partial class operationlogs
    {
        [Key]
        public int OperationLogId { get; set; }

        public string OperationName { get; set; }

        [StringLength(100)]
        public string SysUserId { get; set; }

        public string Description { get; set; }

        public DateTime CTIME { get; set; }
    }
}
