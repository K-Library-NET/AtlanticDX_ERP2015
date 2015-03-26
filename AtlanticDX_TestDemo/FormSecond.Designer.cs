namespace AtlanticDX_TestDemo
{
    partial class FormSecond
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectOrderContractSale = new System.Windows.Forms.Button();
            this.btnSelectProduct = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbProductItems = new System.Windows.Forms.TextBox();
            this.btnAuthorizeAndShipment = new System.Windows.Forms.Button();
            this.tbSaleContractKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectOrderContractSale
            // 
            this.btnSelectOrderContractSale.Location = new System.Drawing.Point(30, 23);
            this.btnSelectOrderContractSale.Name = "btnSelectOrderContractSale";
            this.btnSelectOrderContractSale.Size = new System.Drawing.Size(207, 45);
            this.btnSelectOrderContractSale.TabIndex = 0;
            this.btnSelectOrderContractSale.Text = "整单销售";
            this.btnSelectOrderContractSale.UseVisualStyleBackColor = true;
            this.btnSelectOrderContractSale.Click += new System.EventHandler(this.btnSelectOrderContractSale_Click);
            // 
            // btnSelectProduct
            // 
            this.btnSelectProduct.Location = new System.Drawing.Point(317, 24);
            this.btnSelectProduct.Name = "btnSelectProduct";
            this.btnSelectProduct.Size = new System.Drawing.Size(256, 44);
            this.btnSelectProduct.TabIndex = 1;
            this.btnSelectProduct.Text = "选择货品销售";
            this.btnSelectProduct.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbProductItems);
            this.groupBox1.Location = new System.Drawing.Point(30, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(978, 479);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "货品列表";
            // 
            // tbProductItems
            // 
            this.tbProductItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProductItems.Location = new System.Drawing.Point(3, 31);
            this.tbProductItems.Multiline = true;
            this.tbProductItems.Name = "tbProductItems";
            this.tbProductItems.Size = new System.Drawing.Size(972, 445);
            this.tbProductItems.TabIndex = 0;
            // 
            // btnAuthorizeAndShipment
            // 
            this.btnAuthorizeAndShipment.Location = new System.Drawing.Point(479, 598);
            this.btnAuthorizeAndShipment.Name = "btnAuthorizeAndShipment";
            this.btnAuthorizeAndShipment.Size = new System.Drawing.Size(529, 43);
            this.btnAuthorizeAndShipment.TabIndex = 3;
            this.btnAuthorizeAndShipment.Text = "提交销售订单";
            this.btnAuthorizeAndShipment.UseVisualStyleBackColor = true;
            this.btnAuthorizeAndShipment.Click += new System.EventHandler(this.btnAuthorizeAndShipment_Click);
            // 
            // tbSaleContractKey
            // 
            this.tbSaleContractKey.Location = new System.Drawing.Point(205, 604);
            this.tbSaleContractKey.Name = "tbSaleContractKey";
            this.tbSaleContractKey.Size = new System.Drawing.Size(233, 35);
            this.tbSaleContractKey.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 607);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 24);
            this.label1.TabIndex = 5;
            this.label1.Text = "销售合同编号";
            // 
            // FormSecond
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 669);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSaleContractKey);
            this.Controls.Add(this.btnAuthorizeAndShipment);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSelectProduct);
            this.Controls.Add(this.btnSelectOrderContractSale);
            this.Name = "FormSecond";
            this.Text = "第二步：销售";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectOrderContractSale;
        private System.Windows.Forms.Button btnSelectProduct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAuthorizeAndShipment;
        private System.Windows.Forms.TextBox tbSaleContractKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbProductItems;
    }
}