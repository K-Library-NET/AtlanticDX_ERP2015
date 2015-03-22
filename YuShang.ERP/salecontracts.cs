namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.salecontracts")]
    public partial class salecontracts
    {
        public salecontracts()
        {
            accountsreceivables = new HashSet<accountsreceivables>();
            salebargains = new HashSet<salebargains>();
            salebargains1 = new HashSet<salebargains>();
            saleproductitems = new HashSet<saleproductitems>();
            stockoutrecords = new HashSet<stockoutrecords>();
        }

        [Key]
        public int SaleContractId { get; set; }

        [Required]
        [StringLength(100)]
        public string SaleContractKey { get; set; }

        public int SaleClientId { get; set; }

        public int SaleContractStatus { get; set; }

        public int? SelectedSaleBargainId { get; set; }

        public int OrderType { get; set; }

        public string OperatorSysUser { get; set; }

        public int? SaleClaimCompensationId { get; set; }

        public virtual ICollection<accountsreceivables> accountsreceivables { get; set; }

        public virtual ICollection<salebargains> salebargains { get; set; }

        public virtual ICollection<salebargains> salebargains1 { get; set; }

        public virtual salebargains salebargains2 { get; set; }

        public virtual saleclaimcompensations saleclaimcompensations { get; set; }

        public virtual saleclients saleclients { get; set; }

        public virtual ICollection<saleproductitems> saleproductitems { get; set; }

        public virtual ICollection<stockoutrecords> stockoutrecords { get; set; }
    }
}
