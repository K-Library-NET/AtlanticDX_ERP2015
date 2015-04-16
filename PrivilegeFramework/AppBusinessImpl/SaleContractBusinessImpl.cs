using PrivilegeFramework.AppBusinessImpl;
using PrivilegeFramework.PrivilegeFilters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
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
    internal class SaleContractBusinessImpl
    {

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
            if (model == null)
                return "SaleContract model为空。";

            try
            {
                SaleContract contract = null;
                if (model.ContractId > 0)
                {
                    contract = dbContext.SaleContracts.Find(model.ContractId);
                }

                if (contract == null)
                {
                    contract = dbContext.SaleContracts.Create();
                }

                string errorMessage1 = AddSaleContractFirst(
                    dbContext, model, userName, contract);
                if (!string.IsNullOrWhiteSpace(errorMessage1))
                    return errorMessage1;

                string errorMessage2 = AddOrUpdateSaleContractFinancialObjsSecond(
                    dbContext, userName, contract);
                if (!string.IsNullOrWhiteSpace(errorMessage2))
                    return errorMessage2;

                int effectedRows = dbContext.SaveChanges();

                if (effectedRows < 1)
                {
                    return "添加SaleContract失败。";
                }
            }
            catch (Exception ee)
            {
                LogHelper.Error("添加SaleContract失败。", ee);
                return ee.Message;
            }

            return string.Empty;
        }

        private string AddOrUpdateSaleContractFinancialObjsSecond(
            ExtendedIdentityDbContext db, //SaleContract model,
            string userName, SaleContract contract)
        {
            if (contract.ContractStatus == ContractStatus.AuditPassed)//已经审核
            {
                if (contract.SaleContractId < 1)
                {//Add
                    AddSaleContractFinancialObjsSecond(db, contract);
                }
                else
                {//exists
                    UpdateSaleContractFinancialObjsSecond(db, contract);
                }
            }

            return string.Empty;
        }

        private void UpdateSaleContractFinancialObjsSecond(ExtendedIdentityDbContext db, SaleContract contract)
        {
            var receive1 = db.AccountsReceivables.FirstOrDefault(
                  m => m.EventType == AccountingEventType.SalesDeposite
                  && m.SaleContractId.HasValue
                  && m.SaleContractId.Value == contract.SaleContractId);

            var receive2 = db.AccountsReceivables.FirstOrDefault(
                  m => m.EventType == AccountingEventType.SalesBalancedPayment
                  && m.SaleContractId.HasValue
                  && m.SaleContractId.Value == contract.SaleContractId);

            if (receive1 == null)
            {
                receive1 = db.AccountsReceivables.Create();
                receive1.SaleContractId = contract.SaleContractId;
                receive1.PayStatus = 0;
                receive1.EventType = AccountingEventType.SalesDeposite;
                receive1.CTIME = DateTime.Now;
                receive1.Amount = contract.SaleDeposite;
                db.AccountsReceivables.Add(receive1);
            }
            else
                receive1.Amount = contract.SaleDeposite;

            if (receive2 == null)
            {
                receive2 = db.AccountsReceivables.Create();
                receive2.SaleContractId = contract.SaleContractId;
                receive2.PayStatus = 0;
                receive2.EventType = AccountingEventType.SalesBalancedPayment;
                receive2.CTIME = DateTime.Now;
                receive2.Amount = contract.SaleBalancedPayment;
                db.AccountsReceivables.Add(receive2);
            }
            else
                receive2.Amount = contract.SaleBalancedPayment;
        }

        private void AddSaleContractFinancialObjsSecond(ExtendedIdentityDbContext db, SaleContract contract)
        {
            var receive1 = db.AccountsReceivables.Create();
            receive1.SaleContract = contract;
            receive1.PayStatus = 0;
            receive1.EventType = AccountingEventType.SalesDeposite;
            receive1.CTIME = DateTime.Now;
            receive1.Amount = contract.SaleDeposite;
            db.AccountsReceivables.Add(receive1);

            var receive2 = db.AccountsReceivables.Create();
            receive2.SaleContract = contract;
            receive2.PayStatus = 0;
            receive2.EventType = AccountingEventType.SalesBalancedPayment;
            receive2.CTIME = DateTime.Now;
            receive2.Amount = contract.SaleBalancedPayment;
            db.AccountsReceivables.Add(receive2);
        }

        private string AddSaleContractFirst(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName, SaleContract contract)
        {
            ContractInfo.AssignValues(model, contract);
            contract.SaleCreateTime = DateTime.Now;
            contract.OperatorSysUser = userName;
            contract.EntityPrivLevRequired = PrivilegeManager.Instance.GetEntityPrivilegeLevel(userName);
            StringBuilder builder = new StringBuilder();

            foreach (var item in model.SaleProductItems)
            {
                if (item != null)
                {
                    SaleProductItem saleItem = dbContext.SaleProductItems.Create();
                    SaleProductItemInfo.AssignValues(item, saleItem);
                    saleItem.SaleContract = contract;

                    string errMsg = this.HandleStockItemMove(dbContext, contract, saleItem, userName);
                    if (!string.IsNullOrWhiteSpace(errMsg))
                        builder.AppendLine(errMsg);

                    dbContext.SaleProductItems.Add(saleItem);
                }
            }

            dbContext.SaleContracts.Add(contract);

            string temp = builder.ToString();
            if (!string.IsNullOrWhiteSpace(temp))
                return temp.Trim();

            return string.Empty;
        }

        private string HandleStockItemMove(ExtendedIdentityDbContext dbContext,
            SaleContract contract, SaleProductItem saleItem, string userName)
        {
            if (contract.ContractStatus != ContractStatus.AuditPassed
                || contract.OrderType == 0)
                return string.Empty;

            StockItem stockItem = dbContext.StockItems.FirstOrDefault(
                p => saleItem.StockItemId.HasValue && p.StockItemId == saleItem.StockItemId.Value && p.IsAllSold == false);

            if (stockItem == null)
                return "找不到对应的库存商品项，或者对应的商品项已经被销售完毕。";

            if (stockItem.Quantity <= saleItem.Quantity)
            {
                stockItem.IsAllSold = true;
                stockItem.StockStatus = StockStatus.InStockSelling;
            }
            else
            {//stockItem.Quantity > saleItem.Quantity
                //销售只卖掉一部分的情况
                StockItem newItem = dbContext.StockItems.Create();
                newItem.ProductItemId = stockItem.ProductItemId;
                newItem.StockInDate = stockItem.StockInDate;
                newItem.StockStatus = StockStatus.InStock;
                newItem.StockWeight = stockItem.StockWeight;
                newItem.StoreHouseId = stockItem.StoreHouseId;
                newItem.StoreHouseMountNumber = stockItem.StoreHouseMountNumber;
                newItem.Quantity = stockItem.Quantity - saleItem.Quantity;

                dbContext.StockItems.Add(newItem);

                stockItem.Quantity = saleItem.Quantity;
                stockItem.StockStatus = StockStatus.InStockSelling;
                stockItem.IsAllSold = true;
            }

            return string.Empty;
        }

        [Obsolete("Old")]
        public IQueryable<YuShang.ERP.Entities.Sale.SaleContract> GetIndexListSaleContract(
            ExtendedIdentityDbContext dbContext, int? orderType, DateTime? DateFrom, DateTime? DateTo,
            string filterValue, string userName, out int count)
        {
            try
            {
                //double temp1 = -1;
                //double temp2 = -1;

                SaleContractPrivilegeFilter filter = new SaleContractPrivilegeFilter()
                {
                    SerialOrSupplierFilterValue = filterValue,
                    SaleCreateTimeFrom = DateFrom,
                    SaleCreateTimeTo = DateTo,
                    UserName = userName,
                };

                IQueryable<SaleContract> contractsQuery = this.GetIndexListSaleContract(
                    dbContext, filter); //out temp1, out temp2);

                if (orderType.HasValue)
                {
                    var query = contractsQuery.Where(m => m.OrderType == orderType.GetValueOrDefault()
                        && m.ContractStatus != ContractStatus.Closed);

                    count = query.Count();
                    return query;
                }

                var query2 = contractsQuery.Where(m => m.ContractStatus != ContractStatus.Closed);

                count = query2.Count();
                return query2;
            }
            catch (Exception ee)
            {
                LogHelper.Error("获取SaleContract列表失败。", ee);
                count = -1;
                return null;
            }
        }

        private IQueryable<SaleContract> GetIndexListSaleContract(
            ExtendedIdentityDbContext dbContext, ISaleContractQueryBuilder builder)
        {
            IQueryable<SaleContract> contractQuery = builder.BuildQueryable(dbContext,
                dbContext.SaleContracts.Include("SaleProducts.OrderProductItem.Product")
                    .Include("SaleBargins.BargainItems")
                .AsNoTracking());

            return contractQuery;
        }

        public string UpdateSaleContractCore(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName)
        {
            if (model == null)
                return "SaleContract model为空。";

            try
            {
                SaleContract contract = null;
                if (model.ContractId > 0)
                {
                    contract = dbContext.SaleContracts.Find(model.ContractId);
                }

                if (contract == null)
                {
                    contract = dbContext.SaleContracts.Create();
                }

                string errorMessage1 = this.UpdateSaleContractFirst(dbContext, model,
                    userName, contract);
                if (!string.IsNullOrWhiteSpace(errorMessage1))
                    return errorMessage1;

                string errorMessage2 = AddOrUpdateSaleContractFinancialObjsSecond(
                    dbContext, userName, contract);
                if (!string.IsNullOrWhiteSpace(errorMessage2))
                    return errorMessage2;

                int effectedRows = dbContext.SaveChanges();

                if (effectedRows < 1)
                {
                    return "修改SaleContract失败。";
                }
            }
            catch (Exception ee)
            {
                LogHelper.Error("修改SaleContract失败。", ee);
                return ee.Message;
            }

            return string.Empty;
        }

        private string UpdateSaleContractFirst(ExtendedIdentityDbContext dbContext,
            ContractInfo model, string userName, SaleContract contract)
        {
            ContractInfo.AssignValues(model, contract);
            //contract.SaleCreateTime = DateTime.Now;
            //contract.OperatorSysUser = userName;
            StringBuilder builder = new StringBuilder();

            //需要删除的：contract里面有，但是model里面没有的，这一点应该先做
            List<SaleProductItem> tobeDeleted = new List<SaleProductItem>();

            System.Diagnostics.Debug.Assert(model.OrderType == contract.OrderType);
            if (model.OrderType == 0)
            {
                var lookup0 = model.SaleProductItems.ToLookup<SaleProductItemInfo, int>(
                                   sp => sp.ProductItemId.HasValue ? sp.ProductItemId.Value : 0);
                var result0 = from one in contract.SaleProducts
                              where one.ProductItemId.HasValue && !lookup0.Contains(one.ProductItemId.Value)
                              select one;
                if (result0 != null && result0.Count() > 0)
                {
                    dbContext.SaleProductItems.RemoveRange(result0);
                    tobeDeleted.AddRange(result0);
                }
            }
            else if (model.OrderType == 1)
            {
                var lookup1 = model.SaleProductItems.ToLookup<SaleProductItemInfo, int>(
                                   sp => sp.ProductItemId.HasValue ? sp.ProductItemId.Value : 0);
                var result1 = from one in contract.SaleProducts
                              where one.StockItem != null && !lookup1.Contains(one.StockItem.ProductItemId)
                              select one;
                if (result1 != null && result1.Count() > 0)
                {
                    dbContext.SaleProductItems.RemoveRange(result1);
                    tobeDeleted.AddRange(result1);
                }
            }

            var toDeleteIds = tobeDeleted.ToLookup<SaleProductItem, int>(
                                   sp => sp.SaleProductItemId);

            foreach (var item in model.SaleProductItems)
            {
                if (item != null)
                {
                    SaleProductItem saleItem = null;
                    if (model.OrderType == 0)
                    {
                        saleItem = contract.SaleProducts.FirstOrDefault(
                            s => (s.ProductItemId.HasValue && item.ProductItemId.HasValue &&
                                s.ProductItemId.Value == item.ProductItemId.Value));
                    }
                    else if (model.OrderType == 1)
                    {
                        saleItem = contract.SaleProducts.FirstOrDefault(
                            s => (item.ProductItemId.HasValue && s.StockItem != null
                                && s.StockItem.ProductItemId == item.ProductItemId.Value));
                    }

                    if (saleItem == null)
                    {
                        saleItem = dbContext.SaleProductItems.Create();
                        contract.SaleProducts.Add(saleItem);
                    }
                    SaleProductItemInfo.AssignValues(item, saleItem);
                    saleItem.SaleContract = contract;

                    string errMsg = string.Empty;
                    if (contract.OrderType == 1)
                    {//现货
                        errMsg = this.HandleStockItemMove(dbContext, contract, saleItem, userName);
                    }
                    if (!string.IsNullOrWhiteSpace(errMsg))
                        builder.AppendLine(errMsg);
                    //dbContext.SaleProductItems.Add(saleItem);
                }
            }

            //var tp = from it in contract.SaleProducts
            //         where model.SaleProductItems.Any(s =>
            //         (s.SaleProductItemId != it.ProductItemId && contract.OrderType == 0)
            //         || (s.SaleProductItemId != it.SaleProductItemId && contract.OrderType == 1))
            //         // m => m.ProductItemId == it.SaleProductItemId)
            //         select it;

            //if (tp != null && tp.Count() > 0)
            //{
            //    foreach (var i in tp)
            //    {
            //        contract.SaleProducts.Remove(i);
            //    }
            //    dbContext.SaleProductItems.RemoveRange(tp);
            //}
            //dbContext.SaleContracts.Add(contract);

            if (tobeDeleted != null && tobeDeleted.Count() > 0)
            {
                for (int k = contract.SaleProducts.Count - 1; k >= 0; k--)
                {
                    var tempk = contract.SaleProducts.ElementAt(k);
                    if (toDeleteIds.Contains(tempk.ProductItemId.GetValueOrDefault()))
                    {
                        contract.SaleProducts.Remove(tempk);
                    }
                }
            }

            string temp = builder.ToString();
            if (!string.IsNullOrWhiteSpace(temp))
                return temp.Trim();

            return string.Empty;
        }

        #region 调整还价
        /*
        internal string AddOrUpdateSaleContractBargainCore(ExtendedIdentityDbContext dbContext,
            SaleBargain bargain)
        {
            if (bargain == null || bargain.BargainItems == null || bargain.SaleContractId < 1)
                return "还价信息不正确，请核对还价的销售合同及其对应的货品项数目是否与还价的货品项一致。";

            try
            {
                SaleContract contract = dbContext.SaleContracts.Find(bargain.SaleContractId);
                if (contract == null || contract.SaleProducts.Count != bargain.BargainItems.Count)
                    return "还价信息不正确，请核对还价的销售合同及其对应的货品项数目是否与还价的货品项一致。";

                SaleBargain saleItem = dbContext.SaleBargains.FirstOrDefault(
                    m => m.SaleContractId == bargain.SaleContractId &&
                    m.BargainSysUserKey == bargain.BargainSysUserKey);

                if (saleItem == null)
                {//Add
                    saleItem = AddSaleContractBargainCore(dbContext, bargain, saleItem);
                }
                else
                {//Update
                    UpdateSaleContractBargainCore(dbContext, bargain, saleItem);
                }

                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                LogHelper.Error("添加销售还价失败。", e);
                return "添加销售还价失败。\t\t" + e.Message + "\t\t" + e.StackTrace;
            }

            //TODO:
            return string.Empty;
        }

        private SaleBargain AddSaleContractBargainCore(ExtendedIdentityDbContext dbContext,
            SaleBargain bargain, SaleBargain saleItem)
        {
            saleItem = dbContext.SaleBargains.Create();
            saleItem.BargainSysUserKey = bargain.BargainSysUserKey;
            saleItem.OperationState = bargain.OperationState;
            saleItem.SaleContractId = bargain.SaleContractId;
            var items = from one in bargain.BargainItems
                        select this.CreateNewOne(dbContext, one);
            //foreach (var i in bargain.BargainItems) { 
            saleItem.BargainItems = items.ToList();
            SaleBargainChangeRecord record = dbContext.SaleBargainChangeRecords.Create();
            record.CTIME = DateTime.Now;
            record.SaleBargin = saleItem;
            record.CurrentTotal = saleItem.Total;
            record.Comments = string.Format("{1}创建还价，还价为：{0}。", saleItem.Total, record.CTIME.ToString("yyyyMMdd HH:mm:ss"));
            dbContext.SaleBargainChangeRecords.Add(record);
            dbContext.SaleBargains.Add(saleItem);
            return saleItem;
        }
         
        private void UpdateSaleContractBargainCore(ExtendedIdentityDbContext dbContext,
            SaleBargain bargain, SaleBargain saleItem)
        {
            saleItem.BargainSysUserKey = bargain.BargainSysUserKey;
            saleItem.OperationState = bargain.OperationState;
            saleItem.SaleContractId = bargain.SaleContractId;
            var items = from one in saleItem.BargainItems
                        from two in bargain.BargainItems
                        where one.SaleProductItemId == two.SaleProductItemId
                        select new { one, two };
            foreach (var gp in items)
            {
                gp.one.BargainUnitPrice = gp.two.BargainUnitPrice;
            }

            SaleBargainChangeRecord record = dbContext.SaleBargainChangeRecords.Create();
            record.CTIME = DateTime.Now;
            record.SaleBargin = saleItem;
            record.PrevTotal = bargain.Total;
            record.CurrentTotal = saleItem.Total;
            record.Comments = string.Format("{2}修改还价，还价从 {1} 调整为：{0}。",
                saleItem.Total, bargain.Total, record.CTIME.ToString("yyyyMMdd HH:mm:ss"));
            dbContext.SaleBargainChangeRecords.Add(record);
        }*/
        #endregion

        private SaleBargainItem CreateNewOne(
            ExtendedIdentityDbContext dbContext, SaleBargainItem one)
        {
            var newItem = dbContext.SaleBargainItems.Create();
            newItem.BargainUnitPrice = one.BargainUnitPrice;
            newItem.SaleProductItemId = one.SaleProductItemId;
            return newItem;
        }

        internal string UpdateSaleContractStatusCore(ExtendedIdentityDbContext dbContext,
            SaleContract contract, ContractStatus contractStatus, string userName)
        {
            if (contract == null)
                return "SaleContract model为空。";

            try
            {
                contract.ContractStatus = contractStatus;

                string errorMessage2 = AddOrUpdateSaleContractFinancialObjsSecond(
                    dbContext, userName, contract);
                if (!string.IsNullOrWhiteSpace(errorMessage2))
                    return errorMessage2;

                int effectedRows = dbContext.SaveChanges();

                if (effectedRows < 1)
                {
                    return "修改SaleContract审核状态失败。";
                }
            }
            catch (Exception ee)
            {
                LogHelper.Error("修改SaleContract审核状态失败。", ee);
                return ee.Message;
            }

            return string.Empty;
        }


        internal IQueryable<SaleContract> GetIndexListSaleContract(Microsoft.Owin.IOwinContext owinContext,
            ExtendedIdentityDbContext dbContext, ContractListCondition condition)
        {
            try
            {
                SaleContractPrivilegeFilter filter = new SaleContractPrivilegeFilter()
                {
                    SerialOrSupplierFilterValue = condition.SerialOrSupplierFilterValue,
                    SaleCreateTimeFrom = condition.CTIMEFrom,
                    SaleCreateTimeTo = condition.CTIMETimeTo,
                    UserName = GetUserName(condition, owinContext),
                    OwinContext = owinContext,
                };

                IQueryable<SaleContract> contractsQuery = this.GetIndexListSaleContract(
                    dbContext, filter); //out temp1, out temp2);

                if (condition.OrderType.HasValue)
                {
                    contractsQuery = contractsQuery.Where(m => m.OrderType == condition.OrderType.Value
                        && m.ContractStatus != ContractStatus.Closed);
                }
                if ((condition.ListInclude & ContractListInclude.WithContractStatusClosed)
                    != ContractListInclude.WithContractStatusClosed)
                {
                    contractsQuery = contractsQuery.Where(m => m.ContractStatus
                        != ContractStatus.Closed);
                }
                return contractsQuery;
            }
            catch (Exception ee)
            {
                LogHelper.Error("获取SaleContract列表失败。", ee);
                return null;
            }
        }

        private string GetUserName(ContractListCondition condition,
            Microsoft.Owin.IOwinContext owinContext)
        {
            return AppBusinessManager.GetUserName(condition, owinContext);
        }
    }
}
