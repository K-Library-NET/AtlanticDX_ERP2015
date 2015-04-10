namespace AtlanticDX.ERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabaseV22_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderContracts", "Currency", c => c.String(maxLength: 100));
            AddColumn("dbo.OrderContracts", "CurrencyExchangeRate", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "GuidingPrice", c => c.Double(nullable: false));
            AddColumn("dbo.SaleContracts", "Currency", c => c.String(maxLength: 100));
            AddColumn("dbo.SaleContracts", "CurrencyExchangeRate", c => c.Double(nullable: false));
            DropColumn("dbo.OrderClaimCompensationItems", "Currency");
            DropColumn("dbo.ProductItems", "Currency");
            DropColumn("dbo.ProductItems", "SalesGuidePrice");
            DropColumn("dbo.SaleProductItems", "Currency");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SaleProductItems", "Currency", c => c.String());
            AddColumn("dbo.ProductItems", "SalesGuidePrice", c => c.Double());
            AddColumn("dbo.ProductItems", "Currency", c => c.String(maxLength: 100));
            AddColumn("dbo.OrderClaimCompensationItems", "Currency", c => c.String(maxLength: 100));
            DropColumn("dbo.SaleContracts", "CurrencyExchangeRate");
            DropColumn("dbo.SaleContracts", "Currency");
            DropColumn("dbo.Products", "GuidingPrice");
            DropColumn("dbo.OrderContracts", "CurrencyExchangeRate");
            DropColumn("dbo.OrderContracts", "Currency");
        }
    }
}
