using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEntity3;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<
                TestEntityDbContext, TestEntity3.Migrations.Configuration>());

            using (TestEntityDbContext context = new TestEntityDbContext())
            {
                //bool exists = context.Database.Exists();
                //context.Database.CreateIfNotExists();
                var contextObj = context.CoreConfigs.Count();
                System.Console.WriteLine(contextObj);
            }
        }
    }
}
