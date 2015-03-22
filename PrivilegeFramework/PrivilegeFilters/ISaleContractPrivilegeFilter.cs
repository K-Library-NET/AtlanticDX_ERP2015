using System;
namespace PrivilegeFramework.PrivilegeFilters
{
    public interface ISaleContractPrivilegeFilter
    {
        DateTime? SaleCreateTimeFrom { get; set; }
        DateTime? SaleCreateTimeTo { get; set; }

        /// <summary>
        /// 单据号或供应商
        /// </summary>
        string SerialOrSupplierFilterValue { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }
    }
}
