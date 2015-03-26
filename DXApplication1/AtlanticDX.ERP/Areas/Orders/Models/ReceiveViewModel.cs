using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YuShang.ERP.Entities.Orders;

namespace AtlanticDX.ERP.Areas.Orders.Models
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