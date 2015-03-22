﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Models
{
    [Table("Tb_Core_Company")]
    public class CoreCompany
    {
        public int CoreCompanyId
        {
            get;
            set;
        }

        public int ParentCoreCompanyId
        {
            get;
            set;
        }

        public string Name
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
