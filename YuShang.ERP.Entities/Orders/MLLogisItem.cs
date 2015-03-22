using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Orders
{
    public class MLLogisItem : LogisticsProductItem
    {
        public int MLLogisItemId
        {
            get;
            set;
        }

        /// <summary>
        /// used by NavigationProperty （MLLogis）
        /// </summary>
        public int MLLogisId
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
        //        var temp = (this.FreightCharges * this.ContractWeight);// -this.Compensation;
        //        return temp;
        //    }
        //}
    }
}
