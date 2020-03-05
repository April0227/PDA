using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Forms;

namespace SealedEmulate_PDA._2020.Global
{
    public partial class frmMain : Form
    {
        private static frmMain Instance;
        public static frmMain GetInstance()
        {
            return Instance ?? (Instance = new frmMain());
        }
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 采购管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {
            frmCGGL frm = new frmCGGL();
            frm.Show();
        }
        /// <summary>
        /// 销售管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
        {
            frmXSGL frm = new frmXSGL();
            frm.Show();
        }
        /// <summary>
        /// 生产管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3_Click(object sender, EventArgs e)
        {
            frmSCGL frm = new frmSCGL();
            frm.Show();
        }
        /// <summary>
        /// 库存管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn4_Click(object sender, EventArgs e)
        {
            frmKCGL frm = new frmKCGL();
            frm.Show();
        }
        /// <summary>
        /// 相关报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5_Click(object sender, EventArgs e)
        {
            frmBBGL frm = new frmBBGL();
            frm.Show();
        }
        /// <summary>
        /// 系统管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn6_Click(object sender, EventArgs e)
        {
            frmXTGL frm = new frmXTGL();
            frm.Show(); 
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Closing(object sender, CancelEventArgs e)
        {

            if (MessageBox.Show("确定要退出系统吗？", "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Exit();
            }
        }














        #region 按钮隐藏

        //private void btnStock_Click(object sender, EventArgs e)
        //{
        //    this.btnStock.Enabled = false;
        //    InCome1 form = new InCome1();
        //    form.Show();
        //    btnStock.Enabled = true;
        //}
        //private void btnExit_Click_1(object sender, EventArgs e)
        //{
        //    this.btnExit.Enabled = false;
        //    if (MessageBox.Show("确定要退出系统吗？", "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
        //    {
        //        this.Close();
        //        Application.Exit();
        //    }
        //    else
        //    {
        //        this.btnExit.Enabled = true;
        //    }
        //}

        //private void btnIssueProduce_Click_1(object sender, EventArgs e)
        //{
        //    btnIssueProduce.Enabled = false;
        //    frmProcdut1 frm = new frmProcdut1();
        //    frm.Show();
        //    btnIssueProduce.Enabled = true;
        //}

        //private void BtnSCSH_Click(object sender, EventArgs e)
        //{
        //    BtnSCSH.Enabled = false;
        //    frmSCSH1 frm = new frmSCSH1();
        //    frm.Show();
        //    BtnSCSH.Enabled = true;
        //}

        //private void btnSale_Click(object sender, EventArgs e)
        //{
        //    btnSale.Enabled = false;
        //    frmXSJH2 frm = new frmXSJH2();
        //    frm.Show();
        //    btnSale.Enabled = true;
        //}

        //private void ChangePwd_Click(object sender, EventArgs e)
        //{
        //    ChangePwd.Enabled = false;
         
        //    ChangePwd.Enabled = true;
        //}

        //private void btnZX_Click(object sender, EventArgs e)
        //{
        //    btnZX.Enabled = false;
        //    frmMain.GetInstance().Hide();
        //    frmLogin.GetInstance().Show();
        //    btnZX.Enabled = true;
        //}

        //private void Btnkczc_Click(object sender, EventArgs e)
        //{
        //    Btnkczc.Enabled = false;
        //    frmKCZC frm = new frmKCZC();
        //    frm.Show();
        //    Btnkczc.Enabled = true;
        //}
        #endregion

       

       

      

      

       


       



    }
}