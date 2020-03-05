using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SealedEmulate_PDA.Forms
{
    public partial class frmCGGL : Form
    {
        public frmCGGL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 采购收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            CGSH frm = new CGSH();
            frm.Show();
        }
        /// <summary>
        /// 采购退货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            CGTH frm = new CGTH();
            frm.Show();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}