namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.mainlandlogisticscompanies")]
    public partial class mainlandlogisticscompanies
    {
        public mainlandlogisticscompanies()
        {
            mllogis = new HashSet<mllogis>();
        }

        [Key]
        public int MainlandLogisticsCompanyId { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        [StringLength(100)]
        public string FAX { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(20)]
        public string MobilePhone { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<mllogis> mllogis { get; set; }
    }
}
