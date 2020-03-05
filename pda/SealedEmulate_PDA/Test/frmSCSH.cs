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
    public partial class frmSCSH : Form
    {
        public frmSCSH()
        {
            InitializeComponent();
        }
        private System.Data.DataTable tbl;
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
                    string sql = string.Empty;
                    string orderNum = cmbPro.Text.Trim();
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
                            sql = string.Format("INSERT INTO ProduReceipt (IsFinish,ItemCode,ItemName,OrderQty,QuanTity,UndoneQty,ProOrderNum,Shifts,TagNum,SingleQty,BatchNum,QRCode) VALUES ('1','{0}','{1}','{2}',{3},{4},'{5}','{6}',{7},{8},'{9}','{10}')", row["���ϱ���"], row["��������"], row["�ƻ�"], qTy, wfQty - qTy, orderNum, txtBC.Text.Trim().Length >= 8 ? txtBC.Text.Trim().Substring(0, 7) : txtBC.Text.Trim(), tagNum, singleQty, DateTime.Now.ToString("yyyyMMdd"), qrCode);
                        }
                        else
                        {
                            sql = string.Format("INSERT INTO ProduReceipt (IsFinish,ItemCode,ItemName,OrderQty,QuanTity,UndoneQty,ProOrderNum,Shifts,TagNum,BatchNum,SingleQty,QRCode) VALUES ('0','{0}','{1}',{2},{3},{4},'{5}','{6}',{7},'{8}',25,'{9}')", row["���ϱ���"], row["��������"], row["�ƻ�"], qTy, wfQty - qTy, orderNum, txtBC.Text.Trim().Length >= 8 ? txtBC.Text.Trim().Substring(0, 7) : txtBC.Text.Trim(), tagNum, DateTime.Now.ToString("yyyyMMdd"), qrCode);
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
        /// ���������ı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPro_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPro.Text != "")
                {
                    string sql = string.Format("SELECT A.DocEntry '����',A.ItemCode '���ϱ���',B.ItemName '��������',A.PlannedQty '�ƻ�',ISNULL(A.U_SHQty,0) '�ѷ�',(A.PlannedQty-ISNULL(A.U_SHQty,0)) 'δ��',A.Warehouse '�ֿ�' FROM {0}OWOR AS A JOIN {0}OITM AS B ON B.ItemCode = A.ItemCode WHERE A.DocEntry={1}", ConnModel.commonDB + "..", cmbPro.Text.Trim());
                    DGpro.DataSource = SqlHelper.GetDataTable(sql, CommandType.Text);
                }
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
            ckwg.Checked = false;
            string sql = "SELECT DocEntry FROM " + ConnModel.commonDB + "..OWOR WHERE Status = 'R' AND U_SFWG='W'";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            cmbPro.ValueMember = "DocEntry";
            cmbPro.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                DGpro.DataSource = null;
                btnSave.Enabled = false;
            }

        }
    }
}