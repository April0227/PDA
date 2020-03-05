using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA._2020.Global
{
    public partial class XSMK_XSJH_SQ : Form
    {
        public XSMK_XSJH_SQ()
        {
            InitializeComponent();
        } 
        /// <summary>
        /// —È÷§ ⁄»®’À∫≈
        /// </summary>
        private bool flag = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if ((this.textBox1.Text.Trim() == "manager") && (this.textBox2.Text.Trim() == "manager"))
            {
                flag = true; 
            }
            this.Close();
        }

        private void XSMK_XSJH_SQ_Closed(object sender, EventArgs e)
        {
            if (flag == true)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.No;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((this.textBox1.Text.Trim() == "manager") && (this.textBox2.Text.Trim() == "manager"))
                {
                    flag = true;
                }
                this.Close();
            } 
        } 
    }
}