namespace SealedEmulate_PDA
{
    partial class InCome
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
            this.cmbStockNo = new System.Windows.Forms.ComboBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.CusTraceNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDelrow
            // 
            this.btnDelrow.Location = new System.Drawing.Point(5, 232);
            this.btnDelrow.Name = "btnDelrow";
            this.btnDelrow.Size = new System.Drawing.Size(72, 25);
            this.btnDelrow.TabIndex = 87;
            this.btnDelrow.Text = "删除行";
            this.btnDelrow.Click += new System.EventHandler(this.btnDelrow_Click);
            // 
            // txtSyb
            // 
            this.txtSyb.Enabled = false;
            this.txtSyb.Location = new System.Drawing.Point(111, 199);
            this.txtSyb.Name = "txtSyb";
            this.txtSyb.Size = new System.Drawing.Size(123, 23);
            this.txtSyb.TabIndex = 86;
            // 
            // DG
            // 
            this.DG.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DG.Location = new System.Drawing.Point(10, 30);
            this.DG.Name = "DG";
            this.DG.Size = new System.Drawing.Size(224, 135);
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
            this.Label5.Location = new System.Drawing.Point(10, 199);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(50, 17);
            this.Label5.Text = "事业部";
            // 
            // cmbStockNo
            // 
            this.cmbStockNo.Location = new System.Drawing.Point(86, 7);
            this.cmbStockNo.Name = "cmbStockNo";
            this.cmbStockNo.Size = new System.Drawing.Size(148, 23);
            this.cmbStockNo.TabIndex = 77;
            this.cmbStockNo.SelectedIndexChanged += new System.EventHandler(this.cmbStockNo_SelectedIndexChanged);
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(160, 232);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(80, 25);
            this.btnclose.TabIndex = 76;
            this.btnclose.Text = "关闭";
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(78, 232);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(80, 25);
            this.btnAddRow.TabIndex = 75;
            this.btnAddRow.Text = "添加";
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(10, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(70, 17);
            this.Label1.Text = "质检单号";
            // 
            // CusTraceNo
            // 
            this.CusTraceNo.Location = new System.Drawing.Point(111, 171);
            this.CusTraceNo.Name = "CusTraceNo";
            this.CusTraceNo.Size = new System.Drawing.Size(122, 23);
            this.CusTraceNo.TabIndex = 93;
            this.CusTraceNo.GotFocus += new System.EventHandler(this.CusTraceNo_GotFocus);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 19);
            this.label2.Text = "供应商追溯码";
            // 
            // InCome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.CusTraceNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDelrow);
            this.Controls.Add(this.txtSyb);
            this.Controls.Add(this.DG);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.cmbStockNo);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.Label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InCome";
            this.Text = "采购入库";
            this.Load += new System.EventHandler(this.InCome_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnDelrow;
        internal System.Windows.Forms.TextBox txtSyb;
        internal System.Windows.Forms.DataGrid DG;
        internal System.Windows.Forms.Splitter Splitter1;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cmbStockNo;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnAddRow;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox CusTraceNo;
        internal System.Windows.Forms.Label label2;
    }
}