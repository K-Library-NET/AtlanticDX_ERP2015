using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.ResMgr;

namespace YuShang.ERP.Entities.Orders
{
    /// <summary>
    /// 港口代理
    /// </summary>
    public class HarborAgent
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int HarborAgentId { get; set; }

        /// <summary>
        /// 港口代理名称
        /// </summary>
        [Display(Name = "港口代理名称")]
        [NotMapped]
        public string HarborAgentName
        {
            get
            {
                if (this.DeclarationCompany != null)
                    return this.DeclarationCompany.CompanyName;
                return string.Empty;
            }
        }

        /// <summary>
        /// 报关公司ID（资源管理）
        /// </summary>
        public int DeclarationCompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property 报关公司
        /// </summary>
        public virtual DeclarationCompany DeclarationCompany { get; set; }

        #region old
        ///// <summary>
        ///// 采购合同ID
        ///// used by Navigation Property
        ///// </summary>
        //public int? OrderContractId
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// Navigation Property
        ///// </summary>
        //public virtual OrderContract OrderContract
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 采购订单号
        ///// </summary>
        //[Display(Name = "采购订单号")]
        //[NotMapped]
        //public string OrderContractKey
        //{
        //    get
        //    {
        //        if (this.OrderContract != null)
        //            return this.OrderContract.OrderContractKey;
        //        return string.Empty;
        //    }
        //}

        /*
         *码头费	代理费	关税	    反倾销税	增值税	其他费用	备注	综合费用
          1200	1000	32111	39847	84759	1		    158918 
         */
        #endregion

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
    }
}
