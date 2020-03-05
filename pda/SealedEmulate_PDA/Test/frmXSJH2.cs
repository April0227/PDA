using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA
{
    public partial class frmXSJH2 : Form
    {
        public frmXSJH2()
        {
            InitializeComponent();
        }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        private string cardCode = string.Empty;
        /// <summary>
        /// ������Ϣҳǩ - �к�
        /// </summary>
        private int rowXS = 0;
        /// <summary>
        /// ����ɨ��� - ����
        /// </summary>
        private string pc = string.Empty;
        /// <summary>
        /// ������ϸҳǩ - �洢��������
        /// </summary>
        private const string currSql = "SBO_OnHandQtyForOrdr";
        /// <summary>
        /// ������ϸҳǩ - �к�
        /// </summary>
        private int rowNum;
        /// <summary>
        /// ������ϸҳǩ - �����ʣ���������(�������-��������)
        /// </summary>
        private double rowNumQty = 0;
        /// <summary>
        /// ������ϸҳǩ - �Ƿ�ˢ��
        /// </summary>
        private int currDocEntry;











        /// <summary>
        /// ���۶��� - �س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSaleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            { 
                cmbSaleNoChangeOrKeyPress();
            }
        }
        /// <summary>
        /// ���۽���--������Ϣ �س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1KeyPress();
            }
        }
        /// <summary>
        /// ���۽��� - ���ϱ����ı��� �����ı�
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
            DataTable dt = ((DataTable)DGsale.DataSource);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (val == dt.Rows[i]["���ϱ���"].ToString())
                    {
                        DGsale.Select(i);
                        rowXS = i;
                        this.btnSave.Enabled = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// �����ı��� - �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    string itemCode = txtTraceNo.Text.Trim();
                    double currQty = double.Parse(tb_Quantity.Text.Trim());
                    double xsSyQty = double.Parse(DGsale[rowXS, 3].ToString());
                    double xsQty = double.Parse(DGsale[rowXS, 4].ToString());

                    //�ж϶�����Ϣҳǩ (����ɨ��������� <= ʣ���������)
                    if (currQty > (xsSyQty - xsQty))
                    {
                        MessageBox.Show(string.Format("����ɨ����Ϣ:\r\n���ϱ���:{0}\r\nɨ������:{1}\r\n ��������:{2}\r\n��������Ϣҳǩ - ʣ���������{3}��", itemCode, pc, currQty, xsSyQty - xsQty));
                        return;
                    }

                    //�ж� ������ϸҳǩ (����ɨ��������� <= ʣ���������) 
                    DataTable dt = (DataTable)DG2.DataSource;
                    if (dt != null)
                    {
                        double sumQty = 0;
                        for (int i = rowNum; i < dt.Rows.Count; i++)
                        {
                            if (radioButton1.Checked == true)
                            {
                                if (dt.Rows[i][1].ToString() == itemCode)//"���ϱ���"
                                {
                                    double kcQty = double.Parse(DG2[i, 6].ToString());
                                    double currRowQty = double.Parse(DG2[i, 7].ToString());
                                    sumQty += (kcQty - currRowQty);
                                }
                            }
                            else
                            {
                                if (dt.Rows[i][1].ToString() == itemCode && dt.Rows[i][3].ToString() == pc)//���ϱ��� ����
                                {
                                    double kcQty = double.Parse(DG2[i, 6].ToString());
                                    double currRowQty = double.Parse(DG2[i, 7].ToString());
                                    sumQty += (kcQty - currRowQty);
                                }
                            }

                        }
                        if (currQty > sumQty)
                        {
                            MessageBox.Show(string.Format("����ɨ����Ϣ:\r\n���ϱ���:{0}\r\nɨ������:{1}\r\n ��������:{2}\r\n��������ϸҳǩ - ʣ�����������{3}��", itemCode, pc, currQty, sumQty));
                            return;
                        }
                        //������Ϣҳǩ - ����������ֵ 
                        DGsale[rowXS, 4] = xsQty + currQty;
                        //������ϸҳǩ - ����������ֵ
                        for (int i = rowNum; i < dt.Rows.Count; i++)
                        {
                            if (currQty != 0)
                            {
                                if (dt.Rows[i][1].ToString() == itemCode)//���ϱ���
                                {
                                    double kcQty = double.Parse(DG2[i, 6].ToString());//20
                                    double currRowQty = double.Parse(DG2[i, 7].ToString());//0
                                    double ckQty = kcQty - currRowQty;//20
                                    if (ckQty != 0)
                                    {
                                        //ckQty:20 currQty:30
                                        if (currQty < ckQty)
                                        {
                                            DG2[i, 7] = currQty + currRowQty;
                                            break;
                                        }
                                        else
                                        {
                                            DG2[i, 7] = kcQty.ToString();
                                            currQty = currQty - ckQty;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }




        /// <summary>
        /// ��� ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable tbl = (DataTable)DGsale.DataSource;
                if (tbl.Rows.Count < 0)
                {
                    MessageBox.Show("��ϸ��Ϊ��!");
                    return;
                }
                List<string> list = new List<string>();
                string salesNum = cmbSaleNo.Text.ToString();
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    string itemCode = tbl.Rows[i]["���ϱ���"].ToString();
                    string itemName = tbl.Rows[i]["��������"].ToString();
                    double xsQuanTity = double.Parse(tbl.Rows[i]["��������"].ToString());
                    double syQuanTity = double.Parse(tbl.Rows[i]["ʣ������"].ToString());
                    double quanTity = double.Parse(tbl.Rows[i]["��������"].ToString());
                    string docEntry = cmbSaleNo.Text.ToString();
                    int salesLineNum = int.Parse(tbl.Rows[i]["�к�"].ToString());
                    string whsCode = tbl.Rows[i]["�ֿ�"].ToString();
                    if (quanTity > 0)
                    {
                        //�Զ���� ��ӻ��߸����ж�
                        string sql = string.Format("SELECT ID FROM [dbo].[SalesDelivery] WHERE IsImport=0 AND SalesNum={0} and SalesNumLine={1}", salesNum, salesLineNum);
                        DataTable salesDt = SqlHelper.GetDataTable(sql, CommandType.Text);
                        if (salesDt.Rows.Count > 0)
                        {
                            list.Add(string.Format("UPDATE [dbo].[SalesDelivery] SET Quantity=Quantity+{2} WHERE IsImport=0 AND SalesNum={0} and SalesNumLine={1}", salesNum, salesLineNum, quanTity));
                        }
                        else
                        {
                            list.Add(string.Format("INSERT INTO SalesDelivery (SalesNum,ItemCode,ItemName,Quantity,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", salesNum, itemCode, itemName, quanTity, salesLineNum, cardCode, whsCode));
                        }
                        //SAP
                        list.Add(string.Format(@"UPDATE {0}.. RDR1 SET U_XSQty = {1},U_XSLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END WHERE DocEntry = {2} AND LineNum = {3} ", ConnModel.commonDB, (xsQuanTity + quanTity - syQuanTity), docEntry, salesLineNum));
                        DataTable dt = SqlHelper.GetDataTable(string.Format("SELECT U_XSLineStatus FROM {0}..RDR1 WHERE U_XSLineStatus='O' and DocEntry ={1}", ConnModel.commonDB, docEntry), CommandType.Text);
                        if (dt.Rows.Count > 0)
                        {
                            list.Add(string.Format("UPDATE {0}.. ORDR SET U_XSStatus = 'C' WHERE  DocEntry ={1}", ConnModel.commonDB, docEntry));
                        }
                        else
                        {
                            list.Add(string.Format("UPDATE {0}.. ORDR SET U_XSStatus = 'O' WHERE  DocEntry ={1}", ConnModel.commonDB, docEntry));
                        }
                    }
                }
                if (SqlHelper.ExecuteSqlTran(list) > 0)
                {
                    MessageBox.Show("��ӳɹ���");
                    cmbSaleNoChangeOrKeyPress();
                }
                else
                {
                    MessageBox.Show("���ʧ�ܣ�");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }








        //----------------------------------------------------------��׼��--------------------------------------------------//
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmXSJH2_Load(object sender, EventArgs e)
        {
            //�������ı����ý���
            cmbSaleNo.Focus();
        }
        private void cmbSaleNoChangeOrKeyPress()
        {
            try
            {
                textBox1.Text = "";
                txtTraceNo.Text = "";
                tb_Quantity.Text = "";
                cmbKH.Text = string.Empty;
                cardCode = string.Empty;
                if (cmbSaleNo.Text != "")
                {
                    int docEntry = int.Parse(cmbSaleNo.Text.ToString().Trim());
                    //�����ͻ�
                    DataTable dtkh = SqlHelper.GetDataTable(string.Format("SELECT CardCode,CardName from {0}..ORDR WHERE DocEntry = {1} ", ConnModel.commonDB, docEntry), CommandType.Text);
                    if (dtkh != null && dtkh.Rows.Count > 0)
                    {
                        cmbKH.Text = dtkh.Rows[0]["CardName"].ToString();
                        cardCode = dtkh.Rows[0]["CardCode"].ToString();
                    }
                    LoadOrdrPage(docEntry);
                    LoadItemPage(docEntry);
                    textBox1.Focus();
                }

              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// ������Ϣҳǩ
        /// </summary>
        /// <param name="docEntry"></param>
        private void LoadOrdrPage(int docEntry)
        {
            // ������Ϣ       �������۶����������۶�����ϸ�� AND U_XSQty < Quantity AND  LineStatus ='O' AND U_XSLineStatus = 'O'
            DataTable tbl = SqlHelper.GetDataTable(string.Format(@"SELECT A.ItemCode as ���ϱ���, A.Dscription as ��������,A.Quantity as ��������,(A.Quantity-isnull(A.U_XSQty,0)) as ʣ������, 0 as ��������,A.LineNum as �к�,A.WhsCode as �ֿ� FROM {0}..RDR1 A
WHERE A.DocEntry = {1} AND  A.OpenQty != 0 AND isnull(A.U_XSQty,'0') < A.Quantity AND  A.LineStatus ='O' AND A.U_XSLineStatus = 'O'", ConnModel.commonDB, docEntry), CommandType.Text);
            //����datagrid���п�
            DataGridTableStyle mydata = new DataGridTableStyle();
            GridColumnStylesCollection mycol = null;
            DGsale.DataSource = tbl;
            mydata.MappingName = tbl.TableName;
            DGsale.TableStyles.Clear();
            DGsale.TableStyles.Add(mydata);
            mycol = DGsale.TableStyles[0].GridColumnStyles;
            mycol[0].Width = 80;
            mycol[1].Width = 80;
        }
        /// <summary>
        /// ������ϸҳǩ
        /// </summary>
        /// <param name="docEntry"></param>
        private void LoadItemPage(int docEntry)
        {
            //������ϸ ���ݣ����ϱ���,�ֿ⣩
            DataTable tb2 = SqlHelper.GetDataTable(string.Format(" exec {1}..{2} {0},'' ", docEntry, ConnModel.commonDB, currSql), CommandType.Text);
            //����datagrid���п�
            DataGridTableStyle mydata2 = new DataGridTableStyle();
            GridColumnStylesCollection mycol2 = null;
            DG2.DataSource = tb2;
            mydata2.MappingName = tb2.TableName;
            DG2.TableStyles.Clear();
            DG2.TableStyles.Add(mydata2);
            mycol2 = DG2.TableStyles[0].GridColumnStyles;
            mycol2[1].Width = 80;
            mycol2[2].Width = 80;

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
        /// <summary>
        /// ���۽���--�����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            txtTraceNo.Text = "";
            tb_Quantity.Text = "";
            textBox1.Focus();
        }
        /// <summary>
        /// ������Ϣ - �س��¼�
        /// </summary>
        private void textBox1KeyPress()
        {
            try
            {
                if (DGsale.IsSelected(rowXS))
                {
                    DGsale.UnSelect(rowXS);
                    txtTraceNo.Text = "";
                    int docEntry = int.Parse(cmbSaleNo.Text.ToString().Trim());
                    //LoadItemPage(docEntry);//����Ϊʲô������д�����
                }
                string qrCodeStr = textBox1.Text.Trim();
                if (qrCodeStr != "")
                {
                    string[] array = qrCodeStr.Split(',');
                    if (array.Length == 5 && !qrCodeStr.EndsWith(","))
                    {
                        string itemCode = qrCodeStr.Split(',')[0].Trim();
                        string qty = qrCodeStr.Split(',')[4].Trim();
                        pc = qrCodeStr.Split(',')[2].Trim();
                        DataTable dt = (DataTable)DG2.DataSource;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //�жϵ�ǰɨ������������Ƿ�Ϊ��һ��
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][1].ToString() == itemCode)//���ϱ���
                                {
                                    double kcQty = double.Parse(dt.Rows[i][6].ToString());//�������
                                    double syQty = double.Parse(dt.Rows[i][7].ToString());//��������
                                    if (kcQty - syQty > 0)
                                    {
                                        string currPc = dt.Rows[i][3].ToString();//����
                                        if (currPc == pc)
                                        {
                                            txtTraceNo.Text = itemCode;
                                            tb_Quantity.Text = qty;
                                            rowNum = i;//�к�
                                            rowNumQty = kcQty - syQty;
                                            tb_Quantity.Focus();//�����ı����ý���  
                                            return;
                                        }
                                        else
                                        {
                                            MessageBox.Show(string.Format("����ɨ�����β�ƥ��\r\nɨ�����Ρ�{0}��\r\nӦɨ�����Ρ�{1}��", pc, currPc));
                                            textBox1.Text = "";
                                            textBox1.Focus();
                                            return;
                                        }
                                    }
                                }
                            }
                            MessageBox.Show(string.Format("������ϸҳǩ�����ڱ���ɨ��\r\n���ϱ��롾{0}��", itemCode));
                            textBox1.Text = "";
                            textBox1.Focus();
                        }
                        else
                        {
                            MessageBox.Show("��ǰ������ϸҳǩ����Ϊ��....");
                            textBox1.Text = "";
                            textBox1.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox1.Text = "";
                textBox1.Focus();
            }
        }





    }
}