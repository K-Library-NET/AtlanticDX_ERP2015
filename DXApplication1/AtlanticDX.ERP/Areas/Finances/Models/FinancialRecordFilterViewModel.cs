using System;

namespace AtlanticDX.Model.Areas.Finances.Models
{
    public class FinancialRecordFilterViewModel
    {
        public DateTime? CTIME { get; set; }
        public YuShang.ERP.Entities.Finances.FinancialRecordType? RecordType { get; set; }

        public string ContractKey { get; set; }
    }
}