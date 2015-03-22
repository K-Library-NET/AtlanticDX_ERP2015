using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Configs
{
    public class OperationLog
    {
        public int OperationLogId
        {
            get;
            set;
        }

        /// <summary>
        /// 操作名
        /// </summary>
        public string OperationName
        {
            get;
            set;
        }

        /// <summary>
        /// 操作用户名
        /// </summary>
        [MaxLength(100)]
        public string SysUserId
        {
            get;
            set;
        }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 记录产生时间
        /// </summary>
        public DateTime CTIME
        {
            get;
            set;
        }
    }
}
