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
    public partial class FormHub : Form
    {
        public FormHub()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.ShowActivated(form);
        }

        private FormFirst m_first = null;
        private Form2 m_form2 = null;

        /// <summary>
        /// 采购合同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            ActivateFirst();
        }

        private void ActivateFirst()
        {
            //if (m_first == null)
            //{
            m_first = new FormFirst();
            //}
            ShowActivated(m_first);
        }

        private void ShowActivated(Form form)
        {
            if (form != null)
            {
                form.Show();
                form.Activate();
            }
        }

        /// <summary>
        /// 采购交单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.ActivateFirst();
        }

        /// <summary>
        /// 报关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //if (m_form2 == null)
            //{
            Form2 form = new Form2();

            form.Show();

            //    m_form2 = form;
            //}
            //this.ShowActivated(m_form2);
        }

        /// <summary>
        /// 香港物流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            FormImportLogistics form = new FormImportLogistics();
            form.Switch = 1;
            this.ShowActivated(form);
        }

        /// <summary>
        /// 内地物流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            FormImportLogistics form = new FormImportLogistics();
            this.ShowActivated(form);
        }

        /// <summary>
        /// 收货（采购合同）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            FormImportReceived form = new FormImportReceived();
            this.ShowActivated(form);
        }

        /// <summary>
        /// 采购索赔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            FormClaimCompensation form = new FormClaimCompensation();
            form.Show();
        }

        /// <summary>
        /// 销售合同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            FormSecond second = new FormSecond();
            this.ShowActivated(second);
        }

        /// <summary>
        /// 已提交的销售合同添加还价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            FormBargain form = new FormBargain();
            this.ShowActivated(form);
        }

        /// <summary>
        /// 审核订单，多个还价中选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            FormSaleContractAuth form = new FormSaleContractAuth();
            this.ShowActivated(form);
        }

        /// <summary>
        /// 已审核的订单安排发货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            FormSaleDelivery form = new FormSaleDelivery();
            this.ShowActivated(form);
        }

        /// <summary>
        /// 已发货的订单客户收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {
            FormFirst first = new FormFirst();
            this.ShowActivated(first);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FormFinancialRecords form = new FormFinancialRecords();
            this.ShowActivated(form);
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }
    }
}
