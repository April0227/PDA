using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SealedEmulate_PDA._2020.Global
{
    public partial class frmCGGL : Form
    {
        public frmCGGL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �ɹ��ջ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            CGMK_CGSH frm = new CGMK_CGSH();
            frm.Show();
        }
        /// <summary>
        /// �ɹ��˻�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            CGMK_CGTH frm = new CGMK_CGTH();
            frm.Show();
        }
        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}