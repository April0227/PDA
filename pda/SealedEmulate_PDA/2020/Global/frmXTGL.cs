using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SealedEmulate_PDA._2020.Global
{
    public partial class frmXTGL : Form
    {
        public frmXTGL()
        {
            InitializeComponent();
        }

        private void ChangePwd_Click(object sender, EventArgs e)
        {
            frmPwd frm = new frmPwd();
            frm.Show();
        }

        private void btnZX_Click(object sender, EventArgs e)
        {
            frmMain.GetInstance().Hide();
            frmLogin.GetInstance().Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗？", "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

     
    }
}