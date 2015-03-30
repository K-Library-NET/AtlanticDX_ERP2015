using PrivilegeFramework.OrderRelateds;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuShang.ERP.Entities.Orders;
using YuShang.ERP.Entities.ResMgr;

namespace PrivilegeFramework
{
    public class MainlandLogisticsViewModel : IContractRelatedViewModel
    {
        private MLLogis mllogis;

        public MainlandLogisticsViewModel()
        {

        }

        public MainlandLogisticsViewModel(MLLogis mllogis)
        {
            this.mllogis = mllogis;
            if (this.mllogis != null)
            {
                this.IsEnable = true;
                this.MainlandLogisticsCompanyId = mllogis.MainlandLogisticsCompanyId;
                this.LogisItems = new List<LogisticsItem>();
                foreach (var i in mllogis.MainlandLogisticsItems)
                {
                    LogisticsItem item = new LogisticsItem(i as LogisticsProductItem);
                    this.LogisItems.Add(item);
                }
            }
        }

        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是没有这一附加项
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        //[Required]
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
        /// 内地物流公司
        /// </summary>
        [Display(Name = "内地物流公司")]
        public int? MainlandLogisticsCompanyId
        {
            get;
            set;
        }

        public ICollection<LogisticsItem> LogisItems { get; set; }

        /// <summary>
        /// 内地物流ID
        /// </summary>
        [Display(Name = "内地物流")]
        public int? MLLogisId { get; set; }

        internal static void AssignValues(MLLogis source,
            MainlandLogisticsViewModel target)
        {
            target.MainlandLogisticsCompanyId = source.MainlandLogisticsCompanyId;
            target.MLLogisCompany = new MLLogisCompanyViewModel(source.MLLogisCompany);
            target.LogisItems = new List<LogisticsItem>();
            if (source.MainlandLogisticsItems != null && source.MainlandLogisticsItems.Count > 0)
            {
                foreach (var it in source.MainlandLogisticsItems)
                {
                    target.LogisItems.Add(new LogisticsItem(it));
                }
            }
        }

        /// <summary>
        /// 物流公司
        /// </summary>
        [Display(Name = "物流公司")]
        public MLLogisCompanyViewModel MLLogisCompany { get; set; }

        internal static void AssignValues(MainlandLogisticsViewModel source, MLLogis target)
        {
            target.MainlandLogisticsCompanyId = source.MainlandLogisticsCompanyId.GetValueOrDefault();
            List<YuShang.ERP.Entities.Orders.MLLogisItem> items = new List<YuShang.ERP.Entities.Orders.MLLogisItem>();

            if (source.LogisItems != null && source.LogisItems.Count > 0)
            {
                foreach (var it in source.LogisItems)
                {
                    YuShang.ERP.Entities.Orders.MLLogisItem item = new YuShang.ERP.Entities.Orders.MLLogisItem();
                    LogisticsItem.AssignValues(it, item);
                    items.Add(item);
                    //items.Add(new LogisticsItem(it));
                }
            }
            target.MainlandLogisticsItems = items;
        }
    }

    public class MLLogisCompanyViewModel
    {
        private YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany mainlandLogisticsCompany;

        public MLLogisCompanyViewModel()
        { }

        public MLLogisCompanyViewModel(YuShang.ERP.Entities.ResMgr.MainlandLogisticsCompany mainlandLogisticsCompany)
        {
            this.mainlandLogisticsCompany = mainlandLogisticsCompany;
            if (mainlandLogisticsCompany != null)
                this.IsEnable = true;
            AssignValues(mainlandLogisticsCompany, this);
        }

        internal static void AssignValues(MainlandLogisticsCompany source,
            MLLogisCompanyViewModel target)
        {
            target.Address = source.Address;
            target.CompanyName = source.CompanyName;
            target.Email = source.Email;
            target.FAX = source.FAX;
            target.LogisCompanyCode = source.LogisCompanyCode;
            target.MainlandLogisticsCompanyId = source.MainlandLogisticsCompanyId;
            target.MobilePhone = source.MobilePhone;
            target.Name = source.Name;
            target.QQ_or_WeChat = source.QQ_or_WeChat;
            target.Telephone = source.Telephone;
        }

        public bool? IsEnable
        {
            get;
            set;
        }


        /// <summary>
        /// 自增ID
        /// </summary>
        [Display(Name = "内地物流")]
        public int? MainlandLogisticsCompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// 物流公司编号
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "物流公司编号")]
        public string LogisCompanyCode
        {
            get;
            set;
        }

        [MaxLength(200)]
        [Display(Name = "公司名称")]
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
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        [MaxLength(100)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 手机号码
        /// </summary>
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

        /// <summary>
        /// 传真
        /// </summary>
        [Display(Name = "传真")]
        [MaxLength(100)]
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

        [NotMapped]
        public string QQ_or_WeChat
        {
            get;
            set;
        }
    }

    public class LogisticsItem
    {
        private YuShang.ERP.Entities.Orders.LogisticsProductItem item;
        public LogisticsItem()
        {

        }

        public LogisticsItem(YuShang.ERP.Entities.Orders.LogisticsProductItem item)
        {
            this.item = item;
            if (this.item != null)
            {
                this.IsEnable = true;
                AssignValues(this.item, this);
            }
        }

        public static void AssignValues(LogisticsProductItem
             source, LogisticsItem target)
        {
            target.ContractQuantity = source.ContractQuantity;
            target.ContractWeight = source.ContractWeight;
            target.FreightCharges = source.FreightCharges;
            target.Insurance = source.Insurance;
            target.ProductItemId = source.ProductItemId;
            target.ReceivingQuantity = source.ReceivingQuantity;
            target.ReceivingTime = source.ReceivingTime;
            target.ReceivingWeight = source.ReceivingWeight;
        }

        public static void AssignValues(
            LogisticsItem source, LogisticsProductItem target)
        {
            target.ContractQuantity = source.ContractQuantity;
            target.ContractWeight = source.ContractWeight;
            target.FreightCharges = source.FreightCharges;
            target.Insurance = source.Insurance;
            target.ProductItemId = source.ProductItemId.GetValueOrDefault();
            target.ReceivingQuantity = source.ReceivingQuantity;
            target.ReceivingTime = source.ReceivingTime;
            target.ReceivingWeight = source.ReceivingWeight;
        }

        /// <summary>
        /// 是否处于有效状态：
        /// 如果IsEnable==false，那么就是没有这一附加项
        /// </summary>
        [Display(Name = "是否处于有效状态")]
        //[Required]
        public bool? IsEnable
        {
            get;
            set;
        }

        /// <summary>
        /// 采购商品项ID
        /// used by Navigation Property
        /// </summary>
        public int? ProductItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 收单件数
        /// </summary>
        [Display(Name = "收单件数")]
        public double? ContractQuantity { get; set; }

        /// <summary>
        /// 收单吨重
        /// </summary>
        [Display(Name = "收单吨重")]
        public double? ContractWeight { get; set; }

        /// <summary>
        /// 运费/吨
        /// </summary>
        [Display(Name = "运费/吨")]
        public double? FreightCharges { get; set; }

        /// <summary>
        /// 保险
        /// </summary>
        [Display(Name = "保险")]
        public double? Insurance { get; set; }

        /// <summary>
        /// 收货日期
        /// </summary>
        [Display(Name = "收货日期")]
        public DateTime? ReceivingTime { get; set; }

        /// <summary>
        /// 收货件数
        /// </summary>
        [Display(Name = "收货件数")]
        public double? ReceivingQuantity { get; set; }

        /// <summary>
        /// 收货吨重
        /// </summary>
        [Display(Name = "收货吨重")]
        public double? ReceivingWeight { get; set; }

        /// <summary>
        /// 运费小计
        /// </summary>
        [NotMapped]
        [Display(Name = "运费小计")]
        public virtual double SubTotal
        {
            get
            {
                var temp = (this.FreightCharges.HasValue ? this.FreightCharges.Value : 0)
                    * (this.ContractWeight.HasValue ? this.ContractWeight.Value : 0);// -this.Compensation;
                return temp;
            }
        }
    }
}
