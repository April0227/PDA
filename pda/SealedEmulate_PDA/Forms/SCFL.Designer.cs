namespace SealedEmulate_PDA.Forms
{
    partial class SCFL
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDocEntry = new System.Windows.Forms.TextBox();
            this.checkBoxBatch = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.labKcQty = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGrid2 = new System.Windows.Forms.DataGrid();
            this.txtQuanTity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBrCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label4);
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
            this.tabPage2.Size = new System.Drawing.Size(229, 233);
            this.tabPage2.Text = "扫描过账页";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxBatch);
            this.tabPage1.Controls.Add(this.txtDocEntry);
            this.tabPage1.Controls.Add(this.dataGrid1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(229, 233);
            this.tabPage1.Text = "订单页";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 29);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(223, 200);
            this.dataGrid1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.Text = "生产订单";
            // 
            // txtDocEntry
            // 
            this.txtDocEntry.Location = new System.Drawing.Point(72, 3);
            this.txtDocEntry.Name = "txtDocEntry";
            this.txtDocEntry.Size = new System.Drawing.Size(52, 23);
            this.txtDocEntry.TabIndex = 12;
            // 
            // checkBoxBatch
            // 
            this.checkBoxBatch.Enabled = false;
            this.checkBoxBatch.Location = new System.Drawing.Point(130, 5);
            this.checkBoxBatch.Name = "checkBoxBatch";
            this.checkBoxBatch.Size = new System.Drawing.Size(91, 20);
            this.checkBoxBatch.TabIndex = 13;
            this.checkBoxBatch.Text = "指定批次";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(237, 262);
            this.tabControl1.TabIndex = 1;
            // 
            // labKcQty
            // 
            this.labKcQty.Location = new System.Drawing.Point(143, 27);
            this.labKcQty.Name = "labKcQty";
            this.labKcQty.Size = new System.Drawing.Size(29, 18);
            this.labKcQty.Text = "0";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.button2.Location = new System.Drawing.Point(187, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 20);
            this.button2.TabIndex = 14;
            this.button2.Text = "过账";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 13;
            this.button1.Text = "清";
            // 
            // dataGrid2
            // 
            this.dataGrid2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid2.Location = new System.Drawing.Point(4, 80);
            this.dataGrid2.Name = "dataGrid2";
            this.dataGrid2.Size = new System.Drawing.Size(220, 150);
            this.dataGrid2.TabIndex = 12;
            // 
            // txtQuanTity
            // 
            this.txtQuanTity.Location = new System.Drawing.Point(75, 26);
            this.txtQuanTity.Name = "txtQuanTity";
            this.txtQuanTity.Size = new System.Drawing.Size(62, 23);
            this.txtQuanTity.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.Text = "发料数量";
            // 
            // txtBrCode
            // 
            this.txtBrCode.Location = new System.Drawing.Point(75, 2);
            this.txtBrCode.Name = "txtBrCode";
            this.txtBrCode.Size = new System.Drawing.Size(125, 23);
            this.txtBrCode.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.Text = "条码信息";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(204, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(20, 20);
            this.button3.TabIndex = 20;
            this.button3.Text = "清";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.Text = "机器条码";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 23);
            this.textBox1.TabIndex = 18;
            // 
            // SCFL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SCFL";
            this.Text = "生产发料";
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBoxBatch;
        private System.Windows.Forms.TextBox txtDocEntry;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label labKcQty;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid dataGrid2;
        private System.Windows.Forms.TextBox txtQuanTity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBrCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;

    }
}