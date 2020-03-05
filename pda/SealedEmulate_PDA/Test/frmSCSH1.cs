using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;
using SealedEmulate_PDA.Dal;
using System.Data.SqlClient;

namespace SealedEmulate_PDA
{
    public partial class frmSCSH1 : Form
    {
        public frmSCSH1()
        {
            InitializeComponent();
        }
        private string orderNum = string.Empty; //��������
        private string pici = string.Empty; // ��������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSCSH_Load(object sender, EventArgs e)
        {
            try
            {
                LoadForm();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                DataTable dt = DGpro.DataSource as DataTable;
                if (dt != null)
                {
                    double qTy = Convert.ToDouble(tb_Quantity.Text.Trim());//�ջ�����
                    string itemCode = "";
                    MessageBox.Show("�˹�����������,���Ժ�...");
                    return;
                    string sql = string.Empty;
                    foreach (DataRow row in dt.Rows)
                    {
                        double wfQty = Convert.ToDouble(row["δ��"].ToString());
                        double tagNum = Math.Ceiling(qTy / 25);
                        double singleQty = qTy % 25;
                        if (singleQty == 0)
                        {
                            singleQty = 25;
                        }
                        string qrCode = row["���ϱ���"] + " " + DateTime.Now.ToString("yyyyMMdd");
                        if (ckwg.Checked)//���깤
                        {
                            sql = string.Format("INSERT INTO ProduReceipt (IsFinish,ItemCode,ItemName,OrderQty,QuanTity,UndoneQty,ProOrderNum,Shifts,TagNum,SingleQty,BatchNum,QRCode,SYB,GX) VALUES ('1','{0}','{1}','{2}',{3},{4},'{5}','{6}',{7},{8},'{9}','{10}','{11}','{12}')", row["���ϱ���"], row["��������"], row["�ƻ�"], qTy, wfQty - qTy, orderNum, txtBC.Text.Trim().Length >= 8 ? txtBC.Text.Trim().Substring(0, 7) : txtBC.Text.Trim(), tagNum, singleQty, pici, qrCode, row["��ҵ��"], row["����"]);
                        }
                        else
                        {
                            sql = string.Format("INSERT INTO ProduReceipt (IsFinish,ItemCode,ItemName,OrderQty,QuanTity,UndoneQty,ProOrderNum,Shifts,TagNum,BatchNum,SingleQty,QRCode,SYB,GX) VALUES ('0','{0}','{1}',{2},{3},{4},'{5}','{6}',{7},'{8}',25,'{9}','{10}','{11}')", row["���ϱ���"], row["��������"], row["�ƻ�"], qTy, wfQty - qTy, orderNum, txtBC.Text.Trim().Length >= 8 ? txtBC.Text.Trim().Substring(0, 7) : txtBC.Text.Trim(), tagNum, pici, qrCode, row["��ҵ��"], row["����"]);
                        }
                    }
                    if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) > 0)
                    {
                        if (ckwg.Checked) //���깤
                        {
                            sql = string.Format("UPDATE " + ConnModel.commonDB + "..OWOR SET U_SFWG='Y',U_SHQty+={0} WHERE DocEntry={1}", qTy, orderNum);
                        }
                        else
                        {
                            sql = string.Format("UPDATE " + ConnModel.commonDB + "..OWOR SET U_SHQty+={0} WHERE DocEntry={1}", qTy, orderNum);
                        }
                        if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) > 0)
                        {
                            MessageBox.Show("��ӳɹ�...");
                            LoadForm();
                        }
                        else
                        {
                            MessageBox.Show("����B1����ʧ��...");
                            sql = "Delete from ProduReceipt where ProOrderNum=" + orderNum;
                            SqlHelper.ExecuteNonquery(sql, CommandType.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("���ʧ��...");
                    }
                }
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        private void LoadForm()
        {
            txtBC.Text = DateTime.Now.ToString("yyyyMMdd");
            tb_Quantity.Text = "";
            QRCode.Text = string.Empty;
            QRCode.Focus();
            ckwg.Checked = false;
            DGpro.DataSource = null;
        }
        /// <summary>
        /// ���������ı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QRCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    orderNum = string.Empty;
                    pici = string.Empty;
                    TextBox textBox = (TextBox)sender;
                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        string textBoxValue = textBox.Text.Trim();
                        string[] textBoxArray = textBoxValue.Split(',');
                        if (textBoxArray.Length == 5 && !textBoxValue.EndsWith(","))
                        {
                            orderNum = textBoxArray[3];
                            pici = textBoxArray[2];
                            string docType = SqlHelper.ExecuteScalar(string.Format("SELECT case Type when 'S' then '��׼' when 'P' then '����' else '�ֲ�' end AS Type  FROM {0}..OWOR WHERE Status='R' and DocEntry={1}", ConnModel.commonDB, orderNum), CommandType.Text).ToString();
                            this.labType.Text = docType;
                            string sql = string.Empty;
                            if (docType == "�ֲ�")
                            {
                                sql = string.Format("SELECT A.ItemCode '���ϱ���',B.ItemName '��������',A.PlannedQty '�ƻ�',ISNULL(A.U_SHQty,0) '����',(A.PlannedQty-ISNULL(A.U_SHQty,0)) 'δ��',A.Warehouse '�ֿ�',A.DocEntry '����',A.LineNum '�к�','' '��ҵ��','' '����' FROM {0}..WOR1 AS A JOIN {0}..OITM AS B ON B.ItemCode = A.ItemCode WHERE  A.DocEntry={1} and  A.U_SFWG='W'", ConnModel.commonDB, orderNum);
                            }
                            else
                            {
                                sql = string.Format("SELECT A.ItemCode '���ϱ���',B.ItemName '��������',A.PlannedQty '�ƻ�',ISNULL(A.U_SHQty,0) '����',(A.PlannedQty-ISNULL(A.U_SHQty,0)) 'δ��',A.Warehouse '�ֿ�',A.DocEntry '����',Null '�к�',A.U_SYB '��ҵ��',A.U_GX '����' FROM {0}OWOR AS A JOIN {0}OITM AS B ON B.ItemCode = A.ItemCode WHERE A.DocEntry={1} and  A.U_SFWG='W'", ConnModel.commonDB + "..", orderNum);
                            }
                            DGpro.DataSource = SqlHelper.GetDataTable(sql, CommandType.Text);
                            tb_Quantity.Text = textBoxArray[4].Trim();
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.StackTrace);
                }
            }
        }
    }
}