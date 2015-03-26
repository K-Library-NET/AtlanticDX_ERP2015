using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityFramework;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.Privileges;
using YuShang.ERP.Entities.ResMgr;

namespace PrivilegeFramework.PrivilegeFilters
{
    /// <summary>
    /// 通过数据权限，过滤采购合同/订单
    /// 过滤的左右端点可以只有一个，如果两个都没有，则视为这个条件不过滤
    /// 字符串为空值也视为这个条件不过滤
    /// </summary>
    public class OrderContractPrivilegeFilter : IOrderContractPrivilegeFilter, PrivilegeFramework.PrivilegeFilters.IOrderContractQueryBuilder
    {
        public OrderContractPrivilegeFilter()
            : this(null)
        {

        }

        public OrderContractPrivilegeFilter(IOrderContractPrivilegeFilter sourceFilter)
        {
            if (sourceFilter != null)
            {
                this.ETAFrom = sourceFilter.ETAFrom;
                this.ETATo = sourceFilter.ETATo;
                this.OrderCreateTimeFrom = sourceFilter.OrderCreateTimeFrom;
                this.OrderCreateTimeTo = sourceFilter.OrderCreateTimeTo;
                this.ProductFullNameFilterValue = sourceFilter.ProductFullNameFilterValue;
                this.ProductKeys = sourceFilter.ProductKeys;
                this.SupplierId = sourceFilter.SupplierId;
                this.UserName = sourceFilter.UserName;
            }
        }

        /// <summary>
        /// 当前用户的用户名，空则表示不根据权限过滤
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 订单创建时间过滤的“左端点”，使用闭区间，就是>=
        /// </summary>
        public DateTime? OrderCreateTimeFrom { get; set; }

        /// <summary>
        /// 订单创建时间过滤的“右端点”，使用闭区间，就是<=
        /// </summary>
        public DateTime? OrderCreateTimeTo { get; set; }

        /// <summary>
        /// ETA过滤的“左端点”，使用闭区间，就是>=
        /// </summary>
        public DateTime? ETAFrom { get; set; }

        /// <summary>
        /// ETA过滤的“右端点”，使用闭区间，就是<=
        /// </summary>
        public DateTime? ETATo { get; set; }

        public int? SupplierId { get; set; }

        /// <summary>
        /// 通过半角逗号“,”分割的多个ProductKey
        /// </summary>
        public string ProductKeys { get; set; }

        /// <summary>
        /// 通过"国家/厂号/货品名/品牌" ProductFullName过滤
        /// 目前只支持单个
        /// </summary>
        public string ProductFullNameFilterValue { get; set; }

        public IOwinContext OwinContext { get; set; }


        public IQueryable<YuShang.ERP.Entities.Orders.OrderContract>
            BuildQueryable(ExtendedIdentityDbContext db, IQueryable<OrderContract> sourceQuery)
        {
            if (!this.IsEmptyFilter())
            {
                return this.BuildQueryableCore(db, sourceQuery);
            }

            return sourceQuery != null ? sourceQuery : db.OrderContracts.AsNoTracking();
        }

        public bool IsEmptyFilter()
        {
            string[] splitedProductKeys = new string[] { };
            return this.IsEmptyUserName() && this.IsEmptyOrderCreateTime()
                && this.IsEmptyETA() && this.IsEmptySupplier()
                && this.IsEmptyProductFullNameFilterValue()
                && this.IsEmptyProductKeys(ref splitedProductKeys);
        }

        private bool IsEmptyUserName()
        {
            return string.IsNullOrWhiteSpace(this.UserName);
        }

        private bool IsEmptyOrderCreateTime()
        {
            if (this.OrderCreateTimeFrom.HasValue || this.OrderCreateTimeTo.HasValue)
                return false;
            return true;
        }

        private bool IsEmptyETA()
        {
            if (this.ETAFrom.HasValue || this.ETATo.HasValue)
                return false;
            return true;
        }

        private bool IsEmptySupplier()
        {
            return this.SupplierId.HasValue == false ||
                (this.SupplierId.HasValue && this.SupplierId.Value < 1);
            //值小于1的话，目前认为是不正确的；
        }

        private bool IsEmptyProductKeys(ref string[] splitedProductKeys)
        {
            if (string.IsNullOrWhiteSpace(this.ProductKeys))
                return true;

            string[] splited = this.ProductKeys.Trim().ToLowerInvariant()
                .Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries);
            if (splited == null || splited.Length < 1)
                return true;

            splitedProductKeys = splited;
            return false;
        }

        private bool IsEmptyProductFullNameFilterValue()
        {
            return string.IsNullOrWhiteSpace(this.ProductFullNameFilterValue);
        }

        private IQueryable<YuShang.ERP.Entities.Orders.OrderContract>
            BuildQueryableCore(ExtendedIdentityDbContext db, IQueryable<OrderContract> sourceQuery)
        {
            var noTrackingDbQuery = sourceQuery;
            if (noTrackingDbQuery == null)
                noTrackingDbQuery = db.OrderContracts.Include("OrderProducts").AsNoTracking();

            try
            {
                IQueryable<OrderContract> tempDbQuery = noTrackingDbQuery;

                if (!this.IsEmptySupplier())
                {
                    tempDbQuery = tempDbQuery.Where(m => m.SupplierId == this.SupplierId.Value);
                }

                tempDbQuery = AddTimeConditions(tempDbQuery);

                tempDbQuery = AddLiteralConditions(db, tempDbQuery);

                tempDbQuery = AddEntityPrivilegeControlConditions(db,
                    tempDbQuery, EntityControlType.OrderContract, this.UserName);

                return tempDbQuery;
            }
            catch (Exception ee)
            {
                LogHelper.Error("OrderContractPrivilegeFilter创建过滤条件出错。", ee);
            }

            return noTrackingDbQuery;
        }

        private IQueryable<OrderContract> AddEntityPrivilegeControlConditions(
            ExtendedIdentityDbContext db, IQueryable<OrderContract> tempDbQuery,
            EntityControlType entityControlType, string userName)
        {
            var temp1 = PrivilegeManager.Instance.BuildQueryWithEntityControl(OwinContext,
                  db, tempDbQuery, entityControlType, userName);

            if (temp1 != null && temp1 is IQueryable<OrderContract>)
                tempDbQuery = temp1 as IQueryable<OrderContract>;
            return tempDbQuery;
        }

        private IQueryable<OrderContract> AddLiteralConditions(ExtendedIdentityDbContext db,
            IQueryable<OrderContract> tempDbQuery)
        {
            // var products = ResBusinessManager.Instance.GetProducts();
            //通过找出OrderContractKey，寻找OrderContract对象
            //string[] tempKeys = null;

            //IEnumerable<string> tempKey1 = null;
            //if (!this.IsEmptyProductKeys(ref tempKeys))
            //{
            //    tempKey1 = (from one in db.Products//products
            //                where tempKeys.Contains(one.ProductKey)
            //                select one.ProductKey);
            //}

            IEnumerable<int> tempKey2 = null;
            if (!this.IsEmptyProductFullNameFilterValue())
            {
                var filterValue = this.ProductFullNameFilterValue.Trim().ToLowerInvariant();

                tempKey2 = (from one in db.Products
                            where
                            one.ProductName.ToLowerInvariant().Contains(filterValue)
                            || one.MadeInFactory.ToLowerInvariant().Contains(filterValue)
                            || one.MadeInCountry.ToLowerInvariant().Contains(filterValue)
                            || one.Brand.ToLowerInvariant().Contains(filterValue)
                            || one.ProductNameENG.ToLowerInvariant().Contains(filterValue)
                            select one.ProductId);
            }

            List<int> listTemp = new List<int>();
            //if (tempKey1 != null && tempKey1.Count() > 0)
            //    listTemp.AddRange(tempKey1);
            if (tempKey2 != null && tempKey2.Count() > 0)
                listTemp.AddRange(tempKey2);
            //tempKeys = listTemp.Distinct().ToArray();

            if (listTemp != null && listTemp.Count > 0) //必须是有字符串过滤条件才加入这个Query
            {
                tempDbQuery = tempDbQuery.Where(
                    m1 => m1.OrderProducts.Any(m2 => listTemp.Contains(m2.ProductId)));

                //var productOrderContractKeys = listTemp.Join(db.ProductItems,
                //                productKey => productKey, item => item.ProductId,
                //                (product, item) => item.OrderContractKey).Distinct();

                //tempDbQuery = tempDbQuery.Where(m2 => productOrderContractKeys.Contains(m2.OrderContractKey));
            }

            return tempDbQuery;
        }

        private IQueryable<OrderContract> AddTimeConditions(IQueryable<OrderContract> tempDbQuery)
        {
            if (!this.IsEmptyETA())
            {
                if (this.ETAFrom.HasValue && this.ETATo.HasValue == false)
                {
                    tempDbQuery = tempDbQuery.Where(m => m.ETA >= this.ETAFrom.Value);
                }
                else if (this.ETAFrom.HasValue == false && this.ETATo.HasValue)
                {
                    tempDbQuery = tempDbQuery.Where(m => m.ETA <= this.ETATo.Value);
                }
                else
                {//this.ETAFrom.HasValue && this.ETATo.HasValue
                    tempDbQuery = tempDbQuery.Where(m => m.ETA >= this.ETAFrom.Value
                        && m.ETA <= this.ETATo.Value);
                }
            }
            if (!this.IsEmptyOrderCreateTime())
            {
                if (this.OrderCreateTimeFrom.HasValue && this.OrderCreateTimeTo.HasValue == false)
                {
                    tempDbQuery = tempDbQuery.Where(m => m.OrderCreateTime >= this.OrderCreateTimeFrom.Value);
                }
                else if (this.OrderCreateTimeFrom.HasValue == false && this.OrderCreateTimeTo.HasValue)
                {
                    tempDbQuery = tempDbQuery.Where(m => m.OrderCreateTime <= this.OrderCreateTimeTo.Value);
                }
                else
                {//this.OrderCreateTimeFrom.HasValue && this.OrderCreateTimeTo.HasValue
                    tempDbQuery = tempDbQuery.Where(m => m.OrderCreateTime >= this.OrderCreateTimeFrom.Value
                        && m.OrderCreateTime <= this.OrderCreateTimeTo.Value);
                }
            }
            return tempDbQuery;
        }
    }
}
