using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;
using SealedEmulate_PDA.Dal;

namespace SealedEmulate_PDA._2020
{
    public partial class XSMK_KCZC : Form
    {
        private string serNum = "";
        private string page1LineNum = "";
        public XSMK_KCZC()
        {
            InitializeComponent();
        }
        private void XSMK_KCZC_Load(object sender, EventArgs e)
        {
            BindWhs();
            Grid3DataTable();
            txtDocEntry.Focus();
        }
        private void txtDocEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region �ÿ�
                DataGrid1.DataSource = null;
                DataGrid2.DataSource = null;
                Grid3DataTable();
                ClearControl();
                #endregion
                string docEntry = txtDocEntry.Text.Trim();
                if (System.Text.RegularExpressions.Regex.IsMatch(docEntry, @"^[1-9]\d*$"))
                {
                    if (!string.IsNullOrEmpty(docEntry))
                    {
                        #region ����ҳ
                        //��������Ŀ��ת�����󵥺ţ���ѯ 
                        //string sql = string.Format(@"exec [SAP_KCZC_SelKCZCQQ] '{0}','{1}'", docEntry, ConnModel.commonDB);
                        string sql = string.Format(@"exec [SAP_NEW_SelKCZC_LOAD] {0}", docEntry);
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
                            mycol1["���ݱ��"].Width = 0;
                            mycol1["�����к�"].Width = 0;
                            mycol1["#"].Width = 0;
                            tabControl1.SelectedIndex = 1;
                            txtBarcode.Focus();
                        }
                        else
                        {
                            txtDocEntry.Text = "";
                            MessageBox.Show("��ǰ���۶���������,���������", "��ʾ");

                        }
                        #endregion
                    }
                }
                else
                {
                    txtDocEntry.Text = "";
                    MessageBox.Show("�������۶������Ų��Ϸ�!", "����");
                }

            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lFKW.Text = "0";
                lTKW.Text = "0";
                txtFKW.Text = "";
                txtTKW.Text = "";
                txtQty.Text = "";
                if (txtDocEntry.Text.Trim() == "")
                {
                    this.txtBarcode.Text = "";//���������Ϣ�ؼ�
                    MessageBox.Show("�����������۶�������,��ɨ��.", "����");
                    tabControl1.SelectedIndex = 0;
                    txtDocEntry.Focus();
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
                        string batch = array[2].Trim();//����
                        DataTable dt1 = (DataTable)DataGrid1.DataSource;//����ҳ 
                        foreach (DataRow row in dt1.Rows)
                        {
                            if (row["���ϱ���"].ToString() == itemCode)
                            {
                                //��ѯ��ǰ�����Ƿ���ڵ�ǰ����
                                string sql = string.Format(@"SELECT T1.AbsEntry FROM {1}..OBBQ T0 
INNER JOIN {1}..OBIN T1 ON T0.BinAbs  = T1.AbsEntry
INNER JOIN {1}..OBTN T2 ON T2.AbsEntry = T0.SnBMDAbs   
where t0.OnHandQty <>0  and t2.ItemCode ='{0}' AND T2.DistNumber ='{2}'", itemCode, ConnModel.commonDB, batch);
                                DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                                if (dt.Rows.Count > 0)
                                {
                                    if (double.Parse(row["����"].ToString()) == double.Parse(row["�������"].ToString()))
                                    {
                                        MessageBox.Show("����������ɿ��ת��.", "����");
                                        return;
                                    }
                                    txtItemCode.Text = row["���ϱ���"].ToString();
                                    txtItemName.Text = row["��������"].ToString();
                                    txtBatch.Text = batch;
                                    cmbFCK.SelectedValue = row["�ֿ�"].ToString();
                                    serNum = row["#"].ToString();
                                    page1LineNum = row["�����к�"].ToString();
                                    txtFKW.Focus();
                                    return;
                                }
                                ClearControl();
                                MessageBox.Show("��ǰ�����뵱ǰ���β�ƥ��", "����");
                                return;

                            }
                        }
                        ClearControl();
                        MessageBox.Show("ɨ�����������۶������ϲ�ƥ��", "����");

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
        /// ��ǰ�Ӳֿ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFCK_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQty.Text = "";
            txtFKW.ReadOnly = false;
            txtFKW.Focus();
        }
        private void cmbTCK_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQty.Text = "";
            txtTKW.ReadOnly = false;
            txtTKW.Focus();
        }
        /// <summary>
        /// �ӿ�λ���� �س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFKW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtItemCode.Text == "")
                {
                    MessageBox.Show("����ɨ������", "��ʾ");
                    txtFKW.Text = "";
                    txtBarcode.Focus();
                    return;
                }
                string binCode = txtFKW.Text.Trim();
                string fbinCode = txtFKW.Text.Trim();
                if (binCode == "")
                {
                    txtFKW.Focus();
                }
                else
                {
                    string whsCode = cmbFCK.SelectedValue.ToString();
                    if (binCode.IndexOf(",") != -1)
                    {
                        //��ѡ��Ĳֿ��Ϊɨ���
                        lFKW.Text = binCode.Split(',')[2];
                        whsCode = binCode.Split(',')[0];
                        fbinCode = binCode.Split(',')[1];
                    }
                    string itemCode = txtItemCode.Text;
                    string batch = txtBatch.Text;
                    DataTable page3Data = (DataTable)DataGrid2.DataSource;//����ҳ
                    DataRow[] checkWhsRows = page3Data.Select("�����к�='" + page1LineNum + "'");
                    if (checkWhsRows.Length > 0)
                    {
                        string page2WhsCodeText = checkWhsRows[0]["�Ӳֿ�"].ToString().Trim();
                        if (page2WhsCodeText != whsCode)
                        {
                            //�����ʱ���ŵ���,���������ϲ���ѡ�����������ϲֿ�
                            txtFKW.Text = "";
                            MessageBox.Show("ֻ��ѡ��˲ֿ⣺" + checkWhsRows[0]["�Ӳֿ�����"].ToString().Trim(), "��ʾ");
                        }
                        else
                        {
                            string sql = string.Format(@"SELECT T1.AbsEntry FROM {1}..OBBQ T0 
INNER JOIN {1}..OBIN T1 ON T0.BinAbs  = T1.AbsEntry
INNER JOIN {1}..OBTN T2 ON T2.AbsEntry = T0.SnBMDAbs   
where t0.OnHandQty <>0  and t2.ItemCode ='{0}' AND T2.DistNumber ='{2}' and T1.WhsCode ='{3}' and BinCode='{4}'", itemCode, ConnModel.commonDB, batch, whsCode, fbinCode);
                            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                lFKW.Text = dt.Rows[0]["AbsEntry"].ToString();
                                if (binCode.IndexOf(",") != -1)
                                {
                                    cmbFCK.SelectedValue = binCode.Split(',')[0];
                                }
                                cmbTCK.Focus();
                            }
                            else
                            {
                                txtFKW.Text = "";
                                MessageBox.Show("�����ϵĵ�ǰ�����ڵ�ǰ��λ������", "��ʾ");
                            }
                        }
                    }
                    else
                    {
                        string sql = string.Format(@"SELECT T1.AbsEntry FROM {1}..OBBQ T0 
INNER JOIN {1}..OBIN T1 ON T0.BinAbs  = T1.AbsEntry
INNER JOIN {1}..OBTN T2 ON T2.AbsEntry = T0.SnBMDAbs   
where t0.OnHandQty <>0  and t2.ItemCode ='{0}' AND T2.DistNumber ='{2}' and T1.WhsCode ='{3}' and BinCode='{4}'", itemCode, ConnModel.commonDB, batch, whsCode, fbinCode);
                        DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            lFKW.Text = dt.Rows[0]["AbsEntry"].ToString();
                            if (binCode.IndexOf(",") != -1)
                            {
                                cmbFCK.SelectedValue = binCode.Split(',')[0];
                            }
                            cmbTCK.Focus();
                        }
                        else
                        {
                            txtFKW.Text = "";
                            MessageBox.Show("�����ϵĵ�ǰ�����ڵ�ǰ��λ������", "��ʾ");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// ����λ���� �س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTKW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtItemCode.Text == "")
                {
                    MessageBox.Show("����ɨ������", "��ʾ");
                    txtTKW.Text = "";
                    txtBarcode.Focus();
                    return;
                }
                string binCode = txtTKW.Text.Trim();
                string tbinCode = txtTKW.Text.Trim();
                if (binCode == "")
                {
                    txtTKW.Focus();
                }
                else
                {
                    string whsCode = cmbTCK.SelectedValue.ToString();
                    if (binCode.IndexOf(",") != -1)
                    {
                        lTKW.Text = binCode.Split(',')[2];
                        whsCode = binCode.Split(',')[0];
                        tbinCode = binCode.Split(',')[1];
                    }
                      DataTable page3Data = (DataTable)DataGrid2.DataSource;//����ҳ
                    DataRow[] checkWhsRows = page3Data.Select("�����к�='" + page1LineNum + "'");
                    if (checkWhsRows.Length > 0)
                    {
                        string page2WhsCodeText = checkWhsRows[0]["���ֿ�"].ToString().Trim();
                        if (page2WhsCodeText != whsCode)
                        {
                            //�����ʱ���ŵ���,���������ϲ���ѡ�����������ϲֿ�
                            txtTKW.Text = "";
                            MessageBox.Show("ֻ��ѡ��˲ֿ⣺" + checkWhsRows[0]["���ֿ�����"].ToString().Trim(), "��ʾ");
                        }
                        else
                        {
                            string sql = string.Format("select AbsEntry from {1}..OBIN where BinCode = '{0}' and WhsCode='{2}'", tbinCode, ConnModel.commonDB, whsCode);
                            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string fbinCode = txtFKW.Text.Trim();
                                if (fbinCode.IndexOf(",") != -1)
                                {
                                    fbinCode = fbinCode.Split(',')[1];
                                }
                                if (whsCode == cmbFCK.SelectedValue.ToString() && tbinCode == fbinCode)
                                {
                                    txtTKW.Text = "";
                                    MessageBox.Show("�ջ��ֿⲻ���뷢���ֿ���ͬ", "��ʾ");
                                }
                                else
                                {

                                    lTKW.Text = dt.Rows[0]["AbsEntry"].ToString();
                                    if (binCode.IndexOf(",") != -1)
                                    {
                                        cmbTCK.SelectedValue = binCode.Split(',')[0];
                                    }
                                    txtQty.Focus();
                                    //b = true;
                                }
                            }
                            else
                            {
                                txtTKW.Text = "";
                                MessageBox.Show("��ǰ�ֿⲻ���ڸÿ�λ", "��ʾ");
                            }
                        }
                    }
                    else
                    {
                        string sql = string.Format("select AbsEntry from {1}..OBIN where BinCode = '{0}' and WhsCode='{2}'", tbinCode, ConnModel.commonDB, whsCode);
                        DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string fbinCode = txtFKW.Text.Trim();
                            if (fbinCode.IndexOf(",") != -1)
                            {
                                fbinCode = fbinCode.Split(',')[1];
                            }
                            if (whsCode == cmbFCK.SelectedValue.ToString() && tbinCode == fbinCode)
                            {
                                txtTKW.Text = "";
                                MessageBox.Show("�ջ��ֿⲻ���뷢���ֿ���ͬ", "��ʾ");
                            }
                            else
                            {

                                lTKW.Text = dt.Rows[0]["AbsEntry"].ToString();
                                if (binCode.IndexOf(",") != -1)
                                {
                                    cmbTCK.SelectedValue = binCode.Split(',')[0];
                                }
                                txtQty.Focus();
                            }
                        }
                        else
                        {
                            txtTKW.Text = "";
                            MessageBox.Show("��ǰ�ֿⲻ���ڸÿ�λ", "��ʾ");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// ��������ֻ��Ϊ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(this.txtQty.Text.Trim(), @"^[1-9]\d*$"))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region У��

            string docEntry = txtDocEntry.Text;
            if (docEntry == "")
            {
                MessageBox.Show("����ѡ�����۶���.", "��ʾ");
                return;
            }
            string itemCode = txtItemCode.Text.Trim();
            if (itemCode == "")
            {
                MessageBox.Show("����ɨ������.", "��ʾ");
                return;
            }
            if (txtFKW.ReadOnly == false && lFKW.Text == "0")
            {
                MessageBox.Show("�����ôӿ�λ", "��ʾ");
                return;
            }
            if (txtTKW.ReadOnly == false && lTKW.Text == "0")
            {
                MessageBox.Show("�����õ���λ", "��ʾ");
                return;
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("����д����", "��ʾ");
                return;
            }
            DataTable dt1 = (DataTable)DataGrid1.DataSource;
            DataRow[] row = dt1.Select("���ϱ���='" + itemCode + "'");
            int serNum = int.Parse(row[0]["#"].ToString());
            double syQty = double.Parse(row[0]["����"].ToString()) - double.Parse(row[0]["�������"].ToString());
            double srQty = double.Parse(txtQty.Text);
            if (srQty > syQty)
            {
                MessageBox.Show("ת������ʣ��Ϊ" + syQty, "��ʾ");
                return;
            }
            string batch = txtBatch.Text;
            string fromWhs = cmbFCK.SelectedValue.ToString();
            string fromWhsName = cmbFCK.Text;
            string binCode = txtFKW.Text;
            if (binCode.IndexOf(",") != -1)
            {
                binCode = binCode.Split(',')[1];
            }
            string sql = string.Format(@"
SELECT T0.OnHandQty
FROM {1}..OBBQ T0 
INNER JOIN {1}..OBIN T1 ON T0.BinAbs  = T1.AbsEntry
INNER JOIN {1}..OBTN T2 ON T2.AbsEntry = T0.SnBMDAbs   
INNER JOIN {1}..OITM T4 ON T0.ItemCode = T4.ItemCode 
where t0.OnHandQty <>0  and t2.ItemCode ='{0}' AND T2.DistNumber ='{2}' and T1.WhsCode ='{3}' and BinCode='{4}'", itemCode, ConnModel.commonDB, batch, fromWhs, binCode);
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            #endregion
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToDouble(srQty) > Convert.ToDouble(dt.Rows[0]["OnHandQty"].ToString()))
                {
                    MessageBox.Show(string.Format("�������Ϊ{0}", dt.Rows[0]["OnHandQty"].ToString()), "��ʾ");
                    txtQty.Focus();
                    return;
                }
                #region ������ֶ�

                string lineNum = row[0]["�����к�"].ToString();
                docEntry = row[0]["���ݱ��"].ToString();
                string itemName = row[0]["��������"].ToString();
                string quantity = row[0]["����"].ToString();
                string fromAbsEntry = "";
                string toWhs = cmbTCK.SelectedValue.ToString();
                string toWhsName = cmbTCK.Text;
                string toAbsEntry = "";
                string cardCode = row[0]["�ͻ�����"].ToString();
                string cardName = row[0]["�ͻ�����"].ToString();
                if (txtFKW.ReadOnly == false)
                {
                    fromAbsEntry = lFKW.Text;
                }
                string fBinCode = "";
                if (txtFKW.Text.IndexOf(',') == -1)
                {
                    fBinCode = txtFKW.Text;
                }
                else
                {
                    fBinCode = txtFKW.Text.Split(',')[1];
                }
                if (txtTKW.ReadOnly == false)
                {
                    toAbsEntry = lTKW.Text;
                }
                string tBinCode = "";
                if (txtTKW.Text.IndexOf(',') == -1)
                {
                    tBinCode = txtTKW.Text;
                }
                else
                {
                    tBinCode = txtTKW.Text.Split(',')[1];
                }
                //ת������ srQty
                string zdr = ConnModel.userName;
                #endregion

                DataTable dt2 = (DataTable)DataGrid2.DataSource;
                int sign = 0;

                #region ���¹���ҳ
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["���ϱ���"].ToString() == itemCode && dt2.Rows[i]["����"].ToString() == batch && dt2.Rows[i]["�Ӳֿ�"].ToString() == fromWhs && dt2.Rows[i]["�ӿ�λ��ʶ"].ToString() == fromAbsEntry && dt2.Rows[i]["���ֿ�"].ToString() == toWhs && dt2.Rows[i]["����λ��ʶ"].ToString() == toAbsEntry)
                    {
                        //ֻ�����ת������
                        DataGrid2[i, dt2.Columns.Count - 2] = double.Parse(dt2.Rows[i]["ת������"].ToString()) + srQty;
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
                    newRow["���ݱ��"] = docEntry;
                    newRow["�����к�"] = lineNum;
                    newRow["�ͻ�����"] = cardCode;
                    newRow["�ͻ�����"] = cardName;
                    newRow["���ϱ���"] = itemCode;
                    newRow["��������"] = itemName;
                    newRow["����"] = batch;
                    newRow["����"] = quantity;
                    newRow["�Ӳֿ�"] = fromWhs;
                    newRow["�Ӳֿ�����"] = fromWhsName;
                    newRow["�ӿ�λ��ʶ"] = fromAbsEntry;
                    newRow["�ӿ�λ"] = fBinCode;
                    newRow["���ֿ�"] = toWhs;
                    newRow["���ֿ�����"] = toWhsName;
                    newRow["����λ��ʶ"] = toAbsEntry;
                    newRow["����λ"] = tBinCode;
                    newRow["ת������"] = srQty;
                    newRow["�Ƶ���"] = zdr;
                    dt2.Rows.Add(newRow);
                    DataGrid2.DataSource = dt2;
                }

                #endregion

                DataGrid1[serNum, dt1.Columns.Count - 1] = double.Parse(row[0]["�������"].ToString()) + srQty;
                ClearControl();
            }
            else
            {
                MessageBox.Show("�������Ϊ0", "��ʾ");
                txtQty.Focus();
                return;
            }

        }

        private void btnR_Click(object sender, EventArgs e)
        {
            ClearControl();
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
                    if (dtInCome.Rows[i]["���ϱ���"].ToString() == dt.Rows[n]["���ϱ���"].ToString() && dtInCome.Rows[i]["�����к�"].ToString() == dt.Rows[n]["�����к�"].ToString())
                    {
                        double qty = Convert.ToDouble(dtInCome.Rows[i]["�������"]);
                        dtInCome.Rows[i]["�������"] = qty - Convert.ToDouble(dt.Rows[n]["ת������"]);
                        DataGrid1.DataSource = dtInCome;
                    }

                }
                //�����ݼ�������ɾ���� 
                dt.Rows.RemoveAt(n);
                //ˢ��Datagrid1��ʾɾ���������    
                DataGrid2.Refresh();
            }
        }
        private void btnInCome_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)DataGrid2.DataSource;
            //DataView dv = dt.DefaultView;
            //DataTable DistTable = dv.ToTable("Dist", true, "�������");
            //string parameter = string.Empty;
            //for (int i = 0; i < DistTable.Rows.Count; i++)
            //{
            //    parameter += "{Id:'";
            //    parameter += DistTable.Rows[i]["�������"].ToString();
            //    parameter += "'},";
            //}
            //parameter = parameter.Substring(0, parameter.Length - 1);
            string datatimeflg = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double qty = Convert.ToDouble(dt.Rows[i]["ת������"].ToString());
                    string sql = String.Format("INSERT INTO [Bar_XSMK_KCZC](DocEntry,LineNum,ItemCode,ItemName,Quantity,BatchNum,FromWhsCode,FromBinCode,FromAbsEntry,ToWhsCode,ToBinCode,ToAbsEntry,Creater,DocEntryFlag,FromWhsName,ToWhsName,CardCode,CardName) values ({0},{1},'{2}','{3}',{4},'{5}','{6}','{7}',{8},'{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}','{17}');",
                                    Convert.ToInt32(dt.Rows[i]["���ݱ��"].ToString()),
                                    Convert.ToInt32(dt.Rows[i]["�����к�"].ToString()),
                                    dt.Rows[i]["���ϱ���"].ToString(),
                                    dt.Rows[i]["��������"].ToString(),
                                    Convert.ToDouble(dt.Rows[i]["ת������"].ToString()),
                                    dt.Rows[i]["����"].ToString(),
                                    dt.Rows[i]["�Ӳֿ�"].ToString(),
                                    dt.Rows[i]["�ӿ�λ"].ToString(),
                                    Convert.ToInt32(dt.Rows[i]["�ӿ�λ��ʶ"].ToString()),
                                    dt.Rows[i]["���ֿ�"].ToString(),
                                    dt.Rows[i]["����λ"].ToString(),
                                    dt.Rows[i]["����λ��ʶ"].ToString() != "" ? Convert.ToInt32(dt.Rows[i]["����λ��ʶ"].ToString()) : 0,
                                    dt.Rows[i]["�Ƶ���"].ToString(),
                                    datatimeflg,
                                    dt.Rows[i]["�Ӳֿ�����"].ToString(),
                                    dt.Rows[i]["���ֿ�����"].ToString(),
                                    dt.Rows[i]["�ͻ�����"].ToString(),
                                    dt.Rows[i]["�ͻ�����"].ToString());
                    list.Add(sql);
                    sql = string.Format("");
                    //����SAPϵͳ�� ת�������������ֶ�
                    sql = string.Format("UPDATE {0}..RDR1 SET U_ZCQty =isnull( U_ZCQty,0) + {1} ,U_XSLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END where DocEntry = {2} and LineNum ={3}", ConnModel.commonDB, qty, Convert.ToInt32(dt.Rows[i]["���ݱ��"].ToString()), Convert.ToInt32(dt.Rows[i]["�����к�"].ToString()));
                    list.Add(sql);
                    sql = string.Format("");
                }
                int num = SqlHelper.ExecuteSqlTran(list);
                if (num != -1)
                {
                    ClearControl();
                    DataGrid1.DataSource = null;
                    DataGrid2.DataSource = null;
                    tabControl1.SelectedIndex = 0;
                    txtDocEntry.Text = "";
                    txtDocEntry.Focus();
                    MessageBox.Show("���ֶ���PC����ɹ��˲�����");
                    //if (SapHelper.isLogin)
                    //{
                    //    string url = "SalesReceive/Import";
                    //    string res = HttpHelper.HttpPost(url, parameter);
                    //    string mes = "";
                    //    if (res == "�����ѳ�ʱ��")
                    //    {
                    //        mes = "��������ʧ�ܣ����ֶ���PC����ɹ��˲�����";
                    //    }
                    //    else
                    //    {
                    //        mes = res;
                    //    }
                    //    MessageBox.Show(mes);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("���ֶ���PC����ɹ��˲�����");
                    //}
                }
                else
                {
                    MessageBox.Show("�������ݵ������ԣ��Ժ����ԣ�");
                }
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
            txtQty.Text = "";
            txtFKW.Text = "";
            txtTKW.Text = "";
            cmbFCK.Text = "";
            cmbTCK.Text = "";
            lFKW.Text = "0";
            lTKW.Text = "0";
            page1LineNum = "";
            BindWhs();
        }
        /// <summary>
        /// ���˴�����table
        /// </summary>
        /// <returns></returns>
        public void Grid3DataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("���ݱ��");
            table.Columns.Add("�����к�");
            table.Columns.Add("�ͻ�����");
            table.Columns.Add("�ͻ�����");
            table.Columns.Add("���ϱ���");
            table.Columns.Add("��������");
            table.Columns.Add("����");
            table.Columns.Add("�Ӳֿ�");
            table.Columns.Add("�Ӳֿ�����");
            table.Columns.Add("�ӿ�λ");
            table.Columns.Add("���ֿ�");
            table.Columns.Add("���ֿ�����");
            table.Columns.Add("����λ");
            table.Columns.Add("����");
            table.Columns.Add("�ӿ�λ��ʶ");
            table.Columns.Add("����λ��ʶ");
            table.Columns.Add("ת������");
            table.Columns.Add("�Ƶ���");

            DataGridTableStyle mydata2 = new DataGridTableStyle();
            GridColumnStylesCollection mycol2 = null;
            DataGrid2.DataSource = table;
            mydata2.MappingName = table.TableName;
            DataGrid2.TableStyles.Clear();
            DataGrid2.TableStyles.Add(mydata2);
            mycol2 = DataGrid2.TableStyles[0].GridColumnStyles;
            mycol2["���ݱ��"].Width = 0;
            mycol2["�����к�"].Width = 0;

        }
        /// <summary>
        /// �����вֿ�
        /// </summary>
        private void BindWhs()
        {
            string sql = "EXEC SAP_NEW_OWHS_BIN";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            DataTable dt1 = SqlHelper.GetDataTable(sql, CommandType.Text);
            cmbFCK.DataSource = dt;
            cmbTCK.DataSource = dt1;
            cmbFCK.DisplayMember = "WhsName";
            cmbFCK.ValueMember = "WhsCode";
            cmbTCK.DisplayMember = "WhsName";
            cmbTCK.ValueMember = "WhsCode";
        }

    }
}