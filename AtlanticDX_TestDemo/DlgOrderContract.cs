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
    public partial class DlgOrderContract : Form
    {
        public DlgOrderContract()
        {
            InitializeComponent();

            this.Load += DlgOrderContract_Load;
        }

        void DlgOrderContract_Load(object sender, EventArgs e)
        { 
            foreach (var item in ProductEntities.EntitiesInstance.OrderContracts)
            {
                if (item.OrderType == 1)
                    continue;
                this.comboBox1.Items.Add(item.OrderContractKey);
            }
            this.comboBox1.SelectedIndex = -1; 
        }

        public Entities.OrderContract OrderContract { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem != null)
            {
                string text = this.comboBox1.SelectedItem.ToString();
                this.OrderContract = ProductEntities.EntitiesInstance.OrderContracts
                    .FirstOrDefault(p => p.OrderContractKey == text);
                //this.comboBox1.select
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
