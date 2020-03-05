using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA._2020
{
    public partial class SCMK_SCSH : Form
    {
        public SCMK_SCSH()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ����ҳ - ��ʼ��DataTable
        /// </summary>
        private DataTable Page3Data = new DataTable();


        #region Load
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SCMK_SCSH_Load(object sender, EventArgs e)
        {
            Page3Data.Columns.Add("#");
            Page3Data.Columns.Add("���ϱ���");
            Page3Data.Columns.Add("��������");
            Page3Data.Columns.Add("����");
            Page3Data.Columns.Add("�ֿ����");
            Page3Data.Columns.Add("�ֿ�����");
            Page3Data.Columns.Add("��λ��ʶ");
            Page3Data.Columns.Add("��λ����");
            Page3Data.Columns.Add("�������");
            Page3Data.Columns.Add("�������");
            Page3Data.Columns.Add("��������");
            Page3Data.Rows.Clear();

            #region �������п�λ�ֿ�
            string sql2 = "EXEC SAP_NEW_OWHS_BIN";
            DataTable page2Data = SqlHelper.GetDataTable(sql2, CommandType.Text);
            this.page2Whs.DataSource = page2Data;
            this.page2Whs.DisplayMember = "WhsName";
            this.page2Whs.ValueMember = "WhsCode";
            #endregion

            page1BarCode.Focus();
        }
        #endregion


        #region ����ҳ
        /// <summary>
        /// ������Ϣ�س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page1BarCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    #region �������֤
                    if (this.page1BarCode.Text.Trim() == "")
                    {
                        MessageBox.Show("������Ϣ����Ϊ��", "����");
                        return;
                    }
                    string page1BarCode = this.page1BarCode.Text.Trim();
                    if (page1BarCode.IndexOf(',') == -1)
                    {
                        MessageBox.Show("��ά������ʽ����ȷ", "����");
                        return;//ƥ������Ϊ�������һ��,��
                    }
                    #endregion

                    #region ����ҳ��ɨ��ҳ������ҳ�������
                    //1
                    this.page1Grid.DataSource = null;
                    //2 
                    page2AbsEntry = "";

                    this.page2DocEntry.Text = "";
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2BinCode.Text = "";
                    this.page2Qty.Text = "";
                    //3
                    this.page3Grid.DataSource = null;
                    #endregion

                    string itemCode = page1BarCode.Split(',')[0].ToString();
                    string itemName = page1BarCode.Split(',')[1].ToString();
                    string itemBatch = page1BarCode.Split(',')[2].ToString();
                    string docEntry = page1BarCode.Split(',')[3].ToString();
                    string sql1 = "exec SAP_NEW_SCSH_LOAD " + docEntry;
                    DataTable page1Data = SqlHelper.GetDataTable(sql1, CommandType.Text);
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
                        page1Mycol["���ϱ���"].Width = 80;
                        page1Mycol["��������"].Width = 80;
                        #endregion

                        #region ����ҳ ���ݶ������س���
                        DataTable page3Data = Page3Data;
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
                        page3Mycol["�������"].Width = 0;
                        page3Mycol["��������"].Width = 0;
                        #endregion

                        //��ת��ɨ��ҳ
                        tabControl1.SelectedIndex = 2;//�ȳ�ʼ��һ��ҳǩ,����ҳǩ3���ܸ���;������bug
                        tabControl1.SelectedIndex = 1;
                        //ɨ��ҳ��ֵ
                        page2DocEntry.Text = docEntry;
                        page2ItemCode.Text = itemCode;
                        page2ItemName.Text = itemName;
                        page2ItemBatch.Text = itemBatch;
                        this.page2Whs.SelectedValue = page1Data.Rows[0]["�ֿ�"].ToString();
                        this.page2BinCode.Focus();
                    }
                    else
                    {
                        MessageBox.Show("�ö����ѹرջ��߲�����...", "��ʾ");
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
        /// ��λ��ʶ��
        /// </summary>
        private string page2AbsEntry = "";


        /// <summary>
        /// �ֿⷢ���仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2Whs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.page2BinCode.Text = "";
            this.page2BinCode.Focus();
        }
        /// <summary>
        /// ��λ�س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2BinCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string page2WhsCodeText = "";
                string page2BinCodeText = "";
                string page2DocEntryText = this.page2DocEntry.Text.Trim();
                if (page2DocEntryText == "")
                {
                    MessageBox.Show("����ɨ���ά��,��ѡ���λ", "��ʾ");
                    this.page2BinCode.Text = "";
                    return;
                }
                string page2BinCode = this.page2BinCode.Text.Trim();
                if (page2BinCode == "")
                {
                    return;
                }
                if (page2BinCode.IndexOf(',') == -1)
                {

                    #region �ֶ�����
                    page2BinCodeText = page2BinCode;
                    page2WhsCodeText = this.page2Whs.SelectedValue.ToString();
                    string sql = "exec SAP_NEW_OWHS_OnBinCode '" + page2WhsCodeText + "','" + page2BinCodeText + "'";
                    DataTable whsBinDt = SqlHelper.GetDataTable(sql, CommandType.Text);
                    if (whsBinDt != null && whsBinDt.Rows.Count > 0)
                    {
                        DataTable page3Data = (DataTable)page3Grid.DataSource;//����ҳ
                        DataRow[] checkWhsRows = page3Data.Select("�������='" + page2DocEntryText + "'");
                        if (checkWhsRows.Length > 0)
                        {
                            string whsCode = checkWhsRows[0]["�ֿ����"].ToString().Trim();
                            if (page2WhsCodeText != whsCode)
                            {
                                //�����ʱ���ŵ���,���������ϲ���ѡ�����������ϲֿ�
                                this.page2BinCode.Text = "";
                                MessageBox.Show("ֻ��ѡ��˲ֿ⣺" + checkWhsRows[0]["�ֿ�����"].ToString().Trim(), "��ʾ");
                            }
                            else
                            {
                                page2AbsEntry = whsBinDt.Rows[0][0].ToString();
                                this.page2Qty.Focus();
                            }
                        }
                        else
                        {
                            page2AbsEntry = whsBinDt.Rows[0][0].ToString();
                            this.page2Qty.Focus();
                        }
                    }



                    #endregion

                }
                else
                {
                    #region ɨ��
                    page2BinCodeText = page2BinCode.Split(',')[1];
                    page2WhsCodeText = page2BinCode.Split(',')[0];
                    string sql = "exec SAP_NEW_OWHS_OnBinCode '" + page2WhsCodeText + "','" + page2BinCodeText + "'";
                    DataTable whsBinDt = SqlHelper.GetDataTable(sql, CommandType.Text);
                    if (whsBinDt != null && whsBinDt.Rows.Count > 0)
                    {
                        DataTable page3Data = (DataTable)page3Grid.DataSource;//����ҳ
                        DataRow[] checkWhsRows = page3Data.Select("�������='" + page2DocEntryText + "'");
                        if (checkWhsRows.Length > 0)
                        {
                            string whsCode = checkWhsRows[0]["�ֿ����"].ToString().Trim();
                            if (page2WhsCodeText != whsCode)
                            {
                                //�����ʱ���ŵ���,���������ϲ���ѡ�����������ϲֿ�
                                this.page2BinCode.Text = "";
                                MessageBox.Show("ֻ��ѡ��˲ֿ⣺" + checkWhsRows[0]["�ֿ�����"].ToString().Trim(), "��ʾ");
                            }
                            else
                            {
                                this.page2Whs.SelectedValue = page2WhsCodeText;
                                this.page2BinCode.Text = page2BinCode;
                                page2AbsEntry = whsBinDt.Rows[0][0].ToString();
                                this.page2Qty.Focus();
                            }
                        }
                        else
                        {
                            this.page2Whs.SelectedValue = page2WhsCodeText;
                            this.page2BinCode.Text = page2BinCode;
                            page2AbsEntry = whsBinDt.Rows[0][0].ToString();
                            this.page2Qty.Focus();
                        }
                    }
                    else
                    {
                        this.page2BinCode.Text = "";
                        MessageBox.Show("��λ��ά����ϵͳ�в����ڴ˿�λ", "��ʾ");
                    }
                    #endregion
                }
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
                if ((this.page2DocEntry.Text.Trim() == "") && (this.page2Qty.Text.Trim() == ""))
                {
                    return;
                }
                string docEntry = this.page2DocEntry.Text.Trim();
                string itemCode = this.page2ItemCode.Text.Trim();
                string itemName = this.page2ItemName.Text.Trim();
                string itemBatch = this.page2ItemBatch.Text.Trim();
                string whsCode = this.page2Whs.SelectedValue.ToString();
                string whsName = this.page2Whs.Text.Trim();
                string page2BinCode = this.page2BinCode.Text.Trim();
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
                double page2Qty = Convert.ToDouble(this.page2Qty.Text.Trim());
                if (page2AbsEntry == "")
                {
                    MessageBox.Show("����ɨ�����ϻ�ɨ���λ", "����");
                    return;
                }
                //У����������
                DataTable page1Data = (DataTable)page1Grid.DataSource;//����ҳ
                DataRow page1Row = page1Data.Select("��������=" + docEntry)[0];
                double page1Qty = Convert.ToDouble(page1Row["ʣ������"].ToString()) - Convert.ToDouble(page1Row["�������"].ToString());
                if (page2Qty > page1Qty)
                {
                    MessageBox.Show("ʣ���ջ�����Ϊ��" + page1Qty, "����");
                    return;
                }
                DataTable page3Data = (DataTable)page3Grid.DataSource;//����ҳ
                if (page3Data.Rows.Count > 0)
                {
                    //�ٴ��жϲֿ�
                    string whsCodeCheck = page3Data.Rows[0]["�ֿ����"].ToString().Trim();
                    if (whsCodeCheck != whsCode)
                    {
                        //�����ʱ���ŵ���,���������ϲ���ѡ�����������ϲֿ�
                        MessageBox.Show("ֻ��ѡ��˲ֿ⣺" + page3Data.Rows[0]["�ֿ�����"].ToString().Trim(), "��ʾ");
                        return;
                    }
                }

                //��Ӳ���
                DataRow[] page3Rows = page3Data.Select("�������='" + docEntry + "' and ����='" + itemBatch + "' and ��λ��ʶ='" + page2AbsEntry + "'");
                if (page3Rows.Length > 0)
                {
                    //����������� 8
                    int rowLine = int.Parse(page3Rows[0]["#"].ToString());
                    double qty = double.Parse(page3Rows[0]["�������"].ToString()) + page2Qty;
                    page3Grid[rowLine, 8] = qty.ToString();
                }
                else
                {
                    DataRow page3Row = page3Data.NewRow();
                    //�Ƿ�Ϊ����
                    int count = page3Data.Rows.Count == 0 ? 0 : page3Data.Rows.Count;
                    page3Row["#"] = count.ToString();
                    page3Row["���ϱ���"] = itemCode;
                    page3Row["��������"] = itemName;
                    page3Row["����"] = itemBatch;
                    page3Row["�ֿ����"] = whsCode;
                    page3Row["�ֿ�����"] = whsName;
                    page3Row["��λ��ʶ"] = page2AbsEntry;
                    page3Row["��λ����"] = page2BinCodeText;
                    page3Row["�������"] = page2Qty.ToString();
                    page3Row["�������"] = docEntry;
                    page3Data.Rows.Add(page3Row);
                    page3Grid.DataSource = page3Data;
                }
                //���¶���ҳ
                double updateQty = Convert.ToDouble(page1Grid[0, 4].ToString()) + page2Qty;
                page1Grid[0, 4] = updateQty.ToString();
                //���
                this.page2AbsEntry = "";
                this.page2BinCode.Text = "";
                this.page2Qty.Text = "";
                this.page2BinCode.Focus();
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
                double page3Qty = Convert.ToDouble(this.page3Grid[page3RowSelectIndex, 8].ToString());
                string page3ItemCode = this.page3Grid[page3RowSelectIndex, 1].ToString();
                //���¶���ҳ
                DataTable page1Data = (DataTable)page1Grid.DataSource;
                double page1Qty = (Convert.ToDouble(page1Data.Rows[0]["�������"].ToString())) - page3Qty;
                this.page1Grid[0, 4] = page1Qty;
                //���¹���ҳ
                page3Data.Rows.RemoveAt(page3RowSelectIndex);
                page3Grid.Refresh();
                MessageBox.Show("ɾ���ɹ�", "��ʾ");
            }
        }
        /// <summary>
        /// ���˵�ϵͳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page3BtnAdd_Click(object sender, EventArgs e)
        {
            string dateTimeStr = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            List<string> sql = new List<string>();
            //������м��
            DataTable page3Data = (DataTable)page3Grid.DataSource;//����ҳ
            if (page3Data.Rows.Count > 0)
            {
                foreach (DataRow row in page3Data.Rows)
                {
                    sql.Add(string.Format("insert into [dbo].[Bar_SCMK_SCSH] (DocEntry,ItemCode,ItemName,ItemBatch,Qty,WhsCode,WhsName,AbsEntry,BinCode,InWhsQty,UserSign,DocEntryFlag) values ({0},'{1}','{2}','{3}',{4},'{5}','{6}',{7},'{8}',{9},'{10}','{11}')", this.page2DocEntry.Text.Trim(), row["���ϱ���"].ToString(), row["��������"].ToString(), row["����"].ToString(), this.page1Grid[0, 2].ToString(), row["�ֿ����"].ToString(), row["�ֿ�����"].ToString(), row["��λ��ʶ"].ToString(), row["��λ����"].ToString(), row["�������"].ToString(), ConnModel.userName, dateTimeStr));
                }
                //����SAP�Զ����ֶ�
                DataTable page1Data = (DataTable)page1Grid.DataSource;//����ҳ
                foreach (DataRow row in page1Data.Rows)
                {
                    double page1Qty = Convert.ToDouble(row["�������"].ToString());
                    if (page1Qty > 0)
                    {
                        sql.Add(string.Format("UPDATE {0}.. OWOR SET U_SHQty ={1},U_SFWG = CASE WHEN PlannedQty = {1} THEN 'Y' ELSE 'W' END WHERE DocEntry = {2}", ConnModel.commonDB, (double.Parse(row["��������"].ToString()) + double.Parse(row["�������"].ToString()) - double.Parse(row["ʣ������"].ToString())), this.page2DocEntry.Text.Trim()));
                    }
                }
                if (SqlHelper.ExecuteSqlTran(sql) > 0)
                {
                    #region ���ɨ��ҳ�����������ص���һ��ҳǩ
                    //1
                    this.page1BarCode.Text = "";
                    this.page1Grid.DataSource = null;
                    //2 
                    page2AbsEntry = "";

                    this.page2DocEntry.Text = "";
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2BinCode.Text = "";
                    this.page2Qty.Text = "";
                    //3
                    this.page3Grid.DataSource = null;
                    this.Page3Data.Rows.Clear();
                    //4 ���ص���һ��ҳǩ
                    tabControl1.SelectedIndex = 0;
                    #endregion


                    //����API����ϵͳ����
                    //string url = "XSGL_XSTH/Import";
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