using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 供应商（资源管理）
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public string VendorId
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string VendorKey
        {
            get;
            set;
        }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendorName
        {
            get;
            set;
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string VendorPayment
        {
            get;
            set;
        }

        /// <summary>
        /// 描述（预留字段）
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}
