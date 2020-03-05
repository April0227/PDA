using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;
using SealedEmulate_PDA.Model; 

namespace SealedEmulate_PDA._2020.Global
{
    public partial class frmPwd : Form
    {
        public frmPwd()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ����Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPwd_Load(object sender, EventArgs e)
        {
            txtName.Text = ConnModel.userName;
            txtPwd.Focus();
        }
        /// <summary>
        /// ԭʼ����У��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (txtPwd.Text.Trim() != ConnModel.userPwd)
            {
                label5.Text = "ԭʼ�������...";
            }
            else
            {
                label5.Text = "";
                if (txtPwdX.Text.Trim() != "" && txtPwdQR.Text.Trim() != "")
                {
                    button1.Enabled = true;
                }
                else
                {
                    txtPwdX.Focus();
                }
            }
        }
        /// <summary>
        /// ȷ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPwdQR_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = false;
            string val = txtPwdX.Text.Trim();
            if (val == "")
            {
                label5.Text = "������������...";
                txtPwdX.Focus();
                return;
            }
            if (val != txtPwdQR.Text.Trim())
            {
                label5.Text = "ȷ���������...";
            }
            else
            {
                if (txtPwd.Text.Trim() == ConnModel.userPwd)
                {
                    label5.Text = "";
                    button1.Enabled = true;
                }
                else
                {
                    label5.Text = "ԭʼ���벻��Ϊ��...";
                    txtPwd.Focus();
                }
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string name = ConnModel.userName;
            string pwd = txtPwdX.Text.Trim();
            UserDal dal = new UserDal();
            LoginState state = dal.ChangePwd(name, pwd);
            switch (state)
            {
                case LoginState.Ok:
                    Close();
                    frmMain.GetInstance().Hide();
                    frmLogin.GetInstance().Show();
                    break;
                case LoginState.PwdError:
                    MessageBox.Show("�����޸ĳ�ʱ,���Ժ�����...");
                    break;
            }


        }
        /// <summary>
        /// ����ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}