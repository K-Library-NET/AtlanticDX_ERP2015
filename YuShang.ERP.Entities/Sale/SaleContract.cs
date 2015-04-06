using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.Privileges;
using YuShang.ERP.Entities.Stocks;

namespace YuShang.ERP.Entities.Sale
{
    /// <summary>
    /// 销售订单合同
    /// </summary>
    public class SaleContract
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int SaleContractId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售订单号（人工编写，默认全部大写字母加数字）
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        [Display(Name = "销售订单号")]
        public string SaleContractKey
        {
            get;
            set;
        }

        /// <summary>
        /// 客户编号
        /// </summary>
        //[Required]
        //[MaxLength(100)]
        //[System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        [Display(Name = "销售客户名称")]
        [NotMapped]
        public string SaleClientName
        {
            get
            {
                if (this.SaleClient != null)
                    return this.SaleClient.CompanyName;
                return string.Empty;
            }
        }

        public int SaleClientId
        {
            get;
            set;
        }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual YuShang.ERP.Entities.ResMgr.SaleClient SaleClient
        {
            get;
            set;
        }

        public virtual ICollection<SaleProductItem> SaleProducts
        {
            get;
            set;
        }

        /// <summary>
        /// 销售合同状态：（0，未审核；1，已经审核；……）
        /// </summary>
        [Required]
        [Index(IsUnique = false)]
        public ContractStatus SaleContractStatus
        {
            get;
            set;
        }

        [Display(Name = "销售合同审核通过的还价ID")]
        public int? SelectedSaleBargainId
        {
            get;
            set;
        }

        /// <summary>
        /// 期货销售、现货销售（0是期货销售，1是现货销售）
        /// </summary>
        [Required]
        [Index(IsUnique = false)]
        public int OrderType
        {
            get;
            set;
        }

        [Display(Name = "创建用户")]
        public string OperatorSysUser
        {
            get;
            set;
        }

        //[Display(Name="销售还价")]
        [JsonIgnore]
        public virtual ICollection<SaleBargain> SaleBargins
        {
            get;
            set;
        }

        public int? SaleClaimCompensationId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售索赔
        /// </summary>
        public virtual SaleClaimCompensation ClaimCompensation
        {
            get;
            set;
        }

        /// <summary>
        /// 销售订单创建时间
        /// </summary> 
        [Display(Name = "订单创建时间")]
        public DateTime SaleCreateTime { get; set; }

        /// <summary>
        /// 是否已经结单（对销售合同，采购合同是否结单没关）
        /// </summary>
        public ContractStatus ContractStatus { get; set; }

        /// <summary>
        /// 折扣金额。前端界面可以用折扣率乘以总价得出，存到数据库只存折扣额
        /// </summary>
        [Display(Name = "折扣金额")]
        public double DiscountAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 货款。未减去折扣额。
        /// </summary>
        [Display(Name = "货款")]
        [NotMapped]
        public double Total
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// 货款总计。（货款总计 = 货款 - 折扣金额）
        /// 但从数据上不做约束，用户可以忽略折扣金额字段，直接填货款价格
        /// </summary>
        [Display(Name = "货款总计")]
        public double TotalAfterDiscount
        {
            get;
            set;
        }

        /// <summary>
        /// 销售订金（人工填写）
        /// </summary>
        [Display(Name = "销售订金")]
        public double SaleDeposite
        {
            get;
            set;
        }

        /// <summary>
        /// 销售尾款：货款总计（折扣后）-销售订金
        /// </summary>
        [Display(Name = "销售尾款")]
        public double SaleBalancedPayment
        {
            get
            {
                return this.TotalAfterDiscount - this.SaleDeposite;
            }
        }

        /// <summary>
        /// 用于权限过滤的属性
        /// </summary>
        [Range(0, 99)]
        [Required]
        public int EntityPrivLevRequired
        {
            get { return m_entityPrivLevRequired; }
            set { m_entityPrivLevRequired = value; }
        }

        private int m_entityPrivLevRequired = PrivilegeLevelByEntityControlType.DEFAULT_LEVEL_SALE_CONTRACT;

    }
}
