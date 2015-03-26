using AtlanticDX_TestDemo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlanticDX_TestDemo
{
    public partial class FormFirst : Form
    {
        public FormFirst()
        {
            InitializeComponent();

            if (this.DesignMode)
                return;
            this.Load += FormFirst_Load;
        }

        void FormFirst_Load(object sender, EventArgs e)
        {
            OrderContract contractA1 = new OrderContract()
            {
                ContractStatus = ContractStatusType.UnCommitted,
                OrderContractKey = "ORDER_CONTRACT_A1",
                OrderType = 1,//现货
                OrderProducts = new List<ProductItem>(
                new ProductItem[] 
                { 
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A1",
                        ProductKey = "C123", Quantity = 400, UnitPrice = 20, NetWeight = 50, Currency = "CNY"},
                    new ProductItem(){  OrderContractKey = "ORDER_CONTRACT_A1",
                        ProductKey = "C235", Quantity = 120, UnitPrice = 10, NetWeight = 45, Currency = "USD"},
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A1",
                        ProductKey = "C345", Quantity = 230, UnitPrice = 15, NetWeight = 55, Currency = "USD"},
                }),
                ETA = new DateTime(2014, 10, 20),
                ETD = new DateTime(2014, 10, 10),
            };

            OrderContract contractA2 = new OrderContract()
            {
                OrderContractKey = "ORDER_CONTRACT_A2",
                OrderType = 1,//现货
                OrderProducts = new List<ProductItem>(new ProductItem[] 
                { 
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A2",
                        ProductKey = "C123", Quantity = 400, UnitPrice = 20, NetWeight = 50, Currency = "CNY"},
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A2",
                        ProductKey = "C235", Quantity = 120, UnitPrice = 60, NetWeight = 45, Currency = "CNY"},
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A2",
                        ProductKey = "C345", Quantity = 230, UnitPrice = 75, NetWeight = 55, Currency = "CNY"},
                }),
                ETA = new DateTime(2014, 10, 18),
                ETD = new DateTime(2014, 10, 12),
            };

            OrderContract contractB1 = new OrderContract()
            {
                OrderContractKey = "ORDER_CONTRACT_B1",
                OrderType = 0,//期货
                OrderProducts = new List<ProductItem>(new ProductItem[] 
                { 
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B1",
                        ProductKey = "C123", Quantity = 400, UnitPrice = 20, NetWeight = 50, Currency = "CNY"},
                    new ProductItem(){  OrderContractKey = "ORDER_CONTRACT_B1",
                        ProductKey = "C235", Quantity = 120, UnitPrice = 10, NetWeight = 45, Currency = "USD"},
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B1",
                        ProductKey = "C345", Quantity = 230, UnitPrice = 15, NetWeight = 55, Currency = "USD"},
                }),
                ETA = new DateTime(2014, 11, 18),
                ETD = new DateTime(2014, 11, 12),
            };

            OrderContract contractB2 = new OrderContract()
            {
                OrderContractKey = "ORDER_CONTRACT_B2",
                OrderType = 0,//期货
                OrderProducts = new List<ProductItem>(new ProductItem[] 
                { 
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B2",
                        ProductKey = "C123", Quantity = 400, UnitPrice = 20, NetWeight = 50, Currency = "CNY"},
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B2",
                        ProductKey = "C235", Quantity = 120, UnitPrice = 10, NetWeight = 45, Currency = "USD"},
                    new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B2",
                        ProductKey = "C345", Quantity = 230, UnitPrice = 15, NetWeight = 55, Currency = "USD"},
                }),
                ETA = new DateTime(2014, 11, 18),
                ETD = new DateTime(2014, 11, 12),
            };

            this.contract1 = contractA1;
            this.contract2 = contractA2;
            this.contract3 = contractB1;
            this.contract4 = contractB2;
            this.OrderContracts = new OrderContract[] { contractA1, contractA2, contractB1, contractB2 };

            var temp = ProductEntities.EntitiesInstance.OrderContracts.FirstOrDefault(
                p => p.OrderContractKey == this.contract1.OrderContractKey);
            if (temp != null)
            {
                this.contract1 = temp;
                this.textBox1.Text = this.contract1.ToString();
            }
            temp = null;
            temp = ProductEntities.EntitiesInstance.OrderContracts.FirstOrDefault(
                p => p.OrderContractKey == this.contract2.OrderContractKey);
            if (temp != null)
            {
                this.contract2 = temp;
                this.textBox2.Text = this.contract2.ToString();
            }
            temp = null;
            temp = ProductEntities.EntitiesInstance.OrderContracts.FirstOrDefault(
                p => p.OrderContractKey == this.contract3.OrderContractKey);
            if (temp != null)
            {
                this.contract3 = temp;
                this.textBox3.Text = this.contract3.ToString();
            }
            temp = null;
            temp = ProductEntities.EntitiesInstance.OrderContracts.FirstOrDefault(
                p => p.OrderContractKey == this.contract4.OrderContractKey);
            if (temp != null)
            {
                this.contract4 = temp;
                this.textBox4.Text = this.contract4.ToString();
            }
        }

        private FormSecond m_second = null;

        private void btnNext_Click(object sender, EventArgs e)
        {
            var temp = this.contract4;
            CommitItem(ref temp);
            this.contract4 = temp;
            this.textBox4.Text = this.contract4.ToString();
            //if (m_second == null)
            //{
            //    m_second = new FormSecond();
            //    m_second.ParentMainForm = this.ParentMainForm;
            //    m_second.Show();
            //}
            //else
            //{
            //    m_second.Activate();
            //}
            //m_second.RefreshData();
        }

        internal void RefreshData()
        {
            this.RefreshDataCore();
            if (this.ParentMainForm != null)
                this.ParentMainForm.RefreshData();
        }

        private void RefreshDataCore()
        {
            throw new NotImplementedException();
        }

        public Form1 ParentMainForm { get; set; }

        internal void Start()
        {
            //OrderContract contractA1 = new OrderContract()
            //{
            //    OrderContractKey = "ORDER_CONTRACT_A1",
            //    OrderType = 1,//现货
            //    OrderProducts = new ProductItem[] 
            //    { 
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A1",
            //            ProductKey = "C123", Quantity = 400, UnitPrice = 20, NetWeight = 50, Currency = "CNY"},
            //        new ProductItem(){  OrderContractKey = "ORDER_CONTRACT_A1",
            //            ProductKey = "C235", Quantity = 120, UnitPrice = 10, NetWeight = 45, Currency = "USD"},
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A1",
            //            ProductKey = "C345", Quantity = 230, UnitPrice = 15, NetWeight = 55, Currency = "USD"},
            //    },
            //    ETA = new DateTime(2014, 10, 20),
            //    ETD = new DateTime(2014, 10, 10),
            //};

            //OrderContract contractA2 = new OrderContract()
            //{
            //    OrderContractKey = "ORDER_CONTRACT_A2",
            //    OrderType = 1,//现货
            //    OrderProducts = new ProductItem[] 
            //    { 
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A2",
            //            ProductKey = "C123", Quantity = 400, UnitPrice = 20, NetWeight = 50, Currency = "CNY"},
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A2",
            //            ProductKey = "C235", Quantity = 120, UnitPrice = 60, NetWeight = 45, Currency = "CNY"},
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_A2",
            //            ProductKey = "C345", Quantity = 230, UnitPrice = 75, NetWeight = 55, Currency = "CNY"},
            //    },
            //    ETA = new DateTime(2014, 10, 18),
            //    ETD = new DateTime(2014, 10, 12),
            //};

            //OrderContract contractB1 = new OrderContract()
            //{
            //    OrderContractKey = "ORDER_CONTRACT_B1",
            //    OrderType = 1,//期货
            //    OrderProducts = new ProductItem[] 
            //    { 
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B1",
            //            ProductKey = "C123", Quantity = 400, UnitPrice = 20, NetWeight = 50, Currency = "CNY"},
            //        new ProductItem(){  OrderContractKey = "ORDER_CONTRACT_B1",
            //            ProductKey = "C235", Quantity = 120, UnitPrice = 10, NetWeight = 45, Currency = "USD"},
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B1",
            //            ProductKey = "C345", Quantity = 230, UnitPrice = 15, NetWeight = 55, Currency = "USD"},
            //    },
            //    ETA = new DateTime(2014, 11, 18),
            //    ETD = new DateTime(2014, 11, 12),
            //};

            //OrderContract contractB2 = new OrderContract()
            //{
            //    OrderContractKey = "ORDER_CONTRACT_B2",
            //    OrderType = 0,//期货
            //    OrderProducts = new ProductItem[] 
            //    { 
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B2",
            //            ProductKey = "C123", Quantity = 400, UnitPrice = 20, NetWeight = 50, Currency = "CNY"},
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B2",
            //            ProductKey = "C235", Quantity = 120, UnitPrice = 10, NetWeight = 45, Currency = "USD"},
            //        new ProductItem(){ OrderContractKey = "ORDER_CONTRACT_B2",
            //            ProductKey = "C345", Quantity = 230, UnitPrice = 15, NetWeight = 55, Currency = "USD"},
            //    },
            //    ETA = new DateTime(2014, 11, 18),
            //    ETD = new DateTime(2014, 11, 12),
            //};

            //ProductEntities.EntitiesInstance.OrderContracts =
            //    new OrderContract[] { contractA1, contractA2, contractB1, contractB2 };
        }

        private void btnSubmit1_Click(object sender, EventArgs e)
        {
            AuthorizeItem(this.contract1, this.textBox1);
            //var order = ProductEntities.EntitiesInstance.OrderContracts.ElementAt(0);
            //OrderInRepositories(order);
        }

        private void AuthorizeItem(OrderContract orderContract, TextBox textBox1)
        {
            var item = ProductEntities.EntitiesInstance.OrderContracts.FirstOrDefault(
                   p => p.OrderContractKey == orderContract.OrderContractKey);

            if (item != null && item.ContractStatus == ContractStatusType.UnCommitted)
            {
                item.ContractStatus = ContractStatusType.Committed;
                orderContract = item;

                textBox1.Text = orderContract.ToString();
                orderContract.ContractStatus = ContractStatusType.Committed;
                MessageBox.Show("订单审核通过。");
            }
            else if (item != null && item.ContractStatus == ContractStatusType.Committed)
            {
                textBox1.Text = orderContract.ToString();
                MessageBox.Show("订单已经审核过。");
            }
            else
            {
                MessageBox.Show("订单未提交。");
                //item = orderContract;
                //item.ContractStatus = ContractStatusType.Committed;
                //orderContract = item;
                //MessageBox.Show("订单审核通过。");
            }
        }

        private void OrderInRepositories(OrderContract order)
        {
            if (order.OrderType == 1)
            {
                if (!ProductEntities.EntitiesInstance.AllProductItemSold(order))
                {//如果所有库存都被销售掉，那就视为整单销售掉，不需要更新库存
                    ProductEntities.EntitiesInstance.AddRepositories(order.OrderProducts);
                }
                this.RefreshData();
            }
        }

        private void btnSubmit2_Click(object sender, EventArgs e)
        {
            AuthorizeItem(this.contract2, this.textBox2);

            //this.textBox2.Text = this.contract2.ToString();
            //var order = ProductEntities.EntitiesInstance.OrderContracts.ElementAt(1);
            //OrderInRepositories(order);
            //if (order.OrderType == 1)
            //{
            //    ProductEntities.EntitiesInstance.AddRepositories(order.OrderProducts);
            //    this.RefreshData();
            //}
        }

        private void btnSubmit3_Click(object sender, EventArgs e)
        {
            AuthorizeItem(this.contract3, this.textBox3);
            //this.textBox3.Text = this.contract3.ToString();
            //var order = ProductEntities.EntitiesInstance.OrderContracts.ElementAt(2);
            //OrderInRepositories(order);
            //if (order.OrderType == 1)
            //{
            //    ProductEntities.EntitiesInstance.AddRepositories(order.OrderProducts);
            //    this.RefreshData();
            //}
        }

        private void btnSubmit4_Click(object sender, EventArgs e)
        {
            AuthorizeItem(this.contract4, this.textBox4);
            //this.textBox4.Text = this.contract4.ToString();
            //var order = ProductEntities.EntitiesInstance.OrderContracts.ElementAt(3);
            //OrderInRepositories(order);
            //if (order.OrderType == 1)
            //{
            //    ProductEntities.EntitiesInstance.AddRepositories(order.OrderProducts);
            //    this.RefreshData();
            //}
        }

        public OrderContract[] OrderContracts { get; set; }

        public OrderContract contract1 { get; set; }

        public OrderContract contract2 { get; set; }

        public OrderContract contract3 { get; set; }

        public OrderContract contract4 { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            var temp = this.contract1;
            CommitItem(ref temp);
            this.contract1 = temp;
            this.textBox1.Text = this.contract1.ToString();
        }

        private void CommitItem(ref OrderContract contract1)
        {
            var temp = contract1;
            var item = ProductEntities.EntitiesInstance.OrderContracts.FirstOrDefault(
                p => p.OrderContractKey == temp.OrderContractKey);
            if (item != null)
            {
                //&& item.ContractStatus == ContractStatusType.UnCommitted)
                //{
                //    //item.ContractStatus = ContractStatusType.Committed;
                //    contract1 = item;
                //    ProductEntities.EntitiesInstance.OrderContracts.Add(item);
                //    MessageBox.Show("订单提交成功。");
                //}
                //else if (item != null && item.ContractStatus == ContractStatusType.Committed)
                //{
                MessageBox.Show("订单已经提交过。");
            }
            else
            {
                item = contract1;
                //item.ContractStatus = ContractStatusType.Committed;
                contract1 = item;
                ProductEntities.EntitiesInstance.OrderContracts.Add(item);
                MessageBox.Show("订单提交成功。");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var temp = contract2;
            CommitItem(ref temp);
            contract2 = temp;
            this.textBox2.Text = this.contract2.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var temp = contract3;
            CommitItem(ref temp);
            contract3 = temp;
            this.textBox3.Text = this.contract3.ToString();
        }
    }
}
