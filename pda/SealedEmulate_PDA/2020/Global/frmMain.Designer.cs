namespace SealedEmulate_PDA._2020.Global
{
    partial class frmMain
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
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(16, 14);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(73, 49);
            this.btn1.TabIndex = 29;
            this.btn1.Text = "采购管理";
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(127, 14);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(73, 49);
            this.btn2.TabIndex = 30;
            this.btn2.Text = "销售管理";
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn3
            // 
            this.btn3.Location = new System.Drawing.Point(16, 79);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(73, 49);
            this.btn3.TabIndex = 31;
            this.btn3.Text = "生产管理";
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // btn4
            // 
            this.btn4.Location = new System.Drawing.Point(127, 79);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(73, 49);
            this.btn4.TabIndex = 32;
            this.btn4.Text = "库存管理";
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // btn5
            // 
            this.btn5.Location = new System.Drawing.Point(16, 150);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(73, 49);
            this.btn5.TabIndex = 33;
            this.btn5.Text = "相关报表";
            this.btn5.Click += new System.EventHandler(this.btn5_Click);
            // 
            // btn6
            // 
            this.btn6.Location = new System.Drawing.Point(127, 150);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(73, 49);
            this.btn6.TabIndex = 34;
            this.btn6.Text = "系统管理";
            this.btn6.Click += new System.EventHandler(this.btn6_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "物流数据采集";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
    }
}