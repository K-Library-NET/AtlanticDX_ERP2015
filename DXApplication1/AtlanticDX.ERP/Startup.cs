using System;
using System.Linq;
using System.Reflection;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using PrivilegeFramework;
using UtilityFramework;

[assembly: OwinStartup(typeof(AtlanticDX.Model.Startup))]

namespace AtlanticDX.Model
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigurateAuth(app);

            this.InitDatabases();
        }

        private void ConfigurateAuth(IAppBuilder app)
        {
            // 配置数据库上下文、用户管理器和登录管理器，以便为每个请求使用单个实例
            app.CreatePerOwinContext(PrivilegeFramework.ExtendedIdentityDbContext.Create);
            //ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // 使应用程序可以使用 Cookie 来存储已登录用户的信息
            // 并使用 Cookie 来临时存储有关使用第三方登录提供程序登录的用户的信息
            // 配置登录 Cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Public/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // 当用户登录时使应用程序可以验证安全戳。
                    // 这是一项安全功能，当你更改密码或者向帐户添加外部登录名时，将使用此功能。
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager,
                        YuShang.ERP.Entities.Privileges.SysUser, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => manager.GenerateUserIdentityAsync(user),
                        getUserIdCallback: delegate(System.Security.Claims.ClaimsIdentity ident)
                        {
                            try
                            {
                                int temp = ident.GetUserId<int>();
                                return temp;
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Fatal("ClaimsIdentity.GetUserId<int>() Failed! ", ex);
                            }
                            return -1;
                        }),
                    //(claimidentity) => claimidentity.GetUserId<int>() ),
                    //user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 使应用程序可以在双重身份验证过程中验证第二因素时暂时存储用户信息。
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // 使应用程序可以记住第二登录验证因素，例如电话或电子邮件。
            // 选中此选项后，登录过程中执行的第二个验证步骤将保存到你登录时所在的设备上。
            // 此选项类似于在登录时提供的“记住我”选项。
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // 取消注释以下行可允许使用第三方登录提供程序登录
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        private void InitDatabases()
        {
            //debug
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            var test = Devart.Data.MySql.Entity.MySqlEntityProviderServices.Instance;
            System.Data.Entity.DbConfiguration.Loaded +=
                (sender, e) =>
                e.ReplaceService<System.Data.Entity.Core.Common.DbProviderServices>(
                    (services, o) =>
                        Devart.Data.MySql.Entity.MySqlEntityProviderServices.Instance
                    );

            try
            {
                LogHelper.Info("Start Code First Upgrade Database to latest......");

                //必须留下下面这行，配置使用Devart数据生成MySQL的脚本
                System.Data.Entity.Database.SetInitializer(
                    //new System.Data.Entity.DropCreateDatabaseIfModelChanges<AtlanticDXContext>());
                   new DevartDbMigrationInitializer());

                var config = new Migrations.Configuration();

                using (AtlanticDXContext context = new AtlanticDXContext())
                {
                    System.Data.Entity.Migrations.DbMigrator migrator
                        = new System.Data.Entity.Migrations.DbMigrator(config);
                    config.TargetDatabase = new System.Data.Entity.Infrastructure.DbConnectionInfo(
                        context.Database.Connection.ConnectionString, "Devart.Data.MySql");
                    var migrationsss = migrator.GetDatabaseMigrations();
                    var temp11 = migrator.GetLocalMigrations();
                    var temp22 = migrator.GetPendingMigrations();

                    temp22 = temp22.Except(migrationsss);

                    if (temp22 != null && temp22.Count() > 0)
                    {
                        LogHelper.Info(string.Format("Start Code First force default migrations......"));
                        migrator.Update();
                        LogHelper.Info("Finish Code First default migration. ");
                    }
                    else if (this.GetForceExternalSeedingFromConfig())
                    {//20150120 liangdawen: force to seed! 
                        LogHelper.Info(string.Format("Start Code First force Seeding external......"));
                        migrator.Update();
                        LogHelper.Info("Finish Code First force Seeding external. ");
                    }

                    //20150131 custom migration seeding
                    string[] upgrades = GetUpgradesFromConfig();
                    if (upgrades != null && upgrades.Length > 0)
                    {
                        LogHelper.Info(string.Format("Start Code First force custom seeds......"));
                        foreach (var key in upgrades)
                        {
                            this.CustomMigration(key, migrator, config, context);
                            LogHelper.Info(string.Format("Finish Code First custom seed: {0} .", key));
                        }
                        LogHelper.Info("Finish Code First force custom seeds. ");
                    }

                    LogHelper.Info(string.Format("Code First Upgrade Database to latest completed."));
                }
            }
            catch (Exception e)
            {
                string errMsg = "Start Code First Upgrade Database to latest Exception: "
                    + e.Message + "\t\t" + e.StackTrace;
                if (e.InnerException != null)
                {
                    errMsg += "\r\nInnerException: " + e.InnerException.Message + "\t\t"
                        + e.InnerException.StackTrace;
                }

                LogHelper.Error(errMsg);
            }
        }

        private void CustomMigration(string key, System.Data.Entity.Migrations.DbMigrator migrator,
            Migrations.Configuration config, AtlanticDXContext context)
        {
            //migrator.Update(key);
            ////var config = new AtlanticDX.ERP.Migrations.Configuration();
            //if (this.GetForceExternalSeedingFromConfig())
            config.SeedExternal(context, key);
        }

        private bool GetForceExternalSeedingFromConfig()
        {
            string values = System.Configuration.ConfigurationManager.AppSettings["ForceExternalSeeding"];
            if (!string.IsNullOrEmpty(values)
                && Boolean.TrueString.Equals(values, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private string[] GetUpgradesFromConfig()
        {
            string values = System.Configuration.ConfigurationManager.AppSettings["CodeFirstCustomMigrations"];
            if (!string.IsNullOrEmpty(values))
            {
                string[] splited = values.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                return splited;
            }

            return null;
        }

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("Devart"))
            {
                int shortArgNameLast = args.Name.IndexOf(',');
                string shortArgName = args.Name;
                if (shortArgNameLast > 0 && shortArgNameLast <= shortArgName.Length)
                {
                    shortArgName = shortArgName.Substring(0, shortArgNameLast);
                }

                string dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\");
                var files = System.IO.Directory.EnumerateFiles(dir, shortArgName + "*.DLL", System.IO.SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    try
                    {
                        string shortFile = System.IO.Path.GetFileNameWithoutExtension(file);
                        if (shortFile == shortArgName)
                        {
                            var assem = Assembly.LoadFrom(file);
                            System.Console.WriteLine("Assembly Loaded: "
                                + assem.ToString() + "\t" + file);
                            return assem;
                        }
                    }
                    catch (Exception ee)
                    {
                        LogHelper.Error("Assembly Load Failed: "
                                + file, ee);
                        continue;
                    }
                }
            }

            //string dir = AppDomain.CurrentDomain.BaseDirectory + "bin\\zh-Hans";
            //var assem = Assembly.LoadFrom(System.IO.Path.Combine(dir, "EntityFramework.resources.dll"));

            //return assem;

            return null;
        }
    }
}
