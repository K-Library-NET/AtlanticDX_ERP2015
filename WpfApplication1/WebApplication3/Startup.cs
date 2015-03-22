using Microsoft.Owin;
using Owin;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(WebApplication3.Startup))]
namespace WebApplication3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            Init();
        }

        private void Init()
        {
            //System.Data.Entity.Database.SetInitializer(
            //    new System.Data.Entity.MigrateDatabaseToLatestVersion<
            //    WebApplication3.Models.ApplicationDbContext, WebApplication3.Migrations.Configuration>());

            using (WebApplication3.Models.ApplicationDbContext context
                = new WebApplication3.Models.ApplicationDbContext())
            {
                var items = context.Users.ToList();
                System.Console.WriteLine(items);
            }



            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<
            //    WebApplication3.Models.TestEntity2DbContext, WebApplication3.Migrations.Configuration>());

            //using (TestEntity2.TestEntityDbContext context = new WebApplication3.Models.TestEntity2DbContext())
            //{
            //    var items = context.CoreConfigs.ToList();
            //    System.Console.WriteLine(items);
            //}
        }
    }
}
