namespace YuShang.ERP
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.ordercontracts")]
    public partial class ordercontracts
    {
        public ordercontracts()
        {
            //accountspayables = new HashSet<accountspayables>();
            productitems = new HashSet<productitems>();
        }

        [Key]
        public int OrderContractId { get; set; }

        [Required]
        [StringLength(100)]
        public string OrderContractKey { get; set; }

        public int ContractStatus { get; set; }

        public int OrderType { get; set; }

        public DateTime OrderCreateTime { get; set; }

        public DateTime? ETA { get; set; }

        public DateTime? ETD { get; set; }

        public string ShipmentPeriod { get; set; }

        public string ContainerSerial { get; set; }

        public string DeliveryBillSerial { get; set; }

        public string Payment { get; set; }

        [StringLength(128)]
        public string OrderSysUserKey { get; set; }

        public double ImportDeposite { get; set; }

        public double SalesGuidePrice { get; set; }

        public double ImportBalancedPayment { get; set; }

        public string Comments { get; set; }

        public int SupplierId { get; set; }

        public int HarborId { get; set; }

        public int? HarborAgentId { get; set; }

        public int? HKLogisId { get; set; }

        public int? MLLogisId { get; set; }

        public int? OrderClaimCompensationId { get; set; }

        //public virtual ICollection<accountspayables> accountspayables { get; set; }

        public virtual harboragents harboragents { get; set; }

        public virtual harbors harbors { get; set; }

        public virtual hklogis hklogis { get; set; }

        public virtual mllogis mllogis { get; set; }

        public virtual orderclaimcompensations orderclaimcompensations { get; set; }

        public virtual suppliers suppliers { get; set; }

        //[JsonIgnore]
        public virtual ICollection<productitems> productitems { get; set; }
    }

    [MetadataType(typeof(ordercontracts))]
    public partial class OrderContractCore
    {

    }
}
