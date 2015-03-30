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
    public partial class FormSecond : Form
    {
        public FormSecond()
        {
            InitializeComponent();
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

        private void btnSelectOrderContractSale_Click(object sender, EventArgs e)
        {
            DlgOrderContract dlg = new DlgOrderContract();
            DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK
                || result == System.Windows.Forms.DialogResult.Yes)
            {
                OrderContract orderContract = dlg.OrderContract;
                var orderProducts = orderContract.OrderProducts;

                CreateOrderProductsByItems(orderProducts);
            }
        }

        private void CreateOrderProductsByItems(IEnumerable<ProductItem> orderProducts)
        {
            this.SaleContract = new SaleContract()
            {
                SaleProducts = new List<ProductItem>(orderProducts)
            };

            StringBuilder builder = new StringBuilder();
            foreach (var item in this.SaleContract.SaleProducts)
            {
                builder.AppendLine(
                    string.Format("{0} {1} {2}", item.ProductKey,
                    item.OrderContractKey, item.Quantity));
            }

            this.tbProductItems.Text = builder.ToString();
        }

        private void btnAuthorizeAndShipment_Click(object sender, EventArgs e)
        {
            if (this.SaleContract != null)
            {
                this.SaleContract.SaleContractAuthorizeStatus = 1;
                this.SaleContract.SaleContractKey = this.tbSaleContractKey.Text;
                ProductEntities.EntitiesInstance.SaleContracts.Add(this.SaleContract);

                this.Cls();
            }
        }

        private void Cls()
        {
            this.tbProductItems.Text = string.Empty;
            this.tbSaleContractKey.Text = string.Empty;
        }

        public SaleContract SaleContract { get; set; }
    }
}
