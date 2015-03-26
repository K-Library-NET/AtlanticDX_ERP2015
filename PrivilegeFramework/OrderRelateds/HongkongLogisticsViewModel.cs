using PrivilegeFramework.OrderRelateds;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeFramework
{
    public class HongkongLogisticsViewModel : IContractRelatedViewModel
    {
        private YuShang.ERP.Entities.Orders.HKLogis hklogis;
        public HongkongLogisticsViewModel()
        {

        }

        public HongkongLogisticsViewModel(YuShang.ERP.Entities.Orders.HKLogis hklogis)
        {
            this.hklogis = hklogis;
            if (this.hklogis != null)
            {
                this.IsEnable = true;
                this.HongKongLogisticsCompanyId = hklogis.HongKongLogisticsCompanyId;
                this.LogisItems = new List<LogisticsItem>();
                foreach (var i in hklogis.HongKongLogisticsItems)
                {
                    LogisticsItem item = new LogisticsItem(i as YuShang.ERP.Entities.Orders.LogisticsProductItem);
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
        /// 香港物流公司
        /// </summary>
        [Display(Name = "香港物流公司")]
        public int? HongKongLogisticsCompanyId
        {
            get;
            set;
        }

        public ICollection<LogisticsItem> LogisItems { get; set; }

        /// <summary>
        /// 香港物流ID
        /// </summary>
        [Display(Name = "香港物流")]
        public int? HKLogisId { get; set; }

        internal static void AssignValues(YuShang.ERP.Entities.Orders.HKLogis source,
            HongkongLogisticsViewModel target)
        {
            target.HongKongLogisticsCompanyId = source.HongKongLogisticsCompanyId;
            target.LogisItems = new List<LogisticsItem>();
            if (source.HongKongLogisticsItems != null && source.HongKongLogisticsItems.Count > 0)
            {
                foreach (var it in source.HongKongLogisticsItems)
                {
                    target.LogisItems.Add(new LogisticsItem(it));
                }
            }
        }

        internal static void AssignValues(HongkongLogisticsViewModel source,
            YuShang.ERP.Entities.Orders.HKLogis target)
        {
            target.HongKongLogisticsCompanyId = source.HongKongLogisticsCompanyId.GetValueOrDefault();
            List<YuShang.ERP.Entities.Orders.HKLogisItem> items = new List<YuShang.ERP.Entities.Orders.HKLogisItem>();

            if (source.LogisItems != null && source.LogisItems.Count > 0)
            {
                foreach (var it in source.LogisItems)
                {
                    YuShang.ERP.Entities.Orders.HKLogisItem item = new YuShang.ERP.Entities.Orders.HKLogisItem();
                    LogisticsItem.AssignValues(it, item);
                    items.Add(item);
                    //items.Add(new LogisticsItem(it));
                }
            }
            target.HongKongLogisticsItems = items;
        }
    }
}
