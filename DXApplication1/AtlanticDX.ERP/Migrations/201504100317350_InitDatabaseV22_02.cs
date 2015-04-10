namespace AtlanticDX.ERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabaseV22_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountsPayables", "PaidAmount", c => c.Double(nullable: false));
            AddColumn("dbo.AccountsPayables", "Currency", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AccountsPayables", "UTIME", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.AccountsPayables", "Memo", c => c.String());
            AddColumn("dbo.AccountsReceivables", "ReceiveAmount", c => c.Double(nullable: false));
            AddColumn("dbo.AccountsReceivables", "Currency", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AccountsReceivables", "UTIME", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.AccountsReceivables", "Memo", c => c.String());
            AddColumn("dbo.AccountsRecords", "AccountsPayableId", c => c.Int());
            AddColumn("dbo.AccountsRecords", "AccountsReceivableId", c => c.Int());
            AddColumn("dbo.AccountsRecords", "Currency", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AccountsRecords", "CurrencyExchangeRate", c => c.Double(nullable: false));
            CreateIndex("dbo.AccountsRecords", "AccountsPayableId");
            CreateIndex("dbo.AccountsRecords", "AccountsReceivableId");
            AddForeignKey("dbo.AccountsRecords", "AccountsPayableId", "dbo.AccountsPayables", "AccountsPayableId");
            AddForeignKey("dbo.AccountsRecords", "AccountsReceivableId", "dbo.AccountsReceivables", "AccountsReceivableId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountsRecords", "AccountsReceivableId", "dbo.AccountsReceivables");
            DropForeignKey("dbo.AccountsRecords", "AccountsPayableId", "dbo.AccountsPayables");
            DropIndex("dbo.AccountsRecords", new[] { "AccountsReceivableId" });
            DropIndex("dbo.AccountsRecords", new[] { "AccountsPayableId" });
            DropColumn("dbo.AccountsRecords", "CurrencyExchangeRate");
            DropColumn("dbo.AccountsRecords", "Currency");
            DropColumn("dbo.AccountsRecords", "AccountsReceivableId");
            DropColumn("dbo.AccountsRecords", "AccountsPayableId");
            DropColumn("dbo.AccountsReceivables", "Memo");
            DropColumn("dbo.AccountsReceivables", "UTIME");
            DropColumn("dbo.AccountsReceivables", "Currency");
            DropColumn("dbo.AccountsReceivables", "ReceiveAmount");
            DropColumn("dbo.AccountsPayables", "Memo");
            DropColumn("dbo.AccountsPayables", "UTIME");
            DropColumn("dbo.AccountsPayables", "Currency");
            DropColumn("dbo.AccountsPayables", "PaidAmount");
        }
    }
}
