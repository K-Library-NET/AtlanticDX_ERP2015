using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Configs
{
    public class CoreConfig
    {
        [Required]
        [Display(Name = "配置项类型")]
        public string ConfigTypeKey
        {
            get;
            set;
        }

        public int CoreConfigId
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "配置项Key")]
        public string ConfigKey
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "配置项名称")]
        public string ConfigName
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "配置项值")]
        public string ConfigValue
        {
            get;
            set;
        }
    }
}
