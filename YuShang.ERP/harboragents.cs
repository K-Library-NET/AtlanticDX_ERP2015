namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.harboragents")]
    public partial class harboragents
    {
        public harboragents()
        {
            ordercontracts = new HashSet<ordercontracts>();
        }

        [Key]
        public int HarborAgentId { get; set; }

        public int DeclarationCompanyId { get; set; }

        public double HarborCost { get; set; }

        public double AgentCost { get; set; }

        public double Tariff { get; set; }

        public double AntiDumpingTax { get; set; }

        public double ValueAddedTax { get; set; }

        public double OthersCost { get; set; }

        public string Memo { get; set; }

        public double Total { get; set; }

        public virtual declarationcompanies declarationcompanies { get; set; }

        public virtual ICollection<ordercontracts> ordercontracts { get; set; }
    }
}
