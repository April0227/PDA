using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;
using SealedEmulate_PDA.Dal;

namespace SealedEmulate_PDA.Forms
{
    public partial class XSJH : Form
    {
        private string serNum = "";
        public XSJH()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XSJH_Load(object sender, EventArgs e)
        {
            ClearControl();
            txtDocEntry.Focus();
        }
        /// <summary>
        /// ����ҳ - ������Żس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDocEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region �ÿ�
                dataGrid1.DataSource = null;
                dataGrid2.DataSource = null;
                ClearControl();
                #endregion
                if (System.Text.RegularExpressions.Regex.IsMatch(this.txtDocEntry.Text.Trim(), @"^[1-9]\d*$"))
                {
                    int docEntry = int.Parse(this.txtDocEntry.Text.Trim());
                    string sql = string.Format(@"SELECT Row_Number() OVER ( ORDER BY A.LineNum )-1 '#',A.ItemCode as ���ϱ���, A.Dscription as ��������,A.Quantity as ��������,(A.Quantity-isnull(A.U_XSQty,0)) as ʣ������, 0 as ��������,A.LineNum as �к�,A.WhsCode as �ֿ�,B.CardCode AS �ͻ�����,B.CardName AS �ͻ����� FROM {0}..RDR1 A JOIN {0}..ORDR AS B ON A.DocEntry=B.DocEntry
WHERE A.DocEntry = {1} AND  A.OpenQty != 0 AND isnull(A.U_XSQty,'0') < A.Quantity AND  A.LineStatus ='O' AND A.U_XSLineStatus = 'O'", ConnModel.commonDB, docEntry);
                    DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        #region ����ҳ
                        DataGridTableStyle mydata1 = new DataGridTableStyle();
                        GridColumnStylesCollection mycol1 = null;
                        dataGrid1.DataSource = dt;
                        mydata1.MappingName = dt.TableName;
                        dataGrid1.TableStyles.Clear();
                        dataGrid1.TableStyles.Add(mydata1);
                        mycol1 = dataGrid1.TableStyles[0].GridColumnStyles;
                        mycol1["#"].Width = 0;
                        mycol1["���ϱ���"].Width = 80;
                        mycol1["��������"].Width = 80;
                        #endregion

                        #region ɨ��ҳ
                        //�����Ϣҳǩ ���ݣ����ϱ���,�ֿ⣩ 
                        DataTable tb2 = SqlHelper.GetDataTable(string.Format(" exec {1}..SBO_OnHandQtyForOrdr {0},'' ", docEntry, ConnModel.commonDB), CommandType.Text);
                        //����datagrid���п�
                        DataGridTableStyle mydata2 = new DataGridTableStyle();
                        GridColumnStylesCollection mycol2 = null;
                        dataGrid2.DataSource = tb2;
                        mydata2.MappingName = tb2.TableName;
                        dataGrid2.TableStyles.Clear();
                        dataGrid2.TableStyles.Add(mydata2);
                        mycol2 = dataGrid2.TableStyles[0].GridColumnStyles;
                        mycol2["#"].Width = 0;
                        mycol2["���ϱ���"].Width = 80;
                        mycol2["��������"].Width = 80;
                        mycol2["�ֿ�����"].Width = 0;
                        mycol2["��λ��ʶ"].Width = 0;
                        mycol2["�������"].Width = 0;
                        mycol2["�����к�"].Width = 0;
                        mycol2["�ͻ�����"].Width = 0;
                        mycol2["�ͻ�����"].Width = 0;
                        #endregion

                        tabControl1.SelectedIndex = 1;
                    }
                    else
                    {
                        MessageBox.Show("�ö����ѽ������߲�����...", "��ʾ");
                    }
                }
                else
                {
                    MessageBox.Show("���붩����Ų��Ϸ�!", "����");
                }
            }
        }
        /// <summary>
        /// ɨ��ҳ - �����س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBrCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDocEntry.Text.Trim() == "")
                {
                    this.txtBrCode.Text = "";
                    MessageBox.Show("�������붩��,��ɨ��.", "����");
                    tabControl1.SelectedIndex = 0;
                    txtDocEntry.Focus();
                    return;
                }
                string barCode = this.txtBrCode.Text.Trim();
                if (barCode != "")
                {
                    if (barCode.IndexOf(',') == -1)
                    {
                        ClearControl();
                        MessageBox.Show("������Ϣ�����Ϲ淶!", "����");
                        return;
                    }
                    string[] array = barCode.Split(',');
                    if (array.Length > 0)
                    {
                        string itemCode = array[0].Trim();
                        string batch = array[2].Trim();
                        DataTable dt1 = (DataTable)dataGrid1.DataSource;//����ҳ
                        DataTable dt2 = (DataTable)dataGrid2.DataSource;//���ҳ
                        foreach (DataRow row in dt2.Rows)
                        {
                            //�ж�����
                            if (row["���ϱ���"].ToString() == itemCode)
                            {
                                double kcQty = double.Parse(row["�������"].ToString());//������� 100
                                double ckQty = double.Parse(row["��������"].ToString());//�������� 0 
                                if (kcQty != ckQty)
                                {
                                    #region ǿ���Ƚ��ȳ�

                                    if (!checkBoxBatch.Checked)
                                    {
                                        if (row["����"].ToString() != batch)
                                        {
                                            MessageBox.Show("��ǰ�������β�����������,Ӧɨ������" + row["����"], "��ʾ");
                                            ClearControl();
                                            return;
                                        }
                                    }
                                    #endregion

                                    #region �������� ���п�λ�Զ�ѡ�����

                                    if (row["����"].ToString() == batch)
                                    {
                                        DataRow[] rows = dt2.Select("����='" + batch + "'");
                                        double batchQty = 0;
                                        foreach (DataRow item in rows)
                                        {
                                            double val = double.Parse(item["�������"].ToString()) - double.Parse(item["��������"].ToString());
                                            batchQty += val;
                                        }
                                        labKcQty.Text = batchQty.ToString();
                                        serNum = row["#"].ToString();
                                        txtQuanTity.Focus();
                                        return;
                                    }                                  
                                    #endregion                                   
                                }
                            }
                        }
                        //���ѭ�����,δ����ɨ�������붩������һ��
                        ClearControl();
                        MessageBox.Show("ɨ�����ϻ������붩�����ϲ�ƥ��", "����");
                    }
                    else
                    {
                        ClearControl();
                        MessageBox.Show("������Ϣ�����Ϲ淶!", "����");
                    }
                }
                else
                {
                    serNum = "";
                    labKcQty.Text = "0";
                    MessageBox.Show("������Ϣ����Ϊ��!", "��ʾ");
                }
            }
        }
        /// <summary>
        /// ɨ��ҳ - ���������س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuanTity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(this.txtDocEntry.Text.Trim(), @"^[1-9]\d*$"))
                {
                    if (serNum != "")
                    {
                        double batchQty = double.Parse(labKcQty.Text);
                        double txtQty = double.Parse(txtQuanTity.Text);
                        if (txtQty > batchQty)
                        {
                            MessageBox.Show("�����������ڿ������...", "��ʾ");
                        }
                        else
                        {
                            string itemCode = this.txtBrCode.Text.Trim().Split(',')[0].Trim();
                            DataTable dt1 = (DataTable)dataGrid1.DataSource;//����ҳ
                            DataRow row1 = dt1.Select("���ϱ���='" + itemCode + "'")[0];
                            double qty1 = double.Parse(row1["ʣ������"].ToString()) - double.Parse(row1["��������"].ToString());
                            if (txtQty > qty1)
                            {
                                MessageBox.Show("�����������ڴ���������" + qty1, "��ʾ");
                            }
                            else
                            {
                                int index1 = int.Parse(row1["#"].ToString());
                                int col1 = 5;//����ҳ��������������
                                int col2 = 9;//ɨ��ҳ��������������
                                string batch = this.txtBrCode.Text.Trim().Split(',')[2].Trim();
                                DataTable dt2 = (DataTable)dataGrid2.DataSource;//���ҳ
                                int location = int.Parse(serNum);
                                for (int i = location; i < dt2.Rows.Count; i++)
                                {
                                    if (dt2.Rows[i]["����"].ToString() == batch && txtQty > 0)
                                    {
                                        double lineQty = double.Parse(dt2.Rows[i]["�������"].ToString()) - double.Parse(dt2.Rows[i]["��������"].ToString());
                                        if (txtQty > lineQty)
                                        {
                                            dataGrid1[index1, col1] = (double.Parse(dataGrid1[index1, col1].ToString()) + lineQty).ToString();
                                            dataGrid2[i, col2] = (double.Parse(dataGrid2[i, col2].ToString()) + lineQty).ToString();
                                        }
                                        else
                                        {
                                            dataGrid1[index1, col1] = (double.Parse(dataGrid1[index1, col1].ToString()) + txtQty).ToString();
                                            dataGrid2[i, col2] = (double.Parse(dataGrid2[i, col2].ToString()) + txtQty).ToString();
                                            break;
                                        }
                                        txtQty -= lineQty;
                                    }
                                }
                                ClearControl();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("����ɨ������", "��ʾ");
                    }
                }
                else
                {
                    MessageBox.Show("�����������Ϸ�!", "����");
                }
            }
        }
        /// <summary>
        /// ɨ��ҳ - ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("ȷ��Ҫ������?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                DataTable dt2 = (DataTable)dataGrid2.DataSource;//���ҳ
                List<string> sqlList = new List<string>();
                foreach (DataRow row in dt2.Rows)
                {
                    double val = double.Parse(row["��������"].ToString());
                    if (val != 0)
                    {
                        string sql = string.Format("insert into XSJH(CardCode,CardName,DocEntry,LineNum,ItemCode,ItemName,ItemSpec,ItemBatch,Qty,Price,LineTotal,WhsCode,WhsName,AbsEntry,BinCode,InWhsQty,UserSign) 	values('{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}',{8},{9},{10},'{11}','{12}',{13},'{14}',{15},'{16}')", row["�ͻ�����"].ToString(), row["�ͻ�����"].ToString(), row["�������"].ToString(), row["�����к�"].ToString(), row["���ϱ���"].ToString(), row["��������"].ToString(), "", row["����"].ToString(), row["��������"].ToString(), row["����"].ToString(), row["�кϼ�"].ToString(), row["�ֿ����"].ToString(), row["�ֿ�����"].ToString(), row["��λ��ʶ"].ToString(), row["��λ����"].ToString(), row["��������"].ToString(), ConnModel.userName);
                        sqlList.Add(sql);
                    }
                }
                if (sqlList.Count > 0)
                {
                    DataTable dt1 = (DataTable)dataGrid1.DataSource;//����ҳ
                    foreach (DataRow row in dt1.Rows)
                    {
                        double val = double.Parse(row["��������"].ToString());
                        if (val != 0)
                        {
                            string sql = string.Format("UPDATE {0}.. RDR1 SET U_XSQty = {1},U_XSLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END WHERE DocEntry = {2} AND LineNum = {3}", ConnModel.commonDB, (double.Parse(row["��������"].ToString()) + double.Parse(row["��������"].ToString()) - double.Parse(row["ʣ������"].ToString())), txtDocEntry.Text, row["�к�"].ToString());
                            sqlList.Add(sql);
                        }
                    }
                }
                if (SqlHelper.ExecuteSqlTran(sqlList) > 0)
                {
                    //API ���ն�����ѯ
                    int docEntry = int.Parse(txtDocEntry.Text);

                    //���˳ɹ� ����ʧ�ܶ���ˢ��ҳ��
                    dataGrid1.DataSource = null;
                    dataGrid2.DataSource = null;
                    ClearControl();
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("��������ʧ��,���Ժ�����..", "��ʾ");
                }
            }
        }








        /// <summary>
        /// ɨ��ҳ - �ؼ��ÿ�
        /// </summary>
        private void ClearControl()
        {
            txtBrCode.Text = "";
            txtQuanTity.Text = "";
            serNum = "";
            labKcQty.Text = "0";
        }

    }
}