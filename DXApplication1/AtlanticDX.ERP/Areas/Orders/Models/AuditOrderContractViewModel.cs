using System.ComponentModel.DataAnnotations;
using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.Model.Areas.Orders.Models
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