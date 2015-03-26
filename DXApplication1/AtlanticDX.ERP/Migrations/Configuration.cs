#region usings
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Threading.Tasks;
using System.Xml.Linq;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Host;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using UtilityFramework;
using YuShang.ERP.Entities;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.Privileges;
using YuShang.ERP.Entities.Stocks;
using PrivilegeFramework;
#endregion

namespace AtlanticDX.ERP.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<
        AtlanticDX.ERP.AtlanticDXContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("Devart.Data.MySql",
                new Devart.Data.MySql.Entity.Migrations.MySqlEntityMigrationSqlGenerator());

            SetHistoryContextFactory("Devart.Data.MySql",
                (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

        private static bool m_seedExecuted = false;
        private static bool m_isExternalSeeding = false;
        private static object m_externalSeedingSyncRoot = new object();
        private static string m_customMigrationKey = string.Empty;
        private static Dictionary<string, bool> m_executedMigrations =
            new Dictionary<string, bool>();

        public void SeedExternal(AtlanticDX.ERP.AtlanticDXContext context
            , string customMigrationKey)
        {
            if (m_isExternalSeeding)
                return;
            lock (m_externalSeedingSyncRoot)
            {
                m_isExternalSeeding = true;
                try
                {
                    if (!m_executedMigrations.ContainsKey(customMigrationKey))
                    {
                        m_executedMigrations.Add(customMigrationKey, true);
                        this.SeedInternal(context, customMigrationKey);
                    }
                }
                catch (Exception ee)
                {
                    LogHelper.Error("SeedExternal Exceptions happended. ", ee);
                }
                finally
                {
                    m_isExternalSeeding = false;
                    m_customMigrationKey = string.Empty;
                }
            }
        }

        private static bool m_switcher = false;

        protected override void Seed(AtlanticDX.ERP.AtlanticDXContext context)
        {
            LogHelper.Info("DbMigrationsConfiguration Seed to be executed......");
            if (m_seedExecuted)
            //(m_executedMigrations.ContainsKey(m_customMigrationKey)
            //&& m_executedMigrations[m_customMigrationKey])
            //|| (m_seedExecuted && string.IsNullOrEmpty(m_customMigrationKey)))
            {
                LogHelper.Info("DbMigrationsConfiguration has executed this StartUp, Seed skipped.");
                return;
            }

            this.SeedCore(context);
            m_seedExecuted = true;
            // SeedInternal(context);
        }

        protected void SeedCore(AtlanticDXContext context)
        {
            //throw new NotImplementedException();
        }

        private void SeedInternal(AtlanticDX.ERP.AtlanticDXContext context, string customKey)
        {
            try
            {
                CustomMigrationSeed(context, customKey);

                LogHelper.Info(string.Format("[{1}]DbMigrationsConfiguration Seed execution {0} finished......",
                    m_isExternalSeeding ? string.Format("with custom migration [{0}]", m_customMigrationKey) : string.Empty
                    , string.Format("CurrentThread:{0}",
                System.Threading.Thread.CurrentThread.ManagedThreadId)));
            }
            catch (Exception ex)
            {
                LogHelper.Error("CustomMigrationSeed Exceptions: ", ex);
            }
            finally
            {
                m_executedMigrations[customKey] = false;
            }
        }

        private void CustomMigrationSeed(AtlanticDX.ERP.AtlanticDXContext context,
             string customMigrationKey)
        {
            LogHelper.Info(string.Format(
                "[{1}]DbMigrationsConfiguration Seed start executing {0} : ",
                 string.Format("with custom migration [{0}]", customMigrationKey),
                 string.Format("CurrentThread:{0}",
                 System.Threading.Thread.CurrentThread.ManagedThreadId)));

            CustomMigrationHelper.InvokeCustomMigrations(context, customMigrationKey);
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
