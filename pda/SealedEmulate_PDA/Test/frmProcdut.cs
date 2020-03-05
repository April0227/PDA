using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;
using SealedEmulate_PDA.Dal;

namespace SealedEmulate_PDA
{
    public partial class frmProcdut : Form
    {
        public frmProcdut()
        {
            InitializeComponent();
        }

        private int selectRow = 0;
        private bool proPlan = true;
        private bool Pro = true;
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProcdut_Load(object sender, EventArgs e)
        {
            BindProAndPlan();

        }
        /// <summary>
        /// ���ݼ�������ȡ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (cmbPro.Text.Trim() == "")
            {
                return;
            }
            this.btnGo.Enabled = false;
            string proStr = cmbPro.Text;
            string sql = string.Format("SELECT B.DocEntry '����',B.LineNum '�����к�',B.ItemCode '���ϱ���',C.ItemName '��������',B.PlannedQty '�ƻ�',ISNULL(B.U_SHQty,0) '�ѷ�',(B.PlannedQty-ISNULL(B.U_SHQty,0)) AS 'δ��',B.wareHouse AS '�ֿ�' FROM {0}OWOR AS A JOIN {0}WOR1 AS B ON B.DocEntry = A.DocEntry JOIN {0}OITM AS C ON C.ItemCode = B.ItemCode WHERE  B.U_SFWG='W' AND  A.DocEntry ={1}", ConnModel.commonDB + "..", proStr);
            DGpro.DataSource = SqlHelper.GetDataTable(sql, CommandType.Text);

        }

        /// <summary>
        /// �����ƻ����ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProPlan_SelectedValueChanged(object sender, EventArgs e)
        {
            if (proPlan)
            {
                cmbPro.SelectedValue = cmbProPlan.Text;
                this.btnGo.Enabled = true;
            }
        }
        /// <summary>
        /// ���������ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPro_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Pro == true)
            {
                cmbProPlan.SelectedValue = cmbPro.SelectedValue;
                this.btnGo.Enabled = true;
            }
        }
        /// <summary>
        /// ��Ӱ�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = DGpro.DataSource as DataTable;
            if (dt != null)
            {
                this.btnSave.Enabled = false;
                DataRow row = dt.Rows[selectRow];//��ȡѡ�е���
                string sql = string.Empty;
                if (this.ckwg.Checked) //�깤
                {
                    sql = String.Format("UPDATE {0}WOR1 SET U_SFWG = 'Y',U_SHQty = U_SHQty + {1} WHERE DocEntry={2} AND LineNum={3}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text, row["�����к�"]);
                    if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) <= 0)
                    {
                        MessageBox.Show("����SAP���ݳ�ʱ,���Ժ�����...");
                        this.btnSave.Enabled = true;
                        return;
                    }
                }
                else//δ��ѡ�깤
                {
                    sql = String.Format("UPDATE {0}WOR1 SET U_SHQty = U_SHQty + {1} WHERE DocEntry={2} AND LineNum={3}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text, row["�����к�"]);
                    if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) <= 0)
                    {
                        MessageBox.Show("����SAP���ݳ�ʱ,���Ժ�����...");
                        this.btnSave.Enabled = true;
                        return;
                    }
                }
                //����ʱ���������
                sql = string.Format("INSERT INTO ProduHair(PlanNum,ProOrderNum,LineNum,ItemCode,ItemName,Quantity,BatchNum,WhsCode) VALUES ('{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}')", cmbProPlan.Text, cmbPro.Text, row["�����к�"], row["���ϱ���"], row["��������"], tb_Quantity.Text.Trim(), txtTraceNo.Text.Trim(), row["�ֿ�"]);
                if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) > 0)
                {
                    if (this.ckwg.Checked) //�깤,�Ƴ�������
                    {
                        dt.Rows.RemoveAt(selectRow);
                    }
                    else
                    {
                        row["�ѷ�"] = (Convert.ToDouble(row["�ѷ�"].ToString()) + (Convert.ToDouble(tb_Quantity.Text.Trim()))).ToString();
                        row["δ��"] = (Convert.ToDouble(row["�ƻ�"].ToString()) - Convert.ToDouble(row["�ѷ�"].ToString())).ToString();
                    }
                    DGpro.DataSource = dt;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DGpro.UnSelect(i);
                    }
                    MessageBox.Show("������������...");
                    this.txtTraceNo.Text = "";
                    this.tb_Quantity.Text = "";
                    ckwg.Checked = false;
                    selectRow = 0;
                }
                else
                {
                    MessageBox.Show("������ݳ�ʱ,���Ժ�����...");
                    this.btnSave.Enabled = true;
                }
            }

        }
        /// <summary>
        /// ������δ򿪣��󶨼ƻ���+������
        /// </summary>
        private void BindProAndPlan()
        {
            try
            {
                proPlan = false;
                Pro = false;
                string sql = string.Empty;
                //����δ���ϵ������ƻ���
                sql = string.Format("SELECT distinct A.DocEntry FROM " + ConnModel.commonDB + "..[@SBO_HLJSCJH] AS A JOIN " + ConnModel.commonDB + "..[@SBO_HLJSCJH_H1] AS B ON B.DocEntry = A.DocEntry WHERE B.U_SCDD IN (SELECT distinct C.DocEntry FROM " + ConnModel.commonDB + "..OWOR AS C JOIN " + ConnModel.commonDB + "..WOR1 AS D ON D.DocEntry = C.DocEntry WHERE D.U_SFWG='W')");
                cmbProPlan.DisplayMember = "DocEntry";
                cmbProPlan.ValueMember = "DocEntry";
                cmbProPlan.DataSource = SqlHelper.GetDataTable(sql, CommandType.Text);
                //δ�깤�Ķ���
                sql = "SELECT distinct C.DocEntry as scdd,E.DocEntry as jhh FROM " + ConnModel.commonDB + "..OWOR AS C JOIN " + ConnModel.commonDB + "..WOR1 AS D ON D.DocEntry = C.DocEntry JOIN " + ConnModel.commonDB + "..[@SBO_HLJSCJH_H1] AS E ON C.DocEntry=E.U_SCDD WHERE D.U_SFWG='W'";
                cmbPro.DisplayMember = "scdd";
                cmbPro.ValueMember = "jhh";
                cmbPro.DataSource = SqlHelper.GetDataTable(sql, CommandType.Text);
                proPlan = true;
                Pro = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// ����ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnclose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("���Ժ�ر�...");
            }

        }
        /// <summary>
        /// ѡ�����Ӧ�ı���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTraceNo_TextChanged(object sender, EventArgs e)
        {
            string val = txtTraceNo.Text.Trim();
            if (val == "")
            {
                return;
            }
            DataTable dt = ((DataTable)DGpro.DataSource);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (val == dt.Rows[i]["���ϱ���"].ToString())
                    {
                        selectRow = i;
                        DGpro.Select(i);
                        this.btnSave.Enabled = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// ����������Ϣ ��ȡ���ϱ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            string qrCodeStr = textBox1.Text.Trim();
            if (qrCodeStr != "")
            {
                txtTraceNo.Text = qrCodeStr.Split(',')[0].Trim();
            }
        }
        /// <summary>
        /// ����������Ϣ ��ȡ���ϱ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string qrCodeStr = textBox1.Text.Trim();
            if (qrCodeStr != "")
            {
                txtTraceNo.Text = qrCodeStr.Split(',')[0].Trim();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            txtTraceNo.Text = "";
            textBox1.Focus();
        }
    }
}