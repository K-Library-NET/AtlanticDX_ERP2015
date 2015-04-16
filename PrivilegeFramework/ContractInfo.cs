using PrivilegeFramework.OrderRelateds;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Configs;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.Sale;

namespace PrivilegeFramework
{
    /// <summary>
    /// 20150127 采购+销售合同的ViewModel，字段不一样，但是需要做适配
    /// </summary>
    public class ContractInfo
    {
        private OrderContract orderContract;
        private YuShang.ERP.Entities.Sale.SaleContract saleContract;

        public ContractInfo()
        {

        }

        public ContractInfo(OrderContract orderContract)
        {
            this.orderContract = orderContract;
            if (orderContract != null)
            {
                this.IsEnable = true;
                AssignValues(orderContract, this);
            }
        }

        public ContractInfo(YuShang.ERP.Entities.Sale.SaleContract saleContract)
        {
            this.saleContract = saleContract;
            if (saleContract != null)
            {
                this.IsEnable = true;
                AssignValues(saleContract, this);
            }
        }

        internal void InitMlLogistics()
        {
            if (this.orderContract != null)
            {
                this.MainlandLogistics = new MainlandLogisticsViewModel(this.orderContract.MainlandLogistics);
                this.MainlandLogistics.MLLogisId = this.orderContract.MLLogisId;
            }
            else
                this.MainlandLogistics = new MainlandLogisticsViewModel() { IsEnable = false };
            //if (this.orderContract.MLLogisId.GetValueOrDefault() > 0
            //    && this.orderContract.MainlandLogistics != null)
            //{
            //    MainlandLogisticsViewModel.AssignValues(this.orderContract.MainlandLogistics, 
            //        this.MainlandLogistics);
            //}
            //else
            //{
            //}
        }

        internal void InitHkLogistics()
        {
            if (this.orderContract.HongKongLogistics != null)
            {
                this.HongkongLogistics = new HongkongLogisticsViewModel(this.orderContract.HongKongLogistics);
                this.HongkongLogistics.HKLogisId = this.orderContract.HKLogisId;
            }
            else
                this.HongkongLogistics = new HongkongLogisticsViewModel() { IsEnable = false };
            //if (this.orderContract.HKLogisId.GetValueOrDefault() > 0
            //    && this.orderContract.HongKongLogistics != null)
            //{
            //    HongkongLogisticsViewModel.AssignValues(this.orderContract.HongKongLogistics, 
            //        this.HongkongLogistics);
            //}
            //else
            //{
            //}
        }

        internal void InitHarborAgent()
        {
            if (this.orderContract != null)
            {
                this.HarborAgent = new HarborAgentViewModel(this.orderContract.HarborAgent);
                this.HarborAgent.HarborAgentId =
                    this.orderContract.HarborAgentId;
            }
            else
                this.HarborAgent = new HarborAgentViewModel() { IsEnable = false };
            //if (this.orderContract.HarborAgentId.GetValueOrDefault() > 0
            //    && this.orderContract.HarborAgent != null)
            //{
            //    this.HarborAgent.IsEnable = true;
            //    HarborAgentViewModel.AssignValues(
            //        this.orderContract.HarborAgent, this.HarborAgent);
            //}
            //else
            //{ 
            //}
        }

        internal void InitOrderItems()
        {
            this.ContractItems = new List<ProductItemInfo>();
            if (this.orderContract != null && this.orderContract.OrderProducts != null
                && this.orderContract.OrderProducts.Count > 0)
            {
                foreach (var it in this.orderContract.OrderProducts)
                {
                    this.ContractItems.Add(new ProductItemInfo(it) { Currency = this.orderContract.Currency });
                }
            }
        }

        internal void InitSaleItems()
        {
            this.SaleProductItems = new List<SaleProductItemInfo>();
            this.ContractItems = new List<ProductItemInfo>();
            if (this.saleContract != null && this.saleContract.SaleProducts != null
                && this.saleContract.SaleProducts.Count > 0)
            {
                foreach (var it in this.saleContract.SaleProducts)
                {
                    this.SaleProductItems.Add(new SaleProductItemInfo(it));
                    this.ContractItems.Add(new ProductItemInfo(it.OrderProductItem));
                }
            }
        }

        internal void InitSaleClient()
        {
            this.SaleClient = new SaleClientInfo(this.saleContract.SaleClient);
        }

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

        /// <summary>
        /// 合同编号
        /// </summary>
        [Required]
        [Display(Name = "合同编号")]
        public string ContractKey
        {
            get;
            set;
        }

        /// <summary>
        /// 合同类型
        /// </summary>
        //[Required]
        [Display(Name = "合同类型")]
        public ContractViewModelType ContractType
        {
            get;
            set;
        }

        /// <summary>
        /// 采购或销售合同的ID
        /// </summary>
        //[Required]
        public int? ContractId
        {
            get;
            set;
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        //[Required]
        [Display(Name = "订单状态")]
        public ContractStatus ContractStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 期货销售、现货销售（0是期货销售，1是现货销售）
        /// </summary>
        [Display(Name = "期货/现货")]
        //[Required]
        public int? OrderType
        {
            get;
            set;
        }

        /// <summary>
        /// 订单日期
        /// </summary>
        [Display(Name = "订单日期")]
        ////[Required]
        public DateTime? CTIME { get; set; }

        /// <summary>
        /// 合同中的货品项（不管是否销售订单都会有）
        /// </summary>
        public ICollection<ProductItemInfo> ContractItems
        {
            get;
            set;
        }

        /// <summary>
        /// 销售货品项（仅是销售订单才会有，与合同中的货品项一条一条对应）
        /// </summary>
        public ICollection<SaleProductItemInfo> SaleProductItems
        {
            get;
            set;
        }

        #region related items

        /// <summary>
        /// 港口代理（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        [Display(Name = "港口代理")]
        public HarborAgentViewModel HarborAgent
        {
            get;
            set;
        }

        /// <summary>
        /// 香港物流（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        [Display(Name = "香港物流")]
        public HongkongLogisticsViewModel HongkongLogistics
        {
            get;
            set;
        }

        /// <summary>
        /// 内地物流（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        [Display(Name = "内地物流")]
        public MainlandLogisticsViewModel MainlandLogistics
        {
            get;
            set;
        }

        /// <summary>
        /// 库存信息（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        [Display(Name = "库存信息")]
        public StockViewModel ProductStock
        {
            get;
            set;
        }

        /// <summary>
        /// 采购索赔（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        [Display(Name = "采购索赔")]
        public OrderCompensationViewModel OrderCompensation
        {
            get;
            set;
        }

        /// <summary>
        /// 销售索赔（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        [Display(Name = "销售索赔")]
        public SaleCompensationViewModel SaleCompensation
        {
            get;
            set;
        }

        /// <summary>
        /// 采购财务信息（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        [Display(Name = "采购财务信息")]
        public OrderFinancialsViewModel OrderFinancials
        {
            get;
            set;
        }

        /// <summary>
        /// 销售财务信息（不是必须，当IsEnable =true的时候才相当于此项生效）
        /// </summary>
        [Display(Name = "销售财务信息")]
        public SaleFinancialsViewModel SaleFinancials
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 港口对象ID（主键，for Navigation Property）
        /// </summary>
        [Display(Name = "目的港")]
        public int? HarborId
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商（从资源管理里面选）
        /// </summary>
        [Display(Name = "供应商")]
        public int? SupplierId
        {
            get;
            set;
        }

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
        /// NotMapped
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreateSysUserKey { get; set; }

        /// <summary>
        /// 付款方式：默认取供应商已录入付款方式,可手动更改。
        /// 手动修改的付款方式不影响资源管理内信息。
        /// </summary>
        [Display(Name = "付款方式")]
        public string Payment { get; set; }

        [Display(Name = "货款总计")]
        public double? PaymentTotal { get; set; }

        /// <summary>
        /// NotMapped
        /// 数据权限级别
        /// </summary>
        public int? EntityPrivLevRequired { get; set; }


        [Display(Name = "备注")]
        public string Comments
        {
            get;
            set;
        }


        /// <summary>
        /// 采购订金
        /// </summary>
        [Display(Name = "采购订金")]
        ////[Required]
        public double? ImportDeposite
        {
            get;
            set;
        }

        /// <summary>
        /// 采购尾款
        /// </summary>
        [Display(Name = "采购尾款")]
        ////[Required]
        public double? ImportBalancedPayment
        {
            get;
            set;
        }

        /// <summary>
        /// 销售尾款：货款总计（折扣后）-销售订金
        /// </summary>
        [Display(Name = "销售尾款")]
        public double? SaleBalancedPayment { get; set; }

        /// <summary>
        /// 销售订金（人工填写）
        /// </summary>
        [Display(Name = "销售订金")]
        public double? SaleDeposite { get; set; }

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
        [Display(Name = "汇率")]
        public double? CurrencyExchangeRate
        {
            get;
            set;
        }
        //20150409 added

        /// <summary>
        /// 折扣金额。前端界面可以用折扣率乘以总价得出，存到数据库只存折扣额
        /// </summary>
        [Display(Name = "折扣金额")]
        public double DiscountAmount
        {
            get;
            set;
        }

        public int? SaleClaimCompensationId
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

        [Display(Name = "货款总计")]
        public double? TotalAfterDiscount
        {
            get;
            set;
        }

        public static void AssignValues(SaleContract source, ContractInfo target)
        {
            target.SaleClientId = source.SaleClientId;
            target.DiscountAmount = source.DiscountAmount;
            target.ContractKey = source.SaleContractKey;
            target.ContractId = source.SaleContractId; //增加id
            target.OrderType = source.OrderType;
            target.CTIME = source.SaleCreateTime;
            target.ContractType = ContractViewModelType.SaleContract;
            target.ContractStatus = source.ContractStatus;
            target.EntityPrivLevRequired = source.EntityPrivLevRequired;
            target.SaleBalancedPayment = source.SaleBalancedPayment;
            target.SaleDeposite = source.SaleDeposite;
            target.TotalAfterDiscount = source.TotalAfterDiscount;
            target.CreateSysUserKey = source.OperatorSysUser;

            target.Currency = string.IsNullOrEmpty(source.Currency) ?
                CurrencyConfigs.SALE_DEFAULT_CURRENCY_VALUE : source.Currency;
            target.CurrencyExchangeRate = source.CurrencyExchangeRate > 0 ?
                source.CurrencyExchangeRate : CurrencyConfigs.GetDefaultCurrency(
                target.Currency, (int)target.ContractType);

            target.InitSaleClient();
            target.InitSaleItems();
        }

        public static void AssignValues(ContractInfo source, SaleContract target)
        {
            target.SaleClientId = Convert.ToInt32(source.SaleClientId); //客户信息
            target.SaleContractKey = source.ContractKey;
            target.SaleContractId = source.ContractId.GetValueOrDefault();
            target.OrderType = source.OrderType.GetValueOrDefault();
            target.SaleCreateTime = source.CTIME.GetValueOrDefault();
            //target.ContractType = ContractViewModelType.SaleContract;
            target.ContractStatus = source.ContractStatus;
            target.EntityPrivLevRequired = source.EntityPrivLevRequired.GetValueOrDefault();
            //target.SaleBalancedPayment = source.SaleBalancedPayment.GetValueOrDefault();
            target.SaleDeposite = source.SaleDeposite.GetValueOrDefault();
            target.OperatorSysUser = source.CreateSysUserKey;

            target.CurrencyExchangeRate = source.CurrencyExchangeRate.HasValue ?
                source.CurrencyExchangeRate.Value : CurrencyConfigs.GetDefaultCurrency(
                source.Currency, (int)source.ContractType);
            target.Currency = source.Currency;
        }

        public static void AssignValues(OrderContract source, ContractInfo target)
        {
            target.ContractType = ContractViewModelType.OrderContract;
            target.ContractKey = source.OrderContractKey;
            target.ContractId = source.OrderContractId;
            target.CTIME = source.OrderCreateTime;
            target.ContractStatus = source.ContractStatus;
            target.Comments = source.Comments;
            target.ContractId = source.OrderContractId;
            target.ContainerSerial = source.ContainerSerial;
            target.CreateSysUserKey = source.OrderSysUserKey;
            target.DeliveryBillSerial = source.DeliveryBillSerial;
            target.EntityPrivLevRequired = source.EntityPrivLevRequired;
            target.ETA = source.ETA;
            target.ETD = source.ETD;
            target.HarborId = source.HarborId;
            target.SupplierId = source.SupplierId;
            target.OrderType = source.OrderType;
            target.ImportBalancedPayment = source.ImportBalancedPayment;
            target.ImportDeposite = source.ImportDeposite;
            target.Payment = source.Payment;
            target.PaymentTotal = source.PaymentTotal;
            target.ShipmentPeriod = source.ShipmentPeriod;

            target.Currency = string.IsNullOrEmpty(source.Currency) ?
                CurrencyConfigs.ORDER_DEFAULT_CURRENCY_VALUE : source.Currency; 
            target.CurrencyExchangeRate = source.CurrencyExchangeRate > 0 ?
                source.CurrencyExchangeRate : CurrencyConfigs.GetDefaultCurrency(
                target.Currency, (int)target.ContractType);

            target.InitOrderItems();
            target.InitHarborAgent();
            target.InitHkLogistics();
            target.InitMlLogistics();
        }

        public static void AssignValues(ContractInfo source, OrderContract target)
        {
            target.OrderContractKey = source.ContractKey;
            target.OrderContractId = source.ContractId.GetValueOrDefault();
            target.OrderSysUserKey = source.CreateSysUserKey;
            target.OrderType = source.OrderType.GetValueOrDefault();
            target.Payment = source.Payment;
            target.ShipmentPeriod = source.ShipmentPeriod;
            target.SupplierId = source.SupplierId.GetValueOrDefault();
            target.HarborId = source.HarborId.GetValueOrDefault();
            target.OrderCreateTime = source.CTIME.GetValueOrDefault();
            target.ImportDeposite = source.ImportDeposite.GetValueOrDefault();
            target.ImportBalancedPayment = source.ImportBalancedPayment.GetValueOrDefault();
            target.ETD = source.ETD;
            target.ETA = source.ETA;
            target.DeliveryBillSerial = source.DeliveryBillSerial;
            target.ContractStatus = source.ContractStatus;
            target.ContainerSerial = source.ContainerSerial;
            target.ContractStatus = source.ContractStatus;
            target.Comments = source.Comments;

            target.CurrencyExchangeRate = source.CurrencyExchangeRate.HasValue ?
                source.CurrencyExchangeRate.Value : CurrencyConfigs.GetDefaultCurrency(
                source.Currency, (int)source.ContractType);
            target.Currency = source.Currency;
            //target.ContractId = source.ContractId;
        }

        public int? SaleClientId { get; set; }

        public SaleClientInfo SaleClient { get; set; }

        [Display(Name = "订单日期")]
        public string CTIME_STR
        {
            get
            {
                if (CTIME.HasValue)
                {
                    return CTIME.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }

        [Display(Name = "ETA")]
        public string ETA_STR
        {
            get
            {
                if (ETA.HasValue)
                {
                    return ETA.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }

        [Display(Name = "ETD")]
        public string ETD_STR
        {
            get
            {
                if (ETD.HasValue)
                {
                    return ETD.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }

        [Display(Name = "目的港")]
        public string DestinationHarborKey
        {
            get
            {
                if (this.HarborId.HasValue && this.orderContract != null
                    && this.orderContract.Destination != null)
                {
                    return this.orderContract.DestinationHarborKey;
                }
                return string.Empty;
            }
        }

        [Display(Name = "操作人")]
        public string OperatorPersonName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.CreateSysUserKey))
                {//TODO： 改成姓名展示
                    return this.CreateSysUserKey;
                }
                return string.Empty;
            }
        }
    }

    public enum ContractViewModelType
    {
        OrderContract = 0,
        SaleContract = 1,
    }

}
