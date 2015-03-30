namespace YuShang.ERP
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.productitems")]
    public partial class productitems
    {
        public productitems()
        {
            //hklogisitems = new HashSet<hklogisitems>();
            //mllogisitems = new HashSet<mllogisitems>();
            //orderclaimcompensationitems = new HashSet<orderclaimcompensationitems>();
            //saleproductitems = new HashSet<saleproductitems>();
            //stockitems = new HashSet<stockitems>();
        }

        [Key]
        public int ProductItemId { get; set; }

        public int ProductId { get; set; }

        public int OrderContractId { get; set; }

        public double Quantity { get; set; }

        public double NetWeight { get; set; }

        public double UnitPrice { get; set; }

        [Required]
        [StringLength(100)]
        public string Units { get; set; }

        [StringLength(100)]
        public string Currency { get; set; }

        public int Status { get; set; }

        public DateTime? ReceiveTime { get; set; }

        public string Comments { get; set; }

        //public virtual ICollection<hklogisitems> hklogisitems { get; set; }

        //public virtual ICollection<mllogisitems> mllogisitems { get; set; }

        //public virtual ICollection<orderclaimcompensationitems> orderclaimcompensationitems { get; set; }

        [JsonIgnore]
        public virtual ordercontracts ordercontracts { get; set; }

        //public virtual products products { get; set; }

        //public virtual ICollection<saleproductitems> saleproductitems { get; set; }

        //public virtual ICollection<stockitems> stockitems { get; set; }
    }
}
