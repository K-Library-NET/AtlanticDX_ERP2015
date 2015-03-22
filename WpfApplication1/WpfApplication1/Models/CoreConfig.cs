using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Models
{
    [Table("Tb_Core_Config")]
    public class CoreConfig
    {
        public int CoreConfigId
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }
    }
}
