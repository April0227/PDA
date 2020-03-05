namespace SealedEmulate_PDA
{
    partial class frmXSJH
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
            this.btnclose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.DGsale = new System.Windows.Forms.DataGrid();
            this.tb_Quantity = new System.Windows.Forms.TextBox();
            this.txtTraceNo = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.cmbSaleNo = new System.Windows.Forms.ComboBox();
            this.cmbKH = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(156, 237);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(74, 25);
            this.btnclose.TabIndex = 77;
            this.btnclose.Text = "关闭";
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(13, 237);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 25);
            this.btnSave.TabIndex = 76;
            this.btnSave.Text = "添加";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(11, 29);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(93, 14);
            this.Label1.Text = "销售订单号";
            // 
            // DGsale
            // 
            this.DGsale.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGsale.Location = new System.Drawing.Point(3, 49);
            this.DGsale.Name = "DGsale";
            this.DGsale.Size = new System.Drawing.Size(234, 106);
            this.DGsale.TabIndex = 91;
            // 
            // tb_Quantity
            // 
            this.tb_Quantity.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tb_Quantity.Location = new System.Drawing.Point(83, 206);
            this.tb_Quantity.Name = "tb_Quantity";
            this.tb_Quantity.Size = new System.Drawing.Size(125, 19);
            this.tb_Quantity.TabIndex = 90;
            // 
            // txtTraceNo
            // 
            this.txtTraceNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTraceNo.Location = new System.Drawing.Point(83, 183);
            this.txtTraceNo.Name = "txtTraceNo";
            this.txtTraceNo.ReadOnly = true;
            this.txtTraceNo.Size = new System.Drawing.Size(125, 19);
            this.txtTraceNo.TabIndex = 89;
            this.txtTraceNo.TextChanged += new System.EventHandler(this.txtTraceNo_TextChanged);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(11, 210);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(76, 14);
            this.Label3.Text = "销售数量";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(11, 186);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(95, 14);
            this.Label2.Text = "物料编码";
            // 
            // cmbSaleNo
            // 
            this.cmbSaleNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbSaleNo.Location = new System.Drawing.Point(95, 27);
            this.cmbSaleNo.Name = "cmbSaleNo";
            this.cmbSaleNo.Size = new System.Drawing.Size(125, 19);
            this.cmbSaleNo.TabIndex = 94;
            this.cmbSaleNo.SelectedValueChanged += new System.EventHandler(this.cmbSaleNo_SelectedValueChanged);
            // 
            // cmbKH
            // 
            this.cmbKH.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbKH.Location = new System.Drawing.Point(95, 5);
            this.cmbKH.Name = "cmbKH";
            this.cmbKH.Size = new System.Drawing.Size(125, 19);
            this.cmbKH.TabIndex = 99;
            this.cmbKH.SelectedValueChanged += new System.EventHandler(this.cmbKH_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 14);
            this.label4.Text = "销售客户";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(83, 158);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 19);
            this.textBox1.TabIndex = 105;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.LostFocus += new System.EventHandler(this.textBox1_LostFocus);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 14);
            this.label5.Text = "条码信息";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(211, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 41);
            this.button1.TabIndex = 111;
            this.button1.Text = "清";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmXSJH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbKH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSaleNo);
            this.Controls.Add(this.DGsale);
            this.Controls.Add(this.tb_Quantity);
            this.Controls.Add(this.txtTraceNo);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnSave);
            this.Name = "frmXSJH";
            this.Text = "销售交货";
            this.Load += new System.EventHandler(this.frmXSJH_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.DataGrid DGsale;
        internal System.Windows.Forms.TextBox tb_Quantity;
        internal System.Windows.Forms.TextBox txtTraceNo;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ComboBox cmbSaleNo;
        internal System.Windows.Forms.ComboBox cmbKH;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
    }
}