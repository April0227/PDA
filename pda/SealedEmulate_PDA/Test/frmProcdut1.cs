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
    public partial class frmProcdut1 : Form
    {
        public frmProcdut1()
        {
            InitializeComponent();
        }

        private int selectRow = 0;
        private string syb = string.Empty;// ��ҵ��
        private string gx = string.Empty; // ����
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProcdut_Load(object sender, EventArgs e)
        {
            this.cmbPro.Focus();
        }
        /// <summary>
        /// ���������ı� ʧ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPro_LostFocus(object sender, EventArgs e)
        {
            syb = string.Empty;
            gx = string.Empty;
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                // ���������ƻ����
                string sql = string.Format("SELECT U_SCJHDH,U_SYB,U_GX FROM " + ConnModel.commonDB + "..OWOR where DocEntry=@docentry");
                SqlParameter param = new SqlParameter("@docentry", textBox.Text.Trim());
                DataTable obj = SqlHelper.GetDataTable(sql, CommandType.Text, param);
                if (obj != null && obj.Rows.Count > 0)
                {
                    cmbProPlan.Text = obj.Rows[0]["U_SCJHDH"].ToString();
                    syb = obj.Rows[0]["U_SYB"].ToString();
                    gx = obj.Rows[0]["U_GX"].ToString();
                }
                else
                {
                    cmbProPlan.Text = string.Empty;
                }
                this.btnGo.Enabled = true;
            }
            else
            {
                this.btnGo.Enabled = false;
            }
        }
        /// <summary>
        /// ���������ı������ı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPro_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                this.btnGo.Enabled = true;
            }
            else
            {
                this.btnGo.Enabled = false;
            }
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
            string docType = SqlHelper.ExecuteScalar(string.Format("SELECT case Type when 'S' then '��׼' when 'P' then '����' else '�ֲ�' end AS Type  FROM {0}..OWOR WHERE Status='R' and DocEntry={1}", ConnModel.commonDB, proStr), CommandType.Text).ToString();
            this.labType.Text = docType;
            string sql = string.Empty;
            if (docType == "�ֲ�")
            {
                //����Ϊ��ͷ ��Ʒ����Ʒ
                sql = string.Format("select a.ItemCode '���ϱ���',b.ItemName '��������',a.PlannedQty '�ƻ�',ISNULL(a.U_SHQty,0) '�ѷ�',(a.PlannedQty-ISNULL(a.U_SHQty,0)) AS 'δ��',a.wareHouse AS '�ֿ�',B.DocEntry '����',-1 '�����к�' from {0}..owor a join {0}..oitm b on a.ItemCode=b.ItemCode where a.Status='R' and a.U_SFWG='W'and a.DocEntry={1}", ConnModel.commonDB, proStr);
            }
            else
            {
                //����Ϊ����ϸ
                sql = string.Format("SELECT B.ItemCode '���ϱ���',C.ItemName '��������',B.PlannedQty '�ƻ�',ISNULL(B.U_SHQty,0) '�ѷ�',(B.PlannedQty-ISNULL(B.U_SHQty,0)) AS 'δ��',B.wareHouse AS '�ֿ�',B.DocEntry '����',B.LineNum '�����к�' FROM {0}OWOR AS A JOIN {0}WOR1 AS B ON B.DocEntry = A.DocEntry JOIN {0}OITM AS C ON C.ItemCode = B.ItemCode WHERE A.Status='R' AND  B.U_SFWG='W' and b.IssueType='M' AND  A.DocEntry ={1}", ConnModel.commonDB + "..", proStr);
            }
            DGpro.DataSource = SqlHelper.GetDataTable(sql, CommandType.Text);

        }
        /// <summary>
        /// ����������Ϣ ��ȡ���ϱ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string qrCodeStr = textBox1.Text.Trim();
                if (qrCodeStr != "")
                {
                    string[] arry = qrCodeStr.Split(',');
                    txtTraceNo.Text = arry[0].Trim();
                    if (arry.Length == 5)
                    {
                        tb_Quantity.Text = arry[3].Trim().TrimEnd(new char[] { 'G', 'g' }).TrimEnd(new char[] { 'K', 'k' });
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
                string[] arry = qrCodeStr.Split(',');
                txtTraceNo.Text = arry[0].Trim();
                if (arry.Length == 5)
                {
                    tb_Quantity.Text = arry[3].Trim().TrimEnd(new char[] { 'G', 'g' }).TrimEnd(new char[] { 'K', 'k' });
                }
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
                string[] arry = qrCodeStr.Split(',');
                txtTraceNo.Text = arry[0].Trim();
                if (arry.Length == 5)
                {
                    tb_Quantity.Text = arry[3].Trim().TrimEnd(new char[] { 'G', 'g' }).TrimEnd(new char[] { 'K', 'k' });
                }
            }
        }
        /// <summary>
        /// ���ϱ����ı������ı�
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
        /// ��հ�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            txtTraceNo.Text = "";
            textBox1.Focus();
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
                    if (labType.Text == "�ֲ�")
                    {
                        sql = String.Format("UPDATE {0}OWOR SET U_SFWG = 'Y',U_SHQty = U_SHQty + {1} WHERE DocEntry={2}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text);
                    }
                    else
                    {
                        sql = String.Format("UPDATE {0}WOR1 SET U_SFWG = 'Y',U_SHQty = U_SHQty + {1} WHERE DocEntry={2} AND LineNum={3}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text, row["�����к�"]);
                    }
                    if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) <= 0)
                    {
                        MessageBox.Show("����SAP���ݳ�ʱ,���Ժ�����...");
                        this.btnSave.Enabled = true;
                        return;
                    }
                }
                else//δ��ѡ�깤
                {
                    if (labType.Text == "�ֲ�")
                    {
                        sql = String.Format("UPDATE {0}OWOR SET U_SHQty = U_SHQty + {1} WHERE DocEntry={2}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text);
                    }
                    else
                    {
                        sql = String.Format("UPDATE {0}WOR1 SET U_SHQty = U_SHQty + {1} WHERE DocEntry={2} AND LineNum={3}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text, row["�����к�"]);
                    }

                    if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) <= 0)
                    {
                        MessageBox.Show("����SAP���ݳ�ʱ,���Ժ�����...");
                        this.btnSave.Enabled = true;
                        return;
                    }
                }

                //����ʱ���������
                sql = string.Format("INSERT INTO ProduHair(PlanNum,ProOrderNum,LineNum,ItemCode,ItemName,Quantity,BatchNum,WhsCode,SYB,GX) VALUES ('{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}','{8}','{9}')", cmbProPlan.Text, cmbPro.Text, row["�����к�"], row["���ϱ���"], row["��������"], tb_Quantity.Text.Trim(), txtTraceNo.Text.Trim(), row["�ֿ�"], syb, gx);
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
                    btnGo_Click(null, null);
                    labType.Text = "";
                    MessageBox.Show("��ӳɹ���");
                    this.textBox1.Text = string.Empty;
                    this.textBox1.Focus();
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
        /// ����ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}