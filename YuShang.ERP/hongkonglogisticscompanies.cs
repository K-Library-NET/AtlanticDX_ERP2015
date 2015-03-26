namespace YuShang.ERP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mmport.hongkonglogisticscompanies")]
    public partial class hongkonglogisticscompanies
    {
        public hongkonglogisticscompanies()
        {
            hklogis = new HashSet<hklogis>();
        }

        [Key]
        public int HongkongLogisticsCompanyId { get; set; }

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

        public virtual ICollection<hklogis> hklogis { get; set; }
    }
}
