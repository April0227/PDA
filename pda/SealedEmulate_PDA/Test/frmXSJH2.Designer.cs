namespace SealedEmulate_PDA
{
    partial class frmXSJH2
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cmbSaleNo = new System.Windows.Forms.TextBox();
            this.cmbKH = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_Quantity = new System.Windows.Forms.TextBox();
            this.txtTraceNo = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.DGsale = new System.Windows.Forms.DataGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DG2 = new System.Windows.Forms.DataGrid();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 262);
            this.tabControl1.TabIndex = 125;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.radioButton2);
            this.tabPage1.Controls.Add(this.radioButton1);
            this.tabPage1.Controls.Add(this.cmbSaleNo);
            this.tabPage1.Controls.Add(this.cmbKH);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.Label1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.tb_Quantity);
            this.tabPage1.Controls.Add(this.txtTraceNo);
            this.tabPage1.Controls.Add(this.Label3);
            this.tabPage1.Controls.Add(this.Label2);
            this.tabPage1.Controls.Add(this.btnclose);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.DGsale);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(226, 233);
            this.tabPage1.Text = "订单信息";
            // 
            // radioButton2
            // 
            this.radioButton2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.radioButton2.Location = new System.Drawing.Point(98, 40);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(125, 19);
            this.radioButton2.TabIndex = 131;
            this.radioButton2.TabStop = false;
            this.radioButton2.Text = "物料编码和批次";
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.radioButton1.Location = new System.Drawing.Point(3, 40);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(100, 19);
            this.radioButton1.TabIndex = 130;
            this.radioButton1.Text = "物料编码";
            // 
            // cmbSaleNo
            // 
            this.cmbSaleNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbSaleNo.Location = new System.Drawing.Point(98, 1);
            this.cmbSaleNo.Name = "cmbSaleNo";
            this.cmbSaleNo.Size = new System.Drawing.Size(125, 19);
            this.cmbSaleNo.TabIndex = 127;
            this.cmbSaleNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSaleNo_KeyPress);
            // 
            // cmbKH
            // 
            this.cmbKH.Enabled = false;
            this.cmbKH.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbKH.Location = new System.Drawing.Point(98, 20);
            this.cmbKH.Name = "cmbKH";
            this.cmbKH.Size = new System.Drawing.Size(125, 19);
            this.cmbKH.TabIndex = 126;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 14);
            this.label4.Text = "销售客户";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(3, 1);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(93, 14);
            this.Label1.Text = "销售订单号";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 24);
            this.button1.TabIndex = 120;
            this.button1.Text = "清";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(67, 148);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 19);
            this.textBox1.TabIndex = 119;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 14);
            this.label5.Text = "条码信息";
            // 
            // tb_Quantity
            // 
            this.tb_Quantity.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tb_Quantity.Location = new System.Drawing.Point(67, 186);
            this.tb_Quantity.Name = "tb_Quantity";
            this.tb_Quantity.Size = new System.Drawing.Size(131, 19);
            this.tb_Quantity.TabIndex = 118;
            this.tb_Quantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Quantity_KeyPress);
            // 
            // txtTraceNo
            // 
            this.txtTraceNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTraceNo.Location = new System.Drawing.Point(67, 167);
            this.txtTraceNo.Name = "txtTraceNo";
            this.txtTraceNo.ReadOnly = true;
            this.txtTraceNo.Size = new System.Drawing.Size(131, 19);
            this.txtTraceNo.TabIndex = 117;
            this.txtTraceNo.TextChanged += new System.EventHandler(this.txtTraceNo_TextChanged);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(3, 190);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(76, 14);
            this.Label3.Text = "销售数量";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(3, 170);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(95, 14);
            this.Label2.Text = "物料编码";
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(149, 208);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(74, 25);
            this.btnclose.TabIndex = 116;
            this.btnclose.Text = "关闭";
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(5, 208);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 25);
            this.btnSave.TabIndex = 115;
            this.btnSave.Text = "添加";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // DGsale
            // 
            this.DGsale.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGsale.Location = new System.Drawing.Point(2, 61);
            this.DGsale.Name = "DGsale";
            this.DGsale.Size = new System.Drawing.Size(223, 84);
            this.DGsale.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DG2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(226, 233);
            this.tabPage2.Text = "物料明细";
            // 
            // DG2
            // 
            this.DG2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DG2.Location = new System.Drawing.Point(2, 3);
            this.DG2.Name = "DG2";
            this.DG2.Size = new System.Drawing.Size(223, 227);
            this.DG2.TabIndex = 1;
            // 
            // frmXSJH2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmXSJH2";
            this.Text = "销售交货";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmXSJH2_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGrid DGsale;
        private System.Windows.Forms.DataGrid DG2;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox tb_Quantity;
        internal System.Windows.Forms.TextBox txtTraceNo;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.TextBox cmbSaleNo;
        internal System.Windows.Forms.TextBox cmbKH;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}