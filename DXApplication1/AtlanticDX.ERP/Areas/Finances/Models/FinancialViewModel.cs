using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YuShang.ERP.Entities.Finances;

namespace AtlanticDX.ERP.Areas.Finances.Models
{
    public class AccountsPayableViewModel
    {
        private AccountsPayable m;
        private AtlanticDXContext db;

        public AccountsPayableViewModel(AccountsPayable m, AtlanticDXContext db)
        {
            // TODO: Complete member initialization
            this.m = m;
            this.db = db;
        }

        public int AccountsPayableId
        {
            get
            {
                return m.AccountsPayableId;
            }
        }

        [Display(Name = "时间")]
        public string CTIME_Str
        {
            get
            {
                return m.CTIME.ToString("yyyy年MM月dd日");
            }
        }

        [Display(Name = "会计科目")]
        public string EventType_Str
        {
            get;
            set;
        }

        [Display(Name = "金额")]
        public double Amount_Round
        {
            get
            {
                return Math.Round(this.m.Amount, 2);
            }
        }

        [Display(Name = "记录类型")]
        public string RecordTypeStr
        {
            get;
            set;
        }

        [Display(Name = "支付状态")]
        public string PayStatus_Str
        {
            get
            {
                return this.m.PayStatus == 1 ? "已结清" : "未结清";
            }
        }

        [Display(Name = "关联的采购合同")]
        public string OrderContractKey
        {
            get
            {
                return m.OrderContractKey;
            }
        }

        [Display(Name = "已付账款")]
        public double HasPaidAmount
        {
            get;
            set;
        }

        [Display(Name = "未付账款")]
        public double ToBePaidAmount
        {
            get;
            set;
        }

        [Display(Name = "操作用户")]
        public string OperateSysUser_PersonName
        {
            get;
            set;
        }

        [Display(Name = "备注")]
        public string Comments
        {
            get;
            set;
        }

        /*
                <th data-options="field:'CTIME_Str',width:'150',editor:'text'">时间</th>
                <th data-options="field:'EventType_Str',width:'150',editor:'text'">会计科目</th>
                <th data-options="field:'Amount_Round',width:'150',editor:'text'">金额</th>
                <th data-options="field:'RecordTypeStr',width:'150',editor:'text'">记录类型</th>
                <th data-options="field:'PayStatus_Str',width:'150',editor:'text'">支付状态</th>
                <th data-options="field:'OrderContractKey',width:'150',editor:'text'">关联的采购合同</th>
                <th data-options="field:'PayStatus_Str',width:'150',editor:'text'">已付账款</th>
                <th data-options="field:'PayStatus_Str',width:'150',editor:'text'">未付账款</th>
                <th data-options="field:'OperateSysUser_PersonName',width:'150',editor:'text'">操作用户</th>
                <th data-options="field:'Comments',width:'150',editor:'text'">备注</th>
            */

    }

    public class AccountsReceivableViewModel
    {
        private AccountsReceivable m;
        private AtlanticDXContext db;

        public AccountsReceivableViewModel(AccountsReceivable m, AtlanticDXContext db)
        {
            // TODO: Complete member initialization
            this.m = m;
            this.db = db;
        }

        public int AccountsReceivableId
        {
            get
            {
                return m.AccountsReceivableId;
            }
        }

        public string CTIME_Str
        {
            get
            {
                return m.CTIME.ToString("yyyy年MM月dd日");
            }
        }

        public string EventType_Str
        {
            get;
            set;
        }

        public double Amount_Round
        {
            get
            {
                return Math.Round(this.m.Amount, 2);
            }
        }

        public string RecordTypeStr
        {
            get;
            set;
        }

        public string PayStatus_Str
        {
            get
            {
                return this.m.PayStatus == 1 ? "已结清" : "未结清";
            }
        }

        public string SaleContractKey
        {
            get
            {
                return m.SaleContractKey;
            }
        }

        public double HasReceivedAmount
        {
            get;
            set;
        }

        public double ToBeReceivedAmount
        {
            get;
            set;
        }

        public string OperateSysUser_PersonName
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }
    }

    public class FinancialViewModel
    {
        private AccountsPayable m_payable;
        private AccountsReceivable m_receivable;

        public FinancialViewModel(AccountsPayable obj)
        {
            this.m_payable = obj;
        }

        public FinancialViewModel(AccountsReceivable obj)
        {
            this.m_receivable = obj;
        }

        public DateTime CTIME
        {
            get
            {
                if (m_payable != null)
                    return m_payable.CTIME;
                if (m_receivable != null)
                    return m_receivable.CTIME;

                return DateTime.Now;
            }
        }
    }
}