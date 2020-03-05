namespace SealedEmulate_PDA
{
    partial class frmSCSH1
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
            this.DGpro = new System.Windows.Forms.DataGrid();
            this.tb_Quantity = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckwg = new System.Windows.Forms.CheckBox();
            this.QRCode = new System.Windows.Forms.TextBox();
            this.txtBC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(151, 238);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(74, 25);
            this.btnclose.TabIndex = 75;
            this.btnclose.Text = "关闭";
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(11, 238);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 25);
            this.btnSave.TabIndex = 74;
            this.btnSave.Text = "添加";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(11, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(69, 16);
            this.Label1.Text = "条码信息";
            // 
            // DGpro
            // 
            this.DGpro.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGpro.Location = new System.Drawing.Point(1, 34);
            this.DGpro.Name = "DGpro";
            this.DGpro.Size = new System.Drawing.Size(237, 148);
            this.DGpro.TabIndex = 82;
            // 
            // tb_Quantity
            // 
            this.tb_Quantity.Location = new System.Drawing.Point(126, 188);
            this.tb_Quantity.Name = "tb_Quantity";
            this.tb_Quantity.Size = new System.Drawing.Size(100, 23);
            this.tb_Quantity.TabIndex = 86;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(11, 188);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(93, 15);
            this.Label3.Text = "收货数量";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.Text = "是否完工";
            // 
            // ckwg
            // 
            this.ckwg.Location = new System.Drawing.Point(122, 214);
            this.ckwg.Name = "ckwg";
            this.ckwg.Size = new System.Drawing.Size(100, 20);
            this.ckwg.TabIndex = 91;
            // 
            // QRCode
            // 
            this.QRCode.Location = new System.Drawing.Point(77, 8);
            this.QRCode.Name = "QRCode";
            this.QRCode.Size = new System.Drawing.Size(160, 23);
            this.QRCode.TabIndex = 106;
            this.QRCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QRCode_KeyDown);
            // 
            // txtBC
            // 
            this.txtBC.Location = new System.Drawing.Point(1, 162);
            this.txtBC.Name = "txtBC";
            this.txtBC.Size = new System.Drawing.Size(10, 23);
            this.txtBC.TabIndex = 97;
            this.txtBC.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 12);
            this.label4.Text = "生产日期及班次";
            this.label4.Visible = false;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(151, 214);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(44, 20);
            // 
            // frmSCSH1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.labType);
            this.Controls.Add(this.QRCode);
            this.Controls.Add(this.txtBC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ckwg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Quantity);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.DGpro);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnSave);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSCSH1";
            this.Text = "生产收货";
            this.Load += new System.EventHandler(this.frmSCSH_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.DataGrid DGpro;
        internal System.Windows.Forms.TextBox tb_Quantity;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckwg;
        internal System.Windows.Forms.TextBox QRCode;
        private System.Windows.Forms.TextBox txtBC;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labType;
    }
}