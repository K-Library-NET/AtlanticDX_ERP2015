namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<
        WebApplication3.Models.ApplicationDbContext>
    //WebApplication3.Models.TestEntity2DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());

            SetHistoryContextFactory("MySql.Data.MySqlClient",
                (conn, schema) => new MySqlHistoryContext(conn, schema));
            //here's the thing.

            DbMigrator migrator = new DbMigrator(this);
            migrator.Update("InitDatabase");
        }

        protected override void Seed(WebApplication3.Models.ApplicationDbContext context)
        //WebApplication3.Models.TestEntity2DbContext context)
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
}
