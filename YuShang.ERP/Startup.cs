using Microsoft.Owin;
using Owin;
using System.Linq;
using System;
using YuShang.ERP.Entities;
using Devart.Data.MySql.Entity;

[assembly: OwinStartupAttribute(typeof(YuShang.ERP.Startup))]
namespace YuShang.ERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Init();//debug
            ConfigureAuth(app);
        }

        private void Init()
        {
            ////如果需要更新数据库，则去掉下面的注释
            //System.Data.Entity.Database.SetInitializer(
            //    new System.Data.Entity.MigrateDatabaseToLatestVersion<
            //    PrivilegeFramework.ExtendedIdentityDbContext,//YuShang.ERP.Models.ApplicationDbContext,
            //    Migrations.Configuration>());

            //try
            //{
            //    //需要数据库迁移的时候就去掉如下代码的注释
            //    //Update里面的字符串参数指代每次Add-Migration的Name
            //    System.Data.Entity.Migrations.DbMigrator migrator
            //        = new System.Data.Entity.Migrations.DbMigrator(new Migrations.Configuration());
            //    migrator.Update("InitDatabase");
            //}
            //catch (Exception e)
            //{
            //    LogHelper.Error("Start Code First Upgrade Database to latest......", e);
            //}

            LogHelper.Info("Start Code First Upgrade Database to latest......");
            //using (PrivilegeFramework.ExtendedIdentityDbContext context
            //    = new PrivilegeFramework.ExtendedIdentityDbContext(
            //        PrivilegeFramework.ExtendedIdentityDbContext.GetNameOrConnectionByConfig()))
            //{
            //    bool exists = context.Database.Exists();
            //    //if (exists == false)
            //    //    context.Database.Create();
            //    //var temp = context.Database.CreateIfNotExists();//.ToList();

            //    var temp = context.CoreConfigs.FirstOrDefault();

            //    Console.WriteLine("Database exists: " + exists.ToString());
            //}
        }
    }
}
