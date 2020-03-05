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
    public partial class frmXSJH1 : Form
    {
        public frmXSJH1()
        {
            InitializeComponent();
        }
        private System.Data.DataTable tbl;
        private string strItemCode;//��ȡ���ϱ���
        private int rowXS;//Ҫ��ӵ����ϱ�����к�
        private string cardCode = string.Empty; // �ͻ�����
        /// <summary>
        /// �״μ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmXSJH_Load(object sender, EventArgs e)
        {
            try
            {
                cmbSaleNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// ���۽���--ʧȥ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSaleNo_LostFocus(object sender, EventArgs e)
        {
            try
            {
                cmbKH.Text = string.Empty;
                cardCode = string.Empty;
                if (cmbSaleNo.Text != "")
                {
                    //�����ͻ�
                    string sqlkh = string.Format("SELECT CardCode,CardName from {0}..ORDR WHERE DocEntry = @docentry ", ConnModel.commonDB);
                    //�������۶����������۶�����ϸ�� AND U_XSQty < Quantity AND  LineStatus ='O' AND U_XSLineStatus = 'O'
                    string sql = string.Format("SELECT A.ItemCode as ���ϱ���, A.Dscription as ��������,A.Quantity as ����,(A.Quantity-isnull(A.U_ZCQty,0)) as ʣ��δ������,A.LineNum as �к�,A.WhsCode as �ֿ� FROM {0}..RDR1 A WHERE A.DocEntry = @docentry AND  A.OpenQty != 0 AND isnull(A.U_ZCQty,'0') < A.Quantity AND  A.LineStatus ='O' AND A.U_ZCLineStatus = 'O' ", ConnModel.commonDB);
                    SqlParameter[] ps = new SqlParameter[] { new SqlParameter("@docentry", cmbSaleNo.Text.ToString().Trim()) };
                    tbl = SqlHelper.GetDataTable(sql, CommandType.Text, ps);
                    SqlParameter[] paramkh = new SqlParameter[] { new SqlParameter("@docentry", cmbSaleNo.Text.ToString().Trim()) };
                    DataTable dtkh = SqlHelper.GetDataTable(sqlkh, CommandType.Text, paramkh);
                    if (dtkh != null && dtkh.Rows.Count > 0)
                    {
                        cmbKH.Text = dtkh.Rows[0]["CardName"].ToString();
                        cardCode = dtkh.Rows[0]["CardCode"].ToString();
                    }
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
                txtTraceNo.Text = "";
                tb_Quantity.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }

        }
        /// <summary>
        /// ���۽���--������Ϣ�����仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qrCodeStr = textBox1.Text.Trim();
                if (qrCodeStr != "")
                {
                    string[] array = qrCodeStr.Split(',');
                    if (array.Length == 5 && !qrCodeStr.EndsWith(","))
                    {
                        txtTraceNo.Text = qrCodeStr.Split(',')[0].Trim();
                        tb_Quantity.Text = qrCodeStr.Split(',')[4].Trim();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// ���۽���--������Ϣʧȥ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            try
            {
                string qrCodeStr = textBox1.Text.Trim();
                if (qrCodeStr != "")
                {
                    string[] array = qrCodeStr.Split(',');
                    if (array.Length == 5 && !qrCodeStr.EndsWith(","))
                    {
                        txtTraceNo.Text = qrCodeStr.Split(',')[0].Trim();
                        tb_Quantity.Text = qrCodeStr.Split(',')[4].Trim();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// ���۽���--���ϱ��뷢���仯
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
        /// ���۽���--�����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            txtTraceNo.Text = "";
            textBox1.Focus();
        }




        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                if (CheckData() == false)
                {
                    return;
                }
                System.Data.DataTable dt = (DataTable)DGsale.DataSource;

                if (dt.Rows.Count > 0)
                {
                    if (tbl.Rows[rowXS]["���ϱ���"].ToString() != txtTraceNo.Text.ToString())
                    {
                        MessageBox.Show("���ϲ�ƥ�䣬������ɨ�裡");
                        textBox1.Text = "";
                        txtTraceNo.Text = "";
                        textBox1.Focus();
                        return;
                    }
                    List<string> list = new List<string>();
                    if (double.Parse(tb_Quantity.Text.ToString()) > double.Parse(tbl.Rows[rowXS]["ʣ��δ������"].ToString()))
                    {
                        MessageBox.Show("����ӦС�ڵ��ڣ�" + tbl.Rows[rowXS]["ʣ��δ������"].ToString());
                        return;
                    }
                    if (tb_Quantity.Text.Equals(tbl.Rows[rowXS]["����"].ToString()))
                    {
                        string sqlInsert = string.Format("INSERT INTO SalesDelivery (SalesNum,ItemCode ,ItemName ,Quantity ,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", cmbSaleNo.Text.ToString(), tbl.Rows[rowXS]["���ϱ���"].ToString(), tbl.Rows[rowXS]["��������"].ToString(), double.Parse(tb_Quantity.Text.ToString()), int.Parse(tbl.Rows[rowXS]["�к�"].ToString()), cardCode, tbl.Rows[rowXS]["�ֿ�"].ToString());
                        list.Add(sqlInsert);
                    }
                    else
                    {
                        if (tbl.Rows[rowXS]["ʣ��δ������"].ToString().Equals(tbl.Rows[rowXS]["����"].ToString()))
                        {
                            string sqlInsert = string.Format("INSERT INTO SalesDelivery (SalesNum,ItemCode ,ItemName ,Quantity ,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", cmbSaleNo.Text.ToString(), tbl.Rows[rowXS]["���ϱ���"].ToString(), tbl.Rows[rowXS]["��������"].ToString(), double.Parse(tb_Quantity.Text.ToString()), int.Parse(tbl.Rows[rowXS]["�к�"].ToString()), cardCode, tbl.Rows[rowXS]["�ֿ�"].ToString());
                            list.Add(sqlInsert);
                        }
                        else
                        {
                            string sqlUpdate = string.Format("UPDATE SalesDelivery SET Quantity = {0} WHERE SalesNum={1} AND SalesNumLine={2}", (double.Parse(tb_Quantity.Text.ToString()) + (double.Parse(tbl.Rows[rowXS]["����"].ToString()) - double.Parse(tbl.Rows[rowXS]["ʣ��δ������"].ToString()))), cmbSaleNo.Text.ToString(), tbl.Rows[rowXS]["�к�"].ToString());
                            list.Add(sqlUpdate);
                        }
                    }

                    string sql = string.Format(@"UPDATE {0}.. RDR1 SET U_XSQty = {1},U_XSLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END WHERE 
DocEntry = {2} AND LineNum = {3} ", ConnModel.commonDB, (double.Parse(tb_Quantity.Text.ToString()) + (double.Parse(tbl.Rows[rowXS]["����"].ToString()) - double.Parse(tbl.Rows[rowXS]["ʣ��δ������"].ToString()))), cmbSaleNo.Text.ToString(), tbl.Rows[rowXS]["�к�"].ToString());
                    list.Add(sql);
                    if (SqlHelper.ExecuteSqlTran(list) > 0)
                    {
                        MessageBox.Show("��ӳɹ���");
                        cmbSaleNo_LostFocus(null, null);
                        init();
                    }
                    else
                    {
                        MessageBox.Show("���ʧ�ܣ�");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// �����ɺ�,��ʼ��
        /// </summary>
        private void init()
        {
            //ͬһ���۶����������Ƿ�ȫ������
            string sql3 = "SELECT U_XSLineStatus FROM " + ConnModel.commonDB + " ..RDR1 WHERE DocEntry =@docentry";
            SqlParameter ps3 = new SqlParameter("@docentry", cmbSaleNo.Text.ToString());

            DataTable tbl3 = SqlHelper.GetDataTable(sql3, CommandType.Text, ps3);
            if (tbl3.Rows.Count > 0)
            {
                int i = 0;
                int j = 0;
                for (i = 0; i <= tbl3.Rows.Count - 1; i++)
                {
                    if (tbl3.Rows[i]["U_XSLineStatus"].ToString() == "C")
                    {
                        j = j + 1;
                    }
                }
                if (j == tbl3.Rows.Count)
                {
                    string sql4 = "UPDATE " + ConnModel.commonDB + ".. ORDR SET U_XSStatus = 'C' WHERE  DocEntry =@docentry";
                    SqlParameter ps4 = new SqlParameter("@docentry", cmbSaleNo.Text.ToString());
                    SqlHelper.ExecuteNonquery(sql4, CommandType.Text, ps4);
                }
            }
            txtTraceNo.Text = "";
            tb_Quantity.Text = "";
            textBox1.Text = string.Empty;
            textBox1.Focus();

        }

       
        /// <summary>
        /// ��������Ƿ�Ϊ��
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (txtTraceNo.Text == "")
            {
                MessageBox.Show("���ϱ���Ϊ��ֵ��");
                textBox1.Text = string.Empty;
                textBox1.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
      
      
     
      

    }
}