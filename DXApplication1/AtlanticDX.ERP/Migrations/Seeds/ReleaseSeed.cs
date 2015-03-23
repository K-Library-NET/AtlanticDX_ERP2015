using PrivilegeFramework;
using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using UtilityFramework;
using YuShang.ERP.Entities.Privileges;
using Microsoft.AspNet.Identity;

namespace AtlanticDX.ERP.Migrations.Releases
{
    public class ReleaseSeed : ICustomMigrationHandler
    {
        public const string MIGRATION_KEY = "ReleaseSeed";

        public string MigrationKey
        {
            get
            {
                return MIGRATION_KEY;
            }
        }

        public CustomMigrationResult CustomMigrationSeed(
            ExtendedIdentityDbContext context, bool isExternalSeeding,
            string customMigrationKey)
        {
            try
            {
                InitMenus(context);
            }
            catch (Exception e)
            {
                LogHelper.Error("初始化菜单项出错。", e);
                return new CustomMigrationResult(MigrationKey, false)
                {
                    ErrorMessage = "初始化菜单项出错。",
                    HappenedException = e,
                };
            }

            try
            {
                InitDefaultResItems(context);
            }
            catch (Exception e)
            {
                LogHelper.Error("初始化资源管理默认项出错。", e);
                return new CustomMigrationResult(MigrationKey, false)
                {
                    ErrorMessage = "初始化资源管理默认项出错。",
                    HappenedException = e,
                };
            }

            try
            {
                IdentityManager manager = new IdentityManager(context);
                var userManager = manager.UserManager;
                var roleManager = manager.RoleManager;
                this.InitRoles(roleManager, context);
            }
            catch (Exception e)
            {
                LogHelper.Error("初始化系统默认角色出错。", e);
                return new CustomMigrationResult(MigrationKey, false)
                {
                    ErrorMessage = "初始化系统默认角色出错。",
                    HappenedException = e,
                };
            }

            try
            {
                this.InitUsers(context);
            }
            catch (Exception e)
            {
                LogHelper.Error("初始化系统默认admin出错。", e);
                return new CustomMigrationResult(MigrationKey, false)
                {
                    ErrorMessage = "初始化系统默认admin出错。",
                    HappenedException = e,
                };
            }

            return new CustomMigrationResult(MigrationKey, true);
        }


        private void InitRoles(ApplicationRoleManager roleManager,
            ExtendedIdentityDbContext context)
        {
            if (roleManager.RoleExists("系统管理员") == false)
            {
                IdentityResult result1 = roleManager.Create<SysRole, int>(new SysRole()
                {
                    Name = "系统管理员",
                    ParentId = -1,
                    PrivilegeLevel = 99
                });
                this.OutputInitError(result1);
            }
            var role1 = roleManager.FindByName("系统管理员");
            if (role1 != null && role1.Id > 0)
            {
                context.Database.ExecuteSqlCommand(string.Format(
                    "UPDATE aspnetroles SET Id = -{0} WHERE Id = {0}"
                    , role1.Id));
            }

            SysRole sysadmin = roleManager.FindById(role1.Id);//.FindByName("系统管理员");
            if (roleManager.RoleExists("Boss") == false)
            {
                IdentityResult result2 = roleManager.Create<SysRole, int>(new SysRole()
                {
                    Name = "Boss",
                    ParentId = sysadmin.Id,
                    PrivilegeLevel = 98
                });
                this.OutputInitError(result2);
            }
            var role2 = roleManager.FindByName("Boss");
            if (role2 != null && role2.Id > 0)
            {
                context.Database.ExecuteSqlCommand(string.Format(
                    "UPDATE aspnetroles SET Id = -{0} WHERE Id = {0}"
                    , role2.Id));
            }

            SysRole boss = roleManager.FindById(role2.Id);//.FindByName("Boss");
            if (roleManager.RoleExists("Manager") == false)
            {
                IdentityResult result3 = roleManager.Create<SysRole, int>(new SysRole()
                {
                    Name = "Manager",
                    ParentId = boss.Id,
                    PrivilegeLevel = 75
                });
                this.OutputInitError(result3);
            }
            var role3 = roleManager.FindByName("Manager");
            if (role3 != null && role3.Id > 0)
            {
                context.Database.ExecuteSqlCommand(string.Format(
                    "UPDATE aspnetroles SET Id = -{0} WHERE Id = {0}"
                    , role3.Id));
            }

            SysRole manager = roleManager.FindById(role3.Id);//.FindByName("Manager");
            if (roleManager.RoleExists("一般用户") == false)
            {
                IdentityResult result4 = roleManager.Create<SysRole, int>(new SysRole()
                {
                    Name = "一般用户",
                    ParentId = manager.Id,
                    PrivilegeLevel = SysRole.DEFAULT_PRIVILEGE_LEVEL
                });
                this.OutputInitError(result4);
            }
            var role4 = roleManager.FindByName("一般用户");
            if (role4 != null && role4.Id > 0)
            {
                context.Database.ExecuteSqlCommand(string.Format(
                    "UPDATE aspnetroles SET Id = -{0} WHERE Id = {0}"
                    , role4.Id));
            }

            SysRole userrole = roleManager.FindById(role4.Id);//.FindByName("一般用户");
            if (roleManager.RoleExists("游客") == false)
            {
                IdentityResult result5 = roleManager.Create<SysRole, int>(new SysRole()
                {
                    Name = "游客",
                    ParentId = userrole.Id,
                    PrivilegeLevel = 1
                });
                this.OutputInitError(result5);
            }
            var role5 = roleManager.FindByName("游客");
            if (role5 != null && role5.Id != 0)
            {
                context.Database.ExecuteSqlCommand(string.Format(
                    "UPDATE aspnetroles SET Id = 0 WHERE Id = {0}"
                    , role5.Id));//Default Role
            }
        }

        private void InitUsers(ExtendedIdentityDbContext context)
        {
            IdentityManager manager = new IdentityManager(context);
            var userManager = manager.UserManager;
            var roleManager = manager.RoleManager;

#if DEBUG
            var user = new SysUser()
            {
                UserName = "admin",
                Email = "gzkeith@163.com",
                Name = "梁达文",
                PhoneNumber = "13826403668",
            };
            FindCreateAssignRole(userManager, roleManager, user, "Admin@12345",
                "系统管理员", context);
#else   
           var user = new SysUser()
            { 
                UserName = "root",
                Email = "13826403668@139.com",
                Name = "Administrator",
                PhoneNumber = "13826403668",
            };
            FindCreateAssignRole(userManager, roleManager, user, 
            "Admin@12345", "系统管理员" , context);
#endif
        }

        private void FindCreateAssignRole(ApplicationUserManager userManager,
           ApplicationRoleManager roleManager, SysUser user,
            string password, string roleName, ExtendedIdentityDbContext context)
        {
            int userid = userManager.Users.AsNoTracking().Where(
                c => c.UserName == user.UserName).Select(u => u.Id).FirstOrDefault();
            if (userid < 1)
            {
                user.CTIME = DateTime.Now;
                var result1 = userManager.Create(user, password);
                if (result1.Succeeded)
                {
                    userid = userManager.Users.AsNoTracking().Where(
                    c => c.UserName == user.UserName).Select(u => u.Id).FirstOrDefault();
                }
            }

            var result2 = userManager.AddToRole(userid, roleName);
            OutputInitError(result2);
            using (var tran = context.Database.BeginTransaction())
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                    string.Format("UPDATE aspnetusers SET Id = -{0} WHERE Id = {0}", userid));
                context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                    string.Format("UPDATE aspnetuserroles SET UserId = -{0} WHERE UserId = {0}", userid));
                tran.Commit();
            }
        }

        private void InitDefaultResItems(ExtendedIdentityDbContext context)
        {
            var zeroSupplier = context.Suppliers.Find(0);
            if (zeroSupplier == null)
            {
                zeroSupplier = context.Suppliers.Create();
                zeroSupplier.SupplierName = "（未选择）";
                zeroSupplier.IsDeleted = true;
                context.Suppliers.Add(zeroSupplier);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(
                    "UPDATE suppliers SET SupplierId = 0 WHERE SupplierId = " + zeroSupplier.SupplierId);
            }

            var zeroHarbor = context.Harbors.Find(0);
            if (zeroHarbor == null)
            {
                zeroHarbor = context.Harbors.Create();
                zeroHarbor.HarborKey = "（未选择）";
                zeroHarbor.IsDeleted = true;
                context.Harbors.Add(zeroHarbor);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(
                    "UPDATE harbors SET HarborId = 0 WHERE HarborId = "
                    + zeroHarbor.HarborId);
            }

            var zeroDeclarCom = context.DeclarationCompanies.Find(0);
            if (zeroDeclarCom == null)
            {
                zeroDeclarCom = context.DeclarationCompanies.Create();
                zeroDeclarCom.CompanyName = "（未选择）";
                zeroDeclarCom.IsDeleted = true;
                context.DeclarationCompanies.Add(zeroDeclarCom);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(
                    "UPDATE DeclarationCompanies SET DeclarationCompanyId = 0 WHERE DeclarationCompanyId = "
                    + zeroDeclarCom.DeclarationCompanyId);
            }

            var zeroHkCom = context.HKLogistics.Find(0);
            if (zeroHkCom == null)
            {
                zeroHkCom = context.HKLogistics.Create();
                zeroHkCom.CompanyName = "（未选择）";
                zeroHkCom.IsDeleted = true;
                context.HKLogistics.Add(zeroHkCom);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(
                    "UPDATE hongkonglogisticscompanies SET HongkongLogisticsCompanyId = 0 WHERE HongkongLogisticsCompanyId = "
                    + zeroHkCom.HongkongLogisticsCompanyId);
            }

            var zeroMlCom = context.MainlandLogistics.Find(0);
            if (zeroMlCom == null)
            {
                zeroMlCom = context.MainlandLogistics.Create();
                zeroMlCom.CompanyName = "（未选择）";
                zeroMlCom.IsDeleted = true;
                context.MainlandLogistics.Add(zeroMlCom);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(
                    "UPDATE mainlandlogisticscompanies SET MainlandLogisticsCompanyId = 0 WHERE MainlandLogisticsCompanyId = "
                    + zeroMlCom.MainlandLogisticsCompanyId);
            }

            var zeroProd = context.Products.Find(0);
            if (zeroProd == null)
            {
                zeroProd = context.Products.Create();
                zeroProd.ProductKey = "（未选择）";
                zeroProd.IsDeleted = true;
                context.Products.Add(zeroProd);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(
                    "UPDATE Products SET ProductId = 0 WHERE ProductId = "
                    + zeroProd.ProductId);
            }

            var zeroSale = context.SaleClients.Find(0);
            if (zeroSale == null)
            {
                zeroSale = context.SaleClients.Create();
                zeroSale.CompanyName = "（未选择）";
                zeroSale.IsDeleted = true;
                context.SaleClients.Add(zeroSale);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(
                    "UPDATE SaleClients SET SaleClientId = 0 WHERE SaleClientId = "
                    + zeroSale.SaleClientId);
            }

            var zeroShouse = context.StoreHouses.Find(0);
            if (zeroShouse == null)
            {
                zeroShouse = context.StoreHouses.Create();
                zeroShouse.StoreHouseName = "（未选择）";
                zeroShouse.IsDeleted = true;
                context.StoreHouses.Add(zeroShouse);
                context.SaveChanges();

                context.Database.ExecuteSqlCommand(
                    "UPDATE StoreHouses SET StoreHouseId = 0 WHERE StoreHouseId = "
                    + zeroShouse.StoreHouseId);
            }

            //var zeroShouse = context.Suppliers.Find(0);
            //if (zeroShouse == null)
            //{
            //    zeroShouse = context.Suppliers.Create();
            //    zeroShouse.StoreHouseName = "（未选择）";
            //    context.Suppliers.Add(zeroShouse);
            //    context.SaveChanges();

            //    context.Database.ExecuteSqlCommand(
            //        "UPDATE StoreHouses SET StoreHouseId = 0 WHERE StoreHouseId = "
            //        + zeroShouse.StoreHouseId);
            //}
        }

        private void InitMenus(ExtendedIdentityDbContext context)
        {
            System.Xml.XmlReader reader
                = System.Xml.XmlReader.Create(new System.IO.StreamReader(
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "App_Data", "MenuLeft.xml"), System.Text.Encoding.UTF8));
            XElement element = XElement.Load(reader);
            var items = element.Elements("item");
            int counter1 = 1;

            foreach (var item in items)
            {
                SysMenu menu = ToMenu(item);
                if (menu != null)
                {
                    menu.IsShowInNavTree = counter1;
                    counter1++;
                    context.SysMenus.AddOrUpdate<SysMenu>(m => m.MenuName, menu);
                    //new { m.Area, m.Controller, m.Action }, menu);

                    UtilityFramework.DbUpdateConcurrencyExceptionResolver
                        .SaveAndResolveExceptionServerWin(context);
                }
                var menu2 = context.SysMenus.FirstOrDefault(m => m.MenuName == menu.MenuName);
                if (menu2 != null)
                    AddDecendants(item, menu2, context);
            }

            IEnumerable<XElement> elementText = element.Descendants("item");

            List<string> names = new List<string>();

            foreach (var xe in elementText)
            {
                string text = xe.Attribute("Text").Value;
                names.Add(text);
                foreach (var des in xe.Descendants("item"))
                {
                    text = des.Attribute("Text").Value;
                    names.Add(text);
                }
            }

            List<SysMenu> tobeRemoved = new List<SysMenu>();

            foreach (SysMenu mu in context.SysMenus)
            {
                if (!names.Contains(mu.MenuName))
                    tobeRemoved.Add(mu);
            }

            if (tobeRemoved.Count > 0)
                context.SysMenus.RemoveRange(tobeRemoved);
            UtilityFramework.DbUpdateConcurrencyExceptionResolver
                .SaveAndResolveExceptionServerWin(context);
        }

        private void AddDecendants(XElement element, SysMenu menu, ExtendedIdentityDbContext context)
        {
            if (element == null || menu == null || context == null)
                return;
            var items = element.Elements("item");
            if (items == null || items.Count() <= 0)
                return;

            int counter2 = 1;
            foreach (var item in items)
            {
                SysMenu child = ToMenu(item);
                if (item != null)
                {
                    child.IsShowInNavTree = counter2;
                    counter2++;
                    child.ParentId = menu.SysMenuId;
                    //context.SysMenus.AddOrUpdate<SysMenu>(m =>
                    //new { m.Area, m.Controller, m.Action }, child);
                    context.SysMenus.AddOrUpdate(m => m.MenuName, child);
                }
            }
            UtilityFramework.DbUpdateConcurrencyExceptionResolver
                .SaveAndResolveExceptionServerWin(context);
        }

        private SysMenu ToMenu(XElement item)
        {
            if (item != null)
            {
                SysMenu menu = new SysMenu()
                {
                    Controller = item.Attribute("Controller") != null ? item.Attribute("Controller").Value : string.Empty,
                    Action = item.Attribute("Action") != null ? item.Attribute("Action").Value : string.Empty,
                    MenuName = item.Attribute("Text") != null ? item.Attribute("Text").Value : string.Empty,
                    //IsShowInNavTree = true,
                    Area = item.Attribute("Area") != null ? item.Attribute("Area").Value : string.Empty,
                    StyleClass = item.Attribute("StyleClass") != null ? item.Attribute("StyleClass").Value : string.Empty,
                };
                return menu;
            }
            return null;
        }
		//test

        private void OutputInitError(IdentityResult result2)
        {
            if (result2 != null && result2.Succeeded == false)
            {
                if (result2.Errors != null && result2.Errors.Count() > 0)
                {
                    string resMsg = result2.Errors.FirstOrDefault();
                    System.Diagnostics.Debug.WriteLine(resMsg);
                    LogHelper.Error(resMsg);
                }
            }
        }
    }
}