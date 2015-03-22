using Microsoft.Owin;
using PrivilegeFramework.AppBusinessImpl;
using PrivilegeFramework.PrivilegeFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityFramework;
using YuShang.ERP.Entities.Finances;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.ResMgr;
using YuShang.ERP.Entities.Sale;
using YuShang.ERP.Entities.Stocks;

namespace PrivilegeFramework
{
    public class AppBusinessManager
    {
        protected AppBusinessManager()
        {
            m_orderContractImpl = new OrderContractBusinessImpl();
            m_saleContractImpl = new SaleContractBusinessImpl();
        }

        private static AppBusinessManager m_manager = null;

        private OrderContractBusinessImpl m_orderContractImpl = null;
        private SaleContractBusinessImpl m_saleContractImpl = null;

        public static AppBusinessManager Instance
        {
            get
            {
                if (m_manager == null)
                    m_manager = new AppBusinessManager();

                return m_manager;
            }
        }

        internal static string GetUserName(ContractListCondition condition,
            Microsoft.Owin.IOwinContext owinContext)
        {
            if (!string.IsNullOrEmpty(condition.UserName))
                return condition.UserName;
            if (owinContext.Authentication.User != null
                && owinContext.Authentication.User.Identity != null)
            {
                return owinContext.Authentication.User.Identity.Name;
            }

            return string.Empty;
        }

        #region ordercontract

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="db"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IQueryable<OrderContract> GetIndexListOrderContract(IOwinContext owinContext,
            ExtendedIdentityDbContext dbContext, ContractListCondition condition)
        {
            return m_orderContractImpl.GetIndexListOrderContract(owinContext,
                dbContext, condition);
        }

        /// <summary>
        /// 返回需要数据权限控制的OrderContract列表，
        /// 包括分页/过滤等
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="ETA"></param>
        /// <param name="filterValue"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [Obsolete("Old")]
        public IQueryable<OrderContract> GetIndexListOrderContract(IOwinContext owinContext,
            ExtendedIdentityDbContext dbContext, DateTime? ETA, string filterValue,
            string userName, out int count)
        {
            return m_orderContractImpl.GetIndexListOrderContract(owinContext,
                dbContext, ETA, filterValue, userName, out count);
        }

        /// <summary>
        /// 获取订单列表，根据IOrderContractPrivilegeFilter过滤。
        /// 只返回IQueryable对象，外面业务层决定分页逻辑
        /// </summary>
        /// <param name="db"></param>
        /// <param name="filter"></param>
        /// <param name="ProductsTotal">商品数量小计</param>
        /// <param name="PaymentTotal">采购额小计</param>
        /// <returns></returns>
        public IQueryable<OrderContract> GetIndexListOrderContract(IOwinContext context,
            ExtendedIdentityDbContext db, IOrderContractPrivilegeFilter filter,
            out double ProductsTotal, out double PaymentTotal)
        {
            return m_orderContractImpl.GetIndexListOrderContract(context,
                db, filter, out ProductsTotal, out PaymentTotal);
        }

        /// <summary>
        /// 新增OrderContract。
        /// 返回值为异常信息，如果返回空字符串则没有异常。
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string AddOrderContractCore(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName)
        {
            return m_orderContractImpl.AddOrderContractCore(dbContext, model, userName);
        }

        /// <summary>
        /// 交单（审核通过），如果model的OrderContractId/OrderContractKey找不到数据库的记录，
        /// 会强行创建一个（方便填写完了“直接交单”）。
        /// 只有model.ContractStatus == ContractStatus.AuditPassed才会创建对应的财务记录。
        /// 注意：此方法不会修改OrderContract下面的ProductItem
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string SubmitOrderContract(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName)
        {
            return m_orderContractImpl.SubmitOrderContract(dbContext, model, userName);
        }

        /// <summary>
        /// 修改OrderContract（需要“归档”并且建立新的订单？）
        /// 返回值为异常信息，如果返回空字符串则没有异常。
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string UpdateOrderContractCore(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName)
        {
            return m_orderContractImpl.UpdateOrderContractCore(
                dbContext, model, userName);
        }

        #region ordercontract non-cores

        /// <summary>
        /// 对订单添加“港口代理”
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="harborAgent"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        //public string AddOrUpdateOrderContractHarborAgent(
        //    ExtendedIdentityDbContext dbContext, OrderContract contract,
        //    HarborAgent harborAgent, string userName)
        //{
        //    return m_orderContractImpl.AddOrUpdateOrderContractHarborAgent(
        //        dbContext, contract, harborAgent, userName);
        //}

        ///// <summary>
        ///// 对订单添加“香港物流”
        ///// </summary>
        ///// <param name="dbContext"></param>
        ///// <param name="hklogis"></param>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //public string AddOrUpdateOrderContractHKLogis(
        //    ExtendedIdentityDbContext dbContext, OrderContract contract,
        //    HKLogis hklogis, string userName)
        //{
        //    return m_orderContractImpl.AddOrUpdateOrderContractHKLogis(
        //        dbContext, contract, hklogis, userName);
        //}


        ///// <summary>
        ///// 对订单添加“内地物流”
        ///// </summary>
        ///// <param name="dbContext"></param>
        ///// <param name="hklogis"></param>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //public string AddOrUpdateOrderContractMLLogis(
        //    ExtendedIdentityDbContext dbContext, OrderContract contract,
        //    MLLogis hklogis, string userName)
        //{
        //    return m_orderContractImpl.AddOrUpdateOrderContractMLLogis(
        //        dbContext, contract, hklogis, userName);
        //}

        /// <summary>
        /// 根据一个ProductItem的索赔对象，新增或修改索赔
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddOrUpdateOrderCompensationItem(
            ExtendedIdentityDbContext dbContext, OrderClaimCompensationItem item)
        {
            return m_orderContractImpl.AddOrUpdateOrderCompensationItem(
                dbContext, item);
        }

        public IQueryable<ProductItem> GetIndexListProductItemByReceiveFilter(
            ExtendedIdentityDbContext dbContext, IQueryable<ProductItem> sourceQuery,
            string receiveFilterValue, DateTime? OrderCreateTimeFrom, DateTime? OrderCreateTimeTo,
            string userName)
        {
            return m_orderContractImpl.GetIndexListProductItemByReceiveFilter(dbContext,
                sourceQuery, receiveFilterValue, OrderCreateTimeFrom,
                OrderCreateTimeTo, userName);
        }

        public string UpdateImportProductItemReceive(ExtendedIdentityDbContext db, int productItemId,
            DateTime receivedDate, ProductItemStatus productItemStatus, string comments)
        {
            return m_orderContractImpl.UpdateImportProductItemReceive(
                db, productItemId, receivedDate, productItemStatus, comments);
        }

        #endregion

        #endregion

        #region aggregations

        /// <summary>
        /// 获取订单总数，需要根据权限过滤
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public double GetOrdersTotal(string userName)
        {

            //debug 
            return 1234;
        }

        /// <summary>
        /// 获取订单已付的订金总数，需要根据权限过滤
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public double GetOrderDepositeTotal(string userName)
        {

            //debug
            return 5678.90;
        }

        /// <summary>
        /// 商品数量合计，需要根据用户名进行权限过滤
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public double GetProductsTotal(string userName)
        {

            //debug
            return 3333;
        }

        /// <summary>
        /// 合计采购额，需要根据用户名进行权限过滤
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public double GetOrderPaymentTotal(string userName)
        {

            //debug
            return 4444;
        }

        #endregion

        #region logistics

        public IQueryable<ProductItem> GetIndexListProductItemByHKLogistics(
            ExtendedIdentityDbContext dbContext, IQueryable<ProductItem> sourceQuery,
            string logisticsCompanyName, DateTime? ReceiveTimeFrom, DateTime? ReceiveTimeTo,
            string userName)
        {
            IQueryable<ProductItem> query = sourceQuery;
            //dbContext.ProductItems.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(logisticsCompanyName))
            {
                string temp = logisticsCompanyName.Trim();
                query = query.Where(p => p.OrderContract.HongKongLogistics != null
                    && p.OrderContract.HongKongLogistics.HKLogisCompany.
                        CompanyName.ToLowerInvariant().Contains(temp));
            }

            if (ReceiveTimeFrom.HasValue && ReceiveTimeTo.HasValue)
            {
                query = query.Where(q => q.ReceiveTime.HasValue
                    && q.ReceiveTime.Value >= ReceiveTimeTo.Value
                     && q.ReceiveTime.Value <= ReceiveTimeTo.Value);
            }
            else if (ReceiveTimeFrom.HasValue)
            {
                query = query.Where(q => q.ReceiveTime.HasValue &&
                    q.ReceiveTime.Value >= ReceiveTimeFrom.Value);
            }
            else if (ReceiveTimeTo.HasValue)
            {
                query = query.Where(q => q.ReceiveTime.HasValue &&
                   q.ReceiveTime.Value <= ReceiveTimeTo.Value);
            }
            else
            {
                //不需要加条件
            }

            #region old
            //query = CombineTimeSpan(dbContext, ReceiveTimeFrom, ReceiveTimeTo, query);

            //List<int> ids = new List<int>();

            //FilterByHKLogisComName(dbContext, logisticsCompanyName, ids);

            //var orderContractIds = ids.Distinct();
            //if (orderContractIds != null && orderContractIds.Count() > 0)
            //{//查出来ID，那就需要加入这个条件 
            //    query = query.Where(
            //        ord => orderContractIds.Contains(ord.OrderContractId));
            //}
            #endregion

            //TODO: Filter OrderContract by userName
            if (!string.IsNullOrWhiteSpace(userName))
            {//TODO: add query condition to query

            }

            return query;
        }

        public IQueryable<ProductItem> GetIndexListProductItemByMainlandLogistics(
            ExtendedIdentityDbContext dbContext, IQueryable<ProductItem> sourceQuery,
            string logisticsCompanyName, DateTime? ReceiveTimeFrom, DateTime? ReceiveTimeTo,
            string userName)
        {
            IQueryable<ProductItem> query = sourceQuery;
            //dbContext.ProductItems.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(logisticsCompanyName))
            {
                string temp = logisticsCompanyName.Trim();
                query = query.Where(p => p.OrderContract.MainlandLogistics != null
                    && p.OrderContract.MainlandLogistics.MLLogisCompany.
                        CompanyName.ToLowerInvariant().Contains(temp));
            }

            if (ReceiveTimeFrom.HasValue && ReceiveTimeTo.HasValue)
            {
                query = query.Where(q => q.ReceiveTime.HasValue
                    && q.ReceiveTime.Value >= ReceiveTimeTo.Value
                     && q.ReceiveTime.Value <= ReceiveTimeTo.Value);
            }
            else if (ReceiveTimeFrom.HasValue)
            {
                query = query.Where(q => q.ReceiveTime.HasValue &&
                    q.ReceiveTime.Value >= ReceiveTimeFrom.Value);
            }
            else if (ReceiveTimeTo.HasValue)
            {
                query = query.Where(q => q.ReceiveTime.HasValue &&
                   q.ReceiveTime.Value <= ReceiveTimeTo.Value);
            }
            else
            {
                //不需要加条件
            }

            #region old
            //List<int> ids = new List<int>();

            //this.FilterByMainlandLogisComName(dbContext, logisticsCompanyName, ids);

            //if (!string.IsNullOrWhiteSpace(logisticsCompanyName))
            //{
            //    var logisComs = ResBusinessManager.Instance.GetMainlandLogisticsCompanies();

            //    //var comIds = (from one in logisComs
            //    //              where one.CompanyName.ToLowerInvariant().Contains(logisticsCompanyName.Trim().ToLowerInvariant())
            //    //              select one.MainlandLogisticsCompanyId).Join(dbContext.MainlandLogisticsTable,
            //    //           comId => comId, mainlandlogis => mainlandlogis.MainlandLogisticsCompanyId,
            //    //           (comId, mainlandlogis) => mainlandlogis.OrderContractId);

            //    //query = dbContext.OrderContracts.AsNoTracking().Where(
            //    //     ord => comIds.Contains(ord.OrderContractId));
            //}

            //this.CombineTimeSpan(dbContext, ReceiveTimeFrom, ReceiveTimeTo, ids);

            //var orderContractIds = ids.Distinct();
            //if (orderContractIds != null && orderContractIds.Count() > 0)
            //{//查出来ID，那就需要加入这个条件 
            //    query = query.Where(
            //        ord => orderContractIds.Contains(ord.OrderContractId));
            //}
            #endregion

            //TODO: Filter OrderContract by userName
            if (!string.IsNullOrWhiteSpace(userName))
            {//TODO: add query condition to query

            }

            return query;
        }

        #endregion


        #region salecontract

        public IQueryable<SaleContract> GetIndexListSaleContract(IOwinContext owinContext,
            ExtendedIdentityDbContext dbContext, ContractListCondition condition)
        {
            return m_saleContractImpl.GetIndexListSaleContract(owinContext,
                dbContext, condition);
        }

        [Obsolete("Old")]
        public IQueryable<YuShang.ERP.Entities.Sale.SaleContract> GetIndexListSaleContract(
            ExtendedIdentityDbContext dbContext, int? orderType, DateTime? DateFrom, DateTime? DateTo,
            string filterValue, string userName, out int count)
        {
            return m_saleContractImpl.GetIndexListSaleContract(dbContext, orderType,
                DateFrom, DateTo, filterValue, userName, out count);
        }

        /// <summary>
        /// 添加销售合同，不管期货还是现货，根据model的OrderType来定
        /// </summary>
        /// <param name="dbcontract"></param>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string AddSaleContractCore(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName)
        {
            return m_saleContractImpl.AddSaleContractCore(dbContext,
                model, userName);
        }

        public string UpdateSaleContractCore(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName)
        {
            return m_saleContractImpl.UpdateSaleContractCore(dbContext,
                model, userName);
        }

        public string AddOrUpdateSaleBargain(ExtendedIdentityDbContext
            dbContext, SaleBargain bargain)
        {
            return m_saleContractImpl.AddOrUpdateSaleContractBargainCore(dbContext, bargain);
        }

        public string UpdateSaleContractStatusCore(ExtendedIdentityDbContext dbContext,
            SaleContract contract, ContractStatus contractStatus, string userName)
        {
            return m_saleContractImpl.UpdateSaleContractStatusCore(dbContext,
                contract, contractStatus, userName);
        }

        #endregion

        public string AddOrUpdateStockItem(ExtendedIdentityDbContext db,
            StockItem model, string userName)
        {
            try
            {
                ProductItem productItem = db.ProductItems.Find(model.ProductItemId);
                StockItem item = db.StockItems.Create();
                item.ProductItem = productItem;// model.ProductItem;
                item.ProductItemId = productItem.ProductItemId;// model.ProductItemId;
                item.Quantity = model.Quantity;
                item.StockInDate = DateTime.Now;
                item.StockStatus = model.StockStatus;// StockStatus.InStock;
                item.StockWeight = model.StockWeight;
                item.StoreHouse = model.StoreHouse;
                item.StoreHouseId = model.StoreHouseId;
                item.StoreHouseMountNumber = model.StoreHouseMountNumber;
                item.IsAllSold = model.IsAllSold;
                //if (productItem.Quantity <= item.Quantity)
                //{
                //    productItem.Status = ProductItemStatus.Received;
                //    productItem.ReceiveTime = DateTime.Now;
                //}
                db.StockItems.Add(item);
                db.SaveChanges();

                return string.Empty;
            }
            catch (Exception e)
            {
                string errorMsg = e.Message;
                if ((e is AggregateException) &&
                    (e as AggregateException).InnerException != null)
                {
                    errorMsg = string.Format(errorMsg + "\t\t{0}",
                        (e as AggregateException).InnerException.Message);
                    LogHelper.Error(errorMsg, e.InnerException);
                }
                LogHelper.Error(errorMsg, e);
                return errorMsg;
            }
        }

        public string AddStockOutForUpdateStockItem(ExtendedIdentityDbContext db,
            StockOutRecord model, string userName)
        {
            try
            {
                StockItem stockItem = db.StockItems.Find(model.StockItemId);
                if (stockItem != null)
                {
                    StockOutRecord record = this.GetStockOutRecord(
                        db, stockItem, model);
                    record.OperatorSysUserName = userName;
                    stockItem.StockOutRecords.Add(record);

                    var outWeight = stockItem.StockOutRecords.Sum(
                        m => m.StockWeight.GetValueOrDefault());
                    if (outWeight <= 0)
                    {
                        stockItem.StockStatus = StockStatus.InStock;
                    }
                    else if (stockItem.StockWeight.GetValueOrDefault() > outWeight)
                    {
                        stockItem.StockStatus = StockStatus.InStockSelling;
                    }
                    else
                    {
                        stockItem.StockStatus = StockStatus.IsSold;
                        stockItem.IsAllSold = true;
                    }
                    return AppBusinessManager.Instance.AddOrUpdateStockItem(
                        db, stockItem, userName);
                }
                return "找不到对应的库存，可能已经出仓。";
            }
            catch (Exception e)
            {
                string errorMsg = string.Format("库存出仓失败。发生异常：{0}", e.Message);
                LogHelper.Error(errorMsg, e);
                if ((e is AggregateException) &&
                    (e as AggregateException).InnerException != null)
                {
                    var tmp = (e as AggregateException).InnerException;
                    errorMsg += tmp.Message;
                    LogHelper.Error(errorMsg, tmp);
                }
                return errorMsg;
            }
        }

        private StockOutRecord GetStockOutRecord(ExtendedIdentityDbContext db,
            StockItem stockItem, StockOutRecord model)
        {
            StockOutRecord record = db.StockOutRecords.Create();

            record.StockItemId = model.StockItemId;
            record.StockWeight = model.StockWeight;
            record.StockOutDate = DateTime.Now;
            record.SaleContractId = model.SaleContractId;
            record.RemainderStockWeight = model.RemainderStockWeight;
            record.RemainderQuantity = model.RemainderQuantity;
            record.Quantity = model.Quantity;
            record.InventoriesFeeSubTotal = model.InventoriesFeeSubTotal;
            record.Comments = model.Comments;

            return record;
        }

        public string AddOrUpdateSaleClaimCompensation(IOwinContext owinContext,
            ExtendedIdentityDbContext db, SaleClaimCompensation model, SaleCompensationInfoItem viewModel)
        {
            try
            {
                if (model == null)
                {//新建索赔
                    model = db.SaleClaimCompensations.Create();
                    if (viewModel.SaleProductItemId.HasValue)
                    {
                        var saleItem = db.SaleProductItems.Find(viewModel.SaleProductItemId.Value);

                        if (saleItem != null)
                        {
                            saleItem.SaleContract.ClaimCompensation = model;
                            db.SaleClaimCompensations.Add(model);
                        }
                    }
                }

                if (model != null)
                {
                    var compItem = db.SaleClaimCompensationItems.FirstOrDefault(
                          m => m.SaleProductItemId == viewModel.SaleProductItemId.Value);
                    if (compItem == null)
                    {
                        compItem = db.SaleClaimCompensationItems.Create();
                        model.SaleClaimCompensationItems.Add(compItem);
                        db.SaleClaimCompensationItems.Add(compItem);
                    }
                    compItem.SaleProductItemId = viewModel.SaleProductItemId.Value;
                    compItem.CompensationReason = viewModel.CompensationReason;
                    compItem.Compensation = viewModel.Compensation.GetValueOrDefault();
                }

                int rec = db.SaveChanges();
                if (rec < 1)
                    return "销售索赔失败。";
            }
            catch (Exception e)
            {
                string errorMsg = "销售索赔失败。" + e.Message;
                LogHelper.Error(errorMsg, e);
                if ((e is AggregateException) && (e as AggregateException).InnerException != null)
                {
                    LogHelper.Error(errorMsg, (e as AggregateException).InnerException);
                    errorMsg += "\t\t" + (e as AggregateException).InnerException.Message;
                }
                return errorMsg;
            }
            return string.Empty;
        }

        public string AddOrUpdateSaleProductItem(IOwinContext owinContext,
            ExtendedIdentityDbContext db, SaleProductItem item)
        {
            try
            {
                int rec = db.SaveChanges();
                if (rec < 1)
                    return "销售货品状态信息更新失败。";
            }
            catch (Exception e)
            {
                string errorMsg = "销售货品状态信息更新失败。" + e.Message;
                LogHelper.Error(errorMsg, e);
                if ((e is AggregateException) && (e as AggregateException).InnerException != null)
                {
                    LogHelper.Error(errorMsg, (e as AggregateException).InnerException);
                    errorMsg += "\t\t" + (e as AggregateException).InnerException.Message;
                }
                return errorMsg;
            }
            return string.Empty;
        }
    }
}
