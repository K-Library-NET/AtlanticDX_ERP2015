using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuShang.ERP.Entities.Configs
{
    public class CurrencyConfigs
    {
        public const string ORDER_DEFAULT_CURRENCY_KEY = "OrderContractDefaultCurrency";
        public const string SALE_DEFAULT_CURRENCY_KEY = "SaleContractDefaultCurrency";

        /// <summary>
        /// 采购默认的币种（目前是USD）
        /// </summary>
        public const string ORDER_DEFAULT_CURRENCY_VALUE = "USD";

        /// <summary>
        /// 销售默认的币种（目前是CNY）
        /// </summary>
        public const string SALE_DEFAULT_CURRENCY_VALUE = "CNY";

        public static readonly double DefaultCurrencyExchangeRateUSDtoCNY = 6.2080;

        public static readonly string CurrencyExchangeRate_KEY = "CurrencyExchangeRateUSDtoCNY";

        public static readonly string CurrencyExchangeRate_prefix = "CurrencyExchangeRate";

        public const string FINANCIAL_RECORD_DEFAULT_CURRENCY_VALUE = "CNY";

        public const double ORDER_DEFAULT_CURRENCY_EXCHANGE_RATE = 6.2080;
        public const double SALE_DEFAULT_CURRENCY_EXCHANGE_RATE = 1.0;

        public static double GetDefaultCurrency(string currency, int type)
        {
            if (type == 1)
            {
                if (currency == SALE_DEFAULT_CURRENCY_VALUE)
                    return SALE_DEFAULT_CURRENCY_EXCHANGE_RATE;
            }

            if (currency == ORDER_DEFAULT_CURRENCY_VALUE)
                return ORDER_DEFAULT_CURRENCY_EXCHANGE_RATE;

            //TODO: 暂时还没做汇率动态转换
            return 1.00;
        }
    }
}
