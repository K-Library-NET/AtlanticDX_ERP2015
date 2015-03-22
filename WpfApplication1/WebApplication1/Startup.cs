using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


            System.Data.Entity.Database.SetInitializer(
                new System.Data.Entity.MigrateDatabaseToLatestVersion<
                WebApplication1.Models.ApplicationDbContext, Migrations.Configuration>());

            System.Data.Entity.Migrations.DbMigrator migrator =
                new System.Data.Entity.Migrations.DbMigrator(
                    new Migrations.Configuration());
            migrator.Update("InitDatabase");
        }
    }
}
