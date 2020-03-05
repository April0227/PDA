namespace SealedEmulate_PDA.Forms
{
    partial class XSJH
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
            this.checkBoxBatch = new System.Windows.Forms.CheckBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.txtDocEntry = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labKcQty = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGrid2 = new System.Windows.Forms.DataGrid();
            this.txtQuanTity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBrCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxBatch);
            this.tabPage1.Controls.Add(this.dataGrid1);
            this.tabPage1.Controls.Add(this.txtDocEntry);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(226, 233);
            this.tabPage1.Text = "订单页";
            // 
            // checkBoxBatch
            // 
            this.checkBoxBatch.Enabled = false;
            this.checkBoxBatch.Location = new System.Drawing.Point(132, 5);
            this.checkBoxBatch.Name = "checkBoxBatch";
            this.checkBoxBatch.Size = new System.Drawing.Size(91, 20);
            this.checkBoxBatch.TabIndex = 10;
            this.checkBoxBatch.Text = "指定批次";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 29);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(220, 200);
            this.dataGrid1.TabIndex = 2;
            // 
            // txtDocEntry
            // 
            this.txtDocEntry.Location = new System.Drawing.Point(74, 3);
            this.txtDocEntry.Name = "txtDocEntry";
            this.txtDocEntry.Size = new System.Drawing.Size(52, 23);
            this.txtDocEntry.TabIndex = 1;
            this.txtDocEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocEntry_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.Text = "销售订单";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labKcQty);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.dataGrid2);
            this.tabPage2.Controls.Add(this.txtQuanTity);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtBrCode);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(226, 233);
            this.tabPage2.Text = "扫描过账页";
            // 
            // labKcQty
            // 
            this.labKcQty.Location = new System.Drawing.Point(142, 27);
            this.labKcQty.Name = "labKcQty";
            this.labKcQty.Size = new System.Drawing.Size(29, 18);
            this.labKcQty.Text = "0";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(186, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 20);
            this.button2.TabIndex = 6;
            this.button2.Text = "过账";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(203, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "清";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGrid2
            // 
            this.dataGrid2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid2.Location = new System.Drawing.Point(3, 51);
            this.dataGrid2.Name = "dataGrid2";
            this.dataGrid2.Size = new System.Drawing.Size(220, 179);
            this.dataGrid2.TabIndex = 4;
            // 
            // txtQuanTity
            // 
            this.txtQuanTity.Location = new System.Drawing.Point(74, 26);
            this.txtQuanTity.Name = "txtQuanTity";
            this.txtQuanTity.Size = new System.Drawing.Size(62, 23);
            this.txtQuanTity.TabIndex = 3;
            this.txtQuanTity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuanTity_KeyDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.Text = "交货数量";
            // 
            // txtBrCode
            // 
            this.txtBrCode.Location = new System.Drawing.Point(74, 2);
            this.txtBrCode.Name = "txtBrCode";
            this.txtBrCode.Size = new System.Drawing.Size(125, 23);
            this.txtBrCode.TabIndex = 1;
            this.txtBrCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrCode_KeyDown);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.Text = "条码信息";
            // 
            // XSJH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XSJH";
            this.Text = "销售交货";
            this.Load += new System.EventHandler(this.XSJH_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtDocEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGrid dataGrid2;
        private System.Windows.Forms.TextBox txtQuanTity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBrCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labKcQty;
        private System.Windows.Forms.CheckBox checkBoxBatch;
    }
}