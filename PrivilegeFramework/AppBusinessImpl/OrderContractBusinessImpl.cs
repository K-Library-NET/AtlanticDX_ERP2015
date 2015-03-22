using Microsoft.Owin;
using PrivilegeFramework.PrivilegeFilters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityFramework;
using YuShang.ERP.Entities.Finances;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.ResMgr;
using YuShang.ERP.Entities.Sale;
using YuShang.ERP.Entities.Stocks;

namespace PrivilegeFramework.AppBusinessImpl
{
    internal class OrderContractBusinessImpl
    {
        internal IQueryable<OrderContract> GetIndexListOrderContract(
            IOwinContext owinContext, ExtendedIdentityDbContext dbContext,
            ContractListCondition condition)
        {
            OrderContractPrivilegeFilter instanceFilter = new OrderContractPrivilegeFilter()
            {
                OwinContext = owinContext,
                ETAFrom = condition.ETAFrom,
                ETATo = condition.ETATo,
                OrderCreateTimeFrom = condition.CTIMEFrom,
                OrderCreateTimeTo = condition.CTIMETimeTo,
                ProductFullNameFilterValue = condition.ProductFullNameFilterValue,
                ProductKeys = condition.ProductKeys,
                SupplierId = condition.SupplierId,
                UserName = GetUserName(condition, owinContext),
            };

            IQueryable<OrderContract> contractQuery = instanceFilter.BuildQueryable(dbContext, null);

            return contractQuery;
        }

        private string GetUserName(ContractListCondition condition,
            Microsoft.Owin.IOwinContext owinContext)
        {
            return AppBusinessManager.GetUserName(condition, owinContext);
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
            ExtendedIdentityDbContext dbContext,
             DateTime? ETA, string filterValue, string userName, out int count)
        {
            try
            {
                double temp1 = -1;
                double temp2 = -1;

                OrderContractPrivilegeFilter filter = new OrderContractPrivilegeFilter()
                {
                    ProductFullNameFilterValue = filterValue,
                    UserName = userName,
                    OwinContext = owinContext,
                };

                if (ETA.HasValue)
                {
                    filter.ETAFrom = ETA.Value.Date;
                    filter.ETATo = ETA.Value.Date.AddDays(1);
                }

                IQueryable<OrderContract> contractsQuery = this.GetIndexListOrderContract(
                    owinContext, dbContext, filter, out temp1, out temp2);

                var query = contractsQuery.Where(m => m.ContractStatus != ContractStatus.Closed);

                count = query.Count();
                return query;
            }
            catch (Exception ee)
            {
                LogHelper.Error("获取OrderContract列表失败。", ee);
                count = -1;
                return null;
            }
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
        public IQueryable<OrderContract> GetIndexListOrderContract(IOwinContext owinContext,
            ExtendedIdentityDbContext db, IOrderContractPrivilegeFilter filter,
            out double ProductsTotal, out double PaymentTotal)
        {
            OrderContractPrivilegeFilter instanceFilter = new OrderContractPrivilegeFilter(filter)
            {
                OwinContext = owinContext
            };

            IQueryable<OrderContract> contractQuery = instanceFilter.BuildQueryable(db, null);

            //debug
            ProductsTotal = 0;
            PaymentTotal = 0;
            return contractQuery;
        }

        #region add
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
            try
            {
                OrderContract contract = dbContext.OrderContracts.Create();

                AddOrderContractFirst(dbContext, model, userName, contract);
                AddOrderContractRelatedObjs(dbContext, model, userName, contract);
                //AddOrderContractFinancialObjsSecond(dbContext, model, userName, contract);

                int effectedRows = dbContext.SaveChanges();

                if (effectedRows < 1)
                {
                    return "添加失败";
                }
            }
            catch (Exception ee)
            {
                LogHelper.Error("添加OrderContract失败。", ee);
                return ee.Message;
            }

            return string.Empty;
        }

        private void AddOrderContractRelatedObjs(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName, OrderContract contract)
        {
            if (model == null || model.IsEnable == false
                || model.ContractType == ContractViewModelType.SaleContract)
                return;

            if (model.HarborAgent != null && model.HarborAgent.DeclarationCompanyId.HasValue
                && model.HarborAgent.DeclarationCompanyId.Value > 0)//.IsEnable.GetValueOrDefault())
            {
                HarborAgent harborAgent = dbContext.HarborAgents.Create();
                contract.HarborAgent = harborAgent;
                harborAgent.DeclarationCompanyId
                    = model.HarborAgent.DeclarationCompanyId.GetValueOrDefault();
                this.AssignValues(model.HarborAgent, harborAgent);
                dbContext.HarborAgents.Add(harborAgent);
            }
            if (model.HongkongLogistics != null && model.HongkongLogistics.HongKongLogisticsCompanyId.HasValue
                && model.HongkongLogistics.HongKongLogisticsCompanyId.Value > 0)
            //.IsEnable.GetValueOrDefault())
            {
                HKLogis hklogis = dbContext.HongKongLogisticsTable.Create();
                contract.HongKongLogistics = hklogis;
                HongkongLogisticsViewModel.AssignValues(model.HongkongLogistics,
                    contract.HongKongLogistics);
                dbContext.HongKongLogisticsTable.Add(contract.HongKongLogistics);
                //contract.HongKongLogistics.HongKongLogisticsCompanyId =
                //   model.HongkongLogistics.HongKongLogisticsCompanyId.GetValueOrDefault();

                //contract.HongKongLogistics.HongKongLogisticsItems =
                //    new List<HKLogisItem>();
                //foreach (var i in model.ContractItems)
                //{
                //    contract.HongKongLogistics.HongKongLogisticsItems.Add(
                //        new HKLogisItem() { ProductItemId = i.ProductItemId.GetValueOrDefault() });
                //}
                //if (model.HongkongLogistics.LogisItems != null)
                //{
                //    int cnt = Math.Min(model.HongkongLogistics.LogisItems.Count,
                //        contract.HongKongLogistics.HongKongLogisticsItems.Count);
                //    for (int j = 0; j < cnt; j++)
                //    {
                //        var item = contract.HongKongLogistics.HongKongLogisticsItems.ElementAt(j);
                //        var item2 = model.HongkongLogistics.LogisItems.ElementAt(j);
                //        if (item != null && item2 != null)
                //        {
                //            item.ContractQuantity = item2.ContractQuantity;
                //            item.ContractWeight = item2.ContractWeight;
                //            item.FreightCharges = item2.FreightCharges;
                //            item.Insurance = item2.Insurance;
                //            item.ReceivingQuantity = item2.ReceivingQuantity;
                //            item.ReceivingTime = item2.ReceivingTime;
                //            item.ReceivingWeight = item2.ReceivingWeight;
                //        }
                //    }
                //}
            }
            if (model.MainlandLogistics != null && model.MainlandLogistics.MainlandLogisticsCompanyId.HasValue
                && model.MainlandLogistics.MainlandLogisticsCompanyId.Value > 0)
            //.IsEnable.GetValueOrDefault())
            {
                MLLogis mllogis = dbContext.MainlandLogisticsTable.Create();
                contract.MainlandLogistics = mllogis;
                MainlandLogisticsViewModel.AssignValues(model.MainlandLogistics, contract.MainlandLogistics);
                dbContext.MainlandLogisticsTable.Add(mllogis);

                //contract.MainlandLogistics.MainlandLogisticsCompanyId =
                //     model.MainlandLogistics.MainlandLogisticsCompanyId.GetValueOrDefault();
                //contract.MainlandLogistics.MainlandLogisticsItems =
                //        new List<MLLogisItem>();
                //foreach (var i in model.ContractItems)
                //{
                //    contract.MainlandLogistics.MainlandLogisticsItems.Add(
                //        new MLLogisItem() { ProductItemId = i.ProductItemId.GetValueOrDefault() });
                //}
                //if (model.MainlandLogistics.LogisItems != null)
                //{
                //    int cnt = Math.Min(model.MainlandLogistics.LogisItems.Count,
                //        contract.MainlandLogistics.MainlandLogisticsItems.Count);
                //    for (int j = 0; j < cnt; j++)
                //    {
                //        var item = contract.MainlandLogistics.MainlandLogisticsItems.ElementAt(j);
                //        var item2 = model.MainlandLogistics.LogisItems.ElementAt(j);
                //        if (item != null && item2 != null)
                //        {
                //            item.ContractQuantity = item2.ContractQuantity;
                //            item.ContractWeight = item2.ContractWeight;
                //            item.FreightCharges = item2.FreightCharges;
                //            item.Insurance = item2.Insurance;
                //            item.ReceivingQuantity = item2.ReceivingQuantity;
                //            item.ReceivingTime = item2.ReceivingTime;
                //            item.ReceivingWeight = item2.ReceivingWeight;
                //        }
                //    }
                //}
            }
        }

        /// <summary>
        /// 单独添加采购合同对象本身（包括下面的商品）
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="contract"></param>
        private void AddOrderContractFirst(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName, OrderContract contract)
        {
            this.AssignValues(model, userName, contract);

            foreach (var item in model.ContractItems)//.OrderProducts)
            {
                if (item != null && item.ProductId > 0)
                //!string.IsNullOrEmpty(item.ProductKey))
                {
                    var product = dbContext.ProductItems.Create();
                    product.ProductId = item.ProductId.GetValueOrDefault();
                    product.Currency = item.Currency;
                    product.NetWeight = item.NetWeight;
                    product.OrderContract = contract;
                    product.Quantity = item.Quantity;
                    product.UnitPrice = item.UnitPrice;
                    product.Units = item.Units;

                    dbContext.ProductItems.Add(product);
                }
            }

            //最后添加权限值
            contract.EntityPrivLevRequired = PrivilegeManager.Instance.GetEntityPrivilegeLevel(userName);
            dbContext.OrderContracts.Add(contract);
        }
        #endregion

        #region update
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
            if (model == null || model.ContractId.HasValue == false
                || model.ContractId.Value < 1 //.IsEnable == false
                || model.ContractType == ContractViewModelType.SaleContract)
                return "无法修改采购订单对象，可能此订单已不可用。";
            var contract = dbContext.OrderContracts.Find(model.ContractId);
            if (contract == null || contract.ContractStatus == ContractStatus.Closed)
                return "无法修改采购订单对象，可能此订单已不可用。";

            DbContextTransaction tran = null;
            try
            {
                tran = dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                dbContext.Entry<OrderContract>(contract).Collection<ProductItem>(c => c.OrderProducts).Load();

                AssignValues(model, userName, contract);
                UpdateOrderContractRelatedObjs(dbContext, model, userName, contract);
                ChangeOrderContractItems(dbContext, model, contract);
                ChangeOrderContractFinancialObjs(dbContext, model, userName, contract);

                dbContext.SaveChanges();
                tran.Commit();
                tran.Dispose();

                return string.Empty;
            }
            catch (Exception ee)
            {
                LogHelper.Error("修改OrderContract失败。", ee);
                tran.Rollback();
                return ee.Message;
            }
        }

        private void UpdateOrderContractRelatedObjs(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName, OrderContract contract)
        {
            if (model == null || model.ContractId.HasValue == false
                || model.ContractId.Value < 1
                || model.ContractType == ContractViewModelType.SaleContract)
                return;

            #region harborAgent
            if (model.HarborAgent != null && model.HarborAgent.DeclarationCompanyId
                 .GetValueOrDefault() > 0 && contract.HarborAgentId.GetValueOrDefault() > 0)//Update
            {
                if (contract.HarborAgent == null)
                {
                    /// dbContext.Entry<OrderContract>(contract).Member("").
                }
                HarborAgentViewModel.AssignValues(model.HarborAgent, contract.HarborAgent);
                //this.AssignValues(model.HarborAgent, contract.HarborAgent);
            }
            else if (model.HarborAgent != null && model.HarborAgent.DeclarationCompanyId
                .GetValueOrDefault() > 0
                && contract.HarborAgentId.GetValueOrDefault() < 1)
            {//ADD
                HarborAgent harborAgent = dbContext.HarborAgents.Create();
                contract.HarborAgent = harborAgent;
                HarborAgentViewModel.AssignValues(model.HarborAgent, contract.HarborAgent);
                dbContext.HarborAgents.Add(harborAgent);
            }
            else if (model.HarborAgent == null ||
                model.HarborAgent.DeclarationCompanyId.GetValueOrDefault() < 1)
            {//Remove
                if (contract.HarborAgent != null)
                {
                    dbContext.HarborAgents.Remove(contract.HarborAgent);
                    contract.HarborAgent = null;
                }
                contract.HarborAgentId = null;
            }
            #endregion

            #region hklogis
            if (model.HongkongLogistics != null && model.HongkongLogistics
                .HongKongLogisticsCompanyId.GetValueOrDefault() > 0
                && contract.HKLogisId.GetValueOrDefault() < 1)//.IsEnable.GetValueOrDefault())
            {//ADD
                HKLogis hklogis = dbContext.HongKongLogisticsTable.Create();
                //if (contract.HongKongLogistics == null)
                contract.HongKongLogistics = hklogis;
                HongkongLogisticsViewModel.AssignValues(model.HongkongLogistics, contract.HongKongLogistics);
                //contract.HongKongLogistics.HongKongLogisticsCompanyId =
                //   model.HongkongLogistics.HongKongLogisticsCompanyId.GetValueOrDefault();

                //if (model.HongkongLogistics.LogisItems != null
                //    && model.HongkongLogistics.LogisItems.Count > 0)
                //{
                //    int cnt = model.HongkongLogistics.LogisItems.Count;
                //    //Math.Min(model.HongkongLogistics.LogisItems.Count,
                //    //contract.HongKongLogistics.HongKongLogisticsItems.Count);
                //    for (int j = 0; j < cnt; j++)
                //    {
                //        HKLogisItem item = dbContext.HongKongLogisticsItems.Create();
                //        contract.HongKongLogistics.HongKongLogisticsItems.Add(item);
                //        //var item = contract.HongKongLogistics.HongKongLogisticsItems.ElementAt(j);
                //        var item2 = model.HongkongLogistics.LogisItems.ElementAt(j);
                //        if (item != null && item2 != null)
                //        {
                //            LogisticsItem.AssignValues(item2, item);
                //        }
                //        dbContext.HongKongLogisticsItems.Add(item);
                //    }
                //}
            }
            else if (model.HongkongLogistics != null && model.HongkongLogistics
               .HongKongLogisticsCompanyId.GetValueOrDefault() > 0
               && contract.HKLogisId.GetValueOrDefault() > 0)
            {//UPDATE
                HongkongLogisticsViewModel.AssignValues(model.HongkongLogistics,
                    contract.HongKongLogistics);
            }
            else if (model.HongkongLogistics == null || model.HongkongLogistics
               .HongKongLogisticsCompanyId.GetValueOrDefault() < 1)
            {//DELETE
                if (contract.HongKongLogistics != null)
                    dbContext.HongKongLogisticsTable.Remove(contract.HongKongLogistics);
                contract.HongKongLogistics = null;
                contract.HKLogisId = null;
            }
            #endregion

            #region mllogis
            if (model.MainlandLogistics != null && model.MainlandLogistics
                .MainlandLogisticsCompanyId.GetValueOrDefault() > 0
                && contract.MLLogisId.GetValueOrDefault() < 1)//.IsEnable.GetValueOrDefault())
            {//ADD
                YuShang.ERP.Entities.Orders.MLLogis mllogis =
                    dbContext.MainlandLogisticsTable.Create();
                //if (contract.HongKongLogistics == null)
                contract.MainlandLogistics = mllogis;
                MainlandLogisticsViewModel.AssignValues(model.MainlandLogistics, contract.MainlandLogistics);
                //contract.HongKongLogistics.HongKongLogisticsCompanyId =
                //   model.HongkongLogistics.HongKongLogisticsCompanyId.GetValueOrDefault();

                //if (model.HongkongLogistics.LogisItems != null
                //    && model.HongkongLogistics.LogisItems.Count > 0)
                //{
                //    int cnt = model.HongkongLogistics.LogisItems.Count;
                //    //Math.Min(model.HongkongLogistics.LogisItems.Count,
                //    //contract.HongKongLogistics.HongKongLogisticsItems.Count);
                //    for (int j = 0; j < cnt; j++)
                //    {
                //        HKLogisItem item = dbContext.HongKongLogisticsItems.Create();
                //        contract.HongKongLogistics.HongKongLogisticsItems.Add(item);
                //        //var item = contract.HongKongLogistics.HongKongLogisticsItems.ElementAt(j);
                //        var item2 = model.HongkongLogistics.LogisItems.ElementAt(j);
                //        if (item != null && item2 != null)
                //        {
                //            LogisticsItem.AssignValues(item2, item);
                //        }
                //        dbContext.HongKongLogisticsItems.Add(item);
                //    }
                //}
            }
            else if (model.MainlandLogistics != null && model.MainlandLogistics
               .MainlandLogisticsCompanyId.GetValueOrDefault() > 0
               && contract.MLLogisId.GetValueOrDefault() > 0)
            {//UPDATE
                MainlandLogisticsViewModel.AssignValues(model.MainlandLogistics,
                    contract.MainlandLogistics);
            }
            else if (model.MainlandLogistics == null || model.MainlandLogistics
               .MainlandLogisticsCompanyId.GetValueOrDefault() < 1)
            {//DELETE
                if (contract.MainlandLogistics != null)
                    dbContext.MainlandLogisticsTable.Remove(contract.MainlandLogistics);
                contract.MainlandLogistics = null;
                contract.MLLogisId = null;
            }
            #endregion
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
            try
            {
                OrderContract contract = dbContext.OrderContracts.Find(
                    model.ContractId);
                if (contract == null)
                    contract = dbContext.OrderContracts.FirstOrDefault(
                        m => m.OrderContractKey == model.ContractKey);

                return this.UpdateOrderContractCore(dbContext, model, userName);
            }
            catch (Exception ee)
            {
                LogHelper.Error("OrderContract交单失败。", ee);
                return ee.Message;
            }
        }

        /// <summary>
        /// 同步修改对应的采购合同本身的应付账款对象
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="contract"></param>
        private void ChangeOrderContractFinancialObjs(ExtendedIdentityDbContext
            dbContext, ContractInfo model, string UserName, OrderContract contract)
        {
            if (contract.ContractStatus != ContractStatus.AuditPassed)
                return;
            this.AddOrUpdateOrderContractFinancialObjsSelf(dbContext, model, UserName, contract);

            this.AddOrUpdateOrderContractFinancialObjsRelated(dbContext, model, UserName, contract);

            //在这里暂时不用SaveChanges，外层处理
            //dbContext.SaveChanges();
        }

        /// <summary>
        /// 修改订单时同步修改商品
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="contract"></param>
        private void ChangeOrderContractItems(ExtendedIdentityDbContext dbContext,
            ContractInfo model, OrderContract contract)
        {
            if (model.ContractItems == null || model.ContractItems.Count <= 0)
            {
                contract.OrderProducts.Clear();
            }
            else
            {
                foreach (var item in contract.OrderProducts)
                {
                    var item2 = model.ContractItems.FirstOrDefault(
                        m2 => m2.ProductId == item.ProductId);

                    if (item2 == null)
                    {//如果不存在则删除
                        dbContext.ProductItems.Remove(item);
                    }
                    else
                    {//根据item2修改 
                        this.AssignValues(item2, item);
                    }
                }

                var nItems = from one in model.ContractItems
                             where !contract.OrderProducts.Any(m => m.ProductId == one.ProductId)
                             select one;//没有包含的新元素

                foreach (var ni in nItems)
                {
                    var item3 = dbContext.ProductItems.Create();
                    item3.OrderContractId = ni.OrderContractId.GetValueOrDefault();
                    AssignValues(ni, item3);
                    contract.OrderProducts.Add(item3);
                }
            }
        }
        #endregion

        #region financials
        /// <summary>
        /// 添加相关的财务记录：“应付账款”
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <param name="contract"></param>
        private void AddOrUpdateOrderContractFinancialObjsSelf(
            ExtendedIdentityDbContext dbContext, ContractInfo model,
            string userName, OrderContract contract)
        {
            AccountsPayable pay1 = dbContext.AccountsPayables.FirstOrDefault(
                m => m.OrderContractId.HasValue && m.OrderContractId.Value == contract.OrderContractId
                    && m.EventType == AccountingEventType.ImportDeposite);

            AccountsPayable pay2 = dbContext.AccountsPayables.FirstOrDefault(
                m => m.OrderContractId.HasValue && m.OrderContractId.Value == contract.OrderContractId
                    && m.EventType == AccountingEventType.ImportBalancedPayment);

            if (pay1 != null)
            {
                pay1.Amount = contract.ImportDeposite;

                double accountRecordSum1 = dbContext.FinancialRecordRelations.Include("AccountsRecord")
                     .Where(m1 => m1.AccountsRecord.IsDeleted == false &&
                         (m1.RelatedObjectId == contract.OrderContractId
                         && m1.RelatedObjectId == pay1.AccountsPayableId
                     && m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_OrderContract) //与此订单关联
                      && (m1.RelatedObjectId == pay1.Amount &&  //于此应付账款记录关联
                      m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_AccountsPayable))
                      .Select(m1 => m1.AccountsRecord).Distinct().Sum(m1 => m1.Amount);

                ////如果应付账款已经付款超过数字，则状态变为“已结清” 

                if (accountRecordSum1 >= pay1.Amount)
                    pay1.PayStatus = 1;
                else pay1.PayStatus = 0;
            }
            else
            {
                pay1 = dbContext.AccountsPayables.Create();
                pay1.Amount = contract.ImportDeposite;
                pay1.EventType = AccountingEventType.ImportDeposite;
                pay1.CTIME = DateTime.Now;
                pay1.OrderContract = contract;
                pay1.PayStatus = 0;
                dbContext.AccountsPayables.Add(pay1);
            }
            if (pay2 != null)
            {
                pay2.Amount = contract.ImportBalancedPayment;

                double accountRecordSum2 = dbContext.FinancialRecordRelations.Include("AccountsRecord")
                     .Where(m1 => m1.AccountsRecord.IsDeleted == false &&
                         (m1.RelatedObjectId == contract.OrderContractId
                         && m1.RelatedObjectId == pay2.AccountsPayableId
                     && m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_OrderContract) //与此订单关联
                      && (m1.RelatedObjectId == pay2.Amount &&  //于此应付账款记录关联
                      m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_AccountsPayable))
                      .Select(m1 => m1.AccountsRecord).Distinct().Sum(m1 => m1.Amount);

                //如果应付账款已经付款超过数字，则状态变为“已结清” 

                if (accountRecordSum2 >= pay2.Amount)
                    pay2.PayStatus = 1;
                else pay2.PayStatus = 0;
            }
            else
            {
                pay2 = dbContext.AccountsPayables.Create();
                pay2.Amount = contract.ImportBalancedPayment;
                pay2.EventType = AccountingEventType.ImportBalancedPayment;
                pay2.CTIME = DateTime.Now;
                pay2.OrderContractId = contract.OrderContractId;
                //pay2.OrderContractKey = contract.OrderContractKey;
                pay2.PayStatus = 0;
                dbContext.AccountsPayables.Add(pay2);
            }
        }

        private void AddOrUpdateOrderContractFinancialObjsRelated(
            ExtendedIdentityDbContext dbContext, ContractInfo model,
            string userName, OrderContract contract)
        {
            this.AddOrUpdateOrderContractHarborAgentFinancialObjsSecond(
                dbContext, contract, contract.HarborAgent, userName);
            this.AddOrUpdateOrderContractHKLogisFinancialObjsSecond(
                dbContext, contract, contract.HongKongLogistics, userName);
            this.AddOrUpdateOrderContractMainlandLogisFinancialObjsSecond(
                dbContext, contract, contract.MainlandLogistics, userName);
        }

        /// <summary>
        /// 新增或同步修改港口代理的应收账款记录
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="harborAgent"></param>
        /// <param name="userName"></param>
        private void AddOrUpdateOrderContractHarborAgentFinancialObjsSecond(
            ExtendedIdentityDbContext dbContext, OrderContract contract,
            HarborAgent harborAgent, string userName)
        {
            AccountsPayable pay1 = dbContext.AccountsPayables.FirstOrDefault(
                m => m.OrderContractId == contract.OrderContractId
                    && m.EventType == AccountingEventType.HarborAgentFee);

            if (pay1 == null)
            {
                pay1 = dbContext.AccountsPayables.Create();
                pay1.CTIME = DateTime.Now;
                pay1.EventType = AccountingEventType.HarborAgentFee;
                pay1.OrderContractId = contract.OrderContractId;
                pay1.PayStatus = 0;
                dbContext.AccountsPayables.Add(pay1);
            }
            else
            {
                if (harborAgent == null)
                {
                    dbContext.AccountsPayables.Remove(pay1);
                    return;
                }

                pay1.Amount = harborAgent.Total.GetValueOrDefault();

                double accountRecordSum = dbContext.FinancialRecordRelations.Include("AccountsRecord")
                     .Where(m1 => m1.AccountsRecord.IsDeleted == false &&
                         (m1.RelatedObjectId == contract.OrderContractId
                         && m1.RelatedObjectId == pay1.AccountsPayableId
                     && m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_OrderContract) //与此订单关联
                      && (m1.RelatedObjectId == pay1.Amount &&  //于此应付账款记录关联
                      m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_AccountsPayable))
                      .Select(m1 => m1.AccountsRecord).Distinct().Sum(m1 => m1.Amount);

                //如果应付账款已经付款超过数字，则状态变为“已结清” 

                if (accountRecordSum //amountSum1 
                    >= pay1.Amount)
                    pay1.PayStatus = 1;
                else pay1.PayStatus = 0;
            }
        }

        private void AddOrUpdateOrderContractHKLogisFinancialObjsSecond(
            ExtendedIdentityDbContext dbContext, OrderContract contract,
            HKLogis hklogis, string userName)
        {
            AccountsPayable pay1 = dbContext.AccountsPayables.FirstOrDefault(
                m => m.OrderContractId == contract.OrderContractId
                    && m.EventType == AccountingEventType.HKLogisticsFee);

            if (pay1 == null)
            {
                pay1 = dbContext.AccountsPayables.Create();
                pay1.CTIME = DateTime.Now;
                pay1.EventType = AccountingEventType.HKLogisticsFee;
                pay1.OrderContract = contract;
                pay1.PayStatus = 0;
                dbContext.AccountsPayables.Add(pay1);
            }
            else
            {
                if (hklogis == null)
                {
                    dbContext.AccountsPayables.Remove(pay1);
                    return;
                }

                pay1.Amount = hklogis.Total;

                double accountRecordSum = dbContext.FinancialRecordRelations.Include("AccountsRecord")
                     .Where(m1 => m1.AccountsRecord.IsDeleted == false &&
                         (m1.RelatedObjectId == contract.OrderContractId
                         && m1.RelatedObjectId == pay1.AccountsPayableId
                     && m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_OrderContract) //与此订单关联
                      && (m1.RelatedObjectId == pay1.Amount &&  //于此应付账款记录关联
                      m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_AccountsPayable))
                      .Select(m1 => m1.AccountsRecord).Distinct().Sum(m1 => m1.Amount);

                //如果应付账款已经付款超过数字，则状态变为“已结清”  
                if (accountRecordSum >= pay1.Amount)
                    pay1.PayStatus = 1;
                else pay1.PayStatus = 0;
            }
        }

        #region old add or update objs

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
        //    try
        //    {
        //        HKLogis obj = dbContext.HongKongLogisticsTable.Find(hklogis.HKLogisId);
        //        if (obj == null && contract.HKLogisId.HasValue)
        //        {
        //            obj = dbContext.HongKongLogisticsTable.Find(contract.HKLogisId.Value);
        //            //.FirstOrDefault(
        //            //m => m.OrderContractKey == hklogis.OrderContractKey);
        //        }

        //        if (obj == null)
        //        {
        //            //var orderContract = dbContext.OrderContracts.FirstOrDefault(
        //            //     m => m.OrderContractKey == hklogis.OrderContractKey
        //            //     && m.ContractStatus == ContractStatus.AuditPassed);

        //            //if (orderContract == null)
        //            //{
        //            //    return "找不到对应的采购合同，或者合同尚未交单审核通过。";
        //            //}
        //            obj = dbContext.HongKongLogisticsTable.Create();
        //            contract.HongKongLogistics = obj;
        //            //hklogis.OrderContractId = orderContract.OrderContractId;
        //        }

        //        obj.CommitToPayCost = hklogis.CommitToPayCost;
        //        obj.HongKongLogisticsCompanyId = hklogis.HongKongLogisticsCompanyId;
        //        //obj.OrderContractId = hklogis.OrderContractId;
        //        //obj.OrderContractKey = hklogis.OrderContractKey;

        //        //延迟加载Include fixed
        //        obj.HongKongLogisticsItems = dbContext.Entry<HKLogis>(obj).Collection<HKLogisItem>(
        //            "HongKongLogisticsItems").Query().ToList();

        //        List<LogisticsProductItem> newItems = new List<LogisticsProductItem>(obj.HongKongLogisticsItems);
        //        List<LogisticsProductItem> hkLogisticsItems = new List<LogisticsProductItem>(hklogis.HongKongLogisticsItems);

        //        AddOrUpdateOrderContractLogisItems(dbContext, newItems, hkLogisticsItems, userName);
        //        obj.HongKongLogisticsItems = new List<HKLogisItem>(from i in newItems
        //                                                           where i is HKLogisItem
        //                                                           select i as HKLogisItem);

        //        if (obj.CommitToPayCost.GetValueOrDefault())
        //        {//“同意支付货款”才能新增或修改财务记录
        //            AddOrUpdateOrderContractHKLogisFinancialObjsSecond(
        //                dbContext, contract, hklogis, userName);
        //        }

        //        int effectedRows = dbContext.SaveChanges();

        //        if (effectedRows < 1)
        //        {
        //            return "添加或修改香港物流失败";
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        LogHelper.Error("添加或修改香港物流失败。", ee);
        //        return ee.Message;
        //    }

        //    return string.Empty;
        //}


        ///// <summary>
        ///// 对订单添加“港口代理”
        ///// </summary>
        ///// <param name="dbContext"></param>
        ///// <param name="model"></param>
        ///// <param name="harborAgent"></param>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //public string AddOrUpdateOrderContractHarborAgent(
        //    ExtendedIdentityDbContext dbContext, OrderContract contract,
        //    HarborAgent harborAgent, string userName)
        //{
        //    try
        //    {
        //        HarborAgent agentObj = dbContext.HarborAgents.Find(
        //            harborAgent.HarborAgentId);
        //        if (agentObj == null)
        //        {
        //            agentObj = dbContext.HarborAgents.Find(harborAgent.HarborAgentId);

        //        }

        //        if (agentObj == null)
        //        {
        //            var orderContract = dbContext.OrderContracts.FirstOrDefault(
        //                 m => m.OrderContractId == contract.OrderContractId
        //                 && m.ContractStatus == ContractStatus.AuditPassed);

        //            if (orderContract == null)
        //            {
        //                return "找不到对应的采购合同，或者合同尚未交单审核通过。";
        //            }
        //            agentObj = dbContext.HarborAgents.Create();
        //            orderContract.HarborAgent = agentObj;
        //        }

        //        AssignValues(harborAgent, agentObj);

        //        AddOrUpdateOrderContractHarborAgentFinancialObjsSecond(
        //            dbContext, contract, harborAgent, userName);

        //        int effectedRows = dbContext.SaveChanges();

        //        if (effectedRows < 1)
        //        {
        //            return "添加港口代理失败";
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        LogHelper.Error("添加港口代理失败。", ee);
        //        return ee.Message;
        //    }

        //    return string.Empty;
        //}

        //public string AddOrUpdateOrderContractMLLogis(
        //    ExtendedIdentityDbContext dbContext, OrderContract contract,
        //    MLLogis hklogis, string userName)
        //{
        //    try
        //    {
        //        MLLogis obj = dbContext.MainlandLogisticsTable.Find(hklogis.MLLogisId);
        //        if (obj == null && contract.MLLogisId.HasValue)
        //        {
        //            obj = dbContext.MainlandLogisticsTable.Find(contract.MLLogisId.Value);
        //            //.FirstOrDefault(
        //            //m => m.OrderContractKey == hklogis.OrderContractKey);
        //        }

        //        if (obj == null)
        //        {
        //            //var orderContract = dbContext.OrderContracts.FirstOrDefault(
        //            //     m => m.OrderContractKey == hklogis.OrderContractKey
        //            //     && m.ContractStatus == ContractStatus.AuditPassed);

        //            //if (orderContract == null)
        //            //{
        //            //    return "找不到对应的采购合同，或者合同尚未交单审核通过。";
        //            //}
        //            obj = dbContext.MainlandLogisticsTable.Create();
        //            contract.MainlandLogistics = obj;
        //            //hklogis.OrderContractId = orderContract.OrderContractId;
        //        }

        //        obj.CommitToPayCost = hklogis.CommitToPayCost;
        //        obj.MainlandLogisticsCompanyId = hklogis.MainlandLogisticsCompanyId;
        //        //obj.OrderContractId = hklogis.OrderContractId;
        //        //obj.OrderContractKey = hklogis.OrderContractKey;

        //        //延迟加载Include fixed
        //        obj.MainlandLogisticsItems = dbContext.Entry<MLLogis>(obj).Collection<MLLogisItem>(
        //            "HongKongLogisticsItems").Query().ToList();

        //        List<LogisticsProductItem> newItems = new List<LogisticsProductItem>(obj.MainlandLogisticsItems);
        //        List<LogisticsProductItem> hkLogisticsItems = new List<LogisticsProductItem>(hklogis.MainlandLogisticsItems);

        //        AddOrUpdateOrderContractLogisItems(dbContext, newItems, hkLogisticsItems, userName);
        //        obj.MainlandLogisticsItems = new List<MLLogisItem>(from i in newItems
        //                                                           where i is MLLogisItem
        //                                                           select i as MLLogisItem);

        //        if (obj.CommitToPayCost.GetValueOrDefault())
        //        {//“同意支付货款”才能新增或修改财务记录
        //            AddOrUpdateOrderContractMainlandLogisFinancialObjsSecond(
        //                dbContext, contract, hklogis, userName);
        //        }

        //        int effectedRows = dbContext.SaveChanges();

        //        if (effectedRows < 1)
        //        {
        //            return "添加或修改内地物流失败";
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        LogHelper.Error("添加或修改内地物流失败。", ee);
        //        return ee.Message;
        //    }

        //    return string.Empty;
        //}

        //private void AddOrUpdateOrderContractLogisItems(ExtendedIdentityDbContext
        //    dbContext, ICollection<LogisticsProductItem> newItems, ICollection<LogisticsProductItem> hklogisItems, string userName)
        //{
        //    foreach (var item in hklogisItems)
        //    {
        //        var newItem = newItems.FirstOrDefault(
        //            m => m.ProductItemId == item.ProductItemId);
        //        if (newItem == null)
        //        {
        //            continue;
        //            //newItem = dbContext.HongKongLogisticsItems.Create();
        //            //newItems.Add(newItem);
        //        }
        //        newItem.ProductItemId = item.ProductItemId;
        //        newItem.ReceivingQuantity = item.ReceivingQuantity;
        //        newItem.ReceivingTime = item.ReceivingTime;
        //        newItem.ReceivingWeight = item.ReceivingWeight;
        //        newItem.Insurance = item.Insurance;
        //        newItem.ContractQuantity = item.ContractQuantity;
        //        newItem.ContractWeight = item.ContractWeight;
        //        newItem.FreightCharges = item.FreightCharges;
        //    }
        //}
        #endregion

        private void AddOrUpdateOrderContractMainlandLogisFinancialObjsSecond(
            ExtendedIdentityDbContext dbContext, OrderContract contract,
            MLLogis hklogis, string userName)
        {
            AccountsPayable pay1 = dbContext.AccountsPayables.FirstOrDefault(
                m => m.OrderContractId == contract.OrderContractId
                    && m.EventType == AccountingEventType.MainlandLogisticsFee);

            if (pay1 == null)
            {
                pay1 = dbContext.AccountsPayables.Create();
                pay1.CTIME = DateTime.Now;
                pay1.EventType = AccountingEventType.MainlandLogisticsFee;
                pay1.OrderContract = contract;
                pay1.PayStatus = 0;
                dbContext.AccountsPayables.Add(pay1);
            }
            else
            {
                if (hklogis == null)
                {
                    dbContext.AccountsPayables.Remove(pay1);
                    return;
                }

                pay1.Amount = hklogis.Total;
                double accountRecordSum = dbContext.FinancialRecordRelations.Include("AccountsRecord")
                     .Where(m1 => m1.AccountsRecord.IsDeleted == false &&
                         (m1.RelatedObjectId == contract.OrderContractId
                         && m1.RelatedObjectId == pay1.AccountsPayableId
                     && m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_OrderContract) //与此订单关联
                      && (m1.RelatedObjectId == pay1.Amount &&  //于此应付账款记录关联
                      m1.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_AccountsPayable))
                      .Select(m1 => m1.AccountsRecord).Distinct().Sum(m1 => m1.Amount);

                //如果应付账款已经付款超过数字，则状态变为“已结清”  
                if (accountRecordSum >= pay1.Amount)
                    pay1.PayStatus = 1;
                else pay1.PayStatus = 0;
            }
        }
        #endregion

        private void AssignValues(ProductItemInfo source, ProductItem target)
        {
            ProductItemInfo.AssignValues(source, target);
        }

        private void AssignValues(HarborAgentViewModel source, HarborAgent target)
        {
            HarborAgentViewModel.AssignValues(source, target);
        }

        private void AssignValues(ContractInfo source, string userName, OrderContract target)
        {
            source.CreateSysUserKey = userName;
            ContractInfo.AssignValues(source, target);
        }

        /// <summary>
        /// 根据一个ProductItem的索赔对象，新增或修改索赔
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddOrUpdateOrderCompensationItem(
            ExtendedIdentityDbContext dbContext, OrderClaimCompensationItem item)
        {
            try
            {
                ProductItem productItem = dbContext.ProductItems.FirstOrDefault(
                    p => p.ProductItemId == item.ProductItemId);
                if (productItem == null)
                    return "找不到对应的采购货品。";

                if (productItem.OrderClaimCompensationItem == null)
                {
                    var newItem = dbContext.OrderClaimCompensationItems.Create();
                    productItem.OrderClaimCompensationItem = newItem;
                    productItem.OrderClaimCompensationItem.Compensation = item.Compensation;
                    productItem.OrderClaimCompensationItem.CompensationHappenedType = item.CompensationHappenedType;
                    productItem.OrderClaimCompensationItem.CompensationReason = item.CompensationReason;
                    productItem.OrderClaimCompensationItem.Currency = item.Currency;
                    productItem.OrderClaimCompensationItem.ProductItemId = item.ProductItemId;
                    dbContext.OrderClaimCompensationItems.Add(newItem);
                }
                else
                {
                    productItem.OrderClaimCompensationItem.Compensation = item.Compensation;
                    productItem.OrderClaimCompensationItem.CompensationHappenedType = item.CompensationHappenedType;
                    productItem.OrderClaimCompensationItem.CompensationReason = item.CompensationReason;
                    productItem.OrderClaimCompensationItem.Currency = item.Currency;
                    productItem.OrderClaimCompensationItem.ProductItemId = item.ProductItemId;
                }

                dbContext.SaveChanges();
                return string.Empty;
            }
            catch (Exception e)
            {
                LogHelper.Error("新增/修改采购索赔失败", e);
                return "新增/修改采购索赔失败：" + "\t\t" + e.Message + "\t\t" + e.StackTrace;
            }
        }

        public IQueryable<ProductItem> GetIndexListProductItemByReceiveFilter(
            ExtendedIdentityDbContext dbContext, IQueryable<ProductItem> sourceQuery,
            string receiveFilterValue, DateTime? OrderCreateTimeFrom, DateTime? OrderCreateTimeTo,
            string userName)
        {
            IQueryable<ProductItem> query = sourceQuery;
            // dbContext.ProductItems.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(receiveFilterValue))
            {
                string temp = receiveFilterValue.Trim().ToLowerInvariant();
                query = query.Where(q => q.OrderContract.DeliveryBillSerial.ToLowerInvariant().Contains(temp)
                     || q.OrderContract.Supplier.SupplierName.ToLowerInvariant().Contains(temp)
                     || q.OrderContract.Comments.ToLowerInvariant().Contains(temp));
            }

            if (OrderCreateTimeFrom.HasValue && OrderCreateTimeTo.HasValue)
            {
                query = query.Where(q => q.OrderContract.OrderCreateTime >= OrderCreateTimeFrom.Value
                    && q.OrderContract.OrderCreateTime <= OrderCreateTimeTo.Value);
            }
            else if (OrderCreateTimeFrom.HasValue)
            {
                query = query.Where(q => q.OrderContract.OrderCreateTime
                    >= OrderCreateTimeFrom.Value);
            }
            else if (OrderCreateTimeTo.HasValue)
            {
                query = query.Where(q => q.OrderContract.OrderCreateTime
                     <= OrderCreateTimeTo.Value);
            }
            else
            {
                //不需要加条件
            }

            //TODO: Filter OrderContract by userName
            if (!string.IsNullOrWhiteSpace(userName))
            {//TODO: add query condition to query

            }

            return query;
        }

        public string UpdateImportProductItemReceive(ExtendedIdentityDbContext db, int productItemId,
            DateTime receivedDate, ProductItemStatus productItemStatus, string comments)
        {
            try
            {
                ProductItem item = db.ProductItems.FirstOrDefault(
                    p => p.ProductItemId == productItemId);
                if (item == null)
                    return "找不到对应的收货货品。";
                if (item.Status == ProductItemStatus.Received)
                    return "指定的货品已经确认收货。";

                item.ReceiveTime = receivedDate;
                item.Status = productItemStatus;
                item.Comments = item.Comments + string.Format("{0}收货确认：" + comments, receivedDate.ToShortDateString());
                db.SaveChanges();

                return string.Empty;
            }
            catch (Exception e)
            {
                LogHelper.Error("货品收货确认时失败。", e);
                return "货品收货确认时失败：" + e.Message + "\t\t" + e.StackTrace;
            }
        }


    }
}
