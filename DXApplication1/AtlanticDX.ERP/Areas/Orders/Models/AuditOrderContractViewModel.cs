using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.ERP.Areas.Orders.Models
{
    public class AuditOrderContractViewModel
    {
        [Required]
        public int OrderContractId { get; set; }
        [Required]
        public ContractStatus ContractStatus { get; set; }
        
        public string Comments { get; set; }
    }
}