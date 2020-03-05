using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SealedEmulate_PDA._2020.Global
{
    public partial class frmKCGL : Form
    {
        public frmKCGL()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ¿â´æ×ª´¢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            KCMK_KCZC frm = new KCMK_KCZC();
            frm.Show();
        }
        /// <summary>
        /// ¹Ø±Õ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}