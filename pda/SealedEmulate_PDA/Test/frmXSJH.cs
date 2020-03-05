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
    public partial class frmXSJH : Form
    {
        public frmXSJH()
        {
            InitializeComponent();
        }
        private System.Data.DataTable tbl;
        private string strItemCode;//��ȡ���ϱ���
        private int rowXS;//Ҫ��ӵ����ϱ�����к�
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
                        string sqlInsert = string.Format("INSERT INTO SalesDelivery (SalesNum,ItemCode ,ItemName ,Quantity ,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", cmbSaleNo.SelectedValue.ToString(), tbl.Rows[0]["���ϱ���"].ToString(), tbl.Rows[0]["��������"].ToString(), double.Parse(tb_Quantity.Text.ToString()), int.Parse(tbl.Rows[0]["�к�"].ToString()), cmbKH.SelectedValue.ToString(), tbl.Rows[0]["�ֿ�"].ToString());
                        list.Add(sqlInsert);
                    }
                    else
                    {
                        if (tbl.Rows[rowXS]["ʣ��δ������"].ToString().Equals(tbl.Rows[rowXS]["����"].ToString()))
                        {
                            string sqlInsert = string.Format("INSERT INTO SalesDelivery (SalesNum,ItemCode ,ItemName ,Quantity ,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", cmbSaleNo.SelectedValue.ToString(), tbl.Rows[0]["���ϱ���"].ToString(), tbl.Rows[0]["��������"].ToString(), double.Parse(tb_Quantity.Text.ToString()), int.Parse(tbl.Rows[0]["�к�"].ToString()), cmbKH.SelectedValue.ToString(), tbl.Rows[0]["�ֿ�"].ToString());
                            list.Add(sqlInsert);
                        }
                        else
                        {
                            string sqlUpdate = string.Format("UPDATE SalesDelivery SET Quantity = {0} WHERE SalesNum={1} AND SalesNumLine={2}", (double.Parse(tb_Quantity.Text.ToString()) + (double.Parse(tbl.Rows[rowXS]["����"].ToString()) - double.Parse(tbl.Rows[rowXS]["ʣ��δ������"].ToString()))), cmbSaleNo.SelectedValue.ToString(), tbl.Rows[rowXS]["�к�"].ToString());
                            list.Add(sqlUpdate);
                        }
                    }

                    string sql = string.Format(@"UPDATE {0}.. RDR1 SET U_XSQty = {1},U_XSLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END WHERE 
DocEntry = {2} AND LineNum = {3} ", ConnModel.commonDB, (double.Parse(tb_Quantity.Text.ToString()) + (double.Parse(tbl.Rows[rowXS]["����"].ToString()) - double.Parse(tbl.Rows[rowXS]["ʣ��δ������"].ToString()))), cmbSaleNo.SelectedValue.ToString(), tbl.Rows[rowXS]["�к�"].ToString());
                    list.Add(sql);
                    ////���ȫ����������������۶���
                    //if (tb_Quantity.Text.Equals(tbl.Rows[rowXS]["ʣ��δ������"].ToString()))
                    //{
                    //    sql = string.Format("UPDATE {0}.. ORDR SET  U_XSStatus = 'C' WHERE DocEntry ={1} ", ConnModel.commonDB, int.Parse(cmbSaleNo.SelectedValue.ToString()));
                    //    list.Add(sql);
                    //}
                    if (SqlHelper.ExecuteSqlTran(list) > 0)
                    {
                        MessageBox.Show("��ӳɹ���");
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

        private void frmXSJH_Load(object sender, EventArgs e)
        {
            try
            {
                //�����ͻ�
                string sql = string.Format("SELECT CardCode, CardCode+'--'+CardName  as CardName FROM {0} ..OCRD WHERE CardType ='C'", ConnModel.commonDB);
                DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                cmbKH.ValueMember = "CardCode";
                cmbKH.DisplayMember = "CardName";
                cmbKH.DataSource = dt;
                textBox1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        private void cmbSaleNo_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSaleNo.Text != "")
                {
                    //�������۶����������۶�����ϸ�� AND U_XSQty < Quantity AND  LineStatus ='O' AND U_XSLineStatus = 'O'
                    string sql = string.Format("SELECT ItemCode as ���ϱ���, Dscription as ��������,Quantity as ����,(Quantity-isnull(U_XSQty,0)) as ʣ��δ������,LineNum as �к�,WhsCode as �ֿ� FROM {0} ..RDR1 WHERE DocEntry = @docentry AND  OpenQty != 0 AND isnull(U_XSQty,'0') < Quantity AND  LineStatus ='O' AND U_XSLineStatus = 'O'  and U_ZCLineStatus='O'", ConnModel.commonDB);
                    SqlParameter[] ps = new SqlParameter[] { new SqlParameter("@docentry", cmbSaleNo.SelectedValue.ToString()) };
                    tbl = SqlHelper.GetDataTable(sql, CommandType.Text, ps);

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
        //��������Ƿ�Ϊ��
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
        private void init()
        {
            //ͬһ���۶����������Ƿ�ȫ������
            string sql3 = "SELECT U_XSLineStatus FROM " + ConnModel.commonDB + " ..RDR1 WHERE DocEntry =@docentry";
            SqlParameter ps3 = new SqlParameter("@docentry", cmbSaleNo.SelectedValue.ToString());

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
                    SqlParameter ps4 = new SqlParameter("@docentry", cmbSaleNo.SelectedValue.ToString());
                    SqlHelper.ExecuteNonquery(sql4, CommandType.Text, ps4);
                }
            }


            //����δ���������۶��� AND U_XSStatus = 'O'
            string sql = string.Format("SELECT DocEntry FROM {0} ..ORDR WHERE DocStatus = 'O' AND U_XSStatus = 'O'  AND CardCode ='{1}'", ConnModel.commonDB, cmbKH.SelectedValue.ToString());
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            cmbSaleNo.ValueMember = "DocEntry";
            cmbSaleNo.DataSource = dt;
            txtTraceNo.Text = "";
            tb_Quantity.Text = "";
            textBox1.Text = string.Empty;
            textBox1.Focus();

        }

        private void cmbKH_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbKH.Text != "")
                {
                    //����δ���������۶��� AND U_XSStatus = 'O'
                    string sql = string.Format("SELECT DocEntry FROM {0} ..ORDR WHERE DocStatus = 'O'  AND U_XSStatus = 'O' AND CardCode ='{1}'", ConnModel.commonDB, cmbKH.SelectedValue.ToString());
                    DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                    if (dt != null)
                    {
                        cmbSaleNo.ValueMember = "DocEntry";
                        cmbSaleNo.DataSource = dt;
                    }
                    else
                    {
                        DGsale.DataSource = null;
                    }


                }
                txtTraceNo.Text = "";
                tb_Quantity.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }

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
                
                MessageBox.Show (ex.Message );
            }
           
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            txtTraceNo.Text = "";
            textBox1.Focus();
        }
    }
}