namespace SealedEmulate_PDA
{
    partial class InCome1
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
            this.btnDelrow = new System.Windows.Forms.Button();
            this.txtSyb = new System.Windows.Forms.TextBox();
            this.DG = new System.Windows.Forms.DataGrid();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.Label5 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.CusTraceNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Number = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.QRCode = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.GYS = new System.Windows.Forms.TextBox();
            this.cmbStockNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDelrow
            // 
            this.btnDelrow.Location = new System.Drawing.Point(5, 240);
            this.btnDelrow.Name = "btnDelrow";
            this.btnDelrow.Size = new System.Drawing.Size(72, 25);
            this.btnDelrow.TabIndex = 87;
            this.btnDelrow.Text = "删除行";
            this.btnDelrow.Click += new System.EventHandler(this.btnDelrow_Click);
            // 
            // txtSyb
            // 
            this.txtSyb.Enabled = false;
            this.txtSyb.Location = new System.Drawing.Point(80, 215);
            this.txtSyb.Name = "txtSyb";
            this.txtSyb.Size = new System.Drawing.Size(123, 23);
            this.txtSyb.TabIndex = 6;
            // 
            // DG
            // 
            this.DG.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DG.Location = new System.Drawing.Point(10, 49);
            this.DG.Name = "DG";
            this.DG.Size = new System.Drawing.Size(224, 93);
            this.DG.TabIndex = 84;
            // 
            // Splitter1
            // 
            this.Splitter1.Location = new System.Drawing.Point(0, 0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(3, 268);
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(9, 222);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(50, 17);
            this.Label5.Text = "事业部";
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(160, 240);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(80, 25);
            this.btnclose.TabIndex = 76;
            this.btnclose.Text = "关闭";
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(78, 240);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(80, 25);
            this.btnAddRow.TabIndex = 75;
            this.btnAddRow.Text = "添加";
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(10, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(70, 17);
            this.Label1.Text = "质检单号";
            // 
            // CusTraceNo
            // 
            this.CusTraceNo.Location = new System.Drawing.Point(80, 192);
            this.CusTraceNo.Name = "CusTraceNo";
            this.CusTraceNo.Size = new System.Drawing.Size(122, 23);
            this.CusTraceNo.TabIndex = 5;
            this.CusTraceNo.GotFocus += new System.EventHandler(this.CusTraceNo_GotFocus);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 34);
            this.label2.Text = "供应商追溯码";
            // 
            // Number
            // 
            this.Number.Location = new System.Drawing.Point(80, 169);
            this.Number.Name = "Number";
            this.Number.Size = new System.Drawing.Size(122, 23);
            this.Number.TabIndex = 4;
            this.Number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            this.Number.LostFocus += new System.EventHandler(this.Number_LostFocus);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 19);
            this.label3.Text = "数量";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 19);
            this.label4.Text = "条码信息";
            // 
            // QRCode
            // 
            this.QRCode.Location = new System.Drawing.Point(80, 146);
            this.QRCode.Name = "QRCode";
            this.QRCode.Size = new System.Drawing.Size(122, 23);
            this.QRCode.TabIndex = 3;
            this.QRCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QRCode_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(206, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 39);
            this.button1.TabIndex = 115;
            this.button1.Text = "清";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GYS
            // 
            this.GYS.Enabled = false;
            this.GYS.Location = new System.Drawing.Point(86, 24);
            this.GYS.Name = "GYS";
            this.GYS.Size = new System.Drawing.Size(122, 23);
            this.GYS.TabIndex = 2;
            // 
            // cmbStockNo
            // 
            this.cmbStockNo.Location = new System.Drawing.Point(86, 1);
            this.cmbStockNo.Name = "cmbStockNo";
            this.cmbStockNo.Size = new System.Drawing.Size(122, 23);
            this.cmbStockNo.TabIndex = 1;
            this.cmbStockNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbStockNo_KeyPress);
            this.cmbStockNo.LostFocus += new System.EventHandler(this.cmbStockNo_LostFocus);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 17);
            this.label6.Text = "供应商";
            // 
            // InCome1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbStockNo);
            this.Controls.Add(this.GYS);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.QRCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Number);
            this.Controls.Add(this.CusTraceNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDelrow);
            this.Controls.Add(this.txtSyb);
            this.Controls.Add(this.DG);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.Label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InCome1";
            this.Text = "采购收货";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.InCome_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnDelrow;
        internal System.Windows.Forms.TextBox txtSyb;
        internal System.Windows.Forms.DataGrid DG;
        internal System.Windows.Forms.Splitter Splitter1;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnAddRow;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox CusTraceNo;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox Number;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox QRCode;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.TextBox GYS;
        internal System.Windows.Forms.TextBox cmbStockNo;
        internal System.Windows.Forms.Label label6;
    }
}