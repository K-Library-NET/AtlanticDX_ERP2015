using Microsoft.Owin;
using Owin;
using TestMySQLEntity;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(TestEFwithMySQL.Startup))]
namespace TestEFwithMySQL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            InitDatabase();
        }

        private void InitDatabase()
        {
            System.Data.Entity.Database.SetInitializer(
                new System.Data.Entity.MigrateDatabaseToLatestVersion<
                TestEFwithMySQL.Models.ApplicationDbContext, Migrations.Configuration>());

            using (TestMySQLDbContext dbcontext = new TestMySQLDbContext())
            {
                var items = dbcontext.TestEntity1s.ToList();
                Console.WriteLine("ToList: " + items.ToString());
            }
        }
    }
}
