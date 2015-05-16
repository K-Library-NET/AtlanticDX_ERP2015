using System.ComponentModel.DataAnnotations;
using YuShang.ERP.Entities.Configs;
using YuShang.ERP.Entities.Sale;

namespace AtlanticDX.Model.Areas.Sales.Models
{
    public class SaleProductItemBargainItemViewModel
    {
        private SaleBargainItem m_item;

        public SaleProductItemBargainItemViewModel(SaleBargainItem item)
        {
            this.m_item = item;
        }

        [Display(Name = "币种")]
        public string Currency
        {
            get
            {
                if (this.m_item != null && this.m_item.SaleProductItem != null
                    && m_item.SaleProductItem.SaleContract != null)
                {
                    return m_item.SaleProductItem.SaleContract.Currency;
                }

                return CurrencyConfigs.SALE_DEFAULT_CURRENCY_VALUE;
            }
        }

        [Display(Name = "汇率")]
        public double CurrencyExchangeRate
        {
            get
            {
                if (this.m_item != null && this.m_item.SaleProductItem != null
                    && m_item.SaleProductItem.SaleContract != null)
                {
                    return m_item.SaleProductItem.SaleContract.CurrencyExchangeRate;
                }

                return CurrencyConfigs.SALE_DEFAULT_CURRENCY_EXCHANGE_RATE; 
            }
        }

        public int SalesmanId
        {
            get
            {
                if (this.m_item != null)
                    return this.m_item.SalesmanId;

                return -1;
            }
        }

        /// <summary>
        /// 还价单价
        /// </summary>
        [Display(Name = "还价单价")]
        public double BargainUnitPrice
        {
            get
            {
                if (this.m_item != null)
                    return this.m_item.BargainUnitPrice;
                return 0;
            }
        }
    }
}