using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 港口、目的地
    /// </summary>
    public class Harbor
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int HarborId
        {
            get;
            set;
        }

        /// <summary>
        /// 港口目的地编号
        /// </summary>
        public string HarborKey
        {
            get;
            set;
        }

        /// <summary>
        /// 港口名称
        /// </summary>
        public string HarborName
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}
