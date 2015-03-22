using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
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
        public string SaleContractKey
        {
            get;
            set;
        }

        public IEnumerable<ProductItem> SaleProducts
        {
            get;
            set;
        }

        /// <summary>
        /// 销售合同是否已经审核（0，未审核；1，已经审核）
        /// </summary>
        public int SaleContractAuthorizeStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 期货销售、现货销售（0是期货销售，1是现货销售）
        /// </summary>
        public int OrderType
        {
            get;
            set;
        }
    }
}
