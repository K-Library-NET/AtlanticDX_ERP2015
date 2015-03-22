namespace WebApplication1.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<
        WebApplication1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            this.SetSqlGenerator("Devart.Data.MySql",
               new Devart.Data.MySql.Entity.Migrations.MySqlEntityMigrationSqlGenerator());
        }

        protected override void Seed(WebApplication1.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            this.AddRoles(context);
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

        private async void AddRoles(Models.ApplicationDbContext context)
        {
            return;
            ApplicationRoleManager rolemanager = new ApplicationRoleManager(
                   new Devart.Data.MySql.Web.Identity.MySqlRoleStore<ApplicationUser, ApplicationRole, int>(
                       (new Devart.Data.MySql.Web.Identity.MySqlDataSource(
                           context.Database.Connection.ConnectionString, context.Database.Connection.Database))));

            //new ApplicationDbContext()));
            ApplicationUserManager usermanager = new ApplicationUserManager(
                new Devart.Data.MySql.Web.Identity.MySqlUserStore<ApplicationUser, ApplicationRole, int>//,
                //ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
                   (new Devart.Data.MySql.Web.Identity.MySqlDataSource(
                        context.Database.Connection.ConnectionString, context.Database.Connection.Database)));// (new ApplicationDbContext()));

            try
            {
                var role = await rolemanager.FindByNameAsync("admin");
                if (role == null)
                {
                    var result = await rolemanager.CreateAsync(
                        new ApplicationRole()
                        {
                            Name = "admin"
                        });
                    if (result.Succeeded)
                        role = await rolemanager.FindByNameAsync("admin");
                }

                ApplicationUser user = usermanager.FindByName("gzkeith@163.com");
                if (user != null)
                {
                    if (!usermanager.IsInRole(user.Id, "admin"))
                    {
                        usermanager.AddToRole(user.Id, "admin");
                    }
                }
            }
            catch (Exception ee)
            {
                System.Diagnostics.Debug.WriteLine(ee.Message + "\t\t" + ee.StackTrace);
            }
        }
    }
}
