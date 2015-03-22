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
    public partial class FormBargain : Form
    {
        public FormBargain()
        {
            InitializeComponent();

            if (this.DesignMode)
                return;
            this.Load += FormBargain_Load;
        }

        void FormBargain_Load(object sender, EventArgs e)
        {
            foreach (var item in ProductEntities.EntitiesInstance.SaleContracts)
            {
                //if (item.OrderType == 1)
                //    continue;
                this.comboBox1.Items.Add(item.SaleContractKey);
            }
            this.comboBox1.SelectedIndex = -1; 
        }
    }
}
