using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo.Entities
{
    /// <summary>
    /// 货币、汇率
    /// </summary>
    public class Currency
    {
        private string m_currencyCode = "CNY";

        public string CurrencyCode
        {
            get
            {
                return m_currencyCode;
            }
            set
            {
                m_currencyCode = value;
            }
        }

        private double m_rateToCNY = 1.00;

        public double RateToCNY
        {
            get { return m_rateToCNY; }
            set { m_rateToCNY = value; }
        }
    }
}
