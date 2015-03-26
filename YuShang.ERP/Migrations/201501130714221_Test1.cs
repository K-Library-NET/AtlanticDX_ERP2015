namespace YuShang.ERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "mmport.accountspayables",
                c => new
                    {
                        AccountsPayableId = c.Int(nullable: false, identity: true),
                        OrderContractId = c.Int(),
                        EventType = c.Int(nullable: false),
                        PayStatus = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.AccountsPayableId)
                .ForeignKey("mmport.ordercontracts", t => t.OrderContractId)
                .Index(t => t.OrderContractId);
            
            CreateTable(
                "mmport.accountspayrecords",
                c => new
                    {
                        AccountsPayRecordId = c.Int(nullable: false, identity: true),
                        AccountsPayableId = c.Int(nullable: false),
                        RecordType = c.Int(nullable: false),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                        Amount = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        OperatorSysUserId = c.String(nullable: false, maxLength: 128),
                        Comments = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.AccountsPayRecordId)
                .ForeignKey("mmport.accountspayables", t => t.AccountsPayableId)
                .Index(t => t.AccountsPayableId);
            
            CreateTable(
                "mmport.ordercontracts",
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
                        SalesGuidePrice = c.Double(nullable: false),
                        ImportBalancedPayment = c.Double(nullable: false),
                        Comments = c.String(),
                        SupplierId = c.Int(nullable: false),
                        HarborId = c.Int(nullable: false),
                        HarborAgentId = c.Int(),
                        HKLogisId = c.Int(),
                        MLLogisId = c.Int(),
                        OrderClaimCompensationId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderContractId)
                .ForeignKey("mmport.harboragents", t => t.HarborAgentId)
                .ForeignKey("mmport.harbors", t => t.HarborId, cascadeDelete: true)
                .ForeignKey("mmport.hklogis", t => t.HKLogisId)
                .ForeignKey("mmport.mllogis", t => t.MLLogisId)
                .ForeignKey("mmport.orderclaimcompensations", t => t.OrderClaimCompensationId)
                .ForeignKey("mmport.suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.HarborId)
                .Index(t => t.HarborAgentId)
                .Index(t => t.HKLogisId)
                .Index(t => t.MLLogisId)
                .Index(t => t.OrderClaimCompensationId);
            
            CreateTable(
                "mmport.harboragents",
                c => new
                    {
                        HarborAgentId = c.Int(nullable: false, identity: true),
                        DeclarationCompanyId = c.Int(nullable: false),
                        HarborCost = c.Double(nullable: false),
                        AgentCost = c.Double(nullable: false),
                        Tariff = c.Double(nullable: false),
                        AntiDumpingTax = c.Double(nullable: false),
                        ValueAddedTax = c.Double(nullable: false),
                        OthersCost = c.Double(nullable: false),
                        Memo = c.String(),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.HarborAgentId)
                .ForeignKey("mmport.declarationcompanies", t => t.DeclarationCompanyId)
                .Index(t => t.DeclarationCompanyId);
            
            CreateTable(
                "mmport.declarationcompanies",
                c => new
                    {
                        DeclarationCompanyId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        FAX = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        MobilePhone = c.String(maxLength: 20),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.DeclarationCompanyId);
            
            CreateTable(
                "mmport.harbors",
                c => new
                    {
                        HarborId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        HarborKey = c.String(nullable: false, maxLength: 100),
                        HarborName = c.String(nullable: false, maxLength: 100),
                        HarborNameENG = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.HarborId);
            
            CreateTable(
                "mmport.hklogis",
                c => new
                    {
                        HKLogisId = c.Int(nullable: false, identity: true),
                        HongKongLogisticsCompanyId = c.Int(nullable: false),
                        CommitToPayCost = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HKLogisId)
                .ForeignKey("mmport.hongkonglogisticscompanies", t => t.HongKongLogisticsCompanyId)
                .Index(t => t.HongKongLogisticsCompanyId);
            
            CreateTable(
                "mmport.hklogisitems",
                c => new
                    {
                        HKLogisItemId = c.Int(nullable: false, identity: true),
                        HKLogisId = c.Int(nullable: false),
                        LogisticsProductItemId = c.Int(nullable: false),
                        ProductItemId = c.Int(nullable: false),
                        ContractQuantity = c.Double(nullable: false),
                        ContractWeight = c.Double(nullable: false),
                        FreightCharges = c.Double(nullable: false),
                        Insurance = c.Double(nullable: false),
                        ReceivingTime = c.DateTime(nullable: false, precision: 0),
                        ReceivingQuantity = c.Double(nullable: false),
                        ReceivingWeight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.HKLogisItemId)
                .ForeignKey("mmport.productitems", t => t.ProductItemId, cascadeDelete: true)
                .ForeignKey("mmport.hklogis", t => t.HKLogisId)
                .Index(t => t.HKLogisId)
                .Index(t => t.ProductItemId);
            
            CreateTable(
                "mmport.productitems",
                c => new
                    {
                        ProductItemId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OrderContractId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        NetWeight = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Units = c.String(nullable: false, maxLength: 100),
                        Currency = c.String(maxLength: 100),
                        Status = c.Int(nullable: false),
                        ReceiveTime = c.DateTime(precision: 0),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.ProductItemId)
                .ForeignKey("mmport.ordercontracts", t => t.OrderContractId)
                .ForeignKey("mmport.products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderContractId);
            
            CreateTable(
                "mmport.hongkonglogisticscompanies",
                c => new
                    {
                        HongkongLogisticsCompanyId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        FAX = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        MobilePhone = c.String(maxLength: 20),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.HongkongLogisticsCompanyId);
            
            CreateTable(
                "mmport.mllogis",
                c => new
                    {
                        MLLogisId = c.Int(nullable: false, identity: true),
                        MainlandLogisticsCompanyId = c.Int(nullable: false),
                        CommitToPayCost = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MLLogisId)
                .ForeignKey("mmport.mainlandlogisticscompanies", t => t.MainlandLogisticsCompanyId)
                .Index(t => t.MainlandLogisticsCompanyId);
            
            CreateTable(
                "mmport.mainlandlogisticscompanies",
                c => new
                    {
                        MainlandLogisticsCompanyId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        FAX = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        MobilePhone = c.String(maxLength: 20),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MainlandLogisticsCompanyId);
            
            CreateTable(
                "mmport.mllogisitems",
                c => new
                    {
                        MLLogisItemId = c.Int(nullable: false, identity: true),
                        MLLogisId = c.Int(nullable: false),
                        LogisticsProductItemId = c.Int(nullable: false),
                        ProductItemId = c.Int(nullable: false),
                        ContractQuantity = c.Double(nullable: false),
                        ContractWeight = c.Double(nullable: false),
                        FreightCharges = c.Double(nullable: false),
                        Insurance = c.Double(nullable: false),
                        ReceivingTime = c.DateTime(nullable: false, precision: 0),
                        ReceivingQuantity = c.Double(nullable: false),
                        ReceivingWeight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.MLLogisItemId)
                .ForeignKey("mmport.productitems", t => t.ProductItemId, cascadeDelete: true)
                .ForeignKey("mmport.mllogis", t => t.MLLogisId)
                .Index(t => t.MLLogisId)
                .Index(t => t.ProductItemId);
            
            CreateTable(
                "mmport.orderclaimcompensations",
                c => new
                    {
                        OrderClaimCompensationId = c.Int(nullable: false, identity: true),
                        Currency = c.String(),
                        Compensation = c.Double(nullable: false),
                        CompensationReason = c.String(),
                        IsCompensationClear = c.Boolean(nullable: false),
                        IsCompensationAbandoned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderClaimCompensationId);
            
            CreateTable(
                "mmport.orderclaimcompensationitems",
                c => new
                    {
                        OrderClaimCompensationItemId = c.Int(nullable: false, identity: true),
                        OrderClaimCompensationId = c.Int(nullable: false),
                        ProductItemId = c.Int(nullable: false),
                        CompensationHappenedType = c.Int(nullable: false),
                        Currency = c.String(),
                        Compensation = c.Double(nullable: false),
                        CompensationReason = c.String(),
                    })
                .PrimaryKey(t => t.OrderClaimCompensationItemId)
                .ForeignKey("mmport.productitems", t => t.ProductItemId, cascadeDelete: true)
                .ForeignKey("mmport.orderclaimcompensations", t => t.OrderClaimCompensationId)
                .Index(t => t.OrderClaimCompensationId)
                .Index(t => t.ProductItemId);
            
            CreateTable(
                "mmport.suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        SupplierName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(),
                        FAX = c.String(),
                        SupplierPayment = c.String(),
                        DepositeRates = c.Double(nullable: false),
                        EMail = c.String(maxLength: 200),
                        Address = c.String(),
                        MobilePhone = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "mmport.accountsreceivables",
                c => new
                    {
                        AccountsReceivableId = c.Int(nullable: false, identity: true),
                        SaleContractId = c.Int(),
                        EventType = c.Int(nullable: false),
                        PayStatus = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.AccountsReceivableId)
                .ForeignKey("mmport.salecontracts", t => t.SaleContractId)
                .Index(t => t.SaleContractId);
            
            CreateTable(
                "mmport.accountsreceiverecords",
                c => new
                    {
                        AccountsReceiveRecordId = c.Int(nullable: false, identity: true),
                        AccountsReceivableId = c.Int(nullable: false),
                        RecordType = c.Int(nullable: false),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                        Amount = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        OperatorSysUserId = c.String(nullable: false, maxLength: 128),
                        Comments = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.AccountsReceiveRecordId)
                .ForeignKey("mmport.accountsreceivables", t => t.AccountsReceivableId)
                .Index(t => t.AccountsReceivableId);
            
            CreateTable(
                "mmport.salecontracts",
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
                    })
                .PrimaryKey(t => t.SaleContractId)
                .ForeignKey("mmport.saleclaimcompensations", t => t.SaleClaimCompensationId)
                .ForeignKey("mmport.salebargains", t => t.SelectedSaleBargainId)
                .ForeignKey("mmport.saleclients", t => t.SaleClientId)
                .Index(t => t.SaleClientId)
                .Index(t => t.SelectedSaleBargainId)
                .Index(t => t.SaleClaimCompensationId);
            
            CreateTable(
                "mmport.salebargains",
                c => new
                    {
                        SaleBargainId = c.Int(nullable: false, identity: true),
                        BargainSysUserKey = c.String(nullable: false, maxLength: 100),
                        OperationState = c.Int(nullable: false),
                        SaleContractId = c.Int(nullable: false),
                        SaleContract_SaleContractId = c.Int(),
                        SaleContract_SaleContractId1 = c.Int(),
                    })
                .PrimaryKey(t => t.SaleBargainId)
                .ForeignKey("mmport.salecontracts", t => t.SaleContract_SaleContractId)
                .ForeignKey("mmport.salecontracts", t => t.SaleContract_SaleContractId1)
                .Index(t => t.SaleContract_SaleContractId)
                .Index(t => t.SaleContract_SaleContractId1);
            
            CreateTable(
                "mmport.salebargainchangerecords",
                c => new
                    {
                        SaleBargainChangeRecordId = c.Int(nullable: false, identity: true),
                        SaleBargainId = c.Int(nullable: false),
                        CTIME = c.DateTime(nullable: false, precision: 0),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.SaleBargainChangeRecordId)
                .ForeignKey("mmport.salebargains", t => t.SaleBargainId)
                .Index(t => t.SaleBargainId);
            
            CreateTable(
                "mmport.salebargainitems",
                c => new
                    {
                        SaleBargainItemId = c.Int(nullable: false, identity: true),
                        SaleProductItemId = c.Int(nullable: false),
                        BargainUnitPrice = c.Double(nullable: false),
                        SaleBargainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleBargainItemId)
                .ForeignKey("mmport.saleproductitems", t => t.SaleProductItemId)
                .ForeignKey("mmport.salebargains", t => t.SaleBargainId)
                .Index(t => t.SaleProductItemId)
                .Index(t => t.SaleBargainId);
            
            CreateTable(
                "mmport.saleproductitems",
                c => new
                    {
                        SaleProductItemId = c.Int(nullable: false, identity: true),
                        SaleContractId = c.Int(nullable: false),
                        StockItemId = c.Int(),
                        ProductItemId = c.Int(),
                        Quantity = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Currency = c.String(),
                        SaleClaimCompensationItemId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleProductItemId)
                .ForeignKey("mmport.productitems", t => t.ProductItemId)
                .ForeignKey("mmport.saleclaimcompensationitems", t => t.SaleClaimCompensationItemId)
                .ForeignKey("mmport.stockitems", t => t.StockItemId)
                .ForeignKey("mmport.salecontracts", t => t.SaleContractId)
                .Index(t => t.SaleContractId)
                .Index(t => t.StockItemId)
                .Index(t => t.ProductItemId)
                .Index(t => t.SaleClaimCompensationItemId);
            
            CreateTable(
                "mmport.saleclaimcompensationitems",
                c => new
                    {
                        SaleClaimCompensationItemId = c.Int(nullable: false, identity: true),
                        SaleClaimCompensationId = c.Int(nullable: false),
                        Compensation = c.Double(nullable: false),
                        CompensationReason = c.String(),
                    })
                .PrimaryKey(t => t.SaleClaimCompensationItemId)
                .ForeignKey("mmport.saleclaimcompensations", t => t.SaleClaimCompensationId)
                .Index(t => t.SaleClaimCompensationId);
            
            CreateTable(
                "mmport.saleclaimcompensations",
                c => new
                    {
                        SaleClaimCompensationId = c.Int(nullable: false, identity: true),
                        IsCompensationClear = c.Boolean(nullable: false),
                        IsCompensationAbandoned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SaleClaimCompensationId);
            
            CreateTable(
                "mmport.stockitems",
                c => new
                    {
                        StockItemId = c.Int(nullable: false, identity: true),
                        ProductItemId = c.Int(nullable: false),
                        StoreHouseId = c.Int(nullable: false),
                        StoreHouseMountNumber = c.String(maxLength: 100),
                        Quantity = c.Double(nullable: false),
                        StockWeight = c.Double(nullable: false),
                        StockInDate = c.DateTime(nullable: false, precision: 0),
                        IsSold = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StockItemId)
                .ForeignKey("mmport.productitems", t => t.ProductItemId, cascadeDelete: true)
                .ForeignKey("mmport.storehouses", t => t.StoreHouseId)
                .Index(t => t.ProductItemId)
                .Index(t => t.StoreHouseId);
            
            CreateTable(
                "mmport.stockoutrecords",
                c => new
                    {
                        StockOutRecordId = c.Int(nullable: false, identity: true),
                        StockItemId = c.Int(nullable: false),
                        SaleContractId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        StockWeight = c.Double(nullable: false),
                        InventoriesFeeSubTotal = c.Double(nullable: false),
                        RemainderQuantity = c.Double(nullable: false),
                        RemainderStockWeight = c.Double(nullable: false),
                        StockOutDate = c.DateTime(nullable: false, precision: 0),
                        OperatorSysUserId = c.String(nullable: false, maxLength: 128),
                        Comments = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.StockOutRecordId)
                .ForeignKey("mmport.stockitems", t => t.StockItemId)
                .ForeignKey("mmport.salecontracts", t => t.SaleContractId)
                .Index(t => t.StockItemId)
                .Index(t => t.SaleContractId);
            
            CreateTable(
                "mmport.storehouses",
                c => new
                    {
                        StoreHouseId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        StorageVolume = c.String(),
                        StoreHouseName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        FAX = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        MobilePhone = c.String(maxLength: 20),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.StoreHouseId);
            
            CreateTable(
                "mmport.saleclients",
                c => new
                    {
                        SaleClientId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 200),
                        Telephone = c.String(maxLength: 20),
                        FAX = c.String(maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                        MobilePhone = c.String(maxLength: 20),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SaleClientId);
            
            CreateTable(
                "mmport.coreconfigs",
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
                "mmport.operationlogs",
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
                "mmport.products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        ProductKey = c.String(nullable: false, maxLength: 100),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        ProductNameENG = c.String(nullable: false, maxLength: 100),
                        Units = c.String(maxLength: 100),
                        SupplierId = c.Int(),
                        MadeInCountry = c.String(maxLength: 100),
                        MadeInFactory = c.String(maxLength: 100),
                        Brand = c.String(maxLength: 100),
                        Grade = c.String(maxLength: 100),
                        Specification = c.String(maxLength: 100),
                        Packing = c.String(maxLength: 100),
                        UnitsPerMonth = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "mmport.sysmenus",
                c => new
                    {
                        SysMenuId = c.Int(nullable: false, identity: true),
                        MenuName = c.String(nullable: false, maxLength: 100),
                        ParentId = c.Int(nullable: false),
                        IsShowInNavTree = c.Boolean(nullable: false),
                        Area = c.String(maxLength: 100),
                        Controller = c.String(maxLength: 100),
                        Action = c.String(maxLength: 100),
                        StyleClass = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.SysMenuId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("mmport.productitems", "ProductId", "mmport.products");
            DropForeignKey("mmport.stockoutrecords", "SaleContractId", "mmport.salecontracts");
            DropForeignKey("mmport.saleproductitems", "SaleContractId", "mmport.salecontracts");
            DropForeignKey("mmport.salecontracts", "SaleClientId", "mmport.saleclients");
            DropForeignKey("mmport.salebargains", "SaleContract_SaleContractId1", "mmport.salecontracts");
            DropForeignKey("mmport.salebargains", "SaleContract_SaleContractId", "mmport.salecontracts");
            DropForeignKey("mmport.salecontracts", "SelectedSaleBargainId", "mmport.salebargains");
            DropForeignKey("mmport.salebargainitems", "SaleBargainId", "mmport.salebargains");
            DropForeignKey("mmport.stockitems", "StoreHouseId", "mmport.storehouses");
            DropForeignKey("mmport.stockoutrecords", "StockItemId", "mmport.stockitems");
            DropForeignKey("mmport.saleproductitems", "StockItemId", "mmport.stockitems");
            DropForeignKey("mmport.stockitems", "ProductItemId", "mmport.productitems");
            DropForeignKey("mmport.saleproductitems", "SaleClaimCompensationItemId", "mmport.saleclaimcompensationitems");
            DropForeignKey("mmport.salecontracts", "SaleClaimCompensationId", "mmport.saleclaimcompensations");
            DropForeignKey("mmport.saleclaimcompensationitems", "SaleClaimCompensationId", "mmport.saleclaimcompensations");
            DropForeignKey("mmport.salebargainitems", "SaleProductItemId", "mmport.saleproductitems");
            DropForeignKey("mmport.saleproductitems", "ProductItemId", "mmport.productitems");
            DropForeignKey("mmport.salebargainchangerecords", "SaleBargainId", "mmport.salebargains");
            DropForeignKey("mmport.accountsreceivables", "SaleContractId", "mmport.salecontracts");
            DropForeignKey("mmport.accountsreceiverecords", "AccountsReceivableId", "mmport.accountsreceivables");
            DropForeignKey("mmport.accountspayables", "OrderContractId", "mmport.ordercontracts");
            DropForeignKey("mmport.ordercontracts", "SupplierId", "mmport.suppliers");
            DropForeignKey("mmport.productitems", "OrderContractId", "mmport.ordercontracts");
            DropForeignKey("mmport.ordercontracts", "OrderClaimCompensationId", "mmport.orderclaimcompensations");
            DropForeignKey("mmport.orderclaimcompensationitems", "OrderClaimCompensationId", "mmport.orderclaimcompensations");
            DropForeignKey("mmport.orderclaimcompensationitems", "ProductItemId", "mmport.productitems");
            DropForeignKey("mmport.ordercontracts", "MLLogisId", "mmport.mllogis");
            DropForeignKey("mmport.mllogisitems", "MLLogisId", "mmport.mllogis");
            DropForeignKey("mmport.mllogisitems", "ProductItemId", "mmport.productitems");
            DropForeignKey("mmport.mllogis", "MainlandLogisticsCompanyId", "mmport.mainlandlogisticscompanies");
            DropForeignKey("mmport.ordercontracts", "HKLogisId", "mmport.hklogis");
            DropForeignKey("mmport.hklogis", "HongKongLogisticsCompanyId", "mmport.hongkonglogisticscompanies");
            DropForeignKey("mmport.hklogisitems", "HKLogisId", "mmport.hklogis");
            DropForeignKey("mmport.hklogisitems", "ProductItemId", "mmport.productitems");
            DropForeignKey("mmport.ordercontracts", "HarborId", "mmport.harbors");
            DropForeignKey("mmport.ordercontracts", "HarborAgentId", "mmport.harboragents");
            DropForeignKey("mmport.harboragents", "DeclarationCompanyId", "mmport.declarationcompanies");
            DropForeignKey("mmport.accountspayrecords", "AccountsPayableId", "mmport.accountspayables");
            DropIndex("mmport.stockoutrecords", new[] { "SaleContractId" });
            DropIndex("mmport.stockoutrecords", new[] { "StockItemId" });
            DropIndex("mmport.stockitems", new[] { "StoreHouseId" });
            DropIndex("mmport.stockitems", new[] { "ProductItemId" });
            DropIndex("mmport.saleclaimcompensationitems", new[] { "SaleClaimCompensationId" });
            DropIndex("mmport.saleproductitems", new[] { "SaleClaimCompensationItemId" });
            DropIndex("mmport.saleproductitems", new[] { "ProductItemId" });
            DropIndex("mmport.saleproductitems", new[] { "StockItemId" });
            DropIndex("mmport.saleproductitems", new[] { "SaleContractId" });
            DropIndex("mmport.salebargainitems", new[] { "SaleBargainId" });
            DropIndex("mmport.salebargainitems", new[] { "SaleProductItemId" });
            DropIndex("mmport.salebargainchangerecords", new[] { "SaleBargainId" });
            DropIndex("mmport.salebargains", new[] { "SaleContract_SaleContractId1" });
            DropIndex("mmport.salebargains", new[] { "SaleContract_SaleContractId" });
            DropIndex("mmport.salecontracts", new[] { "SaleClaimCompensationId" });
            DropIndex("mmport.salecontracts", new[] { "SelectedSaleBargainId" });
            DropIndex("mmport.salecontracts", new[] { "SaleClientId" });
            DropIndex("mmport.accountsreceiverecords", new[] { "AccountsReceivableId" });
            DropIndex("mmport.accountsreceivables", new[] { "SaleContractId" });
            DropIndex("mmport.orderclaimcompensationitems", new[] { "ProductItemId" });
            DropIndex("mmport.orderclaimcompensationitems", new[] { "OrderClaimCompensationId" });
            DropIndex("mmport.mllogisitems", new[] { "ProductItemId" });
            DropIndex("mmport.mllogisitems", new[] { "MLLogisId" });
            DropIndex("mmport.mllogis", new[] { "MainlandLogisticsCompanyId" });
            DropIndex("mmport.productitems", new[] { "OrderContractId" });
            DropIndex("mmport.productitems", new[] { "ProductId" });
            DropIndex("mmport.hklogisitems", new[] { "ProductItemId" });
            DropIndex("mmport.hklogisitems", new[] { "HKLogisId" });
            DropIndex("mmport.hklogis", new[] { "HongKongLogisticsCompanyId" });
            DropIndex("mmport.harboragents", new[] { "DeclarationCompanyId" });
            DropIndex("mmport.ordercontracts", new[] { "OrderClaimCompensationId" });
            DropIndex("mmport.ordercontracts", new[] { "MLLogisId" });
            DropIndex("mmport.ordercontracts", new[] { "HKLogisId" });
            DropIndex("mmport.ordercontracts", new[] { "HarborAgentId" });
            DropIndex("mmport.ordercontracts", new[] { "HarborId" });
            DropIndex("mmport.ordercontracts", new[] { "SupplierId" });
            DropIndex("mmport.accountspayrecords", new[] { "AccountsPayableId" });
            DropIndex("mmport.accountspayables", new[] { "OrderContractId" });
            DropTable("mmport.sysmenus");
            DropTable("mmport.products");
            DropTable("mmport.operationlogs");
            DropTable("mmport.coreconfigs");
            DropTable("mmport.saleclients");
            DropTable("mmport.storehouses");
            DropTable("mmport.stockoutrecords");
            DropTable("mmport.stockitems");
            DropTable("mmport.saleclaimcompensations");
            DropTable("mmport.saleclaimcompensationitems");
            DropTable("mmport.saleproductitems");
            DropTable("mmport.salebargainitems");
            DropTable("mmport.salebargainchangerecords");
            DropTable("mmport.salebargains");
            DropTable("mmport.salecontracts");
            DropTable("mmport.accountsreceiverecords");
            DropTable("mmport.accountsreceivables");
            DropTable("mmport.suppliers");
            DropTable("mmport.orderclaimcompensationitems");
            DropTable("mmport.orderclaimcompensations");
            DropTable("mmport.mllogisitems");
            DropTable("mmport.mainlandlogisticscompanies");
            DropTable("mmport.mllogis");
            DropTable("mmport.hongkonglogisticscompanies");
            DropTable("mmport.productitems");
            DropTable("mmport.hklogisitems");
            DropTable("mmport.hklogis");
            DropTable("mmport.harbors");
            DropTable("mmport.declarationcompanies");
            DropTable("mmport.harboragents");
            DropTable("mmport.ordercontracts");
            DropTable("mmport.accountspayrecords");
            DropTable("mmport.accountspayables");
        }
    }
}
