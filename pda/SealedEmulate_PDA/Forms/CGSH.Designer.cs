namespace SealedEmulate_PDA.Forms
{
    partial class CGSH
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
            this.txtdocEntry = new System.Windows.Forms.TextBox();
            this.DataGrid1 = new System.Windows.Forms.DataGrid();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.comboWhs = new System.Windows.Forms.ComboBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labAbsEntry = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBinCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.labBatch = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnInCome = new System.Windows.Forms.Button();
            this.DataGrid2 = new System.Windows.Forms.DataGrid();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.DataGrid3 = new System.Windows.Forms.DataGrid();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 262);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtdocEntry);
            this.tabPage1.Controls.Add(this.DataGrid1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(226, 233);
            this.tabPage1.Text = "订单页";
            // 
            // txtdocEntry
            // 
            this.txtdocEntry.Location = new System.Drawing.Point(99, 5);
            this.txtdocEntry.Name = "txtdocEntry";
            this.txtdocEntry.Size = new System.Drawing.Size(100, 23);
            this.txtdocEntry.TabIndex = 77;
            this.txtdocEntry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdocEntry_KeyPress);
            // 
            // DataGrid1
            // 
            this.DataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DataGrid1.Location = new System.Drawing.Point(-1, 31);
            this.DataGrid1.Name = "DataGrid1";
            this.DataGrid1.Size = new System.Drawing.Size(229, 199);
            this.DataGrid1.TabIndex = 75;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 19);
            this.label9.Text = "采购质检单号";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.comboWhs);
            this.tabPage2.Controls.Add(this.txtItemCode);
            this.tabPage2.Controls.Add(this.txtBarcode);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.labAbsEntry);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtItemName);
            this.tabPage2.Controls.Add(this.btnReset);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.txtQty);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtBinCode);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtBatch);
            this.tabPage2.Controls.Add(this.labBatch);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(226, 233);
            this.tabPage2.Text = "扫描页";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(201, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 21);
            this.button1.TabIndex = 176;
            this.button1.Text = "清";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboWhs
            // 
            this.comboWhs.Location = new System.Drawing.Point(71, 125);
            this.comboWhs.Name = "comboWhs";
            this.comboWhs.Size = new System.Drawing.Size(154, 23);
            this.comboWhs.TabIndex = 166;
            this.comboWhs.SelectedIndexChanged += new System.EventHandler(this.comboWhs_SelectedIndexChanged);
            // 
            // txtItemCode
            // 
            this.txtItemCode.Enabled = false;
            this.txtItemCode.Location = new System.Drawing.Point(71, 37);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.Size = new System.Drawing.Size(154, 23);
            this.txtItemCode.TabIndex = 157;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(72, 7);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(154, 23);
            this.txtBarcode.TabIndex = 155;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.Text = "条码信息";
            // 
            // labAbsEntry
            // 
            this.labAbsEntry.Location = new System.Drawing.Point(84, 207);
            this.labAbsEntry.Name = "labAbsEntry";
            this.labAbsEntry.Size = new System.Drawing.Size(32, 16);
            this.labAbsEntry.Text = "0";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(4, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.Text = "仓库";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(4, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 20);
            this.label7.Text = "物料名称";
            // 
            // txtItemName
            // 
            this.txtItemName.Enabled = false;
            this.txtItemName.Location = new System.Drawing.Point(71, 66);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.ReadOnly = true;
            this.txtItemName.Size = new System.Drawing.Size(154, 23);
            this.txtItemName.TabIndex = 142;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnReset.Location = new System.Drawing.Point(122, 207);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(72, 20);
            this.btnReset.TabIndex = 140;
            this.btnReset.Text = "重置";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnAdd.Location = new System.Drawing.Point(6, 207);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 20);
            this.btnAdd.TabIndex = 139;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(72, 178);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(154, 23);
            this.txtQty.TabIndex = 138;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.Text = "入库数量";
            // 
            // txtBinCode
            // 
            this.txtBinCode.Location = new System.Drawing.Point(72, 152);
            this.txtBinCode.Name = "txtBinCode";
            this.txtBinCode.Size = new System.Drawing.Size(126, 23);
            this.txtBinCode.TabIndex = 137;
            this.txtBinCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBinCode_KeyPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.Text = "库位";
            // 
            // txtBatch
            // 
            this.txtBatch.Enabled = false;
            this.txtBatch.Location = new System.Drawing.Point(71, 96);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.ReadOnly = true;
            this.txtBatch.Size = new System.Drawing.Size(154, 23);
            this.txtBatch.TabIndex = 141;
            // 
            // labBatch
            // 
            this.labBatch.Location = new System.Drawing.Point(4, 100);
            this.labBatch.Name = "labBatch";
            this.labBatch.Size = new System.Drawing.Size(66, 20);
            this.labBatch.Text = "批次";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.Text = "物料编码";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnDel);
            this.tabPage3.Controls.Add(this.btnInCome);
            this.tabPage3.Controls.Add(this.DataGrid2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(226, 233);
            this.tabPage3.Text = "过账页";
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(169, 213);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(54, 20);
            this.btnDel.TabIndex = 73;
            this.btnDel.Text = "删除";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnInCome
            // 
            this.btnInCome.Location = new System.Drawing.Point(2, 213);
            this.btnInCome.Name = "btnInCome";
            this.btnInCome.Size = new System.Drawing.Size(61, 20);
            this.btnInCome.TabIndex = 71;
            this.btnInCome.Text = "过账";
            this.btnInCome.Click += new System.EventHandler(this.btnInCome_Click);
            // 
            // DataGrid2
            // 
            this.DataGrid2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DataGrid2.Location = new System.Drawing.Point(1, 0);
            this.DataGrid2.Name = "DataGrid2";
            this.DataGrid2.Size = new System.Drawing.Size(224, 207);
            this.DataGrid2.TabIndex = 70;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.DataGrid3);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(226, 233);
            this.tabPage4.Text = "库存信息";
            // 
            // DataGrid3
            // 
            this.DataGrid3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DataGrid3.Location = new System.Drawing.Point(0, -4);
            this.DataGrid3.Name = "DataGrid3";
            this.DataGrid3.Size = new System.Drawing.Size(227, 241);
            this.DataGrid3.TabIndex = 72;
            // 
            // CGSH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CGSH";
            this.Text = "采购入库";
            this.Load += new System.EventHandler(this.CGRK_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGrid DataGrid1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labAbsEntry;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBinCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label labBatch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnInCome;
        private System.Windows.Forms.DataGrid DataGrid2;
        private System.Windows.Forms.DataGrid DataGrid3;
        private System.Windows.Forms.ComboBox comboWhs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtdocEntry;
    }
}