using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Models
{
    [Table("Tb_Organizations")]
    public class Organization
    {
        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrganizationId
        {
            get;
            set;
        }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrganizationName
        {
            get;
            set;
        }

        /// <summary>
        /// 组织描述
        /// </summary>
        public string OrganizationDescription
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }
    }
}
