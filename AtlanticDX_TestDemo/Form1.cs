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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (this.DesignMode)
                return;
            this.Load += Form1_Load;

        }

        void Form1_Load(object sender, EventArgs e)
        {
            var ids = from one in ProductEntities.EntitiesInstance.OrderContracts
                      select one.OrderContractKey;

            //this.comboBox1.Items.Add("(空)");
            foreach (var id in ids)
                this.comboBox1.Items.Add(id);

            this.comboBox1.SelectedIndex = -1;
        }

        private FormFirst m_first = null;

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (m_first == null)
            {
                m_first = new FormFirst();
                m_first.ParentMainForm = this;
                m_first.Start();
                m_first.Show();
            }
            else
            {
                m_first.Activate();
            }
            m_first.RefreshData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ProductEntities.EntitiesInstance.Reset();
            m_first = null;
        }

        internal void RefreshData()
        {
            throw new NotImplementedException();
        }

        private void comboBox1_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            string text = this.comboBox1.SelectedItem != null ?
                 this.comboBox1.SelectedItem.ToString() : string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                var item = ProductEntities.EntitiesInstance.OrderContracts.FirstOrDefault(
                    p => p.OrderContractKey == text);

                StringBuilder builder = new StringBuilder();
                if (item.OrderProducts != null)
                {
                    foreach (var i in item.OrderProducts)
                    {
                        builder.AppendLine(i.ToString());
                    }
                }
                this.textBox1.Text = builder.ToString();
            }
        }
    }
}
