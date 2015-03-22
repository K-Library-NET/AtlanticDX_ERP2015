using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Models
{
    [Table("Tb_Core_Account")]
    public class CoreAccount
    {
        public int CoreAccountId
        {
            get;
            set;
        }

        public int CompanyId
        {
            get;
            set;
        }

        public string Nickname
        {
            get;
            set;
        }

        public string Account
        {
            get;
            set;
        }

        public string Password
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
