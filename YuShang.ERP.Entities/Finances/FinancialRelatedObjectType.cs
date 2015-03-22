using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Finances
{
    /// <summary>
    /// 财务记录与业务对象的关联关系
    /// </summary>
    public enum FinancialRelatedObjectType
    {
        /// <summary>
        /// 实付账款记录与采购合同相关，
        /// </summary>
        AccountsPayRecord_To_OrderContract,
        /// <summary>
        /// 实付账款记录与销售合同相关，
        /// </summary>
        AccountsPayRecord_To_SaleContract,
        /// <summary>
        /// 实付账款记录与应付账款相关
        /// </summary>
        AccountsPayRecord_To_AccountsPayable,

        /// <summary>
        /// 实收账款记录与采购合同相关
        /// </summary>
        AccountsReceiveRecord_To_OrderContract,
        /// <summary>
        /// 实收账款记录与销售合同相关
        /// </summary>
        AccountsReceiveRecord_To_SaleContract,
        /// <summary>
        /// 实收账款记录与应收账款相关
        /// </summary>
        AccountsReceiveRecord_To_AccountsReceivable,
    }
}
