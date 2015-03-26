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
    public partial class FormSaleContractAuth : Form
    {
        public FormSaleContractAuth()
        {
            InitializeComponent();

            if (this.DesignMode)
                return;

            this.Load += FormSaleContractAuth_Load;
        }

        void FormSaleContractAuth_Load(object sender, EventArgs e)
        {
            var items = from one in ProductEntities.EntitiesInstance.SaleContracts
                        where one.SaleContractAuthorizeStatus == 0
                        select one;

            foreach (var i in items)
            {
                this.comboBox1.Items.Add(i.SaleContractKey);
            }
            this.comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem != null)
            {
                string text = this.comboBox1.SelectedItem.ToString();
                var item = ProductEntities.EntitiesInstance.SaleContracts.FirstOrDefault(
                     p => p.SaleContractKey == text);
                if (item != null && item.SaleContractAuthorizeStatus == 0)
                {
                    item.SaleContractAuthorizeStatus = 1;
                    MessageBox.Show("审核成功");
                }
            }
        }
    }
}
