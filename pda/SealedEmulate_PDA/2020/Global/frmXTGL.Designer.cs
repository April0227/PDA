namespace SealedEmulate_PDA._2020.Global
{
    partial class frmXTGL
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnZX = new System.Windows.Forms.Button();
            this.ChangePwd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnZX
            // 
            this.btnZX.Location = new System.Drawing.Point(41, 113);
            this.btnZX.Name = "btnZX";
            this.btnZX.Size = new System.Drawing.Size(156, 36);
            this.btnZX.TabIndex = 30;
            this.btnZX.Text = "系统注销";
            this.btnZX.Click += new System.EventHandler(this.btnZX_Click);
            // 
            // ChangePwd
            // 
            this.ChangePwd.Location = new System.Drawing.Point(41, 30);
            this.ChangePwd.Name = "ChangePwd";
            this.ChangePwd.Size = new System.Drawing.Size(156, 36);
            this.ChangePwd.TabIndex = 29;
            this.ChangePwd.Text = "修改密码";
            this.ChangePwd.Click += new System.EventHandler(this.ChangePwd_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(41, 202);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(156, 36);
            this.btnExit.TabIndex = 28;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmXTGL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnZX);
            this.Controls.Add(this.ChangePwd);
            this.Controls.Add(this.btnExit);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "frmXTGL";
            this.Text = "系统管理";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnZX;
        internal System.Windows.Forms.Button ChangePwd;
        internal System.Windows.Forms.Button btnExit;
    }
}