using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityFramework;
using YuShang.ERP.Entities.Privileges;
using YuShang.ERP.Entities.Sale;

namespace PrivilegeFramework.PrivilegeFilters
{
    /// <summary>
    /// 通过数据权限，过滤销售合同/订单
    /// 过滤的左右端点可以只有一个，如果两个都没有，则视为这个条件不过滤
    /// 字符串为空值也视为这个条件不过滤
    /// </summary>
    public class SaleContractPrivilegeFilter : ISaleContractQueryBuilder,
        PrivilegeFramework.PrivilegeFilters.ISaleContractPrivilegeFilter
    {
        public SaleContractPrivilegeFilter()
        {

        }

        public SaleContractPrivilegeFilter(ISaleContractPrivilegeFilter filter)
        {
            this.SaleCreateTimeFrom = filter.SaleCreateTimeFrom;
            this.SaleCreateTimeTo = filter.SaleCreateTimeTo;
            this.SerialOrSupplierFilterValue = filter.SerialOrSupplierFilterValue;
            this.UserName = filter.UserName;
        }

        public DateTime? SaleCreateTimeFrom
        {
            get;
            set;
        }

        public DateTime? SaleCreateTimeTo
        {
            get;
            set;
        }

        /// <summary>
        /// 单据号或供应商
        /// </summary>
        public string SerialOrSupplierFilterValue
        {
            get;
            set;
        }

        /// <summary>
        /// 当前用户的用户名，空则表示不根据权限过滤
        /// </summary>
        public string UserName { get; set; }

        public IQueryable<YuShang.ERP.Entities.Sale.SaleContract> BuildQueryable(
            ExtendedIdentityDbContext db, IQueryable<SaleContract> sourceQuery)
        {
            if (!this.IsEmptyFilter())
            {
                return this.BuildQueryableCore(db, sourceQuery);
            }

            return sourceQuery != null ? sourceQuery : db.SaleContracts.AsNoTracking();
        }

        private IQueryable<SaleContract> BuildQueryableCore(ExtendedIdentityDbContext db,
            IQueryable<SaleContract> sourceQuery)
        {
            var noTrackingDbQuery = sourceQuery;
            if (noTrackingDbQuery == null)
                noTrackingDbQuery = db.SaleContracts.AsNoTracking();

            try
            {
                IQueryable<SaleContract> tempDbQuery = noTrackingDbQuery;

                tempDbQuery = AddTimeConditions(tempDbQuery);

                tempDbQuery = AddLiteralConditions(db, tempDbQuery);

                tempDbQuery = AddEntityPrivilegeControlConditions(db,
                    tempDbQuery, EntityControlType.SaleContract, this.UserName);

                return tempDbQuery; 
            }
            catch (Exception ee)
            {
                LogHelper.Error("SaleContractPrivilegeFilter创建过滤条件出错。", ee);
            }

            return noTrackingDbQuery;
        }

        private IQueryable<SaleContract> AddEntityPrivilegeControlConditions(
            ExtendedIdentityDbContext db, IQueryable<SaleContract> tempDbQuery,
            EntityControlType entityControlType, string userName)
        {
            var temp2 = PrivilegeManager.Instance.BuildQueryWithEntityControl(
                this.OwinContext, db, tempDbQuery, entityControlType, userName);

            if (temp2 != null && temp2 is IQueryable<SaleContract>)
                tempDbQuery = temp2 as IQueryable<SaleContract>;

            return tempDbQuery;
        }

        private IQueryable<SaleContract> AddLiteralConditions(ExtendedIdentityDbContext db,
            IQueryable<SaleContract> tempDbQuery)
        {
            if (this.IsEmptySerialOrSupplierFilterValue())
                return tempDbQuery;

            var target = SerialOrSupplierFilterValue.Trim().ToLowerInvariant();

            var temp = (from pro in db.SaleProductItems.Include("OrderProductItem.OrderContract")
                        where pro.OrderProductItem != null &&
                       pro.OrderProductItem.OrderContract != null
                        select pro).Join(db.Suppliers, p => p.OrderProductItem.OrderContract.SupplierId,
                            q => q.SupplierId,
                            (p, q) => p.SaleContract.SaleContractId);

            //var array = temp.ToArray();//saleContractIds 
            tempDbQuery = tempDbQuery.Where(t => temp.Contains(t.SaleContractId));

            return tempDbQuery;
        }

        private IQueryable<SaleContract> AddTimeConditions(IQueryable<SaleContract> tempDbQuery)
        {
            if (!this.IsEmptySaleCreateTime())
            {
                if (this.SaleCreateTimeFrom.HasValue && this.SaleCreateTimeTo.HasValue == false)
                {
                    tempDbQuery = tempDbQuery.Where(m => m.SaleCreateTime >= this.SaleCreateTimeFrom.Value);
                }
                else if (this.SaleCreateTimeFrom.HasValue == false && this.SaleCreateTimeTo.HasValue)
                {
                    tempDbQuery = tempDbQuery.Where(m => m.SaleCreateTime <= this.SaleCreateTimeTo.Value);
                }
                else
                {//this.OrderCreateTimeFrom.HasValue && this.OrderCreateTimeTo.HasValue
                    tempDbQuery = tempDbQuery.Where(m => m.SaleCreateTime >= this.SaleCreateTimeFrom.Value
                        && m.SaleCreateTime <= this.SaleCreateTimeTo.Value);
                }
            }
            return tempDbQuery;
        }

        public bool IsEmptyFilter()
        {
            return this.IsEmptyUserName() && this.IsEmptySaleCreateTime()
                && this.IsEmptySerialOrSupplierFilterValue();
        }

        private bool IsEmptyUserName()
        {
            return string.IsNullOrWhiteSpace(this.UserName);
        }

        private bool IsEmptySaleCreateTime()
        {
            if (this.SaleCreateTimeFrom.HasValue || this.SaleCreateTimeFrom.HasValue)
                return false;
            return true;
        }

        private bool IsEmptySerialOrSupplierFilterValue()
        {
            return string.IsNullOrWhiteSpace(this.SerialOrSupplierFilterValue);
        }

        public Microsoft.Owin.IOwinContext OwinContext { get; set; }
    }
}
