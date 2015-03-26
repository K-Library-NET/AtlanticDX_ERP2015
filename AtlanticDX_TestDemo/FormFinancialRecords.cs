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
    public partial class FormFinancialRecords : Form
    {
        public FormFinancialRecords()
        {
            InitializeComponent();

            this.Load += FormFinancialRecords_Load;
        }

        void FormFinancialRecords_Load(object sender, EventArgs e)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();

            records.Add(
                new FinancialRecord()
                {
                    CreateDateTime = new DateTime(2014, 8, 23),
                    RecordType = FinancialRecordType.ImportBalancedPayment,
                    Amount = 30000,
                    ExchangeRate = 6.18,
                    OrderContractKey = "A2044872",
                    Currency = "CNY",
                });

            records.Add(
                new FinancialRecord()
                {
                    CreateDateTime = new DateTime(2014, 9, 12),
                    RecordType = FinancialRecordType.HarborAgentFee,
                    Amount = 12300,
                    ExchangeRate = 6.18,
                    OrderContractKey = "A2044872",
                    Currency = "CNY",
                });

            records.Add(
                new FinancialRecord()
                {
                    CreateDateTime = new DateTime(2014, 9, 18),
                    RecordType = FinancialRecordType.HarborAgentFee,
                    Amount = 30000,
                    ExchangeRate = 6.18,
                    OrderContractKey = "A2044872",
                    Currency = "CNY",
                });

            this.bindingSource1.DataSource = records;
        }
    }
}
