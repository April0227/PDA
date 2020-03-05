namespace SealedEmulate_PDA._2020
{
    partial class XSMK_KCZC
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
            this.lTKW = new System.Windows.Forms.Label();
            this.lFKW = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DataGrid1 = new System.Windows.Forms.DataGrid();
            this.txtDocEntry = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnR = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTCK = new System.Windows.Forms.ComboBox();
            this.txtTKW = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFCK = new System.Windows.Forms.ComboBox();
            this.txtFKW = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnInCome = new System.Windows.Forms.Button();
            this.DataGrid2 = new System.Windows.Forms.DataGrid();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lTKW
            // 
            this.lTKW.Location = new System.Drawing.Point(104, 209);
            this.lTKW.Name = "lTKW";
            this.lTKW.Size = new System.Drawing.Size(19, 20);
            this.lTKW.Visible = false;
            // 
            // lFKW
            // 
            this.lFKW.Location = new System.Drawing.Point(91, 209);
            this.lFKW.Name = "lFKW";
            this.lFKW.Size = new System.Drawing.Size(19, 20);
            this.lFKW.Visible = false;
            // 
            // txtItemCode
            // 
            this.txtItemCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtItemCode.Location = new System.Drawing.Point(71, 26);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.Size = new System.Drawing.Size(154, 19);
            this.txtItemCode.TabIndex = 251;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(237, 262);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DataGrid1);
            this.tabPage1.Controls.Add(this.txtDocEntry);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(229, 233);
            this.tabPage1.Text = "订单页";
            // 
            // DataGrid1
            // 
            this.DataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DataGrid1.Location = new System.Drawing.Point(3, 29);
            this.DataGrid1.Name = "DataGrid1";
            this.DataGrid1.Size = new System.Drawing.Size(223, 200);
            this.DataGrid1.TabIndex = 7;
            // 
            // txtDocEntry
            // 
            this.txtDocEntry.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtDocEntry.Location = new System.Drawing.Point(117, 5);
            this.txtDocEntry.Name = "txtDocEntry";
            this.txtDocEntry.Size = new System.Drawing.Size(93, 19);
            this.txtDocEntry.TabIndex = 5;
            this.txtDocEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocEntry_KeyDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(10, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.Text = "销售订单";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtItemName);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.lTKW);
            this.tabPage2.Controls.Add(this.lFKW);
            this.tabPage2.Controls.Add(this.txtItemCode);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.btnR);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.txtQty);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.cmbTCK);
            this.tabPage2.Controls.Add(this.txtTKW);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cmbFCK);
            this.tabPage2.Controls.Add(this.txtFKW);
            this.tabPage2.Controls.Add(this.label20);
            this.tabPage2.Controls.Add(this.label21);
            this.tabPage2.Controls.Add(this.txtBatch);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.txtBarcode);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(229, 233);
            this.tabPage2.Text = "扫描页";
            // 
            // txtItemName
            // 
            this.txtItemName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtItemName.Location = new System.Drawing.Point(71, 48);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.ReadOnly = true;
            this.txtItemName.Size = new System.Drawing.Size(154, 19);
            this.txtItemName.TabIndex = 261;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(1, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 20);
            this.label6.Text = "物料名称";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(1, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.Text = "物料编码";
            // 
            // btnR
            // 
            this.btnR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnR.Location = new System.Drawing.Point(129, 209);
            this.btnR.Name = "btnR";
            this.btnR.Size = new System.Drawing.Size(72, 20);
            this.btnR.TabIndex = 242;
            this.btnR.Text = "重置";
            this.btnR.Click += new System.EventHandler(this.btnR_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnAdd.Location = new System.Drawing.Point(13, 209);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 20);
            this.btnAdd.TabIndex = 241;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtQty.Location = new System.Drawing.Point(71, 185);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(154, 19);
            this.txtQty.TabIndex = 240;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(4, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.Text = "转储数量";
            // 
            // cmbTCK
            // 
            this.cmbTCK.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbTCK.Location = new System.Drawing.Point(71, 140);
            this.cmbTCK.Name = "cmbTCK";
            this.cmbTCK.Size = new System.Drawing.Size(154, 19);
            this.cmbTCK.TabIndex = 239;
            this.cmbTCK.SelectedIndexChanged += new System.EventHandler(this.cmbTCK_SelectedIndexChanged);
            // 
            // txtTKW
            // 
            this.txtTKW.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTKW.Location = new System.Drawing.Point(71, 163);
            this.txtTKW.Name = "txtTKW";
            this.txtTKW.ReadOnly = true;
            this.txtTKW.Size = new System.Drawing.Size(154, 19);
            this.txtTKW.TabIndex = 238;
            this.txtTKW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTKW_KeyDown);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(3, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.Text = "到仓库";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(3, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.Text = "到库位";
            // 
            // cmbFCK
            // 
            this.cmbFCK.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbFCK.Location = new System.Drawing.Point(71, 94);
            this.cmbFCK.Name = "cmbFCK";
            this.cmbFCK.Size = new System.Drawing.Size(154, 19);
            this.cmbFCK.TabIndex = 237;
            this.cmbFCK.SelectedIndexChanged += new System.EventHandler(this.cmbFCK_SelectedIndexChanged);
            // 
            // txtFKW
            // 
            this.txtFKW.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtFKW.Location = new System.Drawing.Point(71, 118);
            this.txtFKW.Name = "txtFKW";
            this.txtFKW.ReadOnly = true;
            this.txtFKW.Size = new System.Drawing.Size(154, 19);
            this.txtFKW.TabIndex = 236;
            this.txtFKW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFKW_KeyDown);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label20.Location = new System.Drawing.Point(3, 98);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(66, 20);
            this.label20.Text = "从仓库";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label21.Location = new System.Drawing.Point(3, 122);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(66, 20);
            this.label21.Text = "从库位";
            // 
            // txtBatch
            // 
            this.txtBatch.Enabled = false;
            this.txtBatch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBatch.Location = new System.Drawing.Point(71, 71);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.ReadOnly = true;
            this.txtBatch.Size = new System.Drawing.Size(154, 19);
            this.txtBatch.TabIndex = 235;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label17.Location = new System.Drawing.Point(4, 75);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 20);
            this.label17.Text = "批次";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtBarcode.Location = new System.Drawing.Point(71, 4);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(154, 19);
            this.txtBarcode.TabIndex = 234;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label15.Location = new System.Drawing.Point(3, 8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 20);
            this.label15.Text = "条码信息";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnDel);
            this.tabPage3.Controls.Add(this.btnInCome);
            this.tabPage3.Controls.Add(this.DataGrid2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(229, 233);
            this.tabPage3.Text = "过账页";
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnDel.Location = new System.Drawing.Point(171, 211);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(54, 20);
            this.btnDel.TabIndex = 83;
            this.btnDel.Text = "删除";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnInCome
            // 
            this.btnInCome.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnInCome.Location = new System.Drawing.Point(4, 211);
            this.btnInCome.Name = "btnInCome";
            this.btnInCome.Size = new System.Drawing.Size(61, 20);
            this.btnInCome.TabIndex = 82;
            this.btnInCome.Text = "过账";
            this.btnInCome.Click += new System.EventHandler(this.btnInCome_Click);
            // 
            // DataGrid2
            // 
            this.DataGrid2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DataGrid2.Location = new System.Drawing.Point(5, 1);
            this.DataGrid2.Name = "DataGrid2";
            this.DataGrid2.Size = new System.Drawing.Size(220, 204);
            this.DataGrid2.TabIndex = 81;
            // 
            // XSMK_KCZC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XSMK_KCZC";
            this.Text = "基于销售订单-库存转储";
            this.Load += new System.EventHandler(this.XSMK_KCZC_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lTKW;
        private System.Windows.Forms.Label lFKW;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGrid DataGrid1;
        private System.Windows.Forms.TextBox txtDocEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnR;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTCK;
        private System.Windows.Forms.TextBox txtTKW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFCK;
        private System.Windows.Forms.TextBox txtFKW;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnInCome;
        private System.Windows.Forms.DataGrid DataGrid2;
    }
}