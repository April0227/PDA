namespace SealedEmulate_PDA._2020.Report
{
    partial class OwhsBinCode
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
            this.page2Whs = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.page1Grid = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // page2Whs
            // 
            this.page2Whs.Location = new System.Drawing.Point(56, 5);
            this.page2Whs.Name = "page2Whs";
            this.page2Whs.Size = new System.Drawing.Size(171, 23);
            this.page2Whs.TabIndex = 34;
            this.page2Whs.SelectedIndexChanged += new System.EventHandler(this.page2Whs_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(4, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.Text = "仓库";
            // 
            // page1Grid
            // 
            this.page1Grid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.page1Grid.Location = new System.Drawing.Point(1, 34);
            this.page1Grid.Name = "page1Grid";
            this.page1Grid.Size = new System.Drawing.Size(237, 231);
            this.page1Grid.TabIndex = 36;
            // 
            // OwhsBinCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.page1Grid);
            this.Controls.Add(this.page2Whs);
            this.Controls.Add(this.label6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OwhsBinCode";
            this.Text = "查询仓库库位信息";
            this.Load += new System.EventHandler(this.OwhsBinCode_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox page2Whs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGrid page1Grid;
    }
}