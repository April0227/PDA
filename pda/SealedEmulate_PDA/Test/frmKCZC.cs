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
    public partial class frmKCZC : Form
    {
        public frmKCZC()
        {
            InitializeComponent();
        }
        private System.Data.DataTable tbl;
        private string strItemCode;//获取物料编码
        private int rowXS;//要添加的物料编码的行号
        private string pici = string.Empty;// 批次
        private string cardCode = string.Empty; // 客户编码
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
                    if (tbl.Rows[rowXS]["物料编码"].ToString() != txtTraceNo.Text.ToString())
                    {
                        MessageBox.Show("物料不匹配，请重新扫描！");
                        textBox1.Text = "";
                        txtTraceNo.Text = "";
                        textBox1.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(tb_Quantity.Text))
                    {
                        MessageBox.Show("转储数量不能为空值！");
                        return;
                    }
                    string sql1 = "SELECT TOP 1 B.DistNumber FROM  " + ConnModel.commonDB + "..OBBQ AS A JOIN  " + ConnModel.commonDB + "..OBTN AS B ON A.SnBMDAbs=B.AbsEntry JOIN " + ConnModel.commonDB + "..OITM AS C ON c.DfltWH=A.WhsCode WHERE A.ItemCode='" + txtTraceNo.Text.ToString() + "' AND A.OnHandQty>0 ORDER BY B.DistNumber";
                    dt = SqlHelper.GetDataTable(sql1, CommandType.Text);

                    if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(pici) && dt.Rows[0][0] != null && pici.ToLower() != dt.Rows[0][0].ToString().ToLower())
                    {
                        MessageBox.Show("该批次不是最早的批次!");
                        textBox1.Text = "";
                        txtTraceNo.Text = "";
                        textBox1.Focus();
                        return;
                    }

                    List<string> list = new List<string>();
                    if (double.Parse(tb_Quantity.Text.ToString()) > double.Parse(tbl.Rows[rowXS]["剩余未清数量"].ToString()))
                    {
                        MessageBox.Show("转储数量应小于等于：" + tbl.Rows[rowXS]["剩余未清数量"].ToString());
                        return;
                    }
                    if (tb_Quantity.Text.Equals(tbl.Rows[rowXS]["数量"].ToString()))
                    {
                        string sqlInsert = string.Format("INSERT INTO StockTransfer (SalesNum,ItemCode ,ItemName ,Quantity ,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", txtDDH.Text.ToString().Trim(), tbl.Rows[rowXS]["物料编码"].ToString(), tbl.Rows[rowXS]["物料名称"].ToString(), double.Parse(tb_Quantity.Text.ToString()), int.Parse(tbl.Rows[rowXS]["行号"].ToString()), cardCode, tbl.Rows[rowXS]["仓库"].ToString());
                        list.Add(sqlInsert);
                    }
                    else
                    { 
                        string sqlInsert = string.Format("INSERT INTO StockTransfer (SalesNum,ItemCode ,ItemName ,Quantity ,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", txtDDH.Text.ToString().Trim(), tbl.Rows[rowXS]["物料编码"].ToString(), tbl.Rows[rowXS]["物料名称"].ToString(), double.Parse(tb_Quantity.Text.ToString()), int.Parse(tbl.Rows[rowXS]["行号"].ToString()), cardCode, tbl.Rows[rowXS]["仓库"].ToString());
                        list.Add(sqlInsert); 
                    }

                    string sql = string.Format(@"UPDATE {0}.. RDR1 SET U_ZCQty = {1},U_ZCLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END WHERE 
DocEntry = {2} AND LineNum = {3} ", ConnModel.commonDB, (double.Parse(tb_Quantity.Text.ToString()) + (double.Parse(tbl.Rows[rowXS]["数量"].ToString()) - double.Parse(tbl.Rows[rowXS]["剩余未清数量"].ToString()))), txtDDH.Text.ToString().Trim(), tbl.Rows[rowXS]["行号"].ToString());
                    list.Add(sql);  
                    if (list != null && list.Count > 0)
                    {
                        if (SqlHelper.ExecuteSqlTran(list) > 0)
                        {
                            txtDDH_LostFocus(null,null);
                            init();
                            MessageBox.Show("添加成功！");
                        }
                        else
                        {
                            MessageBox.Show("添加失败！");
                        }

                    }
                    else
                    {
                        MessageBox.Show("无转储项！");
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
                //检索客户
                //string sql = string.Format("SELECT CardCode, CardCode+'--'+CardName  as CardName FROM {0} ..OCRD WHERE CardType ='C'", ConnModel.commonDB);
                //DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                //cmbKH.ValueMember = "CardCode";
                //cmbKH.DisplayMember = "CardName";
                //cmbKH.DataSource = dt;
                //textBox1.Focus();
                txtDDH.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        //private void cmbSaleNo_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (cmbSaleNo.Text != "")
        //        {
        //            //根据销售订单检索销售订单明细行 AND U_XSQty < Quantity AND  LineStatus ='O' AND U_XSLineStatus = 'O'
        //            string sql = string.Format("SELECT ItemCode as 物料编码, Dscription as 物料名称,Quantity as 数量,(Quantity-isnull(U_ZCQty,0)) as 剩余未清数量,LineNum as 行号,WhsCode as 仓库 FROM {0} ..RDR1 WHERE DocEntry = @docentry AND  OpenQty != 0 AND isnull(U_ZCQty,'0') < Quantity AND  LineStatus ='O' AND U_ZCLineStatus = 'O' ", ConnModel.commonDB);
        //            SqlParameter[] ps = new SqlParameter[] { new SqlParameter("@docentry", cmbSaleNo.SelectedValue.ToString()) };
        //            tbl = SqlHelper.GetDataTable(sql, CommandType.Text, ps);

        //            //设置datagrid的列宽
        //            DataGridTableStyle mydata = new DataGridTableStyle();
        //            GridColumnStylesCollection mycol = null;
        //            DGsale.DataSource = tbl;
        //            mydata.MappingName = tbl.TableName;
        //            DGsale.TableStyles.Clear();
        //            DGsale.TableStyles.Add(mydata);
        //            mycol = DGsale.TableStyles[0].GridColumnStyles;
        //            mycol[0].Width = 80;
        //            mycol[1].Width = 80;
        //        }
        //        txtTraceNo.Text = "";
        //        tb_Quantity.Text = "";
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.StackTrace);
        //    }

        //}
        //检查数据是否为空
        private bool CheckData()
        {
            if (txtTraceNo.Text == "")
            {
                MessageBox.Show("物料编码为空值！");
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
            //同一销售订单，检索是否全部销售
            string sql3 = "SELECT U_ZCLineStatus FROM " + ConnModel.commonDB + " ..RDR1 WHERE DocEntry =@docentry";
            SqlParameter ps3 = new SqlParameter("@docentry", txtDDH.Text.ToString().Trim());

            DataTable tbl3 = SqlHelper.GetDataTable(sql3, CommandType.Text, ps3);
            if (tbl3.Rows.Count > 0)
            {
                int i = 0;
                int j = 0;
                for (i = 0; i <= tbl3.Rows.Count - 1; i++)
                {
                    if (tbl3.Rows[i]["U_ZCLineStatus"].ToString() == "C")
                    {
                        j = j + 1;
                    }
                }
                if (j == tbl3.Rows.Count)
                {
                    string sql4 = "UPDATE " + ConnModel.commonDB + ".. ORDR SET U_ZCStatus = 'C' WHERE  DocEntry =@docentry";
                    SqlParameter ps4 = new SqlParameter("@docentry", txtDDH.Text.ToString().Trim());
                    SqlHelper.ExecuteNonquery(sql4, CommandType.Text, ps4);
                }
            }


            //检索未交货的销售订单 AND U_XSStatus = 'O'
            //string sql = string.Format("SELECT DocEntry FROM {0} ..ORDR WHERE DocStatus = 'O' AND U_ZCStatus = 'O'  AND CardCode ='{1}'", ConnModel.commonDB, cmbKH.SelectedValue.ToString());
            //DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            //cmbSaleNo.ValueMember = "DocEntry";
            //cmbSaleNo.DataSource = dt;
            //txtTraceNo.Text = "";
            //tb_Quantity.Text = "";
            textBox1.Text = string.Empty;
            textBox1.Focus();

        }

        //private void cmbKH_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (cmbKH.Text != "")
        //        {
        //            //检索未转储的销售订单 AND U_XSStatus = 'O'
        //            string sql = string.Format("SELECT DocEntry FROM {0} ..ORDR WHERE DocStatus = 'O'  AND U_ZCStatus = 'O' AND CardCode ='{1}'", ConnModel.commonDB, cmbKH.SelectedValue.ToString());
        //            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
        //            if (dt != null)
        //            {
        //                cmbSaleNo.ValueMember = "DocEntry";
        //                cmbSaleNo.DataSource = dt;
        //            }
        //            else
        //            {
        //                DGsale.DataSource = null;
        //            }


        //        }
        //        txtTraceNo.Text = "";
        //        tb_Quantity.Text = "";
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.StackTrace);
        //    }
        //}

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            try
            {
                pici = string.Empty;
                string qrCodeStr = textBox1.Text.Trim();
                if (qrCodeStr != "")
                {
                    string[] array = qrCodeStr.Split(',');
                    if (array.Length == 5 && !qrCodeStr.EndsWith(","))
                    {
                        txtTraceNo.Text = array[0].Trim();
                        pici = array[2].Trim();
                        tb_Quantity.Text = array[4].Trim();

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pici = string.Empty;
                string qrCodeStr = textBox1.Text.Trim();
                if (qrCodeStr != "")
                {
                    string[] array = qrCodeStr.Split(',');
                    if (array.Length == 5 && !qrCodeStr.EndsWith(","))
                    {
                        txtTraceNo.Text = array[0].Trim();
                        pici = array[2].Trim();
                        tb_Quantity.Text = array[4].Trim();
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
                    if (val == dt.Rows[i]["物料编码"].ToString())
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

        private void txtDDH_LostFocus(object sender, EventArgs e)
        {
            try
            {
                txtKH.Text = string.Empty;
                cardCode = string.Empty;
                if (txtDDH.Text != "")
                {
                    //根据销售订单检索销售订单明细行 AND U_XSQty < Quantity AND  LineStatus ='O' AND U_XSLineStatus = 'O'
                    string sqlkh = string.Format("SELECT CardCode,CardName from {0}..ORDR WHERE DocEntry = @docentry", ConnModel.commonDB);
                    string sql = string.Format("SELECT A.ItemCode as 物料编码, A.Dscription as 物料名称,A.Quantity as 数量,(A.Quantity-isnull(A.U_ZCQty,0)) as 剩余未清数量,A.LineNum as 行号,A.WhsCode as 仓库 FROM {0}..RDR1 A WHERE A.DocEntry = @docentry AND  A.OpenQty <> 0 AND isnull(A.U_ZCQty,'0') < A.Quantity AND  A.LineStatus ='O' AND A.U_ZCLineStatus = 'O' ", ConnModel.commonDB);
                    SqlParameter[] ps = new SqlParameter[] { new SqlParameter("@docentry", txtDDH.Text.ToString().Trim()) };
                    tbl = SqlHelper.GetDataTable(sql, CommandType.Text, ps);
                    SqlParameter[] paramkh = new SqlParameter[] { new SqlParameter("@docentry", txtDDH.Text.ToString().Trim()) };
                    DataTable dtkh = SqlHelper.GetDataTable(sqlkh, CommandType.Text, paramkh);
                    if (dtkh != null && dtkh.Rows.Count > 0)
                    {
                        txtKH.Text = dtkh.Rows[0]["CardName"].ToString();
                        cardCode = dtkh.Rows[0]["CardCode"].ToString();
                    }
                    //设置datagrid的列宽
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

    }
}