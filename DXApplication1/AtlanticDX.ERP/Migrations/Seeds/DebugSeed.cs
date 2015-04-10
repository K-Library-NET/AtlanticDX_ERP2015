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

namespace AtlanticDX.ERP.Migrations.Debugs
{
    public class DebugSeed : ICustomMigrationHandler
    {

        public string MigrationKey
        {
            get
            {
                return "DebugSeed";
            }
        }

        public CustomMigrationResult CustomMigrationSeed(
            ExtendedIdentityDbContext context, bool isExternalSeeding,
            string customMigrationKey)
        {
            try
            {
                InitProducts(context);
                InitSuppliers(context);
                InitHKLogis(context);
                InitMainlandLogis(context);
                InitDeclarationCompanies(context);
                InitHarbors(context);
                InitStoreHouses(context);
                InitSaleClients(context);
            }
            catch (Exception e)
            {
                LogHelper.Error("初始化基础数据项出错。", e);
                //return new CustomMigrationResult(MigrationKey, false)
                //{
                //    ErrorMessage = "初始化基础数据项出错。",
                //    HappenedException = e,
                //};
            }

            try
            {
                InitUsers(context);
            }
            catch (Exception e)
            {
                LogHelper.Error("初始化用户出错。", e);
                //return new CustomMigrationResult(MigrationKey, false)
                //{
                //    ErrorMessage = "初始化用户出错。",
                //    HappenedException = e,
                //};
            }

            try
            {
                InitOrderContracts(context);
                InitStockItems(context);
            }
            catch (Exception e)
            {
                LogHelper.Error("初始化Demo数据出错。", e);
                //return new CustomMigrationResult(MigrationKey, false)
                //{
                //    ErrorMessage = "初始化Demo数据出错。",
                //    HappenedException = e,
                //    GoToNextWhenExceptionHappend = true,
                //};
            }

            return new CustomMigrationResult(MigrationKey, true);
        }

        private void InitOrderContracts(ExtendedIdentityDbContext context)
        {
            int counter = 1;
            InitOneOrderContractType1(context, counter);
            InitOneOrderContractType0(context, counter);
            counter = 2;
            InitOneOrderContractType1(context, counter);
            InitOneOrderContractType0(context, counter);
            counter = 3;
            InitOneOrderContractType1(context, counter);
            InitOneOrderContractType0(context, counter);
            counter = 4;
            InitOneOrderContractType1(context, counter);
            InitOneOrderContractType0(context, counter);

            SaveChangesResolvingConcurrencyException(context);
        }

        private void InitOneOrderContractType0(ExtendedIdentityDbContext context, int counter)
        {
            string key = "DEMO00" + counter;

            if (context.OrderContracts.Any(m => m.OrderContractKey == key))
                return;

            OrderContract contract = context.OrderContracts.Create();
            contract.OrderContractKey = "DEMO00" + counter;
            contract.OrderCreateTime = DateTime.Now;
            contract.OrderSysUserKey = (counter % 2 == 0) ? "manager" : "user";
            contract.OrderType = 0;
            contract.Payment = "unknown";
            contract.ImportDeposite = 10000;
            contract.ImportBalancedPayment = 5000;
            contract.ShipmentPeriod = "DEMO_SHIP_" + counter;
            contract.SupplierId = context.Suppliers.Min(m => m.SupplierId);
            contract.HarborId = context.Harbors.Max(m => m.HarborId);
            //contract.SalesGuidePrice = 15000;
            contract.ContractStatus = ContractStatus.AuditPassed;
            contract.ContainerSerial = "DEMO_SERIAL_" + counter;
            contract.Comments = "DEMO_COMMENT_" + counter;
            contract.DeliveryBillSerial = "DEMO_DELIVERY_" + counter;
            contract.OrderProducts = this.GenerateOrderProducts(context, counter);

            contract.EntityPrivLevRequired = 75;

            context.OrderContracts.AddOrUpdate(p => p.OrderContractKey, (contract));
        }

        private void InitOneOrderContractType1(ExtendedIdentityDbContext context, int counter)
        {
            string key = "DEMO10" + counter;

            if (context.OrderContracts.Any(m => m.OrderContractKey == key))
                return;

            OrderContract contract = context.OrderContracts.Create();
            contract.OrderContractKey = key;// "DEMO10" + counter;
            contract.OrderCreateTime = DateTime.Now;
            contract.OrderSysUserKey = (counter % 2 == 0) ? "user" : "manager";
            contract.OrderType = 1;
            contract.SupplierId = context.Suppliers.Max(m => m.SupplierId);
            //contract.Destination = context.Harbors.FirstOrDefault();
            contract.HarborId = context.Harbors.Min(m => m.HarborId);
            contract.Payment = "unknown";
            contract.ImportDeposite = 10000;
            contract.ImportBalancedPayment = 5000;
            contract.ShipmentPeriod = "DEMO_SHIP_" + counter;
            //contract.SalesGuidePrice = 15000;
            contract.ContractStatus = ContractStatus.AuditPassed;
            contract.ContainerSerial = "DEMO_SERIAL_" + counter;
            contract.Comments = "DEMO_COMMENT_" + counter;
            contract.DeliveryBillSerial = "DEMO_DELIVERY_" + counter;
            contract.OrderProducts = this.GenerateOrderProducts(context, counter);

            context.OrderContracts.AddOrUpdate(p => p.OrderContractKey, (contract));
        }

        private System.Collections.Generic.ICollection<ProductItem>
            GenerateOrderProducts(ExtendedIdentityDbContext context, int p)
        {
            List<ProductItem> items = new List<ProductItem>();

            ProductItem item = context.ProductItems.Create();
            item.ProductId = context.Products.Max(m => m.ProductId);// 1;
            item.Quantity = 400;
            item.UnitPrice = 200;
            item.NetWeight = 0.15;
            item.Units = "吨";
            item.Comments = "DEMO_PRODUCT_COMMENT_" + p;
            //item.SalesGuidePrice = 1000;
            items.Add(item);
            context.ProductItems.Add(item);

            item = context.ProductItems.Create();
            item.ProductId = context.Products.Max(m => m.ProductId) - 1;// 2;
            item.Quantity = 300;
            item.UnitPrice = 200;
            item.NetWeight = 0.15;
            item.Units = "吨";
            item.Comments = "DEMO_PRODUCT_COMMENT_" + p;
            //item.SalesGuidePrice = 1000;
            items.Add(item);
            context.ProductItems.Add(item);

            item = context.ProductItems.Create();
            item.ProductId = context.Products.Max(m => m.ProductId) - 2;
            item.Quantity = 200;
            item.UnitPrice = 200;
            item.NetWeight = 0.15;
            item.Units = "吨";
            item.Comments = "DEMO_PRODUCT_COMMENT_" + p;
            //item.SalesGuidePrice = 1000;
            items.Add(item);
            context.ProductItems.Add(item);

            item = context.ProductItems.Create();
            item.ProductId = context.Products.Max(m => m.ProductId) - 3;
            item.Quantity = 100;
            item.UnitPrice = 200;
            item.NetWeight = 0.15;
            item.Units = "吨";
            item.Comments = "DEMO_PRODUCT_COMMENT_" + p;
            //item.SalesGuidePrice = 1000;
            items.Add(item);
            context.ProductItems.Add(item);

            return items;
        }

        private void InitStockItems(ExtendedIdentityDbContext context)
        {
            StockItem stockItem = context.StockItems.Create();
            stockItem.Quantity = 50;
            stockItem.StockInDate = DateTime.Now;
            stockItem.StockWeight = 100;
            stockItem.StoreHouseId = context.StoreHouses.Max(m => m.StoreHouseId);// 1;
            stockItem.StoreHouseMountNumber = "DEMO_MOUNT_1";
            stockItem.ProductItemId = context.ProductItems.OrderBy(s => s.ProductItemId).Skip(0).First().ProductItemId;// 1;
            context.StockItems.AddOrUpdate(
                i => new { i.ProductItemId, i.StoreHouseId, i.StoreHouseMountNumber },
                stockItem);

            stockItem = context.StockItems.Create();
            stockItem.Quantity = 60;
            stockItem.StockInDate = DateTime.Now;
            stockItem.StockWeight = 90;
            stockItem.StoreHouseId = context.StoreHouses.Max(m=>m.StoreHouseId)-1;
            stockItem.StoreHouseMountNumber = "DEMO_MOUNT_2";
            stockItem.ProductItemId = context.ProductItems.OrderBy(s => s.ProductItemId).Skip(1).First().ProductItemId;//2;
            context.StockItems.AddOrUpdate(
                i => new { i.ProductItemId, i.StoreHouseId, i.StoreHouseMountNumber },
                stockItem);

            stockItem = context.StockItems.Create();
            stockItem.Quantity = 70;
            stockItem.StockInDate = DateTime.Now;
            stockItem.StockWeight = 80;
            stockItem.StoreHouseId = context.StoreHouses.Max(m => m.StoreHouseId) - 2;
            stockItem.StoreHouseMountNumber = "DEMO_MOUNT_3";
            stockItem.ProductItemId = context.ProductItems.OrderBy(s => s.ProductItemId).Skip(2).First().ProductItemId;//3;
            context.StockItems.AddOrUpdate(
                i => new { i.ProductItemId, i.StoreHouseId, i.StoreHouseMountNumber },
                stockItem);

            stockItem = context.StockItems.Create();
            stockItem.Quantity = 80;
            stockItem.StockInDate = DateTime.Now;
            stockItem.StockWeight = 70;
            stockItem.StoreHouseId = context.StoreHouses.Max(m => m.StoreHouseId) - 3;
            stockItem.StoreHouseMountNumber = "DEMO_MOUNT_4";
            stockItem.ProductItemId = context.ProductItems.OrderBy(s => s.ProductItemId).Skip(3).First().ProductItemId;// 4;
            context.StockItems.AddOrUpdate(
                i => new { i.ProductItemId, i.StoreHouseId, i.StoreHouseMountNumber },
                stockItem);

            SaveChangesResolvingConcurrencyException(context);
        }

        private void InitSaleClients(ExtendedIdentityDbContext context)
        {
            YuShang.ERP.Entities.ResMgr.SaleClient client =
                new YuShang.ERP.Entities.ResMgr.SaleClient()
                {
                    CompanyName = "客户1",
                    Address = "1 Address",
                    Email = "microsoft1@live.cn",
                    FAX = "",
                    MobilePhone = "18327143989",
                    Name = "联系人姓名1",
                    QQ_or_WeChat = "WeChat ABC",
                    Telephone = "02028164234",
                    SaleDepositeStatic = "固定订金20万美元",
                };
            context.SaleClients.AddOrUpdate(m => m.CompanyName, client);

            client = new YuShang.ERP.Entities.ResMgr.SaleClient()
            {
                CompanyName = "客户2",
                Address = "2 Address",
                Email = "microsoft2@live.cn",
                FAX = "",
                MobilePhone = "18388393989",
                Name = "联系人姓名2",
                QQ_or_WeChat = "WeChat DEF",
                Telephone = "02028984234",
                SaleDepositeStatic = "固定订金30万美元",
            };
            context.SaleClients.AddOrUpdate(m => m.CompanyName, client);

            client = new YuShang.ERP.Entities.ResMgr.SaleClient()
            {
                CompanyName = "客户3",
                Address = "3 Address",
                Email = "microsoft3@live.cn",
                FAX = "",
                MobilePhone = "18988393859",
                Name = "联系人姓名3",
                QQ_or_WeChat = "WeChat ZHU",
                Telephone = "02028192819",
                SaleDepositeStatic = "固定订金25万美元",
            };
            context.SaleClients.AddOrUpdate(m => m.CompanyName, client);

            client = new YuShang.ERP.Entities.ResMgr.SaleClient()
            {
                CompanyName = "客户4",
                Address = "4 Address",
                Email = "microsoft4@live.cn",
                FAX = "",
                MobilePhone = "18520473989",
                Name = "联系人姓名4",
                QQ_or_WeChat = "WeChat KKK",
                Telephone = "02084614234",
                SaleDepositeStatic = "固定订金10万美元",
            };
            context.SaleClients.AddOrUpdate(m => m.CompanyName, client);

            client = new YuShang.ERP.Entities.ResMgr.SaleClient()
            {
                CompanyName = "客户5",
                Address = "5 Address",
                Email = "microsoft5@live.cn",
                FAX = "",
                MobilePhone = "18527146327",
                Name = "联系人姓名5",
                QQ_or_WeChat = "WeChat LIB",
                Telephone = "02028163192",
                SaleDepositeStatic = "死活不给固定订金",
            };
            context.SaleClients.AddOrUpdate(m => m.CompanyName, client);

            SaveChangesResolvingConcurrencyException(context);
        }

        private void InitStoreHouses(ExtendedIdentityDbContext context)
        {
            YuShang.ERP.Entities.ResMgr.StoreHouse house =
                new YuShang.ERP.Entities.ResMgr.StoreHouse()
                {
                    Address = "",
                    Email = "storehouse1@mail.10086.cn",
                    FAX = "FAX1234567",
                    MobilePhone = "13650886886",
                    Name = "仓管人员A",
                    Telephone = "02025163223",
                    StorageVolume = "50000平方米",
                    StoreHouseName = "新仓一",
                };
            context.StoreHouses.AddOrUpdate(m => m.StoreHouseName, house);

            house = new YuShang.ERP.Entities.ResMgr.StoreHouse()
            {
                Address = "",
                Email = "storehouse2@mail.10086.cn",
                FAX = "FAX2345671",
                MobilePhone = "13650226886",
                Name = "仓管人员B",
                Telephone = "02025163663",
                StorageVolume = "60000平方米",
                StoreHouseName = "新仓二",
            };
            context.StoreHouses.AddOrUpdate(m => m.StoreHouseName, house);

            house = new YuShang.ERP.Entities.ResMgr.StoreHouse()
            {
                Address = "",
                Email = "storehouse3@mail.10086.cn",
                FAX = "FAX3456712",
                MobilePhone = "13650886886",
                Name = "仓管人员c",
                Telephone = "02025169873",
                StorageVolume = "6000平方米",
                StoreHouseName = "山顶",
            };
            context.StoreHouses.AddOrUpdate(m => m.StoreHouseName, house);

            house = new YuShang.ERP.Entities.ResMgr.StoreHouse()
            {
                Address = "",
                Email = "storehouse4@mail.10086.cn",
                FAX = "FAX4567123",
                MobilePhone = "13650886995",
                Name = "仓管人员d",
                Telephone = "02020303223",
                StorageVolume = "25000平方米",
                StoreHouseName = "城东A",
            };
            context.StoreHouses.AddOrUpdate(m => m.StoreHouseName, house);
            SaveChangesResolvingConcurrencyException(context);
        }

        private void InitHarbors(ExtendedIdentityDbContext context)
        {
            YuShang.ERP.Entities.ResMgr.Harbor
                harbor = new YuShang.ERP.Entities.ResMgr.Harbor()
                {
                    HarborKey = "HKHKG",
                    HarborName = "中国香港",
                    HarborNameENG = "Hongkong"
                };
            context.Harbors.AddOrUpdate(m => m.HarborKey, harbor);

            harbor = new YuShang.ERP.Entities.ResMgr.Harbor()
            {
                HarborKey = "CNQIN",
                HarborName = "中国青岛",
                HarborNameENG = "QINGDAO"
            };
            context.Harbors.AddOrUpdate(m => m.HarborKey, harbor);

            harbor = new YuShang.ERP.Entities.ResMgr.Harbor()
            {
                HarborKey = "CNQUA",
                HarborName = "中国泉州",
                HarborNameENG = "QUANZHOU"
            };
            context.Harbors.AddOrUpdate(m => m.HarborKey, harbor);

            harbor = new YuShang.ERP.Entities.ResMgr.Harbor()
            {
                HarborKey = "CNHPU",
                HarborName = "中国黄埔",
                HarborNameENG = "HUANGPU"
            };
            context.Harbors.AddOrUpdate(m => m.HarborKey, harbor);

            harbor = new YuShang.ERP.Entities.ResMgr.Harbor()
            {
                HarborKey = "CNLYG",
                HarborName = "中国连云港",
                HarborNameENG = "LIANYUNGANG"
            };
            context.Harbors.AddOrUpdate(m => m.HarborKey, harbor);

            harbor = new YuShang.ERP.Entities.ResMgr.Harbor()
            {
                HarborKey = "CNSHA",
                HarborName = "中国上海",
                HarborNameENG = "SHANGHAI"
            };
            context.Harbors.AddOrUpdate(m => m.HarborKey, harbor);

            SaveChangesResolvingConcurrencyException(context);
        }

        private void InitDeclarationCompanies(ExtendedIdentityDbContext context)
        {
            YuShang.ERP.Entities.ResMgr.DeclarationCompany company
                = new YuShang.ERP.Entities.ResMgr.DeclarationCompany()
                {
                    Address = "Address 12345",
                    CompanyName = "港口代理公司1",
                    Email = "email1@live.com",
                    FAX = "",
                    Name = "nobody1",
                    MobilePhone = "",
                    Telephone = "17017077778"
                };
            context.DeclarationCompanies.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.DeclarationCompany()
            {
                Address = "Address 23456",
                CompanyName = "港口代理公司2",
                Email = "email2@live.com",
                FAX = "",
                Name = "nobody2",
                MobilePhone = "",
                Telephone = "17017077778"
            };
            context.DeclarationCompanies.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.DeclarationCompany()
            {
                Address = "Address 34567",
                CompanyName = "港口代理公司3",
                Email = "email3@live.com",
                FAX = "",
                Name = "nobody3",
                MobilePhone = "",
                Telephone = "17017077778"
            };
            context.DeclarationCompanies.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.DeclarationCompany()
            {
                Address = "Address 45678",
                CompanyName = "港口代理公司4",
                Email = "email4@live.com",
                FAX = "",
                Name = "nobody4",
                MobilePhone = "",
                Telephone = "17017077778"
            };
            context.DeclarationCompanies.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.DeclarationCompany()
            {
                Address = "Address 67890",
                CompanyName = "港口代理公司5",
                Email = "email5@live.com",
                FAX = "",
                Name = "nobody5",
                MobilePhone = "",
                Telephone = "17017077778"
            };
            context.DeclarationCompanies.AddOrUpdate(m => m.CompanyName, company);

            SaveChangesResolvingConcurrencyException(context);
        }

        private void InitMainlandLogis(ExtendedIdentityDbContext context)
        {
            YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany company =
                new YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany()
                {
                    Address = "Address1",
                    CompanyName = "国通1（内地物流）",
                    Email = "email1@hotmail.com",
                    FAX = "",
                    MobilePhone = "13717399883",
                    Name = "Contact Person 1",
                    QQ_or_WeChat = "QQ1",
                    Telephone = "+02017272666"
                };
            context.MainlandLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany()
            {
                Address = "Address2",
                CompanyName = "圆通2（内地物流）",
                Email = "email2@hotmail.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 2",
                QQ_or_WeChat = "QQ2",
                Telephone = "+0085217272666"
            };
            context.MainlandLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany()
            {
                Address = "Address3",
                CompanyName = "申通3（内地物流）",
                Email = "email3@hotmail.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 3",
                QQ_or_WeChat = "QQ3",
                Telephone = "+01917272666"
            };
            context.MainlandLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany()
            {
                Address = "Address4",
                CompanyName = "中通4（内地物流）",
                Email = "email4@hotmail.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 4",
                QQ_or_WeChat = "QQ4",
                Telephone = "+02917272666"
            };
            context.MainlandLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany()
            {
                Address = "Address5",
                CompanyName = "顺丰5（内地物流）",
                Email = "email5@hotmail.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 5",
                QQ_or_WeChat = "QQ5",
                Telephone = "+02517272666"
            };
            context.MainlandLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany()
            {
                Address = "Address6",
                CompanyName = "德邦6（内地物流）",
                Email = "email6@hotmail.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 6",
                QQ_or_WeChat = "QQ6",
                Telephone = "+02817272666"
            };
            context.MainlandLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany()
            {
                Address = "Address7",
                CompanyName = "韵达7（内地物流）",
                Email = "email7@hotmail.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 7",
                QQ_or_WeChat = "QQ7",
                Telephone = "+02717272666"
            };
            context.MainlandLogistics.AddOrUpdate(m => m.CompanyName, company);

            SaveChangesResolvingConcurrencyException(context);
        }

        private void InitHKLogis(ExtendedIdentityDbContext context)
        {
            YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany company =
                new YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany()
                {
                    Address = "Address1",
                    CompanyName = "新新1（HK物流）",
                    Email = "email1@outlook.com",
                    FAX = "",
                    MobilePhone = "13717399883",
                    Name = "Contact Person 1",
                    QQ_or_WeChat = "QQ1",
                    Telephone = "+0085217272666"
                };
            context.HKLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany()
            {
                Address = "Address2",
                CompanyName = "新新2（HK物流）",
                Email = "email2@outlook.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 2",
                QQ_or_WeChat = "QQ2",
                Telephone = "+0085217272666"
            };
            context.HKLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany()
            {
                Address = "Address3",
                CompanyName = "新新3（HK物流）",
                Email = "email3@outlook.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 3",
                QQ_or_WeChat = "QQ3",
                Telephone = "+0085217272666"
            };
            context.HKLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany()
            {
                Address = "Address4",
                CompanyName = "新新4（HK物流）",
                Email = "email4@outlook.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 4",
                QQ_or_WeChat = "QQ4",
                Telephone = "+0085217272666"
            };
            context.HKLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany()
            {
                Address = "Address5",
                CompanyName = "新新5（HK物流）",
                Email = "email5@outlook.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 5",
                QQ_or_WeChat = "QQ5",
                Telephone = "+0085217272666"
            };
            context.HKLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany()
            {
                Address = "Address6",
                CompanyName = "新新6（HK物流）",
                Email = "email6@outlook.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 6",
                QQ_or_WeChat = "QQ6",
                Telephone = "+0085217272666"
            };
            context.HKLogistics.AddOrUpdate(m => m.CompanyName, company);

            company = new YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany()
            {
                Address = "Address7",
                CompanyName = "新新7（HK物流）",
                Email = "email7@outlook.com",
                FAX = "",
                MobilePhone = "13717399883",
                Name = "Contact Person 7",
                QQ_or_WeChat = "QQ7",
                Telephone = "+0085217272666"
            };
            context.HKLogistics.AddOrUpdate(m => m.CompanyName, company);

            SaveChangesResolvingConcurrencyException(context);
        }

        private void InitSuppliers(ExtendedIdentityDbContext context)
        {
            YuShang.ERP.Entities.ResMgr.Supplier supplier
                = new YuShang.ERP.Entities.ResMgr.Supplier()
                {
                    Address = "Address1",
                    DepositeRates = 0.2,
                    EMail = "abcde@139.com",
                    FAX = "+8602087889232",
                    MobilePhone = "13913913939",
                    Name = "联系人1",
                    QQ_or_WeChat = "QQ1",
                    SupplierName = "供应商1",
                    SupplierPayment = "固定订金20万美金",
                    Telephone = "+8602087998799"
                };
            context.Suppliers.AddOrUpdate(m => m.SupplierName, supplier);

            supplier = new YuShang.ERP.Entities.ResMgr.Supplier()
            {
                Address = "Address2",
                DepositeRates = 0.3,
                EMail = "defgw@139.com",
                FAX = "+8602087889232",
                MobilePhone = "13913913940",
                Name = "联系人2",
                QQ_or_WeChat = "QQ2",
                SupplierName = "供应商2",
                SupplierPayment = "固定订金120万人民币",
                Telephone = "+8602087998789"
            };
            context.Suppliers.AddOrUpdate(m => m.SupplierName, supplier);

            supplier = new YuShang.ERP.Entities.ResMgr.Supplier()
            {
                Address = "Address3",
                DepositeRates = 0.15,
                EMail = "qqqqqqq@139.com",
                FAX = "+8602087882232",
                MobilePhone = "13913813939",
                Name = "联系人3",
                QQ_or_WeChat = "QQ3",
                SupplierName = "供应商3",
                SupplierPayment = "固定订金100万",
                Telephone = "+8602087888799"
            };
            context.Suppliers.AddOrUpdate(m => m.SupplierName, supplier);

            supplier = new YuShang.ERP.Entities.ResMgr.Supplier()
            {
                Address = "Address4",
                DepositeRates = 0.25,
                EMail = "wwwww@139.com",
                FAX = "+8602087949232",
                MobilePhone = "13918913939",
                Name = "联系人4",
                QQ_or_WeChat = "QQ4",
                SupplierName = "供应商4",
                SupplierPayment = "",
                Telephone = "+8602087088708"
            };
            context.Suppliers.AddOrUpdate(m => m.SupplierName, supplier);

            supplier = new YuShang.ERP.Entities.ResMgr.Supplier()
            {
                Address = "Address5",
                DepositeRates = 0.0,
                EMail = "18938653688@139.com",
                FAX = "+8602000269232",
                MobilePhone = "13918613939",
                Name = "联系人5",
                QQ_or_WeChat = "QQ5",
                SupplierName = "供应商5",
                SupplierPayment = "不需要付钱:)",
                Telephone = "+8602087008799"
            };
            context.Suppliers.AddOrUpdate(m => m.SupplierName, supplier);

            supplier = new YuShang.ERP.Entities.ResMgr.Supplier()
            {
                Address = "Address6",
                DepositeRates = 0.12,
                EMail = "13751753873@139.com",
                FAX = "+8602087269233",
                MobilePhone = "13918013939",
                Name = "联系人6",
                QQ_or_WeChat = "QQ6",
                SupplierName = "供应商6",
                SupplierPayment = "固定订金12万美金",
                Telephone = "+8602087080899"
            };
            context.Suppliers.AddOrUpdate(m => m.SupplierName, supplier);

            supplier = new YuShang.ERP.Entities.ResMgr.Supplier()
            {
                Address = "Admin12345",
                DepositeRates = 0.26,
                EMail = "admin12345@139.com",
                FAX = "+8602087881234",
                MobilePhone = "13826313939",
                Name = "联系人7",
                QQ_or_WeChat = "QQ7",
                SupplierName = "供应商7",
                SupplierPayment = "订金26%美金",
                Telephone = "+8603179907799"
            };
            context.Suppliers.AddOrUpdate(m => m.SupplierName, supplier);

            supplier = new YuShang.ERP.Entities.ResMgr.Supplier()
            {
                Address = "Adminqwert",
                DepositeRates = 0.11,
                EMail = "abcde@163.com",
                FAX = "+8602000266329",
                MobilePhone = "13202024141",
                Name = "联系人8",
                QQ_or_WeChat = "QQ8",
                SupplierName = "供应商8",
                SupplierPayment = "订金50%",
                Telephone = "+8602087336496"
            };
            context.Suppliers.AddOrUpdate(m => m.SupplierName, supplier);

            SaveChangesResolvingConcurrencyException(context);
        }

        private void InitUsers(ExtendedIdentityDbContext context)
        {
            IdentityManager manager = new IdentityManager(context);
            var userManager = manager.UserManager;
            var roleManager = manager.RoleManager;

            //InitRoles(roleManager);

#if DEBUG
            //var user = new SysUser()
            //{
            //    UserName = "admin",
            //    Email = "gzkeith@163.com",
            //    Name = "梁达文",
            //    PhoneNumber = "13826403668",
            //};
            //FindCreateAssignRole(userManager, roleManager, user, "Admin@12345", "系统管理员");
            var user = new SysUser()
             {
                 //Id = Guid.NewGuid().ToString(),
                 UserName = "manager",
                 Email = "iris2013@live.cn",
                 Name = "Iris Xiao",
                 PhoneNumber = "18666031989",
             };
            FindCreateAssignRole(userManager, roleManager, user, "Iris2013", "Manager");
            user = new SysUser()
            {
                //Id = Guid.NewGuid().ToString(),
                UserName = "user",
                Email = "common_user@live.cn",
                Name = "Common User",
                PhoneNumber = "18666031989",
            };
            FindCreateAssignRole(userManager, roleManager, user, "CommonUser", "一般用户");
#else
            //var user = new SysUser()
            //{
            //    //Id = Guid.NewGuid().ToString(),
            //    UserName = "root",
            //    Email = "13826403668@139.com",
            //    Name = "Administrator",
            //    PhoneNumber = "13826403668",
            //};
            //FindCreateAssignRole(userManager, roleManager, user, "Admin@12345", "系统管理员");
           var user = new SysUser()
            {
                //Id = Guid.NewGuid().ToString(),
                UserName = "manager",
                Email = "iris2013@live.cn",
                Name = "Iris Xiao",
                PhoneNumber = "18666031989",
            };
            FindCreateAssignRole(userManager, roleManager, user, "Iris2013", "Manager");
            user = new SysUser()
            {
                //Id = Guid.NewGuid().ToString(),
                UserName = "user",
                Email = "common_user@live.cn",
                Name = "Common User",
                PhoneNumber = "18666031989",
            };
            FindCreateAssignRole(userManager, roleManager, user, "CommonUser", "一般用户");
#endif

        }

        private void FindCreateAssignRole(ApplicationUserManager userManager,
           ApplicationRoleManager roleManager, SysUser user, string password, string roleName)
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
            //this.AddToRoleCore(userManager, roleManager, user, userid, roleName);
        }

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

        /* //OLD: 
        private void AddToRoleCore(ApplicationUserManager userManager,
           ApplicationRoleManager roleManager, SysUser user, int userid, string roleName)
        {
            if (userManager.IsInRole(userid, roleName))
                return;

            try
            {
                var result = userManager.AddToRole(userid, roleName);
                if (result.Succeeded == false)
                {
                    LogHelper.Error("添加用户ID {0} 到角色 {1} 时出错：" + result.Errors.First());
                }
            }
            catch (DbUpdateConcurrencyException db)
            {
                var entries = db.Entries.ToArray();
                foreach (var entry in entries)
                {
                    var dbvalues = entry.GetDatabaseValues();
                    var originValues = entry.OriginalValues;
                    var currentValues = entry.CurrentValues;
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    entry.Reload();
                }
            }
        }
        */

        private void InitRoles(ApplicationRoleManager roleManager)
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

            SysRole sysadmin = roleManager.FindByName("系统管理员");
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

            SysRole boss = roleManager.FindByName("Boss");
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

            SysRole manager = roleManager.FindByName("Manager");
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

            SysRole userrole = roleManager.FindByName("一般用户");
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
        }

        private void SaveChangesResolvingConcurrencyException(
            ExtendedIdentityDbContext context)
        {
            DbUpdateConcurrencyExceptionResolver.SaveAndResolveExceptionServerWin(context);
        }

        private void InitProducts(ExtendedIdentityDbContext context)
        {
            YuShang.ERP.Entities.ResMgr.Product product = new YuShang.ERP.Entities.ResMgr.Product()
            {
                MadeInCountry = "波兰",
                MadeInFactory = "10023802",
                Brand = "Polonia",
                ProductNameENG = "Pork front feet",
                ProductName = "猪手",
                Grade = "A",
                Specification = "带趾",
                Packing = "10KG",
                UnitsPerMonth = "45",
                ProductKey = "CC23343",
            };
            context.Products.AddOrUpdate(p => p.ProductKey, product);

            product = new YuShang.ERP.Entities.ResMgr.Product()
            {
                MadeInCountry = "美国",
                MadeInFactory = "P32",
                Brand = "Majar",
                ProductNameENG = "Chick Hands",
                ProductName = "鸡尖",
                Specification = "",
                Packing = "20公斤定装",
                UnitsPerMonth = "45",
                ProductKey = "US23343",
            };
            context.Products.AddOrUpdate(p => p.ProductKey, product);

            product = new YuShang.ERP.Entities.ResMgr.Product()
            {
                MadeInCountry = "美国",
                MadeInFactory = "P32",
                Brand = "Majar",
                ProductNameENG = "Chick Feets",
                ProductName = "凤爪",
                Grade = "A",
                Specification = "",
                Packing = "20公斤定装",
                UnitsPerMonth = "45",
                ProductKey = "US23350",
            };
            context.Products.AddOrUpdate(p => p.ProductKey, product);

            product = new YuShang.ERP.Entities.ResMgr.Product()
            {
                MadeInCountry = "美国",
                MadeInFactory = "P1065",
                Brand = "Kero",
                ProductNameENG = "Chick Feets",
                ProductName = "凤爪",
                Grade = "B",
                Specification = "",
                Packing = "30磅定装",
                UnitsPerMonth = "45",
                ProductKey = "US9950",
            };
            context.Products.AddOrUpdate(p => p.ProductKey, product);

            SaveChangesResolvingConcurrencyException(context);
        }
    }
}