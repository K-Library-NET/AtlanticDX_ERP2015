using AtlanticDX_TestDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlanticDX_TestDemo
{
    public class ProductEntities
    {
        protected ProductEntities()
        {
            this.OrderContracts = new List<OrderContract>();
        }

        private static ProductEntities m_entities = null;

        public static ProductEntities EntitiesInstance
        {
            get
            {
                if (m_entities == null)
                    m_entities = new ProductEntities();

                return m_entities;
            }
        }

        public void Reset()
        {
            m_entities = new ProductEntities();
        }

        public List<Entities.OrderContract> OrderContracts { get; set; }

        private List<SaleContract> m_saleContract = new List<SaleContract>();

        public List<SaleContract> SaleContracts
        {
            get
            {
                return m_saleContract;
            }
        }

        private List<ProductItem> m_repositories = new List<ProductItem>();

        public List<ProductItem> Repositories
        {
            get
            {
                return m_repositories;
            }
        }

        public void AddRepositories(IEnumerable<ProductItem> productItems)
        {
            foreach (var item in productItems)
            {
                m_repositories.Add(item);
            }
        }

        internal bool AllProductItemSold(OrderContract order)
        {
            //整单销售：
            //1. 如果销售合同整个销售了，那就对齐整单销售
            //2. 如果若干个订单的同一个OrderContract的某一个Item数量全部销售完毕，那就销售完毕一个Item
            //3. 如果所有Item都销售完毕，那么就视为整单销售完毕

            //1. 整单销售掉（其实不需要1也是可以的？）
            foreach (var sale in this.SaleContracts)
            {
                if (sale.SaleContractAuthorizeStatus == 1 //已经审核
                    )
                {
                    int cnt = 0;
                    foreach (var item in order.OrderProducts)
                    {
                        foreach (var item2 in sale.SaleProducts)
                        {
                            if (item2.OrderContractKey == item.OrderContractKey
                                && item.ProductKey == item.ProductKey
                                && item.Quantity == item.Quantity)
                            {
                                cnt++;
                            }
                        }
                    }
                    if (cnt == order.OrderProducts.Count())
                        return true;
                }
            }

            Dictionary<string, double> sumQuantity = new Dictionary<string, double>();

            foreach (var item in order.OrderProducts)
            {
                string key = string.Format("{0}##{1}", item.OrderContractKey, item.ProductKey);
                var cnt2 = item.Quantity;
                sumQuantity.Add(key, cnt2);
            }

            //2, 3, 整个订单被多个销售掉
            foreach (var sale in this.SaleContracts)
            {
                if (sale.SaleContractAuthorizeStatus == 0)
                    continue;

                foreach (var saleitem in sale.SaleProducts)
                {
                    string key = string.Format("{0}##{1}",
                        saleitem.OrderContractKey, saleitem.ProductKey);
                    if (sumQuantity.ContainsKey(key))
                    {
                        var cnt2 = saleitem.Quantity;
                        sumQuantity[key] = sumQuantity[key] - cnt2;
                    }

                }
            }

            var sum = sumQuantity.Values.Sum();
            if (sum <= 0)
            {//全部货品加起来小于等于0，就是全都卖掉了
                return true;
            }

            return false;
        }
    }
}
