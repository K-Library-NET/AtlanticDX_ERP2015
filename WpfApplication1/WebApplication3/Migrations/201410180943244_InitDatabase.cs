namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_Core_Config",
                c => new
                    {
                        CoreConfigId = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength:200, unicode: false),
                        Value = c.String(maxLength: 200, unicode: false),
                        Comments = c.String(maxLength: 200, unicode: false),
                        CommentId = c.Int(nullable: false),
                        CommentName = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.CoreConfigId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tb_Core_Config");
        }
    }
}
