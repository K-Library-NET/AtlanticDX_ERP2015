using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Privileges;
using YuShang.ERP.Entities.ResMgr;

namespace YuShang.ERP.Entities.Orders
{
    /// <summary>
    /// 采购订单合同
    /// </summary>
    public class OrderContract
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int OrderContractId
        {
            get;
            set;
        }

        /// <summary>
        /// 订单编号（人工编写，默认全部大写字母加数字）
        /// </summary>
        [Index(IsUnique = true)]
        [MaxLength(100)]
        [Required]
        [Display(Name = "订单编号")]
        public string OrderContractKey
        {
            get;
            set;
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        [Required]
        [Display(Name = "订单状态")]
        [Index]
        public ContractStatus ContractStatus { get; set; }

        /// <summary>
        /// 期货销售、现货销售（0是期货销售，1是现货销售）
        /// </summary>
        [Display(Name = "期货/现货")]
        [Index]
        [Required]
        public int OrderType
        {
            get;
            set;
        }

        /// <summary>
        /// 货品项
        /// </summary>
        public virtual ICollection<ProductItem> OrderProducts
        {
            get;
            set;
        }

        /// <summary>
        /// 订单日期
        /// </summary>
        [Display(Name = "订单日期")]
        [Required]
        public DateTime OrderCreateTime { get; set; }

        /// <summary>
        /// 预计到港时间？
        /// </summary>
        [Display(Name = "ETA")]
        public DateTime? ETA { get; set; }

        /// <summary>
        /// 预计发货时间？
        /// </summary>
        [Display(Name = "ETD")]
        public DateTime? ETD { get; set; }

        /// <summary>
        /// 订单船期（直接手工填写）
        /// </summary>
        [Display(Name = "订单船期")]
        public string ShipmentPeriod { get; set; }

        /// <summary>
        /// 柜号（手工填写）
        /// </summary>
        [Display(Name = "柜号")]
        public string ContainerSerial { get; set; }

        /// <summary>
        /// 提货单号（手工填写）
        /// </summary>
        [Display(Name = "提货单号")]
        public string DeliveryBillSerial { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        //[Display(Name = "目的地")]
        [MaxLength(100)]
        [NotMapped]
        public string DestinationHarborKey
        {
            get
            {
                if (this.Destination != null)
                    return string.Format("{0}/{1}", this.Destination.HarborName
                        , this.Destination.HarborNameENG);

                return string.Empty;
            }
        }

        /// <summary>
        /// 付款方式：默认取供应商已录入付款方式,可手动更改。
        /// 手动修改的付款方式不影响资源管理内信息。
        /// </summary>
        [Display(Name = "付款方式")]
        public string Payment { get; set; }

        /// <summary>
        /// 采购员
        /// </summary>
        [Display(Name = "采购员")]
        [MaxLength(128)]
        public string OrderSysUserKey { get; set; }

        /// <summary>
        /// 采购订金
        /// </summary>
        [Display(Name = "采购订金")]
        [Required]
        public double ImportDeposite
        {
            get;
            set;
        }
         
        //20150409 added: 
        /// <summary>
        /// 币种，默认人民币，但是是通过全局配置的
        /// </summary>
        [Display(Name = "币种")]
        [MaxLength(100)]
        public string Currency
        {
            get;
            set;
        }

        /// <summary>
        /// 汇率，指从当前币种转换到默认币种的比率
        /// </summary>
        public double CurrencyExchangeRate
        {
            get;
            set;
        }
        //20150409 added

        /// <summary>
        /// 采购尾款
        /// </summary>
        [Display(Name = "采购尾款")]
        [Required]
        public double ImportBalancedPayment
        {
            get;
            set;
        }

        [Display(Name = "货款总计")]
        [NotMapped]
        public double PaymentTotal
        {
            get
            {
                if (this.OrderProducts != null)
                {
                    double total = this.OrderProducts.Sum(p => p.SubTotal);
                    return total;
                }

                return 0;
            }
        }

        [Display(Name = "备注")]
        public string Comments
        {
            get;
            set;
        }

        #region navigation Properties

        /// <summary>
        /// 供应商（从资源管理里面选）
        /// </summary>
        [Display(Name = "供应商")]
        [Required]
        public int SupplierId { get; set; }

        /// <summary>
        /// 供应商对象（自动读取）
        /// </summary>
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// 港口对象ID（主键，for Navigation Property）
        /// </summary>
        [Display(Name = "目的港")]
        [Required]
        public int HarborId { get; set; }

        /// <summary>
        /// 目的地，Navigation Property
        /// </summary>
        public virtual Harbor Destination { get; set; }

        /// <summary>
        /// 港口代理ID 
        /// </summary>
        public int? HarborAgentId
        {
            get;
            set;
        }

        /// <summary>
        /// 港口代理
        /// Navigation Property
        /// </summary>
        public virtual HarborAgent HarborAgent
        {
            get;
            set;
        }

        /// <summary>
        /// 香港物流ID
        /// </summary>
        [Display(Name = "香港物流")]
        public int? HKLogisId
        {
            get;
            set;
        }

        /// <summary>
        /// 香港物流
        /// Navigation Property
        /// </summary>
        public virtual HKLogis HongKongLogistics
        {
            get;
            set;
        }

        /// <summary>
        /// 内地物流ID
        /// </summary>
        [Display(Name = "内地物流")]
        public int? MLLogisId
        {
            get;
            set;
        }

        /// <summary>
        /// 内地物流
        /// </summary>
        public virtual MLLogis MainlandLogistics
        {
            get;
            set;
        }

        /// <summary>
        /// used by Navigation Property
        /// </summary> 
        public int? OrderClaimCompensationId
        {
            get;
            set;
        }

        /// <summary>
        /// 采购索赔
        /// Navigation Property
        /// </summary>  
        public virtual OrderClaimCompensation ClaimCompensation
        {
            get;
            set;
        }

        ///// <summary>
        ///// 索赔币种
        ///// </summary>
        //[Display(Name = "索赔币种")]
        //public string CompensationCurrency
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 是否有采购索赔
        /// </summary>
        [Display(Name = "是否有采购索赔")]
        [NotMapped]
        public bool HasOrderCompensations
        {
            get
            {
                if (this.OrderProducts != null && this.OrderProducts.Count > 0)
                {
                    return this.OrderProducts.Any(p => p.OrderClaimCompensationItem != null
                          && p.OrderClaimCompensationItem.CompensationHappenedType != 0);
                }

                return false;
            }
        }

        /// <summary>
        /// 索赔金额（采购货款时发生）
        /// </summary>
        [Display(Name = "索赔总金额")]
        [NotMapped]
        public double Compensation
        {
            get
            {
                //DEBUG 先不管货币
                if (this.HasOrderCompensations)
                //this.OrderProducts != null && this.OrderProducts.Count > 0)
                {
                    double amount = this.OrderProducts.Where(m => m.OrderClaimCompensationItem != null
                        && m.OrderClaimCompensationItem.CompensationHappenedType.GetValueOrDefault() != 0)
                         .Sum(m => m.OrderClaimCompensationItem.Compensation.GetValueOrDefault());
                    return amount;
                }
                return 0;
            }
        }

        ///// <summary>
        ///// 索赔原因（采购货款时发生）
        ///// </summary>
        //[Display(Name = "索赔原因")]
        //public string CompensationReason { get; set; }

        /// <summary>
        /// 采购索赔状态：True,已结清；False,未结清；
        /// </summary>
        [Display(Name = "采购索赔状态")]
        public bool IsCompensationClear
        {
            get
            {
                if (this.ClaimCompensation != null)
                    return this.ClaimCompensation.IsCompensationClear;
                return true;
            }
            set
            {
                if (this.ClaimCompensation != null)
                    this.ClaimCompensation.IsCompensationClear = value;
            }
        }

        /// <summary>
        /// 采购索赔烂账指令：true,取消索赔；False,索赔中
        /// </summary>
        [Display(Name = "采购索赔烂账指令")]
        public bool IsCompensationAbandoned
        {
            get
            {
                if (this.ClaimCompensation != null)
                    return this.ClaimCompensation.IsCompensationAbandoned;
                return true;
            }
            set
            {
                if (this.ClaimCompensation != null)
                    this.ClaimCompensation.IsCompensationAbandoned = value;
            }
        }

        #endregion navigation Properties

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("合同号：{0}\t", this.OrderContractKey);
            builder.AppendFormat("合同状态：{0}\t", this.ContractStatus);
            builder.AppendFormat("ETA：{0}\t", this.ETA);
            builder.AppendFormat("ETD：{0}\t", this.ETD);
            builder.AppendFormat("期货/现货：{0}\t", this.OrderType == 1 ? "现货" : "期货");
            //builder.AppendFormat("合同号：{0}\t", this.OrderContractKey);
            //builder.AppendFormat("合同号：{0}\t", this.OrderContractKey);

            return builder.ToString();
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

        private int m_entityPrivLevRequired = PrivilegeLevelByEntityControlType.DEFAULT_LEVEL_ORDER_CONTRACT;
    }

    public enum ContractStatus
    {
        NotAudited = 0,
        AuditNoPass = 2,
        AuditPassed = 1,
        Closed = -1,
    }

}
