using PrivilegeFramework.PrivilegeFilters;
using System;

namespace AtlanticDX.Model.Areas.Orders.Models
{
    public class OrderSubmitFilterViewModel : IOrderContractPrivilegeFilter
    {
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
        public string ProductFullNameFilterValue
        {
            get;
            set;
        }

        /// <summary>
        /// 当前用户的用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
    }
}