using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    public class SalesLogistics
    {
        public int SalesLogisticsId
        {
            get;
            set;
        }

        public string SalesContractKey
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

        public string LogisticsVendor
        {
            get;
            set;
        }

        public string LogisticsBillNumber
        {
            get;
            set;
        }
    }
}
