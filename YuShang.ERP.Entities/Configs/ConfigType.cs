using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Configs
{
    public class ConfigType
    {
        public string ConfigTypeKey
        {
            get;
            set;
        }

        public string ConfigTypeValue
        {
            get;
            set;
        }

        public static IEnumerable<ConfigType> ConfigTypes
        {
            get
            {
                return new ConfigType[]{
                    new ConfigType{ ConfigTypeKey = "system", ConfigTypeValue = "系统内置配置"},
                     new ConfigType{ ConfigTypeKey = "user", ConfigTypeValue = "用户自定义配置"}
                };
            }
        }
    }
}
