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
        private string strItemCode;//获取物料编码
        private int rowXS;//要添加的物料编码的行号
        private string cardCode = string.Empty; // 客户编码
        /// <summary>
        /// 首次加载
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
        /// 销售交货--失去焦点
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
                    //检索客户
                    string sqlkh = string.Format("SELECT CardCode,CardName from {0}..ORDR WHERE DocEntry = @docentry ", ConnModel.commonDB);
                    //根据销售订单检索销售订单明细行 AND U_XSQty < Quantity AND  LineStatus ='O' AND U_XSLineStatus = 'O'
                    string sql = string.Format("SELECT A.ItemCode as 物料编码, A.Dscription as 物料名称,A.Quantity as 数量,(A.Quantity-isnull(A.U_ZCQty,0)) as 剩余未清数量,A.LineNum as 行号,A.WhsCode as 仓库 FROM {0}..RDR1 A WHERE A.DocEntry = @docentry AND  A.OpenQty != 0 AND isnull(A.U_ZCQty,'0') < A.Quantity AND  A.LineStatus ='O' AND A.U_ZCLineStatus = 'O' ", ConnModel.commonDB);
                    SqlParameter[] ps = new SqlParameter[] { new SqlParameter("@docentry", cmbSaleNo.Text.ToString().Trim()) };
                    tbl = SqlHelper.GetDataTable(sql, CommandType.Text, ps);
                    SqlParameter[] paramkh = new SqlParameter[] { new SqlParameter("@docentry", cmbSaleNo.Text.ToString().Trim()) };
                    DataTable dtkh = SqlHelper.GetDataTable(sqlkh, CommandType.Text, paramkh);
                    if (dtkh != null && dtkh.Rows.Count > 0)
                    {
                        cmbKH.Text = dtkh.Rows[0]["CardName"].ToString();
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
        /// <summary>
        /// 销售交货--条码信息发生变化
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
        /// 销售交货--条码信息失去焦点
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
        /// 销售交货--物料编码发生变化
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
        /// <summary>
        /// 销售交货--清除按钮
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
        /// 添加 按钮
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
                    if (tbl.Rows[rowXS]["物料编码"].ToString() != txtTraceNo.Text.ToString())
                    {
                        MessageBox.Show("物料不匹配，请重新扫描！");
                        textBox1.Text = "";
                        txtTraceNo.Text = "";
                        textBox1.Focus();
                        return;
                    }
                    List<string> list = new List<string>();
                    if (double.Parse(tb_Quantity.Text.ToString()) > double.Parse(tbl.Rows[rowXS]["剩余未清数量"].ToString()))
                    {
                        MessageBox.Show("数量应小于等于：" + tbl.Rows[rowXS]["剩余未清数量"].ToString());
                        return;
                    }
                    if (tb_Quantity.Text.Equals(tbl.Rows[rowXS]["数量"].ToString()))
                    {
                        string sqlInsert = string.Format("INSERT INTO SalesDelivery (SalesNum,ItemCode ,ItemName ,Quantity ,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", cmbSaleNo.Text.ToString(), tbl.Rows[rowXS]["物料编码"].ToString(), tbl.Rows[rowXS]["物料名称"].ToString(), double.Parse(tb_Quantity.Text.ToString()), int.Parse(tbl.Rows[rowXS]["行号"].ToString()), cardCode, tbl.Rows[rowXS]["仓库"].ToString());
                        list.Add(sqlInsert);
                    }
                    else
                    {
                        if (tbl.Rows[rowXS]["剩余未清数量"].ToString().Equals(tbl.Rows[rowXS]["数量"].ToString()))
                        {
                            string sqlInsert = string.Format("INSERT INTO SalesDelivery (SalesNum,ItemCode ,ItemName ,Quantity ,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", cmbSaleNo.Text.ToString(), tbl.Rows[rowXS]["物料编码"].ToString(), tbl.Rows[rowXS]["物料名称"].ToString(), double.Parse(tb_Quantity.Text.ToString()), int.Parse(tbl.Rows[rowXS]["行号"].ToString()), cardCode, tbl.Rows[rowXS]["仓库"].ToString());
                            list.Add(sqlInsert);
                        }
                        else
                        {
                            string sqlUpdate = string.Format("UPDATE SalesDelivery SET Quantity = {0} WHERE SalesNum={1} AND SalesNumLine={2}", (double.Parse(tb_Quantity.Text.ToString()) + (double.Parse(tbl.Rows[rowXS]["数量"].ToString()) - double.Parse(tbl.Rows[rowXS]["剩余未清数量"].ToString()))), cmbSaleNo.Text.ToString(), tbl.Rows[rowXS]["行号"].ToString());
                            list.Add(sqlUpdate);
                        }
                    }

                    string sql = string.Format(@"UPDATE {0}.. RDR1 SET U_XSQty = {1},U_XSLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END WHERE 
DocEntry = {2} AND LineNum = {3} ", ConnModel.commonDB, (double.Parse(tb_Quantity.Text.ToString()) + (double.Parse(tbl.Rows[rowXS]["数量"].ToString()) - double.Parse(tbl.Rows[rowXS]["剩余未清数量"].ToString()))), cmbSaleNo.Text.ToString(), tbl.Rows[rowXS]["行号"].ToString());
                    list.Add(sql);
                    if (SqlHelper.ExecuteSqlTran(list) > 0)
                    {
                        MessageBox.Show("添加成功！");
                        cmbSaleNo_LostFocus(null, null);
                        init();
                    }
                    else
                    {
                        MessageBox.Show("添加失败！");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// 添加完成后,初始化
        /// </summary>
        private void init()
        {
            //同一销售订单，检索是否全部销售
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
        /// 检查数据是否为空
        /// </summary>
        /// <returns></returns>
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
      
      
     
      

    }
}