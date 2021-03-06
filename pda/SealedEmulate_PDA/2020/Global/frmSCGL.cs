using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SealedEmulate_PDA._2020.Global
{
    public partial class frmSCGL : Form
    {
        public frmSCGL()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 生产发料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SCMK_SCFL frm = new SCMK_SCFL();
            frm.Show();
        }
        /// <summary>
        /// 生产收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            SCMK_SCSH frm = new SCMK_SCSH();
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