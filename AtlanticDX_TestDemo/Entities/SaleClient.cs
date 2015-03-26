using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 销售客户（资源管理）
    /// </summary>
    public class SaleClient
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int SaleClientId
        {
            get;
            set;
        }

        /// <summary>
        /// 销售客户编号
        /// </summary>
        public string SaleClientKey { get; set; }
    }
}
