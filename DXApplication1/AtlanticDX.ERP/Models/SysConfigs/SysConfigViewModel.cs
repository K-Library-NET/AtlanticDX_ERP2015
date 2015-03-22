using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtlanticDX.ERP.Models.SysConfigs
{
    public class SysConfigViewModel : INotifyPropertyChanged
    {
        public SysConfigViewModel(CoreConfigConverter converter)
        {
            this.m_converter = converter;
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public int CoreConfigId
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [Required]
        [Display(Name = "配置项类型")]
        public string ConfigTypeStr
        {
            get { return this.m_converter.GetConfigTypeStr(this.ConfigTypeKey); }
            set
            {
                //this.ConfigTypeKey = value;
            }
        }

        [Required]
        [Display(Name = "配置项类型")]
        public string ConfigTypeKey
        {
            get;
            set;
        }


        [Required]
        [Display(Name = "配置项键值")]
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

        [Display(Name = "配置值")]
        public string ConfigValue
        {
            get;
            set;
        }

        internal CoreConfigConverter m_converter { get; set; }
    }

    public class CoreConfigConverter
    {
        Dictionary<string, string> m_displayDic = new Dictionary<string, string>();

        public CoreConfigConverter(IEnumerable<YuShang.ERP.Entities.Configs.ConfigType> types)
        {
            if (types != null)
            {
                foreach (var i in types)
                {
                    if (!m_displayDic.ContainsKey(i.ConfigTypeKey))
                        m_displayDic.Add(i.ConfigTypeKey, i.ConfigTypeValue);
                }
            }
        }

        public SysConfigViewModel FromConfig(YuShang.ERP.Entities.Configs.CoreConfig config)
        {
            if (config != null)
            {
                SysConfigViewModel model = new SysConfigViewModel(this)
                {
                    ConfigKey = config.ConfigKey,
                    ConfigName = config.ConfigName,
                    ConfigValue = config.ConfigValue,
                    ConfigTypeKey = config.ConfigTypeKey,
                    CoreConfigId = config.CoreConfigId,
                };
                return model;
            }
            return null;
        }

        public YuShang.ERP.Entities.Configs.CoreConfig ToConfig(SysConfigViewModel model)
        {
            return new YuShang.ERP.Entities.Configs.CoreConfig()
            {
                ConfigKey = model.ConfigKey,
                ConfigName = model.ConfigName,
                ConfigValue = model.ConfigValue,
                ConfigTypeKey = model.ConfigTypeKey,
                CoreConfigId = model.CoreConfigId,
            };
        }

        public string GetConfigTypeStr(string p)
        {
            if (m_displayDic.ContainsKey(p))
                return m_displayDic[p];

            return string.Empty;
        }
    }
}