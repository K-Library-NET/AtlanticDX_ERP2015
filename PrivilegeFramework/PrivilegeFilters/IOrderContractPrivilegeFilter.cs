using System;
using System.Linq;
using YuShang.ERP.Entities.Orders;
namespace PrivilegeFramework.PrivilegeFilters
{
    /// <summary>
    /// 通过数据权限，过滤采购合同/订单
    /// 过滤的左右端点可以只有一个，如果两个都没有，则视为这个条件不过滤
    /// 字符串为空值也视为这个条件不过滤
    /// </summary>
    public interface IOrderContractPrivilegeFilter
    {
        /// <summary>
        /// ETA过滤的“左端点”，使用闭区间，就是>=
        /// </summary>
        DateTime? ETAFrom { get; set; }

        /// <summary>
        /// ETA过滤的“右端点”，使用闭区间，就是<=
        /// </summary>
        DateTime? ETATo { get; set; }

        /// <summary>
        /// 订单创建时间过滤的“左端点”，使用闭区间，就是>=
        /// </summary>
        DateTime? OrderCreateTimeFrom { get; set; }

        /// <summary>
        /// 订单创建时间过滤的“右端点”，使用闭区间，就是<=
        /// </summary>
        DateTime? OrderCreateTimeTo { get; set; }

        /// <summary>
        /// 通过"国家/厂号/货品名/品牌" ProductFullName过滤
        /// 目前只支持单个
        /// </summary>
        string ProductFullNameFilterValue { get; set; }

        /// <summary>
        /// 通过半角逗号“,”分割的多个ProductKey
        /// </summary>
        string ProductKeys { get; set; }

        int? SupplierId { get; set; }

        /// <summary>
        /// 当前用户的用户名，空则表示不根据权限过滤
        /// </summary>
        string UserName { get; set; }
    }
}
