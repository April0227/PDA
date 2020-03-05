using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA._2020.Report;

namespace SealedEmulate_PDA._2020.Global
{
    public partial class frmBBGL : Form
    {
        public frmBBGL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YLZS frm = new YLZS();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OwhsBinCode frm = new OwhsBinCode();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            YLZS frm = new YLZS();
            frm.Show();
        }
    }
}