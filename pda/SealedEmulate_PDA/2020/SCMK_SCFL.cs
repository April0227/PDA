using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;
using SealedEmulate_PDA._2020.Global;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA._2020
{
    public partial class SCMK_SCFL : Form
    {
        public SCMK_SCFL()
        {
            InitializeComponent();
        }

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SCMK_SCFL_Load(object sender, EventArgs e)
        {
            this.page1DocEntry.Focus();
        }
        #endregion



        #region ����ҳ
        /// <summary>
        /// ���������س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page1DocEntry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(this.page1DocEntry.Text.Trim(), @"^[1-9]\d*$"))
                    {
                        #region ����ҳ��ɨ��ҳ������ҳ�������
                        //1
                        this.page1Grid.DataSource = null;
                        //2
                        this.page2BarCode.Text = "";
                        this.page2ItemCode.Text = "";
                        this.page2ItemName.Text = "";
                        this.page2ItemBatch.Text = "";
                        this.page2WhsCode.Text = null;
                        this.page2BinCode.Text = "";
                        this.page2Qty.Text = "";
                        this.page2Batch.Checked = false;
                        //3
                        this.page3Grid.DataSource = null;
                        #endregion
                        int page1DocEntry = int.Parse(this.page1DocEntry.Text.Trim());
                        string sql = "exec SAP_NEW_SCFL_LOAD " + page1DocEntry;
                        DataTable page1Data = SqlHelper.GetDataTable(sql, CommandType.Text);
                        if (page1Data != null && page1Data.Rows.Count > 0)
                        {
                            #region ����ҳ���ñ����ʽ��������
                            DataGridTableStyle page1Mydata = new DataGridTableStyle();
                            GridColumnStylesCollection page1Mycol = null;
                            this.page1Grid.DataSource = page1Data;
                            page1Mydata.MappingName = page1Data.TableName;
                            this.page1Grid.TableStyles.Clear();
                            this.page1Grid.TableStyles.Add(page1Mydata);
                            page1Mycol = this.page1Grid.TableStyles[0].GridColumnStyles;
                            page1Mycol["#"].Width = 0;
                            page1Mycol["���ϱ���"].Width = 80;
                            page1Mycol["��������"].Width = 80;
                            page1Mycol["�к�"].Width = 0;                           
                            #endregion

                            #region ����ҳ ���ݶ������س���
                            string sql3 = "exec SAP_NEW_SCFL_LOAD_GZ " + page1DocEntry;
                            DataTable page3Data = SqlHelper.GetDataTable(sql3, CommandType.Text);
                            DataGridTableStyle page3Mydata = new DataGridTableStyle();
                            GridColumnStylesCollection page3Mycol = null;
                            this.page3Grid.DataSource = page3Data;
                            page3Mydata.MappingName = page3Data.TableName;
                            this.page3Grid.TableStyles.Clear();
                            this.page3Grid.TableStyles.Add(page3Mydata);
                            page3Mycol = this.page3Grid.TableStyles[0].GridColumnStyles;
                            page3Mycol["#"].Width = 0;
                            page3Mycol["���ϱ���"].Width = 80;
                            page3Mycol["��������"].Width = 80;
                            page3Mycol["�ֿ�����"].Width = 0;
                            page3Mycol["��λ��ʶ"].Width = 0;
                            page3Mycol["�Ƿ���Ȩ"].Width = 0;
                            page3Mycol["��Ȩ��"].Width = 0;
                            page3Mycol["�������"].Width = 0;
                            page3Mycol["�����к�"].Width = 0;
                            page3Mycol["��������"].Width = 0; 
                            #endregion
                            //��ת��ɨ��ҳ
                            tabControl1.SelectedIndex = 2;//�ȳ�ʼ��һ��ҳǩ,����ҳǩ3���ܸ���;������bug
                            tabControl1.SelectedIndex = 1; 
                        }
                        else
                        {
                            MessageBox.Show("�ö����ѷ��ϻ��߲�����...", "��ʾ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("���붩����Ų��Ϸ�!", "����");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����");
            }
        }
        #endregion



        #region ɨ��ҳ
        /// <summary>
        /// ɨ��ҳ - ������ʣ������������
        /// </summary>
        private double page2QuanTity = 0;
        /// <summary>
        /// ����ҳ - ��������ʣ���������ϼ�
        /// </summary>
        private double page3QuanTity = 0;
        /// <summary>
        /// ����ҳ - ������������Ӧ�Ŀ�λ����
        /// </summary>
        private Dictionary<string, double> page3BinCode = new Dictionary<string, double>();
        /// <summary>
        /// ����ҳ - ��һ��λ�Ŀ��
        /// </summary>
        private double page2BinCodeQty = 0;

        /// <summary>
        /// ������Ϣ�س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2BarCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.page1DocEntry.Text.Trim() == "")
                    {
                        MessageBox.Show("����������Ϊ��", "����");
                        return;
                    }
                    string page2BarCode = this.page2BarCode.Text.Trim();
                    if (page2BarCode == "")
                    {
                        return;
                    }
                    if (page2BarCode.IndexOf(',') == -1)
                    {
                        MessageBox.Show("��ά������ʽ����ȷ", "����");
                        return;//ƥ������Ϊ�������һ��,��
                    }
                    #region ɨ��ҳ���

                    page2QuanTity = 0;
                    page3QuanTity = 0;
                    page3BinCode.Clear();
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2WhsCode.Text = "";
                    this.page2BinCode.Text = "";
                    this.page2Qty.Text = "";
                    #endregion
                    string page2ItemCode = page2BarCode.Split(',')[0].Trim();
                    string page2ItemName = page2BarCode.Split(',')[1].Trim();
                    string page2ItemBatch = page2BarCode.Split(',')[2].Trim();
                    //�ж϶���ҳ���Ϻͳ�������
                    DataTable page1Data = (DataTable)page1Grid.DataSource;//����ҳ
                    DataRow[] page1Rows = page1Data.Select("���ϱ���='" + page2ItemCode + "'");
                    if (page1Rows.Length > 0)
                    {
                        DataRow page1Row = page1Rows[0];
                        if ((Convert.ToDouble(page1Row["ʣ������"].ToString()) - Convert.ToDouble(page1Row["��������"].ToString())) == 0)
                        {
                            MessageBox.Show("�������ѳ������", "��ʾ");
                            this.page2BarCode.Text = "";
                            this.page2BarCode.Focus();
                            return;
                        }
                        //�жϹ���ҳ���������Ƿ�Ϊ�Ƚ��ȳ�����
                        DataTable page3Data = (DataTable)page3Grid.DataSource;//����ҳ  
                        #region �ж��Ƿ�Ϊǿ���Ƚ��ȳ�

                        if (this.page2Batch.Checked == false)
                        {
                            DataRow[] page3Rows = page3Data.Select("���ϱ���='" + page2ItemCode + "'");
                            foreach (DataRow page3Row in page3Rows)
                            {
                                if ((Convert.ToDouble(page3Row["�������"].ToString()) - Convert.ToDouble(page3Row["��������"].ToString())) > 0)
                                {
                                    if (page3Row["����"].ToString().Trim() != page2ItemBatch)
                                    {
                                        MessageBox.Show("�����Ƚ��ȳ�ԭ��,Ӧ��ɨ������ϵ�����Ϊ��" + page3Row["����"].ToString().Trim(), "��ʾ");
                                        this.page2BarCode.Text = "";
                                        this.page2BarCode.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion

                        //�ж�ɨ�����������ʣ����
                        DataRow[] page3NewRows = page3Data.Select("���ϱ���='" + page2ItemCode + "' and ����='" + page2ItemBatch + "' and �ֿ����='" + page1Row["�ֿ�"].ToString() + "'");
                        foreach (DataRow page3Row in page3NewRows)
                        {
                            double qty = (Convert.ToDouble(page3Row["�������"].ToString()) - Convert.ToDouble(page3Row["��������"].ToString()));
                            page3QuanTity += qty;
                            page3BinCode.Add(page3Row["��λ����"].ToString(), qty);
                        }
                        if (page3QuanTity == 0)
                        {
                            page3BinCode.Clear();
                            MessageBox.Show("ɨ����������Ӧ�����ο��Ϊ0", "��ʾ");
                            this.page2BarCode.Text = "";
                            this.page2BarCode.Focus();
                            return;
                        }
                        //ɨ��ҳ��ֵ
                        this.page2ItemCode.Text = page2ItemCode;
                        this.page2ItemName.Text = page2ItemName;
                        this.page2ItemBatch.Text = page2ItemBatch;
                        this.page2WhsCode.Text = page1Row["�ֿ�"].ToString();
                        page2QuanTity = (Convert.ToDouble(page1Row["ʣ������"].ToString()) - Convert.ToDouble(page1Row["��������"].ToString()));
                        this.page2BinCode.Focus();
                    }
                    else
                    {
                        MessageBox.Show("ɨ�����������۶�����ƥ��", "��ʾ");
                        this.page2BarCode.Text = "";
                        this.page2BarCode.Focus();
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����");
            }
        }
        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2Batch_Click(object sender, EventArgs e)
        {
            if (this.page2Batch.Checked == true)
            {
                XSMK_XSJH_SQ frm = new XSMK_XSJH_SQ();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("��Ȩ�ɹ�.");
                }
                else
                {
                    this.page2Batch.Checked = false;
                    MessageBox.Show("��Ȩʧ��.");
                }
            }
        }
        /// <summary>
        /// У���λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2BinCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string page2BinCodeText = "";
                string page2BinCode = this.page2BinCode.Text.Trim();
                if (page2BinCode == "")
                {
                    return;
                }
                if (page2BinCode.IndexOf(',') == -1)
                {
                    //�ֶ�����
                    page2BinCodeText = page2BinCode;
                }
                else
                {
                    //ɨ��
                    page2BinCodeText = page2BinCode.Split(',')[1];
                }
                //У���λ�Ƿ����
                foreach (string binCode in page3BinCode.Keys)
                {
                    if (binCode == page2BinCodeText)
                    {
                        page2BinCodeQty = page3BinCode[binCode];
                        this.page2Qty.Focus();
                        return;
                    }
                }
                this.page2BinCode.Text = "";
                MessageBox.Show("ɨ����������Ӧ�����εĸÿ�λ���Ϊ0", "��ʾ");
            }
        }
        /// <summary>
        /// ��������У��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2Qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckPage2Qty();
            }
        }
        /// <summary>
        /// ��������У��
        /// </summary>
        private bool CheckPage2Qty()
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(this.page2Qty.Text.Trim(), @"^[1-9]\d*$"))
            {
                double page2Qty = Convert.ToDouble(this.page2Qty.Text.Trim());
                //У��ɨ��ҳ - ������ʣ���������
                if (page2Qty > page2QuanTity)
                {
                    this.page2Qty.Text = "";
                    MessageBox.Show("�����������ô���ʣ�����������" + page2QuanTity, "��ʾ");
                    return false;
                }
                //�ж��Ƿ������λ
                string page2BinCode = this.page2BinCode.Text.Trim();
                if (page2BinCode == "")
                {
                    //У�����ε��ܿ��
                    if (page2Qty > page3QuanTity)
                    {
                        this.page2Qty.Text = "";
                        MessageBox.Show("��ǰ����ʣ��������" + page3QuanTity, "��ʾ");
                        return false;
                    }
                }
                else
                {
                    if (page2Qty > page2BinCodeQty)
                    {
                        this.page2Qty.Text = "";
                        MessageBox.Show("��ǰ��λʣ��������" + page2BinCodeQty, "��ʾ");
                        return false;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("�����������Ϸ�!", "����");
                return false;
            }
        }
        /// <summary>
        /// ���������ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.page2BarCode.Text.Trim() != "") && (this.page2Qty.Text.Trim() != "") && CheckPage2Qty() == true)
                {
                    string page2ItemCode = this.page2ItemCode.Text.Trim();
                    string page2ItemBatch = this.page2ItemBatch.Text.Trim();
                    string page2WhsCode = this.page2WhsCode.Text.Trim();
                    string page2BinCode = this.page2BinCode.Text.Trim();
                    double page2Qty = Convert.ToDouble(this.page2Qty.Text.Trim());
                    //���¶���ҳ
                    DataTable page1Data = (DataTable)page1Grid.DataSource;
                    DataRow[] page1Rows = page1Data.Select("���ϱ���='" + page2ItemCode + "'");
                    int page1RowIndex = int.Parse(page1Rows[0]["#"].ToString());
                    //��������=��������+��������
                    double qty1 = Convert.ToDouble(this.page1Grid[page1RowIndex, 5].ToString()) + page2Qty;
                    this.page1Grid[page1RowIndex, 5] = qty1.ToString();
                    //���¹���ҳ
                    DataTable page3Data = (DataTable)page3Grid.DataSource;
                    if (page2BinCode == "")
                    {
                        //��ָ����λ����
                        string queryStr = string.Format("���ϱ���='{0}' and ����='{1}' and �ֿ����='{2}'", page2ItemCode, page2ItemBatch, page2WhsCode);
                        DataRow[] page3Rows = page3Data.Select(queryStr);
                        for (int i = 0; i < page3Rows.Length; i++)
                        {
                            double qty3 = Convert.ToDouble(page3Rows[i]["�������"].ToString()) - Convert.ToDouble(page3Rows[i]["��������"].ToString());
                            if (qty3 > 0 && page2Qty > 0)
                            {
                                int page3RowIndex = int.Parse(page3Rows[i]["#"].ToString());
                                if (qty3 > page2Qty)
                                {
                                    this.page3Grid[page3RowIndex, 9] = (Convert.ToDouble(page3Rows[i]["��������"].ToString()) + page2Qty).ToString();
                                    this.page3Grid[page3RowIndex, 10] = this.page2Batch.Checked == true ? "��" : "��";
                                    this.page3Grid[page3RowIndex, 11] = ConnModel.AuthorName;
                                    break;
                                }
                                page2Qty -= qty3;
                                this.page3Grid[page3RowIndex, 9] = page3Rows[i]["�������"].ToString();
                                this.page3Grid[page3RowIndex, 10] = this.page2Batch.Checked == true ? "��" : "��";
                                this.page3Grid[page3RowIndex, 11] = ConnModel.AuthorName;
                            }
                        }
                    }
                    else
                    {
                        //ָ����λ���� 
                        string page2BinCodeText = "";
                        if (page2BinCode.IndexOf(',') == -1)
                        {
                            //�ֶ�����
                            page2BinCodeText = page2BinCode;
                        }
                        else
                        {
                            //ɨ��
                            page2BinCodeText = page2BinCode.Split(',')[1];
                        }
                        string queryStr = string.Format("���ϱ���='{0}' and ����='{1}' and �ֿ����='{2}' and ��λ����='{3}'", page2ItemCode, page2ItemBatch, page2WhsCode, page2BinCodeText);
                        DataRow[] page3Rows = page3Data.Select(queryStr);
                        int page3RowIndex = int.Parse(page3Rows[0]["#"].ToString());
                        double qty3 = Convert.ToDouble(this.page3Grid[page3RowIndex, 9].ToString()) + page2Qty;
                        this.page3Grid[page3RowIndex, 9] = qty3.ToString();
                        this.page3Grid[page3RowIndex, 10] = this.page2Batch.Checked == true ? "��" : "��";
                        this.page3Grid[page3RowIndex, 11] = ConnModel.AuthorName;
                    }
                    #region ���ɨ��ҳ������
                    page2QuanTity = 0;
                    page3QuanTity = 0;
                    page3BinCode.Clear();
                    page2BinCodeQty = 0;
                    this.page2BarCode.Text = "";
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2WhsCode.Text = "";
                    this.page2BinCode.Text = "";
                    this.page2Qty.Text = "";
                    this.page2Batch.Checked = false;
                    #endregion

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����");
            }
        } 
        #endregion



        #region ����ҳ
        /// <summary>
        /// ɾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page3BtnDel_Click(object sender, EventArgs e)
        {
            DataTable page3Data = (DataTable)page3Grid.DataSource;//����ҳ  
            int page3RowSelectIndex = this.page3Grid.CurrentRowIndex;
            if (page3RowSelectIndex != -1)
            {
                double page3Qty = Convert.ToDouble(this.page3Grid[page3RowSelectIndex, 9].ToString());
                if (page3Qty > 0)
                {
                    string page3ItemCode = this.page3Grid[page3RowSelectIndex, 1].ToString();
                    //���¶���ҳ
                    DataTable page1Data = (DataTable)page1Grid.DataSource;
                    DataRow[] page1Rows = page1Data.Select("���ϱ���='" + page3ItemCode + "'");
                    int page1RowSelectIndex = int.Parse(page1Rows[0]["#"].ToString());
                    double page1Qty = (Convert.ToDouble(page1Rows[0]["��������"].ToString())) - page3Qty;
                    this.page1Grid[page1RowSelectIndex, 5] = page1Qty;
                    //���¹���ҳ
                    this.page3Grid[page3RowSelectIndex, 9] = 0.00;
                    this.page3Grid[page3RowSelectIndex, 10] = "��";
                    this.page3Grid[page3RowSelectIndex, 11] = "";
                    MessageBox.Show("ɾ���ɹ�", "��ʾ");
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page3BtnAdd_Click(object sender, EventArgs e)
        {
            string dateTimeStr = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            List<string> sql = new List<string>();
            //������м��
            DataTable page3Data = (DataTable)page3Grid.DataSource;//����ҳ
            foreach (DataRow row in page3Data.Rows)
            {
                double page3Qty = Convert.ToDouble(row["��������"].ToString());
                if (page3Qty > 0)
                {
                    sql.Add(string.Format("insert into [dbo].[Bar_SCMK_SCFL] (DocEntry,LineNum,ItemCode,ItemName,ItemBatch,Qty,WhsCode,WhsName,AbsEntry,BinCode,InWhsQty,UserSign,DocEntryFlag,IsAuthor,AuthorName) values ({0},{1},'{2}','{3}','{4}',{5},'{6}','{7}',{8},'{9}',{10},'{11}',{12},'{13}','{14}')", row["�������"].ToString(), row["�����к�"].ToString(), row["���ϱ���"].ToString(), row["��������"].ToString(), row["����"].ToString(), row["��������"].ToString(), row["�ֿ����"].ToString(), row["�ֿ�����"].ToString(), row["��λ��ʶ"].ToString(), row["��λ����"].ToString(), row["��������"].ToString(), ConnModel.userName, dateTimeStr, row["�Ƿ���Ȩ"].ToString(), row["��Ȩ��"].ToString()));
                }
            }
            if (sql.Count > 0)
            {
                //����SAP�Զ����ֶ�
                DataTable page1Data = (DataTable)page1Grid.DataSource;//����ҳ
                foreach (DataRow row in page1Data.Rows)
                {
                    double page1Qty = Convert.ToDouble(row["��������"].ToString());
                    if (page1Qty > 0)
                    {
                        sql.Add(string.Format("UPDATE {0}.. WOR1 SET U_SHQty={1},U_SFWG = CASE WHEN PlannedQty = {1} THEN 'Y' ELSE 'W' END WHERE DocEntry = {2} AND LineNum = {3}", ConnModel.commonDB, (double.Parse(row["�ƻ�����"].ToString()) + double.Parse(row["��������"].ToString()) - double.Parse(row["ʣ������"].ToString())), this.page1DocEntry.Text.Trim(), row["�к�"].ToString()));
                    }

                }
                if (SqlHelper.ExecuteSqlTran(sql) > 0)
                {
                    #region ���ɨ��ҳ�����������ص���һ��ҳǩ
                    //1
                    this.page1DocEntry.Text = "";
                    this.page1Grid.DataSource = null;
                    //2
                    page2QuanTity = 0;
                    page3QuanTity = 0;
                    page3BinCode.Clear();
                    page2BinCodeQty = 0;
                    this.page2BarCode.Text = "";
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2WhsCode.Text = "";
                    this.page2BinCode.Text = "";
                    this.page2Qty.Text = "";
                    this.page2Batch.Checked = false;
                    //3
                    this.page3Grid.DataSource = null;
                    //4 ���ص���һ��ҳǩ
                    tabControl1.SelectedIndex = 0;
                    #endregion


                    //����API����ϵͳ����
                    //string url = "SCGL_SCFL/Import";
                    //string ps = "{DocEntryFlag:'" + dateTimeStr + "'}";
                    //string valStr = HttpHelper.HttpPost(url, ps);
                    //if (valStr.Split(':')[0].ToString() == "0")
                    //{
                    //    MessageBox.Show("OK");
                    //}
                    //else
                    //{
                    //    MessageBox.Show(valStr.Split(':')[1].ToString(), "����");
                    //}
                }
                else
                {
                    MessageBox.Show("������м��ʧ��,��������������.", "��ʾ");
                }
            }
        }
        #endregion

      


    }
}