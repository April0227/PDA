namespace SealedEmulate_PDA._2020.Report
{
    partial class YLZS
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
            this.page2ItemName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.page2ItemCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.page1Grid = new System.Windows.Forms.DataGrid();
            this.page2Batch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // page2ItemName
            // 
            this.page2ItemName.Location = new System.Drawing.Point(58, 35);
            this.page2ItemName.Name = "page2ItemName";
            this.page2ItemName.Size = new System.Drawing.Size(173, 23);
            this.page2ItemName.TabIndex = 9;
            this.page2ItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.page2ItemName_KeyDown);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(5, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 20);
            this.label4.Text = "物料名称";
            // 
            // page2ItemCode
            // 
            this.page2ItemCode.Location = new System.Drawing.Point(58, 6);
            this.page2ItemCode.Name = "page2ItemCode";
            this.page2ItemCode.Size = new System.Drawing.Size(173, 23);
            this.page2ItemCode.TabIndex = 8;
            this.page2ItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.page2ItemCode_KeyDown);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(5, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.Text = "物料编码";
            // 
            // page1Grid
            // 
            this.page1Grid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.page1Grid.Location = new System.Drawing.Point(2, 92);
            this.page1Grid.Name = "page1Grid";
            this.page1Grid.Size = new System.Drawing.Size(235, 171);
            this.page1Grid.TabIndex = 12;
            // 
            // page2Batch
            // 
            this.page2Batch.Location = new System.Drawing.Point(58, 65);
            this.page2Batch.Name = "page2Batch";
            this.page2Batch.Size = new System.Drawing.Size(173, 23);
            this.page2Batch.TabIndex = 16;
            this.page2Batch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.page2Batch_KeyDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(5, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.Text = "原料批次";
            // 
            // YLZS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.page2Batch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.page1Grid);
            this.Controls.Add(this.page2ItemName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.page2ItemCode);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YLZS";
            this.Text = "原料追溯";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox page2ItemName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox page2ItemCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGrid page1Grid;
        private System.Windows.Forms.TextBox page2Batch;
        private System.Windows.Forms.Label label1;
    }
}