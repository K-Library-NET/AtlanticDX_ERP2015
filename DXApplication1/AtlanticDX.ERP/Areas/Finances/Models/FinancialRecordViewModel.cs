using System;
using System.ComponentModel.DataAnnotations;
using UtilityFramework;
using YuShang.ERP.Entities.Finances;

namespace AtlanticDX.Model.Areas.Finances.Models
{
    public class FinancialRecordViewModel : YuShang.ERP.Entities.Finances.IAccountsRecord
    {
        private YuShang.ERP.Entities.Finances.IAccountsRecord m_record;
        //private IEnumerable<AccountsRecordRelation> relations;
        private YuShang.ERP.Entities.Privileges.SysUser sysUser;

        //public FinancialRecordViewModel(YuShang.ERP.Entities.Finances.IAccountsRecord record,
        //    PrivilegeFramework.ExtendedIdentityDbContext db)
        //{
        //    this.m_record = record;
        //}

        public FinancialRecordViewModel(IAccountsRecord accountsRecord, //IEnumerable<AccountsRecordRelation> relations,
            YuShang.ERP.Entities.Privileges.SysUser sysUser)
        {
            this.m_record = accountsRecord;
            //this.relations = relations;
            this.sysUser = sysUser;

            //if (this.relations == null || relations.Count() < 1)
            //{
            //    this.EventType = AccountingEventType.Others;
            //}
            //else
            //{
            //    var tmp = from one in this.relations
            //              where one.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_AccountsPayable
            //              || one.RelatedObjectType == FinancialRelatedObjectType.AccountsReceiveRecord_To_AccountsReceivable
            //              select one;
            //    if (tmp == null || tmp.Count() < 1)
            //        this.EventType = AccountingEventType.Others;
            //    else
            //    {
            //        var tmp2 = tmp.First();

            //        if (tmp2.RelatedObjectType == FinancialRelatedObjectType.AccountsPayRecord_To_AccountsPayable)
            //            this.EventType = db.AccountsPayables.Find(tmp2.RelatedObjectId).EventType;
            //        else if (tmp2.RelatedObjectType == FinancialRelatedObjectType.AccountsReceiveRecord_To_AccountsReceivable)
            //        {
            //            this.EventType = db.AccountsReceivables.Find(tmp2.RelatedObjectId).EventType;
            //        }
            //        else this.EventType = AccountingEventType.Others;
            //    }
            //}
        }

        [Display(Name = "时间")]
        public string CTIME_Str
        {
            get
            {
                return this.CTIME.ToString("yyyy年MM月dd日");
            }
        }

        [Display(Name = "费用类型")]
        public AccountingEventType EventType
        {
            get
            {
                if (this.m_record.AccountsPayableId.HasValue && this.m_record.AccountsPayable != null)
                    return this.m_record.AccountsPayable.EventType;
                else if (this.m_record.AccountsReceivableId.HasValue && this.m_record.AccountsReceivable != null)
                    return this.m_record.AccountsReceivable.EventType;
                else return AccountingEventType.Others;
            }
        }

        [Display(Name = "实收/实付")]
        public string RecordTypeStr
        {
            get
            {
                return this.RecordType.GetDisplayName();

                //if (this.m_record.RecordType == FinancialRecordType.AccountsPayable)
                //    return "实付账款";
                //if (this.m_record.RecordType == FinancialRecordType.AccountsReceivable)
                //    return "实收账款";
                //return this.RecordType.ToString();
            }
        }

       [Display(Name = "费用类型")]
        public string EventTypeStr
        {
            get
            {
                return this.EventType.GetDisplayName();
            }
        }

        [Display(Name = "操作用户")]
        public string OperateSysUser_PersonName
        {
            get
            {
                return this.sysUser.Name;//this.OperatorSysUserName;
            }
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "账款类型")]
        public YuShang.ERP.Entities.Finances.FinancialRecordType RecordType
        {
            get
            {
                return m_record.RecordType;
            }
            set
            {
            }
        }

        [Display(Name = "金额")]
        public double Amount_Round
        {
            get
            {
                return Math.Round(this.Amount, 2);
            }
        }

        public double Amount
        {
            get
            {
                return m_record.Amount;
            }
            set
            {
            }
        }

        [Display(Name = "备注")]
        public string Comments
        {
            get
            {
                return m_record.Comments;
            }
            set
            {
            }
        }

        [Display(Name = "产生日期")]
        public DateTime CTIME
        {
            get
            {
                return m_record.CTIME;
            }
            set
            {
            }
        }

        public int Day
        {
            get
            {
                return m_record.Day;
            }
            set
            {
            }
        }

        public int Month
        {
            get
            {
                return m_record.Month;
            }
            set
            {
            }
        }

        public int Year
        {
            get
            {
                return m_record.Year;
            }
            set
            {
            }
        }

        public string OperatorSysUserName
        {
            get
            {
                return m_record.OperatorSysUserName;
            }
            set
            {
            }
        }

        public DateTime? UTIME
        {
            get
            {
                return m_record.UTIME;
            }
            set
            {
            }
        }


        /// <summary>
        /// 财务记录的币种
        /// </summary>
        [Display(Name = "币种")]       
        public string Currency
        {
            get
            {
                return m_record.Currency;
            }
            set
            {
            }
        }

        /// <summary>
        /// 财务记录产生或修改时刻的汇率，目前都是转到人民币
        /// </summary>
        [Display(Name = "汇率")]
        public double CurrencyExchangeRate
        {
            get
            {
                return m_record.CurrencyExchangeRate;
            }
            set
            {
            }
        }

        public bool IsDeleted
        {
            get
            {
                return m_record.IsDeleted;
            }
            set
            {
            }
        }

        #region interfaces 暂时不用实现都可以

        /// <summary>
        /// 应付账款信息Id
        /// </summary>
        [Display(Name = "应付账款")]
        public int? AccountsPayableId
        {
            get;
            set;
        }
        /// <summary>
        /// 应收账款信息Id
        /// </summary>
        [Display(Name = "应收账款")]
        public int? AccountsReceivableId
        {
            get;
            set;
        }

        /// <summary>
        /// 应付账款信息
        /// </summary>
        [Display(Name = "应付账款")]
        public AccountsPayable AccountsPayable
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 应收账款信息
        /// </summary>
        [Display(Name = "应收账款")]
        public AccountsReceivable AccountsReceivable
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}