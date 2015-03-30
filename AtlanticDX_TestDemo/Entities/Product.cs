using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{ 
    /// <summary>
    /// 货品种类（资源管理内设定）
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 货品编号
        /// </summary>
        public string ProductKey
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 国家/厂号/货品名/品牌
        /// </summary>
        public string ProductFullName
        {
            get
            {
                return string.Format("{0} {1} {2} {3}",
                    this.MadeInCountry, this.MadeInFactory,
                    this.ProductName, this.Brand);
            }
        }

        /// <summary>
        /// 国家
        /// </summary>
        public string MadeInCountry { get; set; }

        /// <summary>
        /// 厂号
        /// </summary>
        public string MadeInFactory { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specification
        {
            get;
            set;
        }

        /// <summary>
        /// 包装
        /// </summary>
        public string Packing { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }
}
