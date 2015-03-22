using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeFramework
{
    /// <summary>
    /// 通过ContractManager获取的列表对象所返回的信息
    /// </summary>
    [Flags]
    public enum ContractListInclude
    {
        /// <summary>
        /// 不返回
        /// </summary>
        None = 0,
        /// <summary>
        /// 只返回采购
        /// </summary>
        OrderContractOnly = 1,
        /// <summary>
        /// 只返回销售
        /// </summary>
        SaleContractOnly = 2,
        /// <summary>
        /// 同时返回采购和销售
        /// </summary>
        BothOrderAndSaleContract = 3,

        /// <summary>
        /// 返回附加的库存信息
        /// </summary>
        WithProductStock = 4,

        /// <summary>
        /// 返回附加的港口代理信息
        /// </summary>
        WithHarborAgent = 8,

        /// <summary>
        /// 返回附加的香港物流信息
        /// </summary>
        WithHongkongLogistics = 16,

        /// <summary>
        /// 返回附加的内地物流信息
        /// </summary>
        WithMainlandLogistics = 32,

        /// <summary>
        /// 返回附加的采购索赔信息
        /// </summary>
        WithOrderCompensation = 64,

        /// <summary>
        /// 返回附加的销售索赔信息
        /// </summary>
        WithSaleCompensation = 128,

        /// <summary>
        /// 返回附加的采购财务信息
        /// </summary>
        WithOrderFinancials = 256,

        /// <summary>
        /// 返回附加的销售财务信息
        /// </summary>
        WithSaleFinancials = 512,

        /// <summary>
        /// 返回附加的汇总字段值（已经根据权限过滤）
        /// </summary>
        WithAggregations = 1024,

        /// <summary>
        /// 不包含未审核过的订单（采购/销售）
        /// </summary>
        WithoutContractStatusNotAudited = 2048,
        /// <summary>
        /// 不包含已经审核但未通过的订单（采购/销售）
        /// </summary>
        WithoutContractStatusAuditNoPass = 4096,
        /// <summary>
        /// 不包含审核通过的订单（采购/销售）
        /// </summary>
        WithoutContractStatusAuditPassed = 8192,

        /// <summary>
        /// 包含已经结单的合同（采购/销售）
        /// </summary>
        WithContractStatusClosed = 16384,
    }
}
