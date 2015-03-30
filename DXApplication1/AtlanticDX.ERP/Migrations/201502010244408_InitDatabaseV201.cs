namespace AtlanticDX.ERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabaseV201 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "MadeInCountry", c => c.String(maxLength: 100));
            AlterColumn("dbo.Products", "MadeInFactory", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "MadeInFactory", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "MadeInCountry", c => c.String(nullable: false));
        }
    }
}
