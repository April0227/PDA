using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;
using SealedEmulate_PDA.Dal;
using System.IO;
using System.IO.Ports;
using System.Xml;
using Newtonsoft.Json;
using SealedEmulate_PDA._2020.Global;
namespace SealedEmulate_PDA
{
    public partial class frmLogin : Form
    {
        UserDal userDal = new UserDal();
        private static frmLogin Instance;
        public static frmLogin GetInstance()
        {
            return Instance ?? (Instance = new frmLogin());
        }
        public frmLogin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            #region DataTable תjson����
            //DataTable dt = new DataTable();
            //dt.Columns.Add("A");
            //dt.Columns.Add("B");
            //dt.Columns.Add("C");
            //dt.Columns.Add("D");
            //dt.Columns.Add("E");
            //DataRow row = dt.NewRow();
            //row["A"] = "1";
            //row["B"] = "2";
            //row["C"] = "3";
            //row["D"] = "4";
            //row["E"] = "5";
            //dt.Rows.Add(row);
            //string JsonString = string.Empty;
            //if (dt.Rows.Count > 0)
            //{
            //    JsonString = JsonConvert.SerializeObject(dt);
            //}
            //MessageBox.Show(JsonString); 
            #endregion 
            InintConn();
            BindUser();
        }
        /// <summary>
        /// �����½
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogin.Enabled = false;
                CheckLogIn();
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������" + ex.Message, "����");
            }

        }
        /// <summary>
        /// �����½����
        /// </summary>
        private void CheckLogIn()
        {
            if (cmbUser.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ���¼�û�");
                return;
            }
            string msg = string.Empty;
            LoginState state = userDal.Login(cmbUser.Text.Trim(), txtpass.Text.Trim(), out msg);
            switch (state)
            {
                case LoginState.Ok:
                    ConnModel.userName = cmbUser.Text.Trim();
                    ConnModel.userPwd = txtpass.Text.Trim();
                    frmMain form = frmMain.GetInstance();
                    form.Show();
                    this.Hide();
                    break;
                case LoginState.PwdError:
                    MessageBox.Show(msg);
                    break;
                case LoginState.NameError:
                    MessageBox.Show(msg);
                    break;
            }
            btnLogin.Enabled = true;
        }
        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                //���ϵ���
                //���ϼ�
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                //���µ���
                //���¼�
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                //�����
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                //���Ҽ�
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                //Enter
                try
                {
                    CheckLogIn();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��������" + ex.Message, "����");
                }
            }
        }
        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        private void InintConn()
        {
            XmlNodeList nodeList = LoadNodeList();
            XmlNode node = nodeList.Item(0);
            if (node != null)
            {
                ConnModel.P_ConnectString = node.SelectSingleNode("conn").InnerText;
                ConnModel.commonDB = node.SelectSingleNode("CommonDB").InnerText;
                ConnModel.Api = node.SelectSingleNode("Api").InnerText;
            }
        }
        /// <summary>
        /// ��ȡ����ΪPDA�������û�
        /// </summary>
        private void BindUser()
        {
            List<string> list = userDal.GetUser();
            if (list != null)
            {
                foreach (string s in list)
                {
                    cmbUser.Items.Add(s);
                }
                if (cmbUser.Items.Count > 0)
                {
                    cmbUser.SelectedIndex = 0;
                }
            }
        }
        /// <summary>
        /// ���������ļ�����NodeList
        /// </summary>
        /// <returns></returns>
        private XmlNodeList LoadNodeList()
        {
            string apppath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            string fileName = apppath + "\\DataBase.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            return doc.SelectSingleNode("root").ChildNodes;
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = true;
        }



    }
}