using PrivilegeFramework.OrderRelateds;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.ResMgr;

namespace PrivilegeFramework
{
    public class HarborAgentViewModel : IContractRelatedViewModel
    {
        private YuShang.ERP.Entities.Orders.HarborAgent harborAgent;
        public HarborAgentViewModel()
        {

        }

        public HarborAgentViewModel(YuShang.ERP.Entities.Orders.HarborAgent harborAgent)
        {
            this.harborAgent = harborAgent;
            if (this.harborAgent != null)
            {
                this.IsEnable = true;
                AssignValues(harborAgent, this);
            }
        }


        [Display(Name = "港口代理")]
        public int? HarborAgentId { get; set; }

        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是没有这一附加项
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        ////[Required]
        public bool? IsEnable
        {
            get;
            set;
        }


        [Display(Name = "错误信息")]
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 报关公司ID（资源管理）
        /// </summary>
        public int? DeclarationCompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// 码头费
        /// </summary>
        [Display(Name = "码头费")]
        public double? HarborCost { get; set; }

        /// <summary>
        /// 代理费
        /// </summary>
        [Display(Name = "代理费")]
        public double? AgentCost { get; set; }

        /// <summary>
        /// 关税
        /// </summary>
        [Display(Name = "关税")]
        public double? Tariff { get; set; }

        /// <summary>
        /// 反倾销税
        /// </summary>
        [Display(Name = "反倾销税")]
        public double? AntiDumpingTax { get; set; }

        /// <summary>
        /// 增值税
        /// </summary>
        [Display(Name = "增值税")]
        public double? ValueAddedTax { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        [Display(Name = "其他费用")]
        public double? OthersCost { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Memo { get; set; }

        /// <summary>
        /// 综合费用
        /// 自动计算公式：码头费、代理费、关税、反倾销税、增值税、其他费用总和. 或自定义填入数值.
        /// </summary>
        [Display(Name = "综合费用")]
        public double? Total { get; set; }

        public static void AssignValues(HarborAgentViewModel source, YuShang.ERP.Entities.Orders.HarborAgent target)
        {
            target.AgentCost = source.AgentCost;
            target.AntiDumpingTax = source.AntiDumpingTax;
            target.DeclarationCompanyId = source.DeclarationCompanyId.GetValueOrDefault();
            target.HarborCost = source.HarborCost;
            target.Memo = source.Memo;
            target.OthersCost = source.OthersCost;
            target.Tariff = source.Tariff;
            target.ValueAddedTax = source.ValueAddedTax;
        }

        public static void AssignValues(YuShang.ERP.Entities.Orders.HarborAgent source,
            HarborAgentViewModel target)
        {
            target.AgentCost = source.AgentCost;
            target.AntiDumpingTax = source.AntiDumpingTax;
            target.DeclarationCompanyId = source.DeclarationCompanyId;
            target.HarborCost = source.HarborCost;
            target.Memo = source.Memo;
            target.OthersCost = source.OthersCost;
            target.Tariff = source.Tariff;
            target.ValueAddedTax = source.ValueAddedTax;
            target.Total = source.Total;

            target.DeclarationCompany = new DeclarationCompanyViewModel(source.DeclarationCompany);
        }

        [Display(Name = "报关公司")]
        public DeclarationCompanyViewModel DeclarationCompany { get; set; }
    }

    public class DeclarationCompanyViewModel
    {
        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是没有这一附加项
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        ////[Required]
        public bool? IsEnable
        {
            get;
            set;
        }

        private YuShang.ERP.Entities.ResMgr.DeclarationCompany declarationCompany;

        public DeclarationCompanyViewModel()
        {

        }

        public DeclarationCompanyViewModel(DeclarationCompany declarationCompany)
        {
            this.declarationCompany = declarationCompany;
            if (this.declarationCompany != null)
            {
                this.IsEnable = true;
                this.Address = declarationCompany.Address;
                this.CompanyName = declarationCompany.CompanyName;
                this.DeclarationArea = declarationCompany.DeclarationArea;
                this.DeclarationCode = declarationCompany.DeclarationCode;
                this.DeclarationCompanyId = declarationCompany.DeclarationCompanyId;
                this.Email = declarationCompany.Email;
                this.FAX = declarationCompany.FAX;
                this.MobilePhone = declarationCompany.MobilePhone;
                this.Name = declarationCompany.Name;
                this.QQ_or_WeChat = declarationCompany.QQ_or_WeChat;
                this.Telephone = declarationCompany.Telephone;
            }
        }

        /// <summary>
        /// 自增ID
        /// </summary>
        [Display(Name = "港口代理")]
        public int DeclarationCompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// 报关区域
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "报关区域")]
        public string DeclarationArea
        {
            get;
            set;
        }

        /// <summary>
        /// 报关公司名称
        /// </summary> 
        [MaxLength(200)]
        [Display(Name = "报关公司名称")]
        public string CompanyName
        {
            get;
            set;
        }

        /// <summary>
        /// 公司电话
        /// </summary>
        [Display(Name = "公司电话")]
        [MaxLength(20)]
        public string Telephone
        {
            get;
            set;
        }

        /// <summary>
        /// 报关编号
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "报关编号")]
        public string DeclarationCode
        {
            get;
            set;
        }

        [Display(Name = "联系人")]
        [MaxLength(100)]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "手机号码")]
        [MaxLength(20)]
        public string MobilePhone
        {
            get;
            set;
        }

        [Display(Name = "邮箱")]
        [MaxLength(200)]
        [EmailAddress]
        public string Email
        {
            get;
            set;
        }

        [Display(Name = "QQ/微信")]
        [MaxLength(100)]
        public string QQ_or_WeChat
        {
            get;
            set;
        }

        /// <summary>
        /// 传真
        /// </summary>
        [Display(Name = "传真")]
        [MaxLength(50)]
        public string FAX
        {
            get;
            set;
        }

        [Display(Name = "公司地址")]
        [MaxLength(200)]
        public string Address
        {
            get;
            set;
        }
    }
}
