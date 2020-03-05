namespace SealedEmulate_PDA.Forms
{
    partial class CGTH
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
            this.DataGrid1 = new System.Windows.Forms.DataGrid();
            this.txtdocEntry = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtBinCode = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.labBatch = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DataGrid2 = new System.Windows.Forms.DataGrid();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnInCome = new System.Windows.Forms.Button();
            this.comboWhs = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 265);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DataGrid1);
            this.tabPage1.Controls.Add(this.txtdocEntry);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(226, 236);
            this.tabPage1.Text = "订单页";
            // 
            // DataGrid1
            // 
            this.DataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DataGrid1.Location = new System.Drawing.Point(3, 28);
            this.DataGrid1.Name = "DataGrid1";
            this.DataGrid1.Size = new System.Drawing.Size(220, 205);
            this.DataGrid1.TabIndex = 2;
            // 
            // txtdocEntry
            // 
            this.txtdocEntry.Location = new System.Drawing.Point(102, 2);
            this.txtdocEntry.Name = "txtdocEntry";
            this.txtdocEntry.Size = new System.Drawing.Size(100, 23);
            this.txtdocEntry.TabIndex = 1;
            this.txtdocEntry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdocEntry_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.Text = "采购收货单";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.comboWhs);
            this.tabPage2.Controls.Add(this.txtBinCode);
            this.tabPage2.Controls.Add(this.btnReset);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.txtQty);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtItemCode);
            this.tabPage2.Controls.Add(this.txtBarcode);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtItemName);
            this.tabPage2.Controls.Add(this.txtBatch);
            this.tabPage2.Controls.Add(this.labBatch);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(226, 236);
            this.tabPage2.Text = "扫描页";
            // 
            // txtBinCode
            // 
            this.txtBinCode.Location = new System.Drawing.Point(68, 155);
            this.txtBinCode.Name = "txtBinCode";
            this.txtBinCode.ReadOnly = true;
            this.txtBinCode.Size = new System.Drawing.Size(154, 23);
            this.txtBinCode.TabIndex = 195;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnReset.Location = new System.Drawing.Point(117, 213);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(72, 20);
            this.btnReset.TabIndex = 179;
            this.btnReset.Text = "重置";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnAdd.Location = new System.Drawing.Point(1, 213);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 20);
            this.btnAdd.TabIndex = 178;
            this.btnAdd.Text = "添加";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(67, 184);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(154, 23);
            this.txtQty.TabIndex = 177;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(-1, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.Text = "退货数量";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(0, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.Text = "仓库";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.Text = "库位";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Enabled = false;
            this.txtItemCode.Location = new System.Drawing.Point(69, 33);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.Size = new System.Drawing.Size(154, 23);
            this.txtItemCode.TabIndex = 165;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(69, 3);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.ReadOnly = true;
            this.txtBarcode.Size = new System.Drawing.Size(154, 23);
            this.txtBarcode.TabIndex = 164;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.Text = "条码信息";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(1, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 20);
            this.label7.Text = "物料名称";
            // 
            // txtItemName
            // 
            this.txtItemName.Enabled = false;
            this.txtItemName.Location = new System.Drawing.Point(68, 65);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.ReadOnly = true;
            this.txtItemName.Size = new System.Drawing.Size(154, 23);
            this.txtItemName.TabIndex = 163;
            // 
            // txtBatch
            // 
            this.txtBatch.Enabled = false;
            this.txtBatch.Location = new System.Drawing.Point(68, 96);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.ReadOnly = true;
            this.txtBatch.Size = new System.Drawing.Size(154, 23);
            this.txtBatch.TabIndex = 162;
            // 
            // labBatch
            // 
            this.labBatch.Location = new System.Drawing.Point(1, 100);
            this.labBatch.Name = "labBatch";
            this.labBatch.Size = new System.Drawing.Size(66, 20);
            this.labBatch.Text = "批次";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(2, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.Text = "物料编码";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnDel);
            this.tabPage3.Controls.Add(this.btnInCome);
            this.tabPage3.Controls.Add(this.DataGrid2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(226, 236);
            this.tabPage3.Text = "过账页";
            // 
            // DataGrid2
            // 
            this.DataGrid2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DataGrid2.Location = new System.Drawing.Point(3, 3);
            this.DataGrid2.Name = "DataGrid2";
            this.DataGrid2.Size = new System.Drawing.Size(220, 204);
            this.DataGrid2.TabIndex = 0;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(169, 213);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(54, 20);
            this.btnDel.TabIndex = 75;
            this.btnDel.Text = "删除";
            // 
            // btnInCome
            // 
            this.btnInCome.Location = new System.Drawing.Point(2, 213);
            this.btnInCome.Name = "btnInCome";
            this.btnInCome.Size = new System.Drawing.Size(61, 20);
            this.btnInCome.TabIndex = 74;
            this.btnInCome.Text = "过账";
            // 
            // comboWhs
            // 
            this.comboWhs.Location = new System.Drawing.Point(68, 126);
            this.comboWhs.Name = "comboWhs";
            this.comboWhs.Size = new System.Drawing.Size(154, 23);
            this.comboWhs.TabIndex = 203;
            // 
            // CGTH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CGTH";
            this.Text = "采购退货";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtdocEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid DataGrid1;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label labBatch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGrid DataGrid2;
        private System.Windows.Forms.TextBox txtBinCode;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnInCome;
        private System.Windows.Forms.ComboBox comboWhs;
    }
}