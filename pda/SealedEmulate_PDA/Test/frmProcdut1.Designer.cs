namespace SealedEmulate_PDA
{
    partial class frmProcdut1
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
            this.tb_Quantity = new System.Windows.Forms.TextBox();
            this.txtTraceNo = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.DGpro = new System.Windows.Forms.DataGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ckwg = new System.Windows.Forms.CheckBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbPro = new System.Windows.Forms.TextBox();
            this.cmbProPlan = new System.Windows.Forms.TextBox();
            this.labType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_Quantity
            // 
            this.tb_Quantity.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tb_Quantity.Location = new System.Drawing.Point(76, 202);
            this.tb_Quantity.Name = "tb_Quantity";
            this.tb_Quantity.Size = new System.Drawing.Size(125, 19);
            this.tb_Quantity.TabIndex = 74;
            // 
            // txtTraceNo
            // 
            this.txtTraceNo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTraceNo.Location = new System.Drawing.Point(76, 183);
            this.txtTraceNo.Name = "txtTraceNo";
            this.txtTraceNo.ReadOnly = true;
            this.txtTraceNo.Size = new System.Drawing.Size(125, 19);
            this.txtTraceNo.TabIndex = 71;
            this.txtTraceNo.TextChanged += new System.EventHandler(this.txtTraceNo_TextChanged);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(8, 203);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(88, 14);
            this.Label3.Text = "发料数量";
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(148, 241);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(74, 25);
            this.btnclose.TabIndex = 73;
            this.btnclose.Text = "关闭";
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(3, 241);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 25);
            this.btnSave.TabIndex = 72;
            this.btnSave.Text = "添加";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(8, 184);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(88, 14);
            this.Label2.Text = "物料编码";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(3, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(69, 16);
            this.Label1.Text = "生产订单";
            // 
            // DGpro
            // 
            this.DGpro.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGpro.Location = new System.Drawing.Point(3, 45);
            this.DGpro.Name = "DGpro";
            this.DGpro.Size = new System.Drawing.Size(234, 115);
            this.DGpro.TabIndex = 81;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 14);
            this.label4.Text = "生产计划";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 14);
            this.label5.Text = "是否完工";
            // 
            // ckwg
            // 
            this.ckwg.Location = new System.Drawing.Point(76, 220);
            this.ckwg.Name = "ckwg";
            this.ckwg.Size = new System.Drawing.Size(100, 20);
            this.ckwg.TabIndex = 94;
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnGo.Location = new System.Drawing.Point(207, 6);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(30, 31);
            this.btnGo.TabIndex = 100;
            this.btnGo.Text = "go";
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(76, 164);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 19);
            this.textBox1.TabIndex = 107;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.LostFocus += new System.EventHandler(this.textBox1_LostFocus);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 14);
            this.label6.Text = "条码信息";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 39);
            this.button1.TabIndex = 114;
            this.button1.Text = "清";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbPro
            // 
            this.cmbPro.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbPro.Location = new System.Drawing.Point(76, 4);
            this.cmbPro.Name = "cmbPro";
            this.cmbPro.Size = new System.Drawing.Size(62, 19);
            this.cmbPro.TabIndex = 121;
            this.cmbPro.TextChanged += new System.EventHandler(this.cmbPro_TextChanged);
            this.cmbPro.LostFocus += new System.EventHandler(this.cmbPro_LostFocus);
            // 
            // cmbProPlan
            // 
            this.cmbProPlan.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cmbProPlan.Location = new System.Drawing.Point(76, 23);
            this.cmbProPlan.Name = "cmbProPlan";
            this.cmbProPlan.ReadOnly = true;
            this.cmbProPlan.Size = new System.Drawing.Size(125, 19);
            this.cmbProPlan.TabIndex = 122;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(144, 4);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(36, 16);
            // 
            // frmProcdut1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.labType);
            this.Controls.Add(this.cmbProPlan);
            this.Controls.Add(this.cmbPro);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.ckwg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DGpro);
            this.Controls.Add(this.tb_Quantity);
            this.Controls.Add(this.txtTraceNo);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProcdut1";
            this.Text = "生产发料";
            this.Load += new System.EventHandler(this.frmProcdut_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox tb_Quantity;
        internal System.Windows.Forms.TextBox txtTraceNo;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btnclose;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.DataGrid DGpro;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckwg;
        private System.Windows.Forms.Button btnGo;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.TextBox cmbPro;
        internal System.Windows.Forms.TextBox cmbProPlan;
        private System.Windows.Forms.Label labType;
    }
}