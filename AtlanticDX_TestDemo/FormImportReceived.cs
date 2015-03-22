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
    public partial class FormImportReceived : Form
    {
        public FormImportReceived()
        {
            InitializeComponent();
            this.Load += FormImportLogistics_Load;
        }

        void FormImportLogistics_Load(object sender, EventArgs e)
        {
            var item = from one in ProductEntities.EntitiesInstance.OrderContracts
                       where one.OrderType == 1 && one.ContractStatus == ContractStatusType.Committed
                       select one;

            foreach (var i in item)
            {
                this.comboBox1.Items.Add(i.OrderContractKey);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            var order = this.FindByKey(this.comboBox1.SelectedItem);

            //HKLogistics(order);
            MainlandLogisticsPay(order);
        }

        private OrderContract FindByKey(object p2)
        {
            if (p2 != null)
            {
                return ProductEntities.EntitiesInstance.OrderContracts.FirstOrDefault(
                    p => p.OrderContractKey == p2.ToString());
            }
            return null;
        }

        private void MainlandLogisticsPay(OrderContract order)
        {
            if (order != null)
            {
                MainlandLogistics logistics = new MainlandLogistics() { MainlandLogisticsName = "国通" };

                //this.comboBoxMainLand.Items.Clear();
                //this.comboBoxMainLand.Items.Add(logistics.MainlandLogisticsName);
                //this.comboBoxMainLand.SelectedIndex = 0;

                List<MainlandLogisticsItem> items = new List<MainlandLogisticsItem>();

                items.Add(new MainlandLogisticsItem()
                {
                    ContractQuantity = 120,
                    ProductKey = "美国 P32 凤爪 Majar",
                    ContractWeight = 24.7664764,
                    FreightCharges = 80,
                    //Insurance = 12000,
                    Compensation = 20000,
                    CompensationReason = "少20件",
                    ReceivingTime = new DateTime(2014, 9, 12),
                    ReceivingQuantity = 110,
                });
                items.Add(new MainlandLogisticsItem()
                {
                    ProductKey = "美国 P32 鸡尖 Majar",
                    ContractQuantity = 302,
                    ContractWeight = 11.53248306,
                    FreightCharges = 80,
                    //Insurance = 11000,
                    Compensation = 300,
                    CompensationReason = "少2件",
                    ReceivingTime = new DateTime(2014, 9, 18),
                    ReceivingQuantity = 300,
                });
                items.Add(new MainlandLogisticsItem()
                {
                    ProductKey = "美国 P1065 凤爪 Kero",
                    ContractQuantity = 110,
                    ContractWeight = 9.123,
                    FreightCharges = 80,
                    //Insurance = 800,
                    ReceivingTime = new DateTime(2014, 9, 18),
                    ReceivingQuantity = 110,
                });

                //this.textBoxCostMainland.Text = items.Sum(new Func<MainlandLogisticsItem, decimal?>(
                //    delegate(MainlandLogisticsItem item1)
                //    {
                //        return Convert.ToDecimal(item1.SubTotal);
                //    })).ToString();

                logistics.CostPayItems = new List<PayItem>(new PayItem[]{
                    new PayItem() { PayCost = 12000, PayTime = new DateTime(2014, 9, 14) },
                    //new PayItem() { PayCost = 280000, PayTime = new DateTime(2014, 9, 21) },
                });


                this.MainlandLogistics = logistics;
                this.bindingSource1.DataSource = items;
            }
            else
            {
                this.MainlandLogistics = null;
                this.bindingSource1.DataSource = null;
            }

            //if (this.MainlandLogistics == null || this.MainlandLogistics.CommitToPayCost)
            //    this.btnSubmitMainlandLogPay.Enabled = false;
            //else this.btnSubmitMainlandLogPay.Enabled = true;

            //if (this.MainlandLogistics == null || this.MainlandLogistics.CommitToPayCost == false)
            //{
            //    this.btnAddMainlandPayCost.Enabled = false;
            //}
            //else this.btnAddMainlandPayCost.Enabled = true;
        }

        public MainlandLogistics MainlandLogistics { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            FormClaimCompensation form = new FormClaimCompensation();
            form.Show();
        }
    }
}
