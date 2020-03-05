namespace SealedEmulate_PDA.Test
{
    partial class XSJH_TEST1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.conName = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.DG2 = new System.Windows.Forms.DataGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tb_Quantity = new System.Windows.Forms.TextBox();
            this.txtTraceNo = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.DGsale = new System.Windows.Forms.DataGrid();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.cmbSaleNo = new System.Windows.Forms.TextBox();
            this.cmbKH = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // conName
            // 
            this.conName.Location = new System.Drawing.Point(83, 0);
            this.conName.Name = "conName";
            this.conName.Size = new System.Drawing.Size(133, 23);
            this.conName.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(76, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 19);
            this.textBox1.TabIndex = 119;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 14);
            this.label5.Text = "条码信息";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.conName);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.DG2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(226, 184);
            this.tabPage2.Text = "物料明细";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(5, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "物料名称";
            // 
            // DG2
            // 
            this.DG2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DG2.Location = new System.Drawing.Point(2, 27);
            this.DG2.Name = "DG2";
            this.DG2.Size = new System.Drawing.Size(223, 154);
            this.DG2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 41);
            this.button1.TabIndex = 120;
            this.button1.Text = "清";
            // 
            // tabPage1
            // 
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
            this.tabPage1.Size = new System.Drawing.Size(226, 184);
            this.tabPage1.Text = "订单信息";
            // 
            // tb_Quantity
            // 
            this.tb_Quantity.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tb_Quantity.Location = new System.Drawing.Point(76, 132);
            this.tb_Quantity.Name = "tb_Quantity";
            this.tb_Quantity.Size = new System.Drawing.Size(125, 19);
            this.tb_Quantity.TabIndex = 118;
            // 
            // txtTraceNo
            // 
            this.txtTraceNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTraceNo.Location = new System.Drawing.Point(76, 110);
            this.txtTraceNo.Name = "txtTraceNo";
            this.txtTraceNo.ReadOnly = true;
            this.txtTraceNo.Size = new System.Drawing.Size(125, 19);
            this.txtTraceNo.TabIndex = 117;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(4, 136);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(76, 14);
            this.Label3.Text = "销售数量";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(4, 113);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(95, 14);
            this.Label2.Text = "物料编码";
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(149, 156);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(74, 25);
            this.btnclose.TabIndex = 116;
            this.btnclose.Text = "关闭";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 156);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 25);
            this.btnSave.TabIndex = 115;
            this.btnSave.Text = "添加";
            // 
            // DGsale
            // 
            this.DGsale.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGsale.Location = new System.Drawing.Point(2, 4);
            this.DGsale.Name = "DGsale";
            this.DGsale.Size = new System.Drawing.Size(223, 79);
            this.DGsale.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(202, 144);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 213);
            this.tabControl1.TabIndex = 130;
            // 
            // cmbSaleNo
            // 
            this.cmbSaleNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbSaleNo.Location = new System.Drawing.Point(297, 98);
            this.cmbSaleNo.Name = "cmbSaleNo";
            this.cmbSaleNo.Size = new System.Drawing.Size(125, 19);
            this.cmbSaleNo.TabIndex = 129;
            // 
            // cmbKH
            // 
            this.cmbKH.Enabled = false;
            this.cmbKH.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbKH.Location = new System.Drawing.Point(297, 119);
            this.cmbKH.Name = "cmbKH";
            this.cmbKH.Size = new System.Drawing.Size(125, 19);
            this.cmbKH.TabIndex = 128;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(202, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 14);
            this.label4.Text = "销售客户";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(202, 98);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(93, 14);
            this.Label1.Text = "销售订单号";
            // 
            // XSJH_TEST1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cmbSaleNo);
            this.Controls.Add(this.cmbKH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Label1);
            this.Menu = this.mainMenu1;
            this.Name = "XSJH_TEST1";
            this.Text = "XSJH_TEST1";
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox conName;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGrid DG2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.TextBox tb_Quantity;
        internal System.Windows.Forms.TextBox txtTraceNo;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGrid DGsale;
        private System.Windows.Forms.TabControl tabControl1;
        internal System.Windows.Forms.TextBox cmbSaleNo;
        internal System.Windows.Forms.TextBox cmbKH;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label Label1;
    }
}