using System;

namespace YuShang.ERP.Entities.Finances
{
    /// <summary>
    /// 财务记录
    /// </summary>
    public interface IAccountsRecord
    {
        /// <summary>
        /// 操作用户名
        /// </summary>
        string OperatorSysUserName { get; set; }

        /// <summary>
        /// 财务记录类型
        /// </summary>
        FinancialRecordType RecordType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        double Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        string Comments { get; set; }

        /// <summary>
        /// 记录产生时间
        /// </summary>
        DateTime CTIME { get; set; }

        /// <summary>
        /// 记录更改时间
        /// </summary>
        DateTime? UTIME { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        int Day { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        int Month { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        int Year { get; set; }

        /// <summary>
        /// 是否已经删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
