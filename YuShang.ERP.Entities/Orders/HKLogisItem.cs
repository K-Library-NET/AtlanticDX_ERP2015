using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Orders
{
    public class HKLogisItem : LogisticsProductItem
    {
        public int HKLogisItemId
        {
            get;
            set;
        }

        /// <summary> 
        /// used by Navigation Property
        /// </summary>
        public int HKLogisId
        {
            get;
            set;
        }

        ///// <summary>
        ///// 运费小计
        ///// </summary>
        //[Display(Name = "运费小计")]
        //public override double SubTotal
        //{
        //    get
        //    {
        //        var temp = (this.FreightCharges * this.ContractWeight);// - this.Compensation;
        //        return temp;
        //    }
        //}
    }
}
