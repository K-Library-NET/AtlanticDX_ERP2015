using System;
using System.ComponentModel.DataAnnotations;
using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.Model.Areas.Orders.Models
{
    public class ReceiveViewModel
    {
        [Required]
        public int ProductItemId { get; set; }

        [Required]
        public DateTime ReceiveTime { get; set; }

        [Required]
        public ProductItemStatus Status { get; set; }

        public string Comments { get; set; }
    }
}