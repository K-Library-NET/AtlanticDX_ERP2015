using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.Sale;
using YuShang.ERP.Entities.Stocks;

namespace PrivilegeFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ContractManager
    {
        protected ContractManager()
        {

        }

        private static ContractManager m_manager = null;

        public static ContractManager Instance
        {
            get
            {
                if (m_manager == null)
                    m_manager = new ContractManager();

                return m_manager;
            }
        }

        public ContractViewModel GetIndexListContracts(IOwinContext context,
            ExtendedIdentityDbContext db, ContractListCondition filter)
        {
            //Debug: 先不管那么多，只返回最简单的
            if (context == null)
                throw new ArgumentNullException("context");
            if (db == null)
                throw new ArgumentNullException("db");

            if (filter == null || filter.IsEnable == false)
            {
                var defCondition = ContractListCondition.GetDefault();
                defCondition.UserName = context.Authentication.User.Identity.Name;
                filter = defCondition;
            }
            return this.GetIndexListContracts(db, context, filter);

            //return new ContractViewModel() { IsEnable = false };
        }

        private ContractViewModel GetIndexListContracts(
            ExtendedIdentityDbContext db, IOwinContext context,
            ContractListCondition condition)
        {
            IQueryable<OrderContract> orderContractQuery = null;
            IQueryable<SaleContract> saleContractQuery = null; 
            int count = 0;
            ContractListInclude include = condition.ListInclude & ContractListInclude.OrderContractOnly;
            if (include == ContractListInclude.OrderContractOnly)
            {
                orderContractQuery = AppBusinessManager.Instance.GetIndexListOrderContract(
                   context, db, condition);
                //  null, string.Empty, condition.UserName, out ot1);
            }
            include = condition.ListInclude & ContractListInclude.SaleContractOnly;
            if (include == ContractListInclude.SaleContractOnly)
            {
                saleContractQuery = AppBusinessManager.Instance.GetIndexListSaleContract(
                    context, db, condition);
                // db, condition.OrderType, null, null, string.Empty, condition.UserName, out ot2);
            }
            if (orderContractQuery == null)
                orderContractQuery = db.OrderContracts;

            var dbQuery = orderContractQuery.Select(m => new
              {
                  ContractId = m.OrderContractId,
                  ContractKey = m.OrderContractKey,
                  ContractType = ContractViewModelType.OrderContract,
                  CTIME = m.OrderCreateTime,
                  OrderContract = m,
                  SaleContract = default(SaleContract),
              }); 
            if ((condition.ListInclude & ContractListInclude.SaleContractOnly)
                == ContractListInclude.SaleContractOnly)
            {
                dbQuery.Concat(saleContractQuery.Select(n => new
               {
                   ContractId = n.SaleContractId,
                   ContractKey = n.SaleContractKey,
                   ContractType = ContractViewModelType.SaleContract,
                   CTIME = n.SaleCreateTime,
                   OrderContract = default(OrderContract),
                   SaleContract = n,
               }));
            }

            if ((condition.ListInclude & ContractListInclude.WithAggregations)
                == ContractListInclude.WithAggregations)
            {
                count = dbQuery.Count();
            }

            switch (condition.OrderField)
            {
                case ContractOrderField.CTIME_DESC:
                    {
                        dbQuery = dbQuery.OrderByDescending(v => v.CTIME);
                        break;
                    }
                case ContractOrderField.CONTRACT_KEY_DESC:
                    {
                        dbQuery = dbQuery.OrderByDescending(v => v.ContractKey);
                        break;
                    }
                case ContractOrderField.CONTRACT_KEY_ASC:
                    {
                        dbQuery = dbQuery.OrderBy(v => v.ContractKey);
                        break;
                    }
                case ContractOrderField.CTIME_ASC:
                    {
                        dbQuery = dbQuery.OrderBy(v => v.CTIME);
                        break;
                    }
                default: break;
            }

            if (condition.Page.HasValue && condition.Rows.HasValue)
            {
                dbQuery = dbQuery.Skip((condition.Page.Value - 1)
                       * condition.Rows.Value).Take(condition.Rows.Value);
            }

            IEnumerable<ContractInfo> infos = dbQuery.ToList().Select(res =>
                res.ContractType == ContractViewModelType.SaleContract ?
                new ContractInfo(res.SaleContract) : new ContractInfo(res.OrderContract));

            ContractViewModel resultModel = new ContractViewModel()
            {
                IsEnable = true,
                ContractItems = new List<ContractInfo>(infos),
                Aggregations = new AggregationsViewModel()
                {
                    IsEnable = true,
                    Count = count,
                }
            };

            return resultModel;


            //return GetIndexListContractsBothOrderAndSale(db, context, condition,
            //    orderContractQuery, saleContractQuery);
        }

        private ContractViewModel GetIndexListContractsBothOrderAndSale(
            ExtendedIdentityDbContext db, IOwinContext context,
            ContractListCondition condition, IQueryable<OrderContract> orderQuery,
            IQueryable<SaleContract> saleQuery)
        {
            List<ContractInfo> viewModels = new List<ContractInfo>();

            UnionRecords(condition, orderQuery, saleQuery, viewModels);

            var finalResult = HandleOrderBy(condition, viewModels);

            return finalResult;
        }

        private void UnionRecords(ContractListCondition condition,
            IQueryable<OrderContract> orderQuery, IQueryable<SaleContract> saleQuery,
            List<ContractInfo> viewModels)
        {
            if (orderQuery != null)
            {
                var temp1 = orderQuery.ToArray();
                IEnumerable<ContractInfo> result1 = from one in temp1//orderQuery
                                                    select new ContractInfo(one);
                //ContractInfo.Build(one, condition.ListInclude);
                if (result1.Count() > 0)
                    viewModels.AddRange(result1);
            }
            if (saleQuery != null)
            {
                var temp2 = saleQuery.ToArray();
                IEnumerable<ContractInfo> result2 = from two in temp2//saleQuery
                                                    select new ContractInfo(two);
                //ContractInfo.Build(two, condition.ListInclude);
                if (result2.Count() > 0)
                    viewModels.AddRange(result2);
            }
        }

        private ContractViewModel HandleOrderBy(
            ContractListCondition condition, List<ContractInfo> viewModels)
        {
            ContractViewModel resultModel = new ContractViewModel();
            IOrderedEnumerable<ContractInfo> orderResult = null;

            switch (condition.OrderField)
            {
                case ContractOrderField.CTIME_DESC:
                    {
                        orderResult = viewModels.OrderByDescending(v => v.CTIME);
                        break;
                    }
                case ContractOrderField.CONTRACT_KEY_DESC:
                    {
                        orderResult = viewModels.OrderByDescending(v => v.ContractKey);
                        break;
                    }
                case ContractOrderField.CONTRACT_KEY_ASC:
                    {
                        orderResult = viewModels.OrderBy(v => v.ContractKey);
                        break;
                    }
                case ContractOrderField.CTIME_ASC:
                    {
                        orderResult = viewModels.OrderBy(v => v.CTIME);
                        break;
                    }
                default: break;
            }

            if (condition.Page.HasValue && condition.Rows.HasValue)
            {
                resultModel.IsEnable = true;
                var finalResult = orderResult.Skip((condition.Page.Value - 1)
                     * condition.Rows.Value).Take(condition.Rows.Value);
                resultModel.ContractItems = new List<ContractInfo>(finalResult);
                resultModel.Aggregations = new AggregationsViewModel() { IsEnable = true };
                resultModel.Aggregations.Count = orderResult.Count();
            }

            return resultModel;
        }

        public ContractInfo FindBy(ExtendedIdentityDbContext db, IOwinContext context,
            string contractKey, ContractViewModelType contractType, ContractListCondition condition)
        {
            if (db == null || context == null)
                throw new ArgumentNullException();

            if (contractType != ContractViewModelType.OrderContract)
            {
                if (condition == null || condition.IsEnable.GetValueOrDefault() == false)
                {
                    var first = db.SaleContracts.FirstOrDefault(
                        m => m.SaleContractKey == contractKey);
                    if (first != null)
                        return new ContractInfo(first);
                }
                else
                {
                    return this.FindSaleContractBy(db, context, contractKey, condition);
                }
            }
            else
            {
                if (condition == null || condition.IsEnable.GetValueOrDefault() == false)
                    return this.FindOrderContractBy(db, context, contractKey, null, null);
                return this.FindOrderContractBy(db, context, contractKey, condition);
            }

            return null;
        }

        private ContractInfo FindSaleContractBy(ExtendedIdentityDbContext db,
            IOwinContext context, string contractKey, ContractListCondition condition)
        {
            throw new NotImplementedException();
        }

        private ContractInfo FindOrderContractBy(ExtendedIdentityDbContext db,
            IOwinContext context, string contractKey, ContractListCondition condition)
        {
            DbQuery<OrderContract> contractQuery = db.OrderContracts.Include("OrderProducts.Product");
            //ContractListInclude include = ContractListInclude.None;
            if ((ContractListInclude.WithHarborAgent & condition.ListInclude)
                == ContractListInclude.WithHarborAgent)
            {
                contractQuery = contractQuery.Include("HarborAgent.DeclarationCompany");
            }
            if ((ContractListInclude.WithHongkongLogistics & condition.ListInclude)
                 == ContractListInclude.WithHongkongLogistics)
            {
                contractQuery = contractQuery.Include("HongKongLogistics.HongKongLogisticsItems.ProductItem.Product");
                contractQuery = contractQuery.Include("HongKongLogistics.HKLogisCompany");
            }
            if ((ContractListInclude.WithMainlandLogistics & condition.ListInclude)
                 == ContractListInclude.WithMainlandLogistics)
            {
                contractQuery = contractQuery.Include("MainlandLogistics.MainlandLogisticsItems.ProductItem.Product");
                contractQuery = contractQuery.Include("MainlandLogistics.MLLogisCompany");
            }

            return FindOrderContractBy(db, context, contractKey, condition, contractQuery);
        }

        private ContractInfo FindOrderContractBy(ExtendedIdentityDbContext db,
            IOwinContext context, string contractKey, ContractListCondition condition, DbQuery<OrderContract> contractQuery)
        {
            if (contractQuery == null)
                contractQuery = db.OrderContracts.AsNoTracking();
            var query = contractQuery as IQueryable<OrderContract>;

            if (condition != null && condition.IsEnable.GetValueOrDefault() &&
                !string.IsNullOrEmpty(condition.UserName))
            {
                query = AppendOrderContractEntityPrivilege(query, condition.UserName);
            }
            var first = query.FirstOrDefault(m => m.OrderContractKey == contractKey);
            if (first != null)
                return new ContractInfo(first);

            return null;
        }

        internal static IQueryable<OrderContract> AppendOrderContractEntityPrivilege(
            IQueryable<OrderContract> source, string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                int level = PrivilegeFramework.PrivilegeManager.Instance.GetEntityPrivilegeLevel(userName);

                source = source.Where(m => (
                    m.EntityPrivLevRequired < level || (m.EntityPrivLevRequired == level
                    && m.OrderSysUserKey == userName)));
            }
            return source;
        }

        public ProductItemViewModel GetIndexListProductItemsNonStock(
            IOwinContext owinContext, ExtendedIdentityDbContext db,
            ContractListCondition condition, ProductItemStatus? productItemStatus)
        {
            //Debug: 先不管那么多，只返回最简单的
            if (owinContext == null)
                throw new ArgumentNullException("owinContext");
            if (db == null)
                throw new ArgumentNullException("db");

            if (condition == null || condition.IsEnable == false)
            {
                var defCondition = ContractListCondition.GetDefault();
                defCondition.OrderField = ContractOrderField.CONTRACT_KEY_ASC;
                defCondition.UserName = owinContext.Authentication.User.Identity.Name;
                return this.GetIndexListProductItemsNonStock(db, owinContext,
                    defCondition, productItemStatus);
            }

            return this.GetIndexListProductItemsNonStock(db, owinContext,
                condition, productItemStatus);

            //return new ProductItemViewModel() { IsEnable = false };
        }

        private ProductItemViewModel GetIndexListProductItemsNonStock(ExtendedIdentityDbContext db,
            IOwinContext owinContext, ContractListCondition condition, ProductItemStatus? productItemStatus)
        {
            int userEntityPriv = PrivilegeManager.Instance.GetEntityPrivilegeLevel(condition.UserName);

            //IQueryable<ProductItem> 
            var productItemQueryable = db.ProductItems.Include(
                "OrderContract").Where(pEntity => (
                    (pEntity.OrderContract.EntityPrivLevRequired < userEntityPriv
                    || (pEntity.OrderContract.EntityPrivLevRequired == userEntityPriv
                    && pEntity.OrderContract.OrderSysUserKey == condition.UserName)) //根据OrderContract进行数据权限控制
                       && !db.StockItems.Any(
                    stEntity => stEntity.ProductItemId == pEntity.ProductItemId))
                        )
                    .Select(ProductItem => new
                    {
                        ProductItem,
                        ProductItem.OrderContract,
                        ProductItem.OrderContract.OrderContractKey,
                        ProductItem.OrderContract.EntityPrivLevRequired,
                    });
            //.Join(db.StockItems, pi => pi.ProductItemId,
            //si => si.ProductItemId, (pi, si) => new {pi,si}).Where(
            //entity =>( entity.si !=null && entity.si.)
            if (condition.OrderType.HasValue)
            {
                productItemQueryable = productItemQueryable.Where(
                    item => item.OrderContract != null
                && item.OrderContract.OrderType == condition.OrderType.Value);
            }

            AggregationsViewModel aggr = new AggregationsViewModel();

            if (ContractListInclude.WithAggregations ==
                (condition.ListInclude & ContractListInclude.WithAggregations))
            {
                aggr.IsEnable = true;
                aggr.Count = productItemQueryable.Count();
            }

            if (condition.OrderField == ContractOrderField.CONTRACT_KEY_ASC)
            {
                productItemQueryable = productItemQueryable.OrderBy(m => m.OrderContractKey);
            }
            else if (condition.OrderField == ContractOrderField.CONTRACT_KEY_DESC)
            {
                productItemQueryable = productItemQueryable
                    .OrderByDescending(m => m.OrderContractKey);
            }
            else if (condition.OrderField == ContractOrderField.CTIME_DESC)
            {
                productItemQueryable = productItemQueryable
                    .OrderByDescending(m => m.OrderContract.OrderCreateTime);
            }
            else
            {
                productItemQueryable = productItemQueryable
                    .OrderBy(m => m.OrderContract.OrderCreateTime);
            }

            if (condition.Page.HasValue && condition.Rows.HasValue)
            {
                productItemQueryable = productItemQueryable.Skip(
                    (condition.Page.Value - 1) * condition.Rows.Value)
                    .Take(condition.Rows.Value);
            }

            var list = productItemQueryable.ToList();
            List<ProductItemInfo> result = new List<ProductItemInfo>();
            foreach (var item in list)
            {
                ProductItemInfo info = new ProductItemInfo(item.ProductItem);
                result.Add(info);
            }

            return new ProductItemViewModel()
            {
                IsEnable = true,
                Aggregations = aggr,
                ContractItems = result
            };
        }

        public StockOutViewModel GetIndexListStockItemForSold(IOwinContext owinContext,
            ExtendedIdentityDbContext db, ContractListCondition condition)
        {
            var saleItemQuery = db.SaleProductItems.Include("SaleContract").Include("StockItem")
                .Include("OrderProductItem").Where(si => si.StockItem != null
                && si.StockItem.StockStatus == StockStatus.InStock)
                .Select(SaleItem => new
                {
                    SaleItem,
                    SaleItem.SaleContract.SaleContractKey,
                    SaleItem.SaleContract.SaleCreateTime,
                    SaleItem.StockItem
                });

            StockOutViewModel resultViewModel = new StockOutViewModel();

            AggregationsViewModel aggregation = new AggregationsViewModel();

            if (condition != null && condition.IsEnable.GetValueOrDefault())
            {
                if (condition.OrderType.HasValue)
                {
                    saleItemQuery = saleItemQuery.Where(m => condition.OrderType.Value
                         == m.SaleItem.SaleContract.OrderType);
                }

                if (ContractListInclude.WithAggregations ==
                    (condition.ListInclude & ContractListInclude.WithAggregations))
                {
                    aggregation.IsEnable = true;
                    aggregation.Count = saleItemQuery.Count();
                    resultViewModel.Aggregations = aggregation;
                }

                if (condition.OrderField == ContractOrderField.CONTRACT_KEY_ASC)
                {
                    saleItemQuery = saleItemQuery.OrderBy(m => m.SaleContractKey);
                }
                else if (condition.OrderField == ContractOrderField.CONTRACT_KEY_DESC)
                {
                    saleItemQuery = saleItemQuery.OrderByDescending(m => m.SaleContractKey);
                }
                else if (condition.OrderField == ContractOrderField.CTIME_DESC)
                {
                    saleItemQuery = saleItemQuery.OrderByDescending(
                        m => m.SaleCreateTime);
                }
                else
                {
                    saleItemQuery = saleItemQuery.OrderBy(
                        m => m.SaleCreateTime);
                }

                if (condition.Page.HasValue && condition.Rows.HasValue)
                {
                    saleItemQuery = saleItemQuery.Skip(
                        (condition.Page.Value - 1) * condition.Rows.Value)
                        .Take(condition.Rows.Value);
                }

                resultViewModel.IsEnable = true;

                List<SaleProductItemInfo> saleItemInfos = new List<SaleProductItemInfo>();
                List<StockItemInfo> stockInfos = new List<StockItemInfo>();
                resultViewModel.StockViewModel = new StockViewModel() { IsEnable = true };



                var list = saleItemQuery.ToList();
                foreach (var one in list)
                {

                    SaleProductItemInfo info = new SaleProductItemInfo(one.SaleItem);
                    saleItemInfos.Add(info);
                    StockItemInfo stInfo = new StockItemInfo(one.StockItem);
                    stockInfos.Add(stInfo);
                }

                resultViewModel.SaleProductItems = saleItemInfos;
                resultViewModel.StockViewModel.StockItems = stockInfos;
            }

            return resultViewModel;
        }

        public ProductItemViewModel GetIndexListProductItemsForShipment(IOwinContext owinContext,
            ExtendedIdentityDbContext db, ContractListCondition condition)
        {
            if (db == null || owinContext == null)
                throw new ArgumentNullException("DbContext或IOwinContext对象为空。",
                    new Exception("DbContext或IOwinContext对象为空。"));

            if (condition == null)
            {
                UtilityFramework.LogHelper.Warn("获取待发货列表不可以不带条件参数。");
                condition = new ContractListCondition()
                {
                    UserName = owinContext.Authentication.User.Identity.Name,
                    Page = 1,
                    Rows = 10,
                    IsEnable = true,
                    ListInclude = ContractListInclude.SaleContractOnly,
                };
            }

            int userEntityPriv = PrivilegeManager.Instance.GetEntityPrivilegeLevel(condition.UserName);
            DbQuery<SaleProductItem> dbQuery = db.SaleProductItems;
            if (condition != null && condition.OrderType.HasValue == false)//期货 and 现货
            {
                dbQuery = dbQuery.Include("SaleContract.StockItem.ProductItem.Product");
                dbQuery = dbQuery.Include("SaleContract.ProductItem.Product");
            }
            else if (condition != null && condition.OrderType.GetValueOrDefault() == 0)
            {//期货 only
                dbQuery = dbQuery.Include("SaleContract.ProductItem.Product");
            }
            else if (condition != null && condition.OrderType.GetValueOrDefault() == 1)
            {//现货 only
                dbQuery = dbQuery.Include("SaleContract.StockItem.ProductItem.Product");
            }
            else
            {
                dbQuery = dbQuery.Include("SaleContract");
            }

            var productItemQueryable = dbQuery.Include(
                "SaleContract").Where(pEntity => (
                    (pEntity.SaleContract.EntityPrivLevRequired < userEntityPriv
                    || (pEntity.SaleContract.EntityPrivLevRequired == userEntityPriv
                    && pEntity.SaleContract.OperatorSysUser == condition.UserName)) //根据SaleContract进行数据权限控制
                       && (pEntity.ShipmentStatus == SaleShipmentStatus.NonShipment)))
                    .Select(SaleItem => new
                    {
                        SaleItem,
                        SaleItem.SaleContract,
                        SaleItem.SaleContract.SaleContractKey,
                        SaleItem.SaleContract.EntityPrivLevRequired,
                    });

            AggregationsViewModel aggr = new AggregationsViewModel();

            if (ContractListInclude.WithAggregations ==
                (condition.ListInclude & ContractListInclude.WithAggregations))
            {
                aggr.IsEnable = true;
                aggr.Count = productItemQueryable.Count();
            }

            if (condition.OrderField == ContractOrderField.CONTRACT_KEY_ASC)
            {
                productItemQueryable = productItemQueryable.OrderBy(m => m.SaleContractKey);
            }
            else if (condition.OrderField == ContractOrderField.CONTRACT_KEY_DESC)
            {
                productItemQueryable = productItemQueryable
                    .OrderByDescending(m => m.SaleContractKey);
            }
            else if (condition.OrderField == ContractOrderField.CTIME_DESC)
            {
                productItemQueryable = productItemQueryable
                    .OrderByDescending(m => m.SaleContract.SaleCreateTime);
            }
            else
            {
                productItemQueryable = productItemQueryable
                    .OrderBy(m => m.SaleContract.SaleCreateTime);
            }

            if (condition.Page.HasValue && condition.Rows.HasValue)
            {
                productItemQueryable = productItemQueryable.Skip(
                    (condition.Page.Value - 1) * condition.Rows.Value)
                    .Take(condition.Rows.Value);
            }

            var list = productItemQueryable.ToList();
            List<SaleProductItemInfo> result = new List<SaleProductItemInfo>();
            foreach (var item in list)
            {
                SaleProductItemInfo info = new SaleProductItemInfo(item.SaleItem);

                result.Add(info);
            }

            return new ProductItemViewModel()
            {
                IsEnable = true,
                Aggregations = aggr,
                SaleProductItems = result,
            };
        }
    }
}
