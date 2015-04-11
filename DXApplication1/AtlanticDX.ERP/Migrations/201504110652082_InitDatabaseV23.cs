namespace AtlanticDX.ERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabaseV23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountsPayables",
                c => new
                    {
                        AccountsPayableId = c.Int(nullable: false, identity: true),
                        OrderContractId = c.Int(),
                        EventType = c.Int(nullable: false),
                        PayStatus = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        PaidAmount = c.Double(nullable: false),
                        Currency = c.String(nullable: false, maxLength: 100),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                        UTIME = c.DateTime(nullable: false, precision: 0),
                        Memo = c.String(),
                    })
                .PrimaryKey(t => t.AccountsPayableId)
                .ForeignKey("dbo.OrderContracts", t => t.OrderContractId)
                .Index(t => t.OrderContractId);
            
            CreateTable(
                "dbo.OrderContracts",
                c => new
                    {
                        OrderContractId = c.Int(nullable: false, identity: true),
                        OrderContractKey = c.String(nullable: false, maxLength: 100),
                        ContractStatus = c.Int(nullable: false),
                        OrderType = c.Int(nullable: false),
                        OrderCreateTime = c.DateTime(nullable: false, precision: 0),
                        ETA = c.DateTime(precision: 0),
                        ETD = c.DateTime(precision: 0),
                        ShipmentPeriod = c.String(),
                        ContainerSerial = c.String(),
                        DeliveryBillSerial = c.String(),
                        Payment = c.String(),
                        OrderSysUserKey = c.String(maxLength: 128),
                        ImportDeposite = c.Double(nullable: false),
                        Currency = c.String(maxLength: 100),
                        CurrencyExchangeRate = c.Double(nullable: false),
                        ImportBalancedPayment = c.Double(nullable: false),
                        Comments = c.String(),
                        SupplierId = c.Int(nullable: false),
                        HarborId = c.Int(nullable: false),
                        HarborAgentId = c.Int(),
                        HKLogisId = c.Int(),
                        MLLogisId = c.Int(),
                        OrderClaimCompensationId = c.Int(),
                        IsCompensationClear = c.Boolean(nullable: false),
                        IsCompensationAbandoned = c.Boolean(nullable: false),
                        EntityPrivLevRequired = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderContractId)
                .ForeignKey("dbo.OrderClaimCompensations", t => t.OrderClaimCompensationId)
                .ForeignKey("dbo.Harbors", t => t.HarborId, cascadeDelete: true)
                .ForeignKey("dbo.HarborAgents", t => t.HarborAgentId)
                .ForeignKey("dbo.HKLogis", t => t.HKLogisId)
                .ForeignKey("dbo.MLLogis", t => t.MLLogisId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.OrderContractKey, unique: true)
                .Index(t => t.ContractStatus)
                .Index(t => t.OrderType)
                .Index(t => t.SupplierId)
                .Index(t => t.HarborId)
                .Index(t => t.HarborAgentId)
                .Index(t => t.HKLogisId)
                .Index(t => t.MLLogisId)
                .Index(t => t.OrderClaimCompensationId);
            
            CreateTable(
                "dbo.OrderClaimCompensations",
                c => new
                    {
                        OrderClaimCompensationId = c.Int(nullable: false, identity: true),
                        IsCompensationClear = c.Boolean(nullable: false),
                        IsCompensationAbandoned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderClaimCompensationId);
            
            CreateTable(
                "dbo.OrderClaimCompensationItems",
                c => new
                    {
                        OrderClaimCompensationItemId = c.Int(nullable: false, identity: true),
                        ProductItemId = c.Int(nullable: false),
                        CompensationHappenedType = c.Int(),
                        Compensation = c.Double(),
                        CompensationReason = c.String(maxLength: 200),
                        OrderClaimCompensation_OrderClaimCompensationId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderClaimCompensationItemId)
                .ForeignKey("dbo.OrderClaimCompensations", t => t.OrderClaimCompensation_OrderClaimCompensationId)
                .Index(t => t.OrderClaimCompensation_OrderClaimCompensationId);
            
            CreateTable(
                "dbo.Harbors",
                c => new
                    {
                        HarborId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        HarborKey = c.String(nullable: false, maxLength: 100),
                        HarborName = c.String(maxLength: 100),
                        HarborNameENG = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.HarborId)
                .Index(t => t.HarborKey, unique: true);
            
            CreateTable(
                "dbo.HarborAgents",
                c => new
                    {
                        HarborAgentId = c.Int(nullable: false, identity: true),
                        DeclarationCompanyId = c.Int(nullable: false),
                        HarborCost = c.Double(),
                        AgentCost = c.Double(),
                        Tariff = c.Double(),
                        AntiDumpingTax = c.Double(),
                        ValueAddedTax = c.Double(),
                        OthersCost = c.Double(),
                        Memo = c.String(),
                        Total = c.Double(),
                    })
                .PrimaryKey(t => t.HarborAgentId)
                .ForeignKey("dbo.DeclarationCompanies", t => t.DeclarationCompanyId, cascadeDelete: true)
                .Index(t => t.DeclarationCompanyId);
            
            CreateTable(
                "dbo.DeclarationCompanies",
                c => new
                    {
                        DeclarationCompanyId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        DeclarationArea = c.String(maxLength: 100),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        DeclarationCode = c.String(maxLength: 100),
                        Name = c.String(maxLength: 100),
                        MobilePhone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        QQ_or_WeChat = c.String(maxLength: 100),
                        FAX = c.String(maxLength: 50),
                        Address = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.DeclarationCompanyId)
                .Index(t => t.CompanyName, unique: true);
            
            CreateTable(
                "dbo.HKLogis",
                c => new
                    {
                        HKLogisId = c.Int(nullable: false, identity: true),
                        HongKongLogisticsCompanyId = c.Int(nullable: false),
                        CommitToPayCost = c.Boolean(),
                    })
                .PrimaryKey(t => t.HKLogisId)
                .ForeignKey("dbo.HongkongLogisticsCompanies", t => t.HongKongLogisticsCompanyId, cascadeDelete: true)
                .Index(t => t.HongKongLogisticsCompanyId);
            
            CreateTable(
                "dbo.HongkongLogisticsCompanies",
                c => new
                    {
                        HongkongLogisticsCompanyId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        LogisCompanyCode = c.String(maxLength: 100),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        Name = c.String(maxLength: 100),
                        MobilePhone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        FAX = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.HongkongLogisticsCompanyId)
                .Index(t => t.CompanyName, unique: true);
            
            CreateTable(
                "dbo.HKLogisItems",
                c => new
                    {
                        HKLogisItemId = c.Int(nullable: false, identity: true),
                        HKLogisId = c.Int(nullable: false),
                        ProductItemId = c.Int(nullable: false),
                        ContractQuantity = c.Double(),
                        ContractWeight = c.Double(),
                        FreightCharges = c.Double(),
                        Insurance = c.Double(),
                        ReceivingTime = c.DateTime(precision: 0),
                        ReceivingQuantity = c.Double(),
                        ReceivingWeight = c.Double(),
                    })
                .PrimaryKey(t => t.HKLogisItemId)
                .ForeignKey("dbo.ProductItems", t => t.ProductItemId, cascadeDelete: true)
                .ForeignKey("dbo.HKLogis", t => t.HKLogisId, cascadeDelete: true)
                .Index(t => t.HKLogisId)
                .Index(t => t.ProductItemId);
            
            CreateTable(
                "dbo.ProductItems",
                c => new
                    {
                        ProductItemId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OrderContractId = c.Int(nullable: false),
                        Quantity = c.Double(),
                        NetWeight = c.Double(),
                        UnitPrice = c.Double(),
                        Units = c.String(maxLength: 100),
                        Status = c.Int(nullable: false),
                        ReceiveTime = c.DateTime(precision: 0),
                        Comments = c.String(),
                        OrderClaimCompensationItem_OrderClaimCompensationItemId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductItemId)
                .ForeignKey("dbo.OrderClaimCompensationItems", t => t.OrderClaimCompensationItem_OrderClaimCompensationItemId)
                .ForeignKey("dbo.OrderContracts", t => t.OrderContractId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId, name: "IX_ProductItem_ProductId")
                .Index(t => t.OrderContractId, name: "IX_ProductItem_OrderId")
                .Index(t => t.OrderClaimCompensationItem_OrderClaimCompensationItemId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        ProductKey = c.String(nullable: false, maxLength: 100),
                        ProductName = c.String(maxLength: 100),
                        ProductNameENG = c.String(maxLength: 100),
                        ProductType = c.String(maxLength: 100),
                        Units = c.String(maxLength: 100),
                        MadeInCountry = c.String(maxLength: 100),
                        MadeInFactory = c.String(maxLength: 100),
                        Brand = c.String(maxLength: 100),
                        Grade = c.String(maxLength: 100),
                        Specification = c.String(maxLength: 100),
                        Packing = c.String(maxLength: 100),
                        UnitsPerMonth = c.String(maxLength: 100),
                        Comments = c.String(maxLength: 200),
                        GuidingPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .Index(t => t.ProductKey, unique: true);
            
            CreateTable(
                "dbo.MLLogis",
                c => new
                    {
                        MLLogisId = c.Int(nullable: false, identity: true),
                        MainlandLogisticsCompanyId = c.Int(nullable: false),
                        CommitToPayCost = c.Boolean(),
                    })
                .PrimaryKey(t => t.MLLogisId)
                .ForeignKey("dbo.MainlandLogisticsCompanies", t => t.MainlandLogisticsCompanyId, cascadeDelete: true)
                .Index(t => t.MainlandLogisticsCompanyId);
            
            CreateTable(
                "dbo.MLLogisItems",
                c => new
                    {
                        MLLogisItemId = c.Int(nullable: false, identity: true),
                        MLLogisId = c.Int(nullable: false),
                        ProductItemId = c.Int(nullable: false),
                        ContractQuantity = c.Double(),
                        ContractWeight = c.Double(),
                        FreightCharges = c.Double(),
                        Insurance = c.Double(),
                        ReceivingTime = c.DateTime(precision: 0),
                        ReceivingQuantity = c.Double(),
                        ReceivingWeight = c.Double(),
                    })
                .PrimaryKey(t => t.MLLogisItemId)
                .ForeignKey("dbo.ProductItems", t => t.ProductItemId, cascadeDelete: true)
                .ForeignKey("dbo.MLLogis", t => t.MLLogisId, cascadeDelete: true)
                .Index(t => t.MLLogisId)
                .Index(t => t.ProductItemId);
            
            CreateTable(
                "dbo.MainlandLogisticsCompanies",
                c => new
                    {
                        MainlandLogisticsCompanyId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        LogisCompanyCode = c.String(maxLength: 100),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        Name = c.String(maxLength: 100),
                        MobilePhone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 200),
                        FAX = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.MainlandLogisticsCompanyId)
                .Index(t => t.CompanyName, unique: true);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        SupplierArea = c.String(maxLength: 100),
                        SupplierName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 50),
                        Name = c.String(maxLength: 100),
                        MobilePhone = c.String(maxLength: 100),
                        EMail = c.String(maxLength: 200),
                        QQ_or_WeChat = c.String(maxLength: 100),
                        DepositeRates = c.Double(nullable: false),
                        SaleDepositeStatic = c.String(maxLength: 100),
                        SupplierPayment = c.String(maxLength: 100),
                        SupplierMoney = c.String(maxLength: 100),
                        FAX = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.AccountsRecords",
                c => new
                    {
                        AccountsRecordId = c.Int(nullable: false, identity: true),
                        RecordType = c.Int(nullable: false),
                        AccountsPayableId = c.Int(),
                        AccountsReceivableId = c.Int(),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                        UTIME = c.DateTime(precision: 0),
                        Amount = c.Double(nullable: false),
                        Currency = c.String(nullable: false, maxLength: 100),
                        CurrencyExchangeRate = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        OperatorSysUserName = c.String(nullable: false, maxLength: 128),
                        Comments = c.String(maxLength: 256),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountsRecordId)
                .ForeignKey("dbo.AccountsPayables", t => t.AccountsPayableId)
                .ForeignKey("dbo.AccountsReceivables", t => t.AccountsReceivableId)
                .Index(t => t.RecordType)
                .Index(t => t.AccountsPayableId)
                .Index(t => t.AccountsReceivableId);
            
            CreateTable(
                "dbo.AccountsReceivables",
                c => new
                    {
                        AccountsReceivableId = c.Int(nullable: false, identity: true),
                        SaleContractId = c.Int(),
                        EventType = c.Int(nullable: false),
                        PayStatus = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        ReceiveAmount = c.Double(nullable: false),
                        Currency = c.String(nullable: false, maxLength: 100),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                        UTIME = c.DateTime(nullable: false, precision: 0),
                        Memo = c.String(),
                    })
                .PrimaryKey(t => t.AccountsReceivableId)
                .ForeignKey("dbo.SaleContracts", t => t.SaleContractId)
                .Index(t => t.SaleContractId);
            
            CreateTable(
                "dbo.SaleContracts",
                c => new
                    {
                        SaleContractId = c.Int(nullable: false, identity: true),
                        SaleContractKey = c.String(nullable: false, maxLength: 100),
                        SaleClientId = c.Int(nullable: false),
                        SaleContractStatus = c.Int(nullable: false),
                        SelectedSaleBargainId = c.Int(),
                        OrderType = c.Int(nullable: false),
                        OperatorSysUser = c.String(),
                        SaleClaimCompensationId = c.Int(),
                        SaleCreateTime = c.DateTime(nullable: false, precision: 0),
                        ContractStatus = c.Int(nullable: false),
                        DiscountAmount = c.Double(nullable: false),
                        TotalAfterDiscount = c.Double(nullable: false),
                        SaleDeposite = c.Double(nullable: false),
                        Currency = c.String(maxLength: 100),
                        CurrencyExchangeRate = c.Double(nullable: false),
                        EntityPrivLevRequired = c.Int(nullable: false),
                        SelectedSaleBargain_SaleBargainId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleContractId)
                .ForeignKey("dbo.SaleClaimCompensations", t => t.SaleClaimCompensationId)
                .ForeignKey("dbo.SaleClients", t => t.SaleClientId, cascadeDelete: true)
                .ForeignKey("dbo.SaleBargains", t => t.SelectedSaleBargain_SaleBargainId)
                .Index(t => t.SaleContractKey, unique: true)
                .Index(t => t.SaleClientId)
                .Index(t => t.SaleContractStatus)
                .Index(t => t.OrderType)
                .Index(t => t.SaleClaimCompensationId)
                .Index(t => t.SelectedSaleBargain_SaleBargainId);
            
            CreateTable(
                "dbo.SaleClaimCompensations",
                c => new
                    {
                        SaleClaimCompensationId = c.Int(nullable: false, identity: true),
                        IsCompensationClear = c.Boolean(nullable: false),
                        IsCompensationAbandoned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SaleClaimCompensationId);
            
            CreateTable(
                "dbo.SaleClaimCompensationItems",
                c => new
                    {
                        SaleClaimCompensationItemId = c.Int(nullable: false, identity: true),
                        SaleClaimCompensationId = c.Int(nullable: false),
                        SaleProductItemId = c.Int(nullable: false),
                        Compensation = c.Double(nullable: false),
                        CompensationReason = c.String(),
                    })
                .PrimaryKey(t => t.SaleClaimCompensationItemId)
                .ForeignKey("dbo.SaleClaimCompensations", t => t.SaleClaimCompensationId, cascadeDelete: true)
                .Index(t => t.SaleClaimCompensationId);
            
            CreateTable(
                "dbo.SaleClients",
                c => new
                    {
                        SaleClientId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        SaleClientType = c.String(maxLength: 100),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        FAX = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        MobilePhone = c.String(maxLength: 20),
                        Name = c.String(maxLength: 100),
                        QQ_or_WeChat = c.String(maxLength: 100),
                        SaleDepositeStatic = c.String(maxLength: 100),
                        SaleDepositeRate = c.Double(nullable: false),
                        SaleClientsMoney = c.String(maxLength: 100),
                        SaleClientPayment = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SaleClientId);
            
            CreateTable(
                "dbo.SaleProductItems",
                c => new
                    {
                        SaleProductItemId = c.Int(nullable: false, identity: true),
                        SaleContractId = c.Int(nullable: false),
                        StockItemId = c.Int(),
                        ProductItemId = c.Int(),
                        Quantity = c.Double(),
                        Weight = c.Double(),
                        UnitPrice = c.Double(),
                        ShipmentStatus = c.Int(nullable: false),
                        SaleClaimCompensationItemId = c.Int(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.SaleProductItemId)
                .ForeignKey("dbo.ProductItems", t => t.ProductItemId)
                .ForeignKey("dbo.SaleClaimCompensationItems", t => t.SaleClaimCompensationItemId)
                .ForeignKey("dbo.SaleContracts", t => t.SaleContractId, cascadeDelete: true)
                .ForeignKey("dbo.StockItems", t => t.StockItemId)
                .Index(t => t.SaleContractId)
                .Index(t => t.StockItemId)
                .Index(t => t.ProductItemId)
                .Index(t => t.SaleClaimCompensationItemId);
            
            CreateTable(
                "dbo.StockItems",
                c => new
                    {
                        StockItemId = c.Int(nullable: false, identity: true),
                        ProductItemId = c.Int(nullable: false),
                        StoreHouseId = c.Int(nullable: false),
                        StoreHouseMountNumber = c.String(maxLength: 100),
                        Quantity = c.Double(),
                        StockWeight = c.Double(),
                        StockInDate = c.DateTime(precision: 0),
                        IsAllSold = c.Boolean(nullable: false),
                        StockStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockItemId)
                .ForeignKey("dbo.ProductItems", t => t.ProductItemId, cascadeDelete: true)
                .ForeignKey("dbo.StoreHouses", t => t.StoreHouseId, cascadeDelete: true)
                .Index(t => t.ProductItemId)
                .Index(t => t.StoreHouseId)
                .Index(t => t.IsAllSold);
            
            CreateTable(
                "dbo.StockOutRecords",
                c => new
                    {
                        StockOutRecordId = c.Int(nullable: false, identity: true),
                        StockItemId = c.Int(nullable: false),
                        SaleContractId = c.Int(),
                        Quantity = c.Double(),
                        StockWeight = c.Double(),
                        InventoriesFeeSubTotal = c.Double(),
                        RemainderQuantity = c.Double(),
                        RemainderStockWeight = c.Double(),
                        StockOutDate = c.DateTime(precision: 0),
                        OperatorSysUserName = c.String(maxLength: 128),
                        Comments = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.StockOutRecordId)
                .ForeignKey("dbo.SaleContracts", t => t.SaleContractId)
                .ForeignKey("dbo.StockItems", t => t.StockItemId, cascadeDelete: true)
                .Index(t => t.StockItemId)
                .Index(t => t.SaleContractId);
            
            CreateTable(
                "dbo.StoreHouses",
                c => new
                    {
                        StoreHouseId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        StorageVolume = c.String(),
                        StoreHouseName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        FAX = c.String(maxLength: 20),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        MobilePhone = c.String(maxLength: 20),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.StoreHouseId);
            
            CreateTable(
                "dbo.SaleBargains",
                c => new
                    {
                        SaleBargainId = c.Int(nullable: false, identity: true),
                        BargainSysUserKey = c.String(maxLength: 100),
                        BargainSalesmanId = c.Int(nullable: false),
                        OperationState = c.Int(nullable: false),
                        SaleContractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleBargainId)
                .ForeignKey("dbo.AspNetUsers", t => t.BargainSalesmanId, cascadeDelete: true)
                .Index(t => t.BargainSysUserKey)
                .Index(t => t.BargainSalesmanId);
            
            CreateTable(
                "dbo.SaleBargainItems",
                c => new
                    {
                        SaleBargainItemId = c.Int(nullable: false, identity: true),
                        SaleProductItemId = c.Int(nullable: false),
                        BargainUnitPrice = c.Double(nullable: false),
                        SalesmanId = c.Int(nullable: false),
                        SaleBargainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleBargainItemId)
                .ForeignKey("dbo.SaleBargains", t => t.SaleBargainId, cascadeDelete: true)
                .ForeignKey("dbo.SaleProductItems", t => t.SaleProductItemId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SalesmanId, cascadeDelete: true)
                .Index(t => t.SaleProductItemId)
                .Index(t => t.SalesmanId)
                .Index(t => t.SaleBargainId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.String(),
                        MobilePhone = c.String(maxLength: 15),
                        Address = c.String(),
                        QQ_or_WeChat = c.String(maxLength: 100),
                        Name = c.String(),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                        CreatorUserName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.CoreConfigs",
                c => new
                    {
                        CoreConfigId = c.Int(nullable: false, identity: true),
                        ConfigTypeKey = c.String(nullable: false),
                        ConfigKey = c.String(nullable: false),
                        ConfigName = c.String(nullable: false),
                        ConfigValue = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CoreConfigId);
            
            CreateTable(
                "dbo.InheritedPrivilegeLevelRelations",
                c => new
                    {
                        InheritedPrivilegeLevelRelationId = c.Int(nullable: false, identity: true),
                        LevelRequired = c.Int(nullable: false),
                        RoleId = c.Int(),
                        Area = c.String(maxLength: 100),
                        Controller = c.String(maxLength: 100),
                        Action = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.InheritedPrivilegeLevelRelationId)
                .Index(t => new { t.Area, t.Controller, t.Action }, name: "IX_Relation_CompFunction");
            
            CreateTable(
                "dbo.OperationLogs",
                c => new
                    {
                        OperationLogId = c.Int(nullable: false, identity: true),
                        OperationName = c.String(),
                        SysUserId = c.String(maxLength: 100),
                        Description = c.String(),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.OperationLogId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        PrivilegeLevel = c.Int(nullable: false),
                        IsSystemRole = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SaleBargainChangeRecords",
                c => new
                    {
                        SaleBargainChangeRecordId = c.Int(nullable: false, identity: true),
                        SaleBargainId = c.Int(nullable: false),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                        PrevTotal = c.Double(),
                        CurrentTotal = c.Double(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.SaleBargainChangeRecordId)
                .ForeignKey("dbo.SaleBargains", t => t.SaleBargainId, cascadeDelete: true)
                .Index(t => t.SaleBargainId);
            
            CreateTable(
                "dbo.SysMenus",
                c => new
                    {
                        SysMenuId = c.Int(nullable: false, identity: true),
                        MenuName = c.String(nullable: false, maxLength: 100),
                        ParentId = c.Int(nullable: false),
                        IsShowInNavTree = c.Int(nullable: false),
                        Area = c.String(maxLength: 100),
                        Controller = c.String(maxLength: 100),
                        Action = c.String(maxLength: 100),
                        StyleClass = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.SysMenuId)
                .Index(t => new { t.Area, t.Controller, t.Action }, name: "IX_Menu_CompFunction");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleBargainChangeRecords", "SaleBargainId", "dbo.SaleBargains");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AccountsReceivables", "SaleContractId", "dbo.SaleContracts");
            DropForeignKey("dbo.SaleContracts", "SelectedSaleBargain_SaleBargainId", "dbo.SaleBargains");
            DropForeignKey("dbo.SaleBargains", "BargainSalesmanId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SaleBargainItems", "SalesmanId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SaleBargainItems", "SaleProductItemId", "dbo.SaleProductItems");
            DropForeignKey("dbo.SaleBargainItems", "SaleBargainId", "dbo.SaleBargains");
            DropForeignKey("dbo.SaleProductItems", "StockItemId", "dbo.StockItems");
            DropForeignKey("dbo.StockItems", "StoreHouseId", "dbo.StoreHouses");
            DropForeignKey("dbo.StockOutRecords", "StockItemId", "dbo.StockItems");
            DropForeignKey("dbo.StockOutRecords", "SaleContractId", "dbo.SaleContracts");
            DropForeignKey("dbo.StockItems", "ProductItemId", "dbo.ProductItems");
            DropForeignKey("dbo.SaleProductItems", "SaleContractId", "dbo.SaleContracts");
            DropForeignKey("dbo.SaleProductItems", "SaleClaimCompensationItemId", "dbo.SaleClaimCompensationItems");
            DropForeignKey("dbo.SaleProductItems", "ProductItemId", "dbo.ProductItems");
            DropForeignKey("dbo.SaleContracts", "SaleClientId", "dbo.SaleClients");
            DropForeignKey("dbo.SaleContracts", "SaleClaimCompensationId", "dbo.SaleClaimCompensations");
            DropForeignKey("dbo.SaleClaimCompensationItems", "SaleClaimCompensationId", "dbo.SaleClaimCompensations");
            DropForeignKey("dbo.AccountsRecords", "AccountsReceivableId", "dbo.AccountsReceivables");
            DropForeignKey("dbo.AccountsRecords", "AccountsPayableId", "dbo.AccountsPayables");
            DropForeignKey("dbo.AccountsPayables", "OrderContractId", "dbo.OrderContracts");
            DropForeignKey("dbo.OrderContracts", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.OrderContracts", "MLLogisId", "dbo.MLLogis");
            DropForeignKey("dbo.MLLogis", "MainlandLogisticsCompanyId", "dbo.MainlandLogisticsCompanies");
            DropForeignKey("dbo.MLLogisItems", "MLLogisId", "dbo.MLLogis");
            DropForeignKey("dbo.MLLogisItems", "ProductItemId", "dbo.ProductItems");
            DropForeignKey("dbo.OrderContracts", "HKLogisId", "dbo.HKLogis");
            DropForeignKey("dbo.HKLogisItems", "HKLogisId", "dbo.HKLogis");
            DropForeignKey("dbo.HKLogisItems", "ProductItemId", "dbo.ProductItems");
            DropForeignKey("dbo.ProductItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductItems", "OrderContractId", "dbo.OrderContracts");
            DropForeignKey("dbo.ProductItems", "OrderClaimCompensationItem_OrderClaimCompensationItemId", "dbo.OrderClaimCompensationItems");
            DropForeignKey("dbo.HKLogis", "HongKongLogisticsCompanyId", "dbo.HongkongLogisticsCompanies");
            DropForeignKey("dbo.OrderContracts", "HarborAgentId", "dbo.HarborAgents");
            DropForeignKey("dbo.HarborAgents", "DeclarationCompanyId", "dbo.DeclarationCompanies");
            DropForeignKey("dbo.OrderContracts", "HarborId", "dbo.Harbors");
            DropForeignKey("dbo.OrderContracts", "OrderClaimCompensationId", "dbo.OrderClaimCompensations");
            DropForeignKey("dbo.OrderClaimCompensationItems", "OrderClaimCompensation_OrderClaimCompensationId", "dbo.OrderClaimCompensations");
            DropIndex("dbo.SysMenus", "IX_Menu_CompFunction");
            DropIndex("dbo.SaleBargainChangeRecords", new[] { "SaleBargainId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.InheritedPrivilegeLevelRelations", "IX_Relation_CompFunction");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SaleBargainItems", new[] { "SaleBargainId" });
            DropIndex("dbo.SaleBargainItems", new[] { "SalesmanId" });
            DropIndex("dbo.SaleBargainItems", new[] { "SaleProductItemId" });
            DropIndex("dbo.SaleBargains", new[] { "BargainSalesmanId" });
            DropIndex("dbo.SaleBargains", new[] { "BargainSysUserKey" });
            DropIndex("dbo.StockOutRecords", new[] { "SaleContractId" });
            DropIndex("dbo.StockOutRecords", new[] { "StockItemId" });
            DropIndex("dbo.StockItems", new[] { "IsAllSold" });
            DropIndex("dbo.StockItems", new[] { "StoreHouseId" });
            DropIndex("dbo.StockItems", new[] { "ProductItemId" });
            DropIndex("dbo.SaleProductItems", new[] { "SaleClaimCompensationItemId" });
            DropIndex("dbo.SaleProductItems", new[] { "ProductItemId" });
            DropIndex("dbo.SaleProductItems", new[] { "StockItemId" });
            DropIndex("dbo.SaleProductItems", new[] { "SaleContractId" });
            DropIndex("dbo.SaleClaimCompensationItems", new[] { "SaleClaimCompensationId" });
            DropIndex("dbo.SaleContracts", new[] { "SelectedSaleBargain_SaleBargainId" });
            DropIndex("dbo.SaleContracts", new[] { "SaleClaimCompensationId" });
            DropIndex("dbo.SaleContracts", new[] { "OrderType" });
            DropIndex("dbo.SaleContracts", new[] { "SaleContractStatus" });
            DropIndex("dbo.SaleContracts", new[] { "SaleClientId" });
            DropIndex("dbo.SaleContracts", new[] { "SaleContractKey" });
            DropIndex("dbo.AccountsReceivables", new[] { "SaleContractId" });
            DropIndex("dbo.AccountsRecords", new[] { "AccountsReceivableId" });
            DropIndex("dbo.AccountsRecords", new[] { "AccountsPayableId" });
            DropIndex("dbo.AccountsRecords", new[] { "RecordType" });
            DropIndex("dbo.MainlandLogisticsCompanies", new[] { "CompanyName" });
            DropIndex("dbo.MLLogisItems", new[] { "ProductItemId" });
            DropIndex("dbo.MLLogisItems", new[] { "MLLogisId" });
            DropIndex("dbo.MLLogis", new[] { "MainlandLogisticsCompanyId" });
            DropIndex("dbo.Products", new[] { "ProductKey" });
            DropIndex("dbo.ProductItems", new[] { "OrderClaimCompensationItem_OrderClaimCompensationItemId" });
            DropIndex("dbo.ProductItems", "IX_ProductItem_OrderId");
            DropIndex("dbo.ProductItems", "IX_ProductItem_ProductId");
            DropIndex("dbo.HKLogisItems", new[] { "ProductItemId" });
            DropIndex("dbo.HKLogisItems", new[] { "HKLogisId" });
            DropIndex("dbo.HongkongLogisticsCompanies", new[] { "CompanyName" });
            DropIndex("dbo.HKLogis", new[] { "HongKongLogisticsCompanyId" });
            DropIndex("dbo.DeclarationCompanies", new[] { "CompanyName" });
            DropIndex("dbo.HarborAgents", new[] { "DeclarationCompanyId" });
            DropIndex("dbo.Harbors", new[] { "HarborKey" });
            DropIndex("dbo.OrderClaimCompensationItems", new[] { "OrderClaimCompensation_OrderClaimCompensationId" });
            DropIndex("dbo.OrderContracts", new[] { "OrderClaimCompensationId" });
            DropIndex("dbo.OrderContracts", new[] { "MLLogisId" });
            DropIndex("dbo.OrderContracts", new[] { "HKLogisId" });
            DropIndex("dbo.OrderContracts", new[] { "HarborAgentId" });
            DropIndex("dbo.OrderContracts", new[] { "HarborId" });
            DropIndex("dbo.OrderContracts", new[] { "SupplierId" });
            DropIndex("dbo.OrderContracts", new[] { "OrderType" });
            DropIndex("dbo.OrderContracts", new[] { "ContractStatus" });
            DropIndex("dbo.OrderContracts", new[] { "OrderContractKey" });
            DropIndex("dbo.AccountsPayables", new[] { "OrderContractId" });
            DropTable("dbo.SysMenus");
            DropTable("dbo.SaleBargainChangeRecords");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OperationLogs");
            DropTable("dbo.InheritedPrivilegeLevelRelations");
            DropTable("dbo.CoreConfigs");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SaleBargainItems");
            DropTable("dbo.SaleBargains");
            DropTable("dbo.StoreHouses");
            DropTable("dbo.StockOutRecords");
            DropTable("dbo.StockItems");
            DropTable("dbo.SaleProductItems");
            DropTable("dbo.SaleClients");
            DropTable("dbo.SaleClaimCompensationItems");
            DropTable("dbo.SaleClaimCompensations");
            DropTable("dbo.SaleContracts");
            DropTable("dbo.AccountsReceivables");
            DropTable("dbo.AccountsRecords");
            DropTable("dbo.Suppliers");
            DropTable("dbo.MainlandLogisticsCompanies");
            DropTable("dbo.MLLogisItems");
            DropTable("dbo.MLLogis");
            DropTable("dbo.Products");
            DropTable("dbo.ProductItems");
            DropTable("dbo.HKLogisItems");
            DropTable("dbo.HongkongLogisticsCompanies");
            DropTable("dbo.HKLogis");
            DropTable("dbo.DeclarationCompanies");
            DropTable("dbo.HarborAgents");
            DropTable("dbo.Harbors");
            DropTable("dbo.OrderClaimCompensationItems");
            DropTable("dbo.OrderClaimCompensations");
            DropTable("dbo.OrderContracts");
            DropTable("dbo.AccountsPayables");
        }
    }
}
