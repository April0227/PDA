using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SealedEmulate_PDA.Forms
{
    public partial class frmXSGL : Form
    {
        public frmXSGL()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 销售交货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            XSJH frm = new XSJH();
            frm.Show();
        }
        /// <summary>
        /// 销售退货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            XSTH frm = new XSTH();
            frm.Show();
        }
        /// <summary>
        /// 库存转储 - 基于订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            XSKCZC frm = new XSKCZC();
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