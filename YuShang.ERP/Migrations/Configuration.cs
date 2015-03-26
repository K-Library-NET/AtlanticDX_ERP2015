namespace YuShang.ERP.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Model1>
    //PrivilegeFramework.ExtendedIdentityDbContext>
    ////YuShang.ERP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("Devart.Data.MySql",
                new Devart.Data.MySql.Entity.Migrations.MySqlEntityMigrationSqlGenerator());

            SetHistoryContextFactory("Devart.Data.MySql",
                (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

        protected override void Seed(Model1 context)
        //YuShang.ERP.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }

    public class MySqlHistoryContext :
       System.Data.Entity.Migrations.History.HistoryContext
    {
        public MySqlHistoryContext(System.Data.Common.DbConnection connection,
            string defaultSchema)
            : base(connection, defaultSchema)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<System.Data.Entity.Migrations.History.HistoryRow>()
                .Property(h => h.MigrationId).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<System.Data.Entity.Migrations.History.HistoryRow>()
                .Property(h => h.ContextKey).HasMaxLength(200).IsRequired();
        }
    }

    //public class MySqlConfiguration : DbConfiguration
    //{
    //    public MySqlConfiguration()
    //    {
    //        SetHistoryContext("MySql.Data.MySqlClient", 
    //            (conn, schema) => new MySqlHistoryContext(conn, schema));
    //    }
    //}

    //public class MySqlInitializer : IDatabaseInitializer<YuShang.ERP.Models.ApplicationDbContext>
    //{
    //    public void InitializeDatabase(YuShang.ERP.Models.ApplicationDbContext context)
    //    {
    //        if (!context.Database.Exists())
    //        {
    //            context.Database.Create();
    //        }
    //        else
    //        {
    //            var migrationHistoryTableExists = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
    //                string.Format(
    //                "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '__MigrationHistory'",
    //                "XXXÊý¾Ý¿âÃû"
    //                ));

    //            if (migrationHistoryTableExists.FirstOrDefault() == 0)
    //            {
    //                context.Database.Delete();
    //                context.Database.Create();
    //            }
    //        }
    //    }
    //}
}
