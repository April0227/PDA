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
        /// ���۽���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            XSJH frm = new XSJH();
            frm.Show();
        }
        /// <summary>
        /// �����˻�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            XSTH frm = new XSTH();
            frm.Show();
        }
        /// <summary>
        /// ���ת�� - ���ڶ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            XSKCZC frm = new XSKCZC();
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