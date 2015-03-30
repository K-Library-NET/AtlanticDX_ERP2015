using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 港口代理（资源管理对象）
    /// </summary>
    public class HarborAgent
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int HarborAgentId { get; set; }

        /// <summary>
        /// 港口代理编号
        /// </summary>
        public string HarborAgentKey { get; set; }

        /// <summary>
        /// 港口代理名称
        /// </summary>
        public string HarborAgentName { get; set; }

        /// <summary>
        /// 描述（预留字段）
        /// </summary>
        public string Description { get; set; }
    }
}
