namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.salebargains")]
    public partial class salebargains
    {
        public salebargains()
        {
            salebargainchangerecords = new HashSet<salebargainchangerecords>();
            salebargainitems = new HashSet<salebargainitems>();
            salecontracts2 = new HashSet<salecontracts>();
        }

        [Key]
        public int SaleBargainId { get; set; }

        [Required]
        [StringLength(100)]
        public string BargainSysUserKey { get; set; }

        public int OperationState { get; set; }

        public int SaleContractId { get; set; }

        public int? SaleContract_SaleContractId { get; set; }

        public int? SaleContract_SaleContractId1 { get; set; }

        public virtual ICollection<salebargainchangerecords> salebargainchangerecords { get; set; }

        public virtual ICollection<salebargainitems> salebargainitems { get; set; }

        public virtual salecontracts salecontracts { get; set; }

        public virtual salecontracts salecontracts1 { get; set; }

        public virtual ICollection<salecontracts> salecontracts2 { get; set; }
    }
}
