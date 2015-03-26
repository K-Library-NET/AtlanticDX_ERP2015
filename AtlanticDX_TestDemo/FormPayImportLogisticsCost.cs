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
    public partial class FormPayImportLogisticsCost : Form
    {
        public FormPayImportLogisticsCost()
        {
            InitializeComponent();

            if (this.DesignMode)
                return;

            this.Load += FormPayImportLogisticsCost_Load;
        }

        void FormPayImportLogisticsCost_Load(object sender, EventArgs e)
        {
            if (this.Logistics != null && this.Logistics.Count() > 0)
                this.bindingSource1.DataSource = new List<Entities.PayItem>(this.Logistics);
        }

        public IEnumerable<Entities.PayItem> Logistics { get; set; }
    }
}
