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
    /// 香港物流
    /// </summary>
    public class HKLogis
    {
        public int HKLogisId
        {
            get;
            set;
        }

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
        #endregion

        public int HongKongLogisticsCompanyId
        {
            get;
            set;
        }

        public virtual HongkongLogisticsCompany HKLogisCompany
        {
            get;
            set;
        }

        /// <summary>
        /// 香港物流的每一件货
        /// </summary>
        public virtual ICollection<HKLogisItem> HongKongLogisticsItems
        {
            get;
            set;
        }

        [NotMapped]
        public double Total
        {
            get
            {
                if (this.HongKongLogisticsItems != null
                    && this.HongKongLogisticsItems.Count() > 0)
                {
                    return this.HongKongLogisticsItems.Sum(m => m.SubTotal);
                }

                return 0;
            }
        }

        /// <summary>
        /// 确认/同意支付货款
        /// </summary>
        [Display(Name = "确认/同意支付货款")]
        public bool? CommitToPayCost { get; set; }
    }
}
