namespace YuShang.ERP
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Configuration.Types;
    using System.Data.Entity.ModelConfiguration.Conventions.Sets;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Core.Metadata.Edm;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<accountspayables> accountspayables { get; set; }
        public virtual DbSet<accountspayrecords> accountspayrecords { get; set; }
        public virtual DbSet<accountsreceivables> accountsreceivables { get; set; }
        public virtual DbSet<accountsreceiverecords> accountsreceiverecords { get; set; }
        public virtual DbSet<coreconfigs> coreconfigs { get; set; }
        public virtual DbSet<declarationcompanies> declarationcompanies { get; set; }
        public virtual DbSet<harboragents> harboragents { get; set; }
        public virtual DbSet<harbors> harbors { get; set; }
        public virtual DbSet<hklogis> hklogis { get; set; }
        public virtual DbSet<hklogisitems> hklogisitems { get; set; }
        public virtual DbSet<hongkonglogisticscompanies> hongkonglogisticscompanies { get; set; }
        public virtual DbSet<mainlandlogisticscompanies> mainlandlogisticscompanies { get; set; }
        public virtual DbSet<mllogis> mllogis { get; set; }
        public virtual DbSet<mllogisitems> mllogisitems { get; set; }
        public virtual DbSet<operationlogs> operationlogs { get; set; }
        public virtual DbSet<orderclaimcompensationitems> orderclaimcompensationitems { get; set; }
        public virtual DbSet<orderclaimcompensations> orderclaimcompensations { get; set; }
        public virtual DbSet<ordercontracts> ordercontracts { get; set; }
        public virtual DbSet<productitems> productitems { get; set; }
        public virtual DbSet<products> products { get; set; }
        public virtual DbSet<salebargainchangerecords> salebargainchangerecords { get; set; }
        public virtual DbSet<salebargainitems> salebargainitems { get; set; }
        public virtual DbSet<salebargains> salebargains { get; set; }
        public virtual DbSet<saleclaimcompensationitems> saleclaimcompensationitems { get; set; }
        public virtual DbSet<saleclaimcompensations> saleclaimcompensations { get; set; }
        public virtual DbSet<saleclients> saleclients { get; set; }
        public virtual DbSet<salecontracts> salecontracts { get; set; }
        public virtual DbSet<saleproductitems> saleproductitems { get; set; }
        public virtual DbSet<stockitems> stockitems { get; set; }
        public virtual DbSet<stockoutrecords> stockoutrecords { get; set; }
        public virtual DbSet<storehouses> storehouses { get; set; }
        public virtual DbSet<suppliers> suppliers { get; set; }
        public virtual DbSet<sysmenus> sysmenus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accountspayables>()
                .HasMany(e => e.accountspayrecords)
                .WithRequired(e => e.accountspayables)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<accountsreceivables>()
                .HasMany(e => e.accountsreceiverecords)
                .WithRequired(e => e.accountsreceivables)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<coreconfigs>()
                .Property(e => e.ConfigTypeKey)
                .IsUnicode(false);

            modelBuilder.Entity<coreconfigs>()
                .Property(e => e.ConfigKey)
                .IsUnicode(false);

            modelBuilder.Entity<coreconfigs>()
                .Property(e => e.ConfigName)
                .IsUnicode(false);

            modelBuilder.Entity<coreconfigs>()
                .Property(e => e.ConfigValue)
                .IsUnicode(false);

            modelBuilder.Entity<declarationcompanies>()
                .HasMany(e => e.harboragents)
                .WithRequired(e => e.declarationcompanies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<harboragents>()
                .Property(e => e.Memo)
                .IsUnicode(false);

            modelBuilder.Entity<harbors>()
                .HasMany(e => e.ordercontracts);
            //.WithRequired(e => e.harbors)
            // .WillCascadeOnDelete(false);

            modelBuilder.Entity<hklogis>()
                .HasMany(e => e.hklogisitems)
                .WithRequired(e => e.hklogis)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<hongkonglogisticscompanies>()
                .HasMany(e => e.hklogis)
                .WithRequired(e => e.hongkonglogisticscompanies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<mainlandlogisticscompanies>()
                .HasMany(e => e.mllogis)
                .WithRequired(e => e.mainlandlogisticscompanies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<mllogis>()
                .HasMany(e => e.mllogisitems)
                .WithRequired(e => e.mllogis)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<operationlogs>()
                .Property(e => e.OperationName)
                .IsUnicode(false);

            modelBuilder.Entity<operationlogs>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<orderclaimcompensationitems>()
                .Property(e => e.Currency)
                .IsUnicode(false);

            modelBuilder.Entity<orderclaimcompensationitems>()
                .Property(e => e.CompensationReason)
                .IsUnicode(false);

            modelBuilder.Entity<orderclaimcompensations>()
                .Property(e => e.Currency)
                .IsUnicode(false);

            modelBuilder.Entity<orderclaimcompensations>()
                .Property(e => e.CompensationReason)
                .IsUnicode(false);

            modelBuilder.Entity<orderclaimcompensations>()
                .HasMany(e => e.orderclaimcompensationitems)
                .WithRequired(e => e.orderclaimcompensations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ordercontracts>()
                .Property(e => e.ShipmentPeriod)
                .IsUnicode(false);

            modelBuilder.Entity<ordercontracts>()
                .Property(e => e.ContainerSerial)
                .IsUnicode(false);

            modelBuilder.Entity<ordercontracts>()
                .Property(e => e.DeliveryBillSerial)
                .IsUnicode(false);

            modelBuilder.Entity<ordercontracts>()
                .Property(e => e.Payment)
                .IsUnicode(false);

            modelBuilder.Entity<ordercontracts>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<ordercontracts>()
                .HasMany(e => e.productitems)
                .WithRequired(e => e.ordercontracts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<productitems>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            //modelBuilder.Entity<productitems>()
            //    .HasMany(e => e.hklogisitems)
            //    .WithRequired(e => e.productitems)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<productitems>()
            //    .HasMany(e => e.mllogisitems)
            //    .WithRequired(e => e.productitems)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<productitems>()
            //    .HasMany(e => e.orderclaimcompensationitems)
            //    .WithRequired(e => e.productitems)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<productitems>()
            //    .HasMany(e => e.stockitems)
            //    .WithRequired(e => e.productitems)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<products>()
            //    .HasMany(e => e.productitems)
            //    .WithRequired(e => e.products)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<salebargainchangerecords>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<salebargains>()
                .HasMany(e => e.salebargainchangerecords)
                .WithRequired(e => e.salebargains)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<salebargains>()
                .HasMany(e => e.salebargainitems)
                .WithRequired(e => e.salebargains)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<salebargains>()
                .HasMany(e => e.salecontracts2)
                .WithOptional(e => e.salebargains2)
                .HasForeignKey(e => e.SelectedSaleBargainId);

            modelBuilder.Entity<saleclaimcompensationitems>()
                .Property(e => e.CompensationReason)
                .IsUnicode(false);

            modelBuilder.Entity<saleclaimcompensations>()
                .HasMany(e => e.saleclaimcompensationitems)
                .WithRequired(e => e.saleclaimcompensations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<saleclients>()
                .HasMany(e => e.salecontracts)
                .WithRequired(e => e.saleclients)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<salecontracts>()
                .Property(e => e.OperatorSysUser)
                .IsUnicode(false);

            modelBuilder.Entity<salecontracts>()
                .HasMany(e => e.salebargains)
                .WithOptional(e => e.salecontracts)
                .HasForeignKey(e => e.SaleContract_SaleContractId);

            modelBuilder.Entity<salecontracts>()
                .HasMany(e => e.salebargains1)
                .WithOptional(e => e.salecontracts1)
                .HasForeignKey(e => e.SaleContract_SaleContractId1);

            modelBuilder.Entity<salecontracts>()
                .HasMany(e => e.saleproductitems)
                .WithRequired(e => e.salecontracts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<salecontracts>()
                .HasMany(e => e.stockoutrecords)
                .WithRequired(e => e.salecontracts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<saleproductitems>()
                .Property(e => e.Currency)
                .IsUnicode(false);

            modelBuilder.Entity<saleproductitems>()
                .HasMany(e => e.salebargainitems)
                .WithRequired(e => e.saleproductitems)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<stockitems>()
                .HasMany(e => e.stockoutrecords)
                .WithRequired(e => e.stockitems)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<storehouses>()
                .Property(e => e.StorageVolume)
                .IsUnicode(false);

            modelBuilder.Entity<storehouses>()
                .HasMany(e => e.stockitems)
                .WithRequired(e => e.storehouses)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<suppliers>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<suppliers>()
                .Property(e => e.FAX)
                .IsUnicode(false);

            modelBuilder.Entity<suppliers>()
                .Property(e => e.SupplierPayment)
                .IsUnicode(false);

            modelBuilder.Entity<suppliers>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<suppliers>()
                .Property(e => e.MobilePhone)
                .IsUnicode(false);

            modelBuilder.Entity<suppliers>()
                .Property(e => e.Name)
                .IsUnicode(false);

            //modelBuilder.Entity<suppliers>()
            //    .HasMany(e => e.ordercontracts);
            //.WithRequired(e => e.suppliers)
            //.WillCascadeOnDelete(false);

            //modelBuilder.Conventions.Add<CustomKeyConvention>();
        }
    }
    //public class CustomKeyConvention : Convention
    //{
    //    public CustomKeyConvention()
    //    {this.Properties<ordercontracts>().Where(prop => prop.Name == "orderproducts")
    //            .
    //    }
    //}


    //public class ModelBasedConvention : IStoreModelConvention<EdmProperty>
    //{
    //    public void Apply(EdmProperty property, System.Data.Entity.Infrastructure.DbModel model)
    //    {
    //        if (property..PrimitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Decimal
    //            && property.Scale == null)
    //        {
    //            property.Scale = 4;
    //        }
    //    }
    //}

}
