namespace TestEntity3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_Core_Config",
                c => new
                    {
                        CoreConfigId = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        Comments = c.String(),
                        CommentId = c.Int(nullable: false),
                        CommentName = c.String(),
                    })
                .PrimaryKey(t => t.CoreConfigId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tb_Core_Config");
        }
    }
}
