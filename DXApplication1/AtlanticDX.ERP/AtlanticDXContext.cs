namespace AtlanticDX.Model
{
    public class AtlanticDXContext : PrivilegeFramework.ExtendedIdentityDbContext
    {
        public AtlanticDXContext()
            : base(PrivilegeFramework.ExtendedIdentityDbContext.GetNameOrConnectionByConfig())
        {

        }

        public AtlanticDXContext(string nameOrConnection)
            : base(nameOrConnection)
        {

        } 
    }

    public class DevartDbMigrationInitializer :
        System.Data.Entity.MigrateDatabaseToLatestVersion<
                    AtlanticDXContext, Migrations.Configuration>
    {
        //public override void InitializeDatabase(AtlanticDXContext context)
        //{
        //    var config = new Migrations.Configuration();
        //    base.InitializeDatabase(context);
        //    config.SeedExternal(context);
        //}

        #region old
        //static DevartDbConfiguration()
        //{
        //    Devart.Data.MySql.Entity.MySqlEntityProviderServicesConfiguration.Loaded
        //        += MySqlEntityProviderServicesConfiguration_Loaded;
        //}

        //public DevartDbConfiguration()
        //{
        //    //Devart.Data.MySql.MySqlConnection conn =
        //    //    new Devart.Data.MySql.MySqlConnection(
        //    //        System.Configuration.ConfigurationManager.ConnectionStrings["DevelopmentConnection"].ConnectionString);
        //    //var instance = Devart.Data.MySql.Entity.MySqlEntityProviderServices.GetProviderServices(conn);
        //    SetProviderServices("Devart.Data.MySql",
        //        Devart.Data.MySql.Entity.MySqlEntityProviderServices.Instance);
        //}

        //static void MySqlEntityProviderServicesConfiguration_Loaded(object sender,
        //    System.Data.Entity.Infrastructure.DependencyResolution.DbConfigurationLoadedEventArgs e)
        //{
        //}
        #endregion
    }
}