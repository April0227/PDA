using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA.Forms
{
    public partial class CGSH : Form
    {
        public CGSH()
        {
            InitializeComponent();
        }
        private void CGRK_Load(object sender, EventArgs e)
        {
            BindWhs();
            Grid3DataTable();
            txtdocEntry.Focus();
        }

        /// <summary>
        /// �ʼ쵥�Żس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtdocEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                #region �ÿ�
                DataGrid1.DataSource = null;
                DataGrid2.DataSource = null;
                DataGrid3.DataSource = null;
                Grid3DataTable();
                ClearControl();
                #endregion
                string docEntry = txtdocEntry.Text.Trim();
                if (System.Text.RegularExpressions.Regex.IsMatch(docEntry, @"^[1-9]\d*$"))
                {
                    if (!string.IsNullOrEmpty(docEntry))
                    {
                        #region ����ҳ
                        //����������ʼ쵥�ţ���ѯ 
                        string sql = string.Format(@"SELECT Row_Number() OVER ( ORDER BY A.LineId )-1 '#', C.ItemCode ���ϱ���,C.Dscription ��������,case when A.U_WQSL is null or A.U_WQSL = 0 then A.U_HGSL else A.U_WQSL end ����,
D.WhsCode �ֿ����,D.WhsName �ֿ�����,D.BinActivat �Ƿ��λ,B.U_CardCode ��Ӧ�̱���,B.U_CardName ��Ӧ������,A.DocEntry �ʼ쵥��,A.LineId �ʼ��к�,C.DocEntry �ɹ�����,C.LineNum �ɹ������к�,C.Quantity ��������,
C.Price ����,C.LineTotal �кϼ�,0 �������
FROM {1}..[@SBO_ZJD_H] A
JOIN {1}..[@SBO_ZJD] B ON A.DocEntry=B.DocEntry
JOIN {1}..POR1 C ON A.U_CGDD=C.DocEntry AND A.U_CGDDH=C.LineNum
JOIN {1}..OWHS D ON C.WhsCode=D.WhsCode
WHERE (A.U_ZJJG ='Y'or A.U_ZJJG ='J' or A.U_PSJG ='Y') AND A.U_SFSH ='N' AND A.DocEntry={0}", docEntry, ConnModel.commonDB);
                        DataTable dt1 = SqlHelper.GetDataTable(sql, CommandType.Text);
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            DataGridTableStyle mydata1 = new DataGridTableStyle();
                            GridColumnStylesCollection mycol1 = null;
                            DataGrid1.DataSource = dt1;
                            mydata1.MappingName = dt1.TableName;
                            DataGrid1.TableStyles.Clear();
                            DataGrid1.TableStyles.Add(mydata1);
                            mycol1 = DataGrid1.TableStyles[0].GridColumnStyles;
                            mycol1["#"].Width = 0;
                            mycol1["�Ƿ��λ"].Width = 0;
                            mycol1["��Ӧ�̱���"].Width = 0;
                            mycol1["��Ӧ������"].Width = 0;
                            mycol1["�ʼ쵥��"].Width = 0;
                            mycol1["�ʼ��к�"].Width = 0;
                            mycol1["�ɹ�����"].Width = 0;
                            mycol1["�ɹ������к�"].Width = 0;
                            mycol1["��������"].Width = 0;
                            mycol1["����"].Width = 0;
                            mycol1["�кϼ�"].Width = 0;
                            tabControl1.SelectedIndex = 1;
                            txtBarcode.Focus();
                        }
                        else
                        {
                            MessageBox.Show("��ǰ�ʼ쵥������,������ʼ쵥", "��ʾ");
                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show("���붩����Ų��Ϸ�!", "����");
                }

            }
        }
        /// <summary>
        /// ������Ϣɨ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                labAbsEntry.Text = "0";
                txtBinCode.Text = "";
                txtQty.Text = "";
                if (txtdocEntry.Text.Trim() == "")
                {
                    this.txtBarcode.Text = "";
                    MessageBox.Show("�������붩��,��ɨ��.", "����");
                    tabControl1.SelectedIndex = 0;
                    txtdocEntry.Focus();
                    return;
                }
                string barCode = this.txtBarcode.Text.Trim();
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
                        DataTable dt1 = (DataTable)DataGrid1.DataSource;//����ҳ 
                        foreach (DataRow row in dt1.Rows)
                        {
                            if (row["���ϱ���"].ToString() == itemCode)
                            {
                                if (double.Parse(row["����"].ToString()) == double.Parse(row["�������"].ToString()))
                                {
                                    MessageBox.Show("������������ջ�.", "����");
                                    return;
                                }
                                txtItemCode.Text = row["���ϱ���"].ToString();
                                txtItemName.Text = row["��������"].ToString();
                                txtBatch.Text = array[2].Trim();
                                comboWhs.Text = row["�ֿ����"].ToString();
                                return;
                            }
                        }
                        ClearControl();
                        MessageBox.Show("ɨ�������붩�����ϲ�ƥ��", "����");
                    }
                    else
                    {
                        ClearControl();
                        MessageBox.Show("������Ϣ�����Ϲ淶!", "����");
                    }
                }
                else
                {
                    ClearControl();
                    MessageBox.Show("������Ϣ����Ϊ��!", "��ʾ");
                }
            }
        }
        /// <summary>
        /// ��ǰ�ֿ��� - �Ƿ����ÿ�λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboWhs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string binActivat = comboWhs.SelectedValue.ToString();
            if (binActivat == "N")
            {
                txtQty.Text = "";
                txtBinCode.ReadOnly = true;
                txtQty.Focus();
            }
            else if (binActivat == "Y")
            {
                txtQty.Text = "";
                txtBinCode.ReadOnly = false;
                txtBinCode.Focus();
            }
        }
        /// <summary>
        /// ��λ���� �س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtItemCode.Text == "")
                {
                    MessageBox.Show("����ɨ������", "��ʾ");
                    txtBinCode.Text = "";
                    txtBarcode.Focus();
                    return;
                }
                string binCode = txtBinCode.Text.Trim();
                if (binCode == "")
                {
                    txtBinCode.Focus();
                }
                else
                {
                    string whsCode = comboWhs.Text.Trim();
                    bool b = false;
                    if (binCode.IndexOf(",") != -1)
                    {
                        if (whsCode == binCode.Split(',')[0])
                        {
                            //ɨ��� ���ȡ��λ����,��ѯabsEntry 
                            labAbsEntry.Text = binCode.Split(',')[2];
                            b = true;
                        }
                        else
                        {
                            txtBinCode.Text = "";
                            MessageBox.Show("ɨ���λ��ֿⲻһ��", "��ʾ");
                            txtBinCode.Focus();
                        }
                    }
                    else
                    {
                        string sql = string.Format("select AbsEntry from {1}..OBIN where BinCode = '{0}' and WhsCode='{2}'", binCode, ConnModel.commonDB, whsCode);
                        DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            labAbsEntry.Text = dt.Rows[0]["AbsEntry"].ToString();
                            b = true;
                        }
                        else
                        {
                            MessageBox.Show("��ǰ�ֿⲻ���ڸÿ�λ", "��ʾ");
                        }
                    }
                    if (b)
                    {
                        //������ֵ
                        DataTable dt1 = (DataTable)DataGrid1.DataSource;
                        DataRow row = dt1.Select("���ϱ���='" + txtItemCode.Text + "'")[0];
                        double txtqty = double.Parse(row["����"].ToString()) - double.Parse(row["�������"].ToString());
                        txtQty.Text = txtqty.ToString();
                    }
                }
            }
        }
        /// <summary>
        /// ��������ֻ��Ϊ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //�������Ĳ����˸�����֣�����������
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// ��λ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            txtBinCode.Text = "";
            txtBinCode.Focus();
        }
        /// <summary>
        /// ɨ��ҳ - ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region У��

            string docEntry = txtdocEntry.Text;
            if (docEntry == "")
            {
                MessageBox.Show("����ѡ�񶩵�.", "��ʾ");
                return;
            }
            string itemCode = txtItemCode.Text;
            string itemName = txtItemName.Text;
            if (itemCode == "")
            {
                MessageBox.Show("����ɨ������.", "��ʾ");
                return;
            }
            if (txtBinCode.ReadOnly == false && labAbsEntry.Text == "0")
            {
                MessageBox.Show("�����ÿ�λ", "��ʾ");
                return;
            }
            DataTable dt1 = (DataTable)DataGrid1.DataSource;
            DataRow row = dt1.Select("���ϱ���='" + itemCode + "'")[0];
            int serNum = int.Parse(row["#"].ToString());
            double syQty = double.Parse(row["����"].ToString()) - double.Parse(row["�������"].ToString());
            double srQty = double.Parse(txtQty.Text);
            if (srQty > syQty)
            {
                MessageBox.Show("�������ʣ��Ϊ" + syQty, "��ʾ");
                return;
            }
            #endregion

            #region ������ֶ�

            string cardCode = row["��Ӧ�̱���"].ToString();
            string cardName = row["��Ӧ������"].ToString();
            // �ɹ����� docEntry
            string lineNum = row["�ɹ������к�"].ToString();
            string zjd = row["�ʼ쵥��"].ToString();
            string zjdhh = row["�ʼ��к�"].ToString();
            //���ϱ��� �������� itemCode itemName
            string batch = txtBatch.Text;
            string quantity = row["��������"].ToString();
            string price = row["����"].ToString();
            string lineTotal = row["�кϼ�"].ToString();
            string binActivat = row["�Ƿ��λ"].ToString();
            string whsCode = comboWhs.Text;
            string whsName = row["�ֿ�����"].ToString();
            string absEntry = "";
            if (txtBinCode.ReadOnly == false)
            {
                absEntry = labAbsEntry.Text;
            }
            string binCode = "";
            if (txtBinCode.Text.IndexOf(',') == -1)
            {
                binCode = txtBinCode.Text;
            }
            else
            {
                binCode = txtBinCode.Text.Split(',')[3];
            }
            //������� srQty
            string zdr = ConnModel.userName;
            #endregion

            DataTable dt2 = (DataTable)DataGrid2.DataSource;
            int sign = 0;

            #region ���¹���ҳ
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (dt2.Rows[i]["���ϱ���"].ToString() == itemCode && dt2.Rows[i]["����"].ToString() == batch && dt2.Rows[i]["�ֿ����"].ToString() == whsCode && dt2.Rows[i]["��λ��ʶ"].ToString() == absEntry)
                {
                    //ֻ������������
                    DataGrid2[i, dt2.Columns.Count - 2] = double.Parse(dt2.Rows[i]["�������"].ToString()) + srQty;
                }
                else
                {
                    sign++;
                }
            }
            #endregion

            #region ��ӹ���ҳ

            if (sign == dt2.Rows.Count)
            {
                DataRow newRow = dt2.NewRow();
                newRow["��Ӧ�̱���"] = cardCode;
                newRow["��Ӧ������"] = cardName;
                newRow["�ɹ�����"] = docEntry;
                newRow["�ɹ������к�"] = lineNum;
                newRow["�ʼ쵥��"] = zjd;
                newRow["�ʼ쵥�к�"] = zjdhh;
                newRow["���ϱ���"] = itemCode;
                newRow["��������"] = itemName;
                newRow["����"] = batch;
                newRow["��������"] = quantity;
                newRow["����"] = price;
                newRow["�кϼ�"] = lineTotal;
                newRow["�Ƿ��λ"] = binActivat;
                newRow["�ֿ����"] = whsCode;
                newRow["�ֿ�����"] = whsName;
                newRow["��λ��ʶ"] = absEntry;
                newRow["��λ����"] = binCode;
                newRow["�������"] = srQty;
                newRow["�Ƶ���"] = zdr;
                dt2.Rows.Add(newRow);
                DataGrid2.DataSource = dt2;
            }

            #endregion

            DataGrid1[serNum, dt1.Columns.Count - 1] = double.Parse(row["�������"].ToString()) + srQty;
        }
        /// <summary>
        /// ���ð�ť���ɨ��ҳֵ
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearControl();
        }















        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInCome_Click(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {

            if (DataGrid2.CurrentRowIndex != -1)
            {
                DataTable dtInCome = (DataTable)DataGrid1.DataSource;
                DataTable dt = (DataTable)DataGrid2.DataSource;
                int n = 0;
                //��ȡ��ǰѡ�����к�   
                n = DataGrid2.CurrentRowIndex;
                for (int i = 0; i < dtInCome.Rows.Count; i++)
                {
                    if (dtInCome.Rows[i]["���ϱ���"].ToString() == dt.Rows[n]["���ϱ���"].ToString() && dtInCome.Rows[i]["�ɹ������к�"].ToString() == dt.Rows[n]["�ɹ������к�"].ToString() && dtInCome.Rows[i]["����"].ToString() == dt.Rows[n]["����"].ToString())
                    {
                        double qty = Convert.ToDouble(dtInCome.Rows[i]["�������"]);
                        dtInCome.Rows[i]["�������"] = qty - Convert.ToDouble(dt.Rows[n]["����"]);
                        DataGrid1.DataSource = dtInCome;
                    }

                }
                //�����ݼ�������ɾ���� 
                dt.Rows.RemoveAt(n);
                //ˢ��Datagrid1��ʾɾ���������    
                DataGrid2.Refresh();
            }
        }










        /// <summary>
        /// ɨ��ҳ�������
        /// </summary>
        private void ClearControl()
        {
            txtBarcode.Text = "";
            txtItemCode.Text = "";
            txtItemName.Text = "";
            txtBatch.Text = "";
            txtBinCode.Text = "";
            txtQty.Text = "";
            labAbsEntry.Text = "0";
        }
        /// <summary>
        /// �����вֿ�
        /// </summary>
        private void BindWhs()
        {
            string sql = "SELECT WhsCode,BinActivat FROM " + ConnModel.commonDB + "..OWHS WHERE Inactive='N'";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            comboWhs.DataSource = dt;
            //comboWhs.DisplayMember = "WhsCode";
            //comboWhs.ValueMember = "WhsName";
            comboWhs.DisplayMember = "WhsCode";
            comboWhs.ValueMember = "BinActivat";
        }
        /// <summary>
        /// ���˴�����table
        /// </summary>
        /// <returns></returns>
        public void Grid3DataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("��Ӧ�̱���");
            table.Columns.Add("��Ӧ������");
            table.Columns.Add("�ɹ�����");
            table.Columns.Add("�ɹ������к�");
            table.Columns.Add("�ʼ쵥��");
            table.Columns.Add("�ʼ쵥�к�");
            table.Columns.Add("���ϱ���");//6
            table.Columns.Add("��������");
            table.Columns.Add("����");
            table.Columns.Add("��������");
            table.Columns.Add("����");
            table.Columns.Add("�кϼ�");
            table.Columns.Add("�Ƿ��λ");
            table.Columns.Add("�ֿ����");
            table.Columns.Add("�ֿ�����");
            table.Columns.Add("��λ��ʶ");
            table.Columns.Add("��λ����");
            table.Columns.Add("�������");
            table.Columns.Add("�Ƶ���");

            DataGridTableStyle mydata2 = new DataGridTableStyle();
            GridColumnStylesCollection mycol2 = null;
            DataGrid2.DataSource = table;
            mydata2.MappingName = table.TableName;
            DataGrid2.TableStyles.Clear();
            DataGrid2.TableStyles.Add(mydata2);
            mycol2 = DataGrid2.TableStyles[0].GridColumnStyles;
            mycol2["��Ӧ�̱���"].Width = 0;
            mycol2["��Ӧ������"].Width = 0;
            mycol2["�ɹ�����"].Width = 0;
            mycol2["�ɹ������к�"].Width = 0;
            mycol2["�ʼ쵥��"].Width = 0;
            mycol2["�ʼ쵥�к�"].Width = 0;
            mycol2["��������"].Width = 0;
            mycol2["����"].Width = 0;
            mycol2["�кϼ�"].Width = 0;
            mycol2["�Ƿ��λ"].Width = 0;
            mycol2["��λ��ʶ"].Width = 0;
            mycol2["�Ƶ���"].Width = 0;
        }




    }
}