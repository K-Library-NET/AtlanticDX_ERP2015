using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Finances;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.Privileges;
using YuShang.ERP.Entities.Sale;
using YuShang.ERP.Entities.Stocks;

namespace PrivilegeFramework
{
    public class ExtendedIdentityDbContext
        : Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext<SysUser,
        SysRole, int, SysUserLogin, SysUserRole, SysUserClaim>
    {
        public ExtendedIdentityDbContext()
            : base(DEFAULT_NAME_OR_CONNECTION)
        {
            this.DebugOutput(
                "DbContext Inited:" + this.GetHashCode().ToString()
                + " nameOrConnection: " + DEFAULT_NAME_OR_CONNECTION);
        }

        private void DebugOutput(string output)
        {
            System.Diagnostics.Debug.WriteLine(output);
        }

        public ExtendedIdentityDbContext(string nameOrConnection)
            : base(string.IsNullOrEmpty(nameOrConnection) ? DEFAULT_NAME_OR_CONNECTION : nameOrConnection)
        {
            this.DebugOutput(
                "DbContext Inited:" + this.GetHashCode().ToString()
                + " nameOrConnection: " + (string.IsNullOrEmpty(nameOrConnection)
                ? DEFAULT_NAME_OR_CONNECTION : nameOrConnection));
        }

#if DEBUG
        public static string DEFAULT_NAME_OR_CONNECTION = "name=DevelopmentConnection";
#else 
        public static string DEFAULT_NAME_OR_CONNECTION = "name=ProductionConnection"; 
#endif

        public static string GetNameOrConnectionByConfig()
        {
            try
            {
                var confConnStr = System.Configuration.ConfigurationManager.AppSettings["EF_NameOrConnection"];
                return confConnStr;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return DEFAULT_NAME_OR_CONNECTION;
            }
        }

        public static ExtendedIdentityDbContext Create()
        {
            var confConnStr = GetNameOrConnectionByConfig();
            //System.Configuration.ConfigurationManager.ConnectionStrings["DevelopmentConnection"];
            if (string.IsNullOrEmpty(confConnStr))
            {
                return new ExtendedIdentityDbContext();
            }
            return new ExtendedIdentityDbContext(confConnStr);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SysUser>()
                .Property(h => h.UserName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<SysRole>()
                .Property(h => h.Name).HasMaxLength(100).IsRequired();
        }

        //系统操作
        #region systems

        /// <summary>
        /// 系统配置项
        /// </summary>
        public DbSet<YuShang.ERP.Entities.Configs.CoreConfig> CoreConfigs
        {
            get;
            set;
        }

        /// <summary>
        /// 系统菜单
        /// </summary>
        public DbSet<YuShang.ERP.Entities.Privileges.SysMenu> SysMenus
        {
            get;
            set;
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        public DbSet<YuShang.ERP.Entities.Configs.OperationLog> OperationLogs { get; set; }

        /// <summary>
        /// 功能（area/controller/action）与权限功能挂钩
        /// </summary>
        public DbSet<InheritedPrivilegeLevelRelation> InherPrivLevRelations { get; set; }

        #endregion

        //采购管理
        #region orders

        /// <summary>
        /// 采购合同
        /// </summary>
        public DbSet<OrderContract> OrderContracts { get; set; }

        /// <summary>
        /// 采购合同中的每一件货品
        /// </summary>
        public DbSet<ProductItem> ProductItems { get; set; }

        /// <summary>
        /// 港口代理
        /// </summary>
        public DbSet<HarborAgent> HarborAgents { get; set; }

        /// <summary>
        /// 香港物流
        /// </summary>
        public DbSet<HKLogis> HongKongLogisticsTable { get; set; }

        /// <summary>
        /// 香港物流每件货
        /// </summary>
        public DbSet<HKLogisItem> HongKongLogisticsItems { get; set; }

        /// <summary>
        /// 内地物流
        /// </summary>
        public DbSet<MLLogis> MainlandLogisticsTable { get; set; }

        /// <summary>
        /// 内地物流每件货
        /// </summary>
        public DbSet<MLLogisItem> MainlandLogisticsItems { get; set; }

        /// <summary>
        /// 采购索赔
        /// </summary>
        public DbSet<OrderClaimCompensation> OrderClaimCompensation { get; set; }

        /// <summary>
        /// 采购索赔项
        /// </summary>
        public DbSet<OrderClaimCompensationItem> OrderClaimCompensationItems { get; set; }

        #endregion

        //销售管理
        #region sales

        /// <summary>
        /// 销售合同
        /// </summary>
        public DbSet<SaleContract> SaleContracts { get; set; }

        /// <summary>
        /// 销售货品项
        /// </summary>
        public DbSet<SaleProductItem> SaleProductItems { get; set; }

        /// <summary>
        /// 销售还价
        /// </summary>
        public DbSet<SaleBargain> SaleBargains { get; set; }

        /// <summary>
        /// 销售还价项
        /// </summary>
        public DbSet<SaleBargainItem> SaleBargainItems { get; set; }

        /// <summary>
        /// 销售还价修改记录
        /// </summary>
        public DbSet<SaleBargainChangeRecord> SaleBargainChangeRecords { get; set; }

        /// <summary>
        /// 销售索赔
        /// </summary>
        public DbSet<SaleClaimCompensation> SaleClaimCompensations { get; set; }

        /// <summary>
        /// 销售索赔项
        /// </summary>
        public DbSet<SaleClaimCompensationItem> SaleClaimCompensationItems { get; set; }

        #endregion sales

        //库存管理
        #region inventories

        /// <summary>
        /// 入仓库存项
        /// </summary>
        public DbSet<StockItem> StockItems { get; set; }

        /// <summary>
        /// 出仓记录
        /// </summary>
        public DbSet<StockOutRecord> StockOutRecords { get; set; }

        #endregion

        //资源管理
        #region resource managements

        /// <summary>
        /// 供应商——资源管理
        /// </summary>
        public DbSet<YuShang.ERP.Entities.ResMgr.Supplier> Suppliers
        {
            get;
            set;
        }

        /// <summary>
        /// 报关公司——资源管理
        /// </summary>
        public DbSet<YuShang.ERP.Entities.ResMgr.DeclarationCompany> DeclarationCompanies
        { get; set; }

        /// <summary>
        /// 港口——资源管理
        /// </summary>
        public DbSet<YuShang.ERP.Entities.ResMgr.Harbor> Harbors { get; set; }

        /// <summary>
        /// 销售客户——资源管理
        /// </summary>
        public DbSet<YuShang.ERP.Entities.ResMgr.SaleClient> SaleClients { get; set; }

        /// <summary>
        /// 香港物流公司——资源管理
        /// </summary>
        public DbSet<YuShang.ERP.Entities.ResMgr.HongkongLogisticsCompany> HKLogistics { get; set; }

        /// <summary>
        /// 内地物流公司——资源管理
        /// </summary>
        public DbSet<YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany> MainlandLogistics { get; set; }

        /// <summary>
        /// 商品——资源管理
        /// </summary>
        public DbSet<YuShang.ERP.Entities.ResMgr.Product> Products { get; set; }

        /// <summary>
        /// 仓库——资源管理
        /// </summary>
        public DbSet<YuShang.ERP.Entities.ResMgr.StoreHouse> StoreHouses { get; set; }

        #endregion

        //财务管理
        #region finances

        /// <summary>
        /// 应付账款（欠着供应商的钱）
        /// </summary>
        public DbSet<AccountsPayable> AccountsPayables { get; set; }

        /// <summary>
        /// 应收账款
        /// </summary>
        public DbSet<AccountsReceivable> AccountsReceivables { get; set; }

        /// <summary>
        /// 所有对外对内的财务记录
        /// </summary>
        public DbSet<AccountsRecord> FinancialRecords { get; set; }

        /// <summary>
        /// 财务记录与业务对象的关联关系
        /// </summary>
        public DbSet<AccountsRecordRelation> FinancialRecordRelations { get; set; }

        ///// <summary>
        ///// （对外）付款记录
        ///// </summary>
        //public DbSet<AccountsPayRecord> AccountsPayRecords { get; set; }

        ///// <summary>
        ///// 收款记录
        ///// </summary>
        //public DbSet<AccountsReceiveRecord> AccountsReceiveRecords { get; set; }

        #endregion
    }
}
