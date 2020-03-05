using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;
using SealedEmulate_PDA.Dal;

namespace SealedEmulate_PDA
{
    public partial class InCome1 : Form
    {
        private DataTable tbl;
        private string cardcode;
        private string wlCode = string.Empty;
        public InCome1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InCome_Load(object sender, EventArgs e)
        {
            cmbStockNo.Focus();
        }
        /// <summary>
        /// 质检单号失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStockNo_LostFocus(object sender, EventArgs e)
        {
            GYS.Text = string.Empty;
            string docEntry = cmbStockNo.Text.Trim();
            if (!string.IsNullOrEmpty(docEntry))
            {
                string sql = "select U_CardName from " + ConnModel.commonDB + "..[@SBO_ZJD] where U_DJZT = 'W' and DocEntry=" + docEntry + "";
                DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    GYS.Text = dt.Rows[0]["U_CardName"].ToString();

                }
                //检索质检单
                string sql1 = "SELECT U_ItemCode as 物料编码,U_ItemName as 物料名称,case when U_WQSL is null or U_WQSL = 0 then U_HGSL else U_WQSL end as 合格数量,'' as 入库数量,LineId as 质检单行号,U_CGDD as 采购订单,U_CGDDH as 采购订单行号,'' as 批次 FROM " + ConnModel.commonDB + ".. [@SBO_ZJD_H] WHERE DocEntry = @docentry AND (U_ZJJG ='Y'or U_ZJJG ='J' or U_PSJG ='Y') AND U_SFSH = 'N'";
                SqlParameter ps = new SqlParameter("@docentry", Convert.ToString(cmbStockNo.Text.Trim()));
                tbl = SqlHelper.GetDataTable(sql1, CommandType.Text, ps);
                DG.DataSource = tbl;
                string sql2 = "SELECT U_SYB,U_CardCode FROM " + ConnModel.commonDB + ".. [@SBO_ZJD] WHERE DocEntry =@docentry";
                SqlParameter ps2 = new SqlParameter("@docentry", Convert.ToString(cmbStockNo.Text.Trim()));
                DataTable tbl2 = SqlHelper.GetDataTable(sql2, CommandType.Text, ps2);
                if (tbl2.Rows.Count > 0)
                {
                    txtSyb.Text = Convert.ToString(tbl2.Rows[0]["U_SYB"]);
                    cardcode = Convert.ToString(tbl2.Rows[0]["U_CardCode"]); 
                }
            }
        }
        /// <summary>
        /// 质检单号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStockNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                GYS.Text = string.Empty;
                string docEntry = cmbStockNo.Text.Trim();
                if (!string.IsNullOrEmpty(docEntry))
                {
                    string sql = "select U_CardName from " + ConnModel.commonDB + "..[@SBO_ZJD] where U_DJZT = 'W' and DocEntry=" + docEntry + "";
                    DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GYS.Text = dt.Rows[0]["U_CardName"].ToString();

                    }
                    //检索质检单
                    string sql1 = "SELECT U_ItemCode as 物料编码,U_ItemName as 物料名称,case when U_WQSL is null or U_WQSL = 0 then U_HGSL else U_WQSL end as 合格数量,'' as 入库数量,LineId as 质检单行号,U_CGDD as 采购订单,U_CGDDH as 采购订单行号,'' as 批次 FROM " + ConnModel.commonDB + ".. [@SBO_ZJD_H] WHERE DocEntry = @docentry AND (U_ZJJG ='Y'or U_ZJJG ='J' or U_PSJG ='Y') AND U_SFSH = 'N'";
                    SqlParameter ps = new SqlParameter("@docentry", Convert.ToString(cmbStockNo.Text.Trim()));
                    tbl = SqlHelper.GetDataTable(sql1, CommandType.Text, ps);
                    DG.DataSource = tbl;
                    string sql2 = "SELECT U_SYB,U_CardCode FROM " + ConnModel.commonDB + ".. [@SBO_ZJD] WHERE DocEntry =@docentry";
                    SqlParameter ps2 = new SqlParameter("@docentry", Convert.ToString(cmbStockNo.Text.Trim()));
                    DataTable tbl2 = SqlHelper.GetDataTable(sql2, CommandType.Text, ps2);
                    if (tbl2.Rows.Count > 0)
                    {
                        txtSyb.Text = Convert.ToString(tbl2.Rows[0]["U_SYB"]);
                        cardcode = Convert.ToString(tbl2.Rows[0]["U_CardCode"]);
                    }
                }
            } 
        }
        /// <summary>
        /// 条码信息框回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QRCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                wlCode = string.Empty;
                TextBox textBox = (TextBox)sender;
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    string textBoxValue = textBox.Text.Trim();
                    string[] textBoxArray = textBoxValue.Split(',');
                    if (textBoxArray.Length == 5)
                    {
                        wlCode = textBoxArray[0];
                        string pici = textBoxArray[2];
                        Number.Text = textBoxArray[3].TrimEnd(new char[] { 'G', 'g' }).TrimEnd(new char[] { 'K', 'k' });
                        DataRow[] rows = tbl.Select("物料编码 = '" + wlCode + "'");
                        if (rows != null && rows.Length > 0)
                        { 
                            rows[0]["入库数量"] = Number.Text;
                            rows[0]["批次"] = pici;
                            DG.Refresh();
                        }
                    }

                }
            }
        } 
        /// <summary>
        /// 数量失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text) && !string.IsNullOrEmpty(wlCode))
            {
                string textValue = textBox.Text.Trim();
                DataRow[] rows = tbl.Select("物料编码 = '" + wlCode + "'");
                if (rows != null && rows.Length > 0)
                {
                    rows[0]["入库数量"] = textValue;
                    DG.Refresh();
                }
            }
        }
        /// <summary>
        /// 数量回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                TextBox textBox = (TextBox)sender;
                if (!string.IsNullOrEmpty(textBox.Text) && !string.IsNullOrEmpty(wlCode))
                {
                    string textValue = textBox.Text.Trim();
                    DataRow[] rows = tbl.Select("物料编码 = '" + wlCode + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        rows[0]["入库数量"] = textValue;
                        DG.Refresh();
                    }
                }
            } 
        } 
        /// <summary>
        /// 供应商追溯码获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CusTraceNo_GotFocus(object sender, EventArgs e)
        {
            if (CusTraceNo.Text != "")
            {
                CusTraceNo.Text = "";
            }
        }
        /// <summary>
        /// 清空按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            QRCode.Text = string.Empty;
            Number.Text = string.Empty;
            CusTraceNo.Text = string.Empty;
            QRCode.Focus();
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelrow_Click(object sender, EventArgs e)
        {
            int n = 0;
            //获取当前选定的行号   
            n = DG.CurrentRowIndex;
            //从数据集集合中删除行 
            tbl.Rows.RemoveAt(n);
            //刷新Datagrid1显示删除后的数据    
            DG.Refresh();

        }
        /// <summary>
        /// 添加到PC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbl.Rows.Count == 0)
                {
                    QRCode.Focus();
                    throw new Exception("请先选择质检单号！"); 
                }
                //添加数据
                string str = Convert.ToString(cmbStockNo.Text.Trim());
                char[] separator = { '-' };
                string tempcmbStockNo = str.Split(separator)[0];
                if (tbl.Rows.Count > 0)
                {
                    List<string> list = new List<string>();
                    string sqlStr = string.Empty;
                    string rukuNumStr = string.Empty;
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        rukuNumStr = tbl.Rows[i]["入库数量"].ToString(); 
                        if (!string.IsNullOrEmpty(rukuNumStr) && Convert.ToDouble(rukuNumStr) > 0)
                        {
                            sqlStr = string.Empty;
                            string itemcode = tbl.Rows[i]["物料编码"].ToString();
                            string itemname = tbl.Rows[i]["物料名称"].ToString();
                            double quantity = Convert.ToDouble(tbl.Rows[i]["合格数量"].ToString());
                            double ruKuNum = Convert.ToDouble(rukuNumStr);
                            int checknumline = Convert.ToInt32(tbl.Rows[i]["质检单行号"].ToString());
                            string buynum = tbl.Rows[i]["采购订单"].ToString();
                            int buynumline = Convert.ToInt32(tbl.Rows[i]["采购订单行号"].ToString());
                            string pici = tbl.Rows[i]["批次"].ToString();
                            string custraceno = CusTraceNo.Text.ToString();
                            string nowtime = System.DateTime.Now.ToString("yyyyMMdd");
                            string qrcode = itemcode + " " + nowtime;
                            string sql1 = string.Format("select count(1) from InCome where BuyNum='{0}' AND BuyNumLine={1} AND IsImport=0", buynum, buynumline);
                            object rtnValue = SqlHelper.ExecuteScalar(sql1, CommandType.Text);
                            if (rtnValue != null && Convert.ToInt32(rtnValue) > 0)
                            {
                                sqlStr = String.Format("UPDATE InCome SET RuKuNum=ISNULL(RuKuNum,0)+{0} where BuyNum='{1}' AND BuyNumLine={2} AND IsImport=0", ruKuNum, buynum, buynumline);
                            }
                            else//增加
                            {
                                sqlStr = String.Format("INSERT INTO InCome(CheckNum,ItemCode,ItemName,QuanTity,CheckNumLine,BuyNum,BuyNumLine,CardCode,CusTraceNo,BatchNum,QRCode,RuKuNum) values ('{0}','{1}','{2}',{3},{4},'{5}',{6},'{7}','{8}','{9}','{10}',{11})", tempcmbStockNo, itemcode, itemname, quantity, checknumline, buynum, buynumline, cardcode, custraceno, pici, qrcode, ruKuNum);
                            }

                            list.Add(sqlStr);
                            if (ruKuNum >= quantity)
                            {
                                sqlStr = String.Format("UPDATE {0}.. [@SBO_ZJD_H] SET U_SFSH = 'Y',U_WQSL = 0 WHERE  DocEntry ='{1}' and LineId = {2}", ConnModel.commonDB, tempcmbStockNo, checknumline);
                            }
                            else
                            {
                                sqlStr = String.Format("UPDATE {0}.. [@SBO_ZJD_H] SET U_WQSL = {3} WHERE  DocEntry ='{1}' and LineId = {2}", ConnModel.commonDB, tempcmbStockNo, checknumline, quantity - ruKuNum);
                            }

                            list.Add(sqlStr);
                        }
                    }
                    if (list != null && list.Count > 0)
                    {
                        if (SqlHelper.ExecuteSqlTran(list) > 0)
                        {
                            tbl = null;
                            Init(tempcmbStockNo);
                            MessageBox.Show("添加成功...");
                        }
                        else
                        {
                            MessageBox.Show("添加失败...");
                        }

                    }
                    else
                    {
                        MessageBox.Show("无入库项！");
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误：" + ex.Message);
            } 
        }
        /// <summary>
        /// 添加成功,初始化
        /// </summary>
        /// <param name="tempcmbStockNo"></param>
        private void Init(string tempcmbStockNo)
        {
            //检索该单据的行明细是否全部收货，如果全部收货，则更新表头 单据状态为R 已入库
            string sql3 = "SELECT U_SFSH FROM " + ConnModel.commonDB + " ..[@SBO_ZJD_H] WHERE DocEntry =@docentry";
            SqlParameter ps3 = new SqlParameter("@docentry", tempcmbStockNo);
            DataTable tbl3 = SqlHelper.GetDataTable(sql3, CommandType.Text, ps3);
            if (tbl3.Rows.Count > 0)
            {
                int i = 0;
                int j = 0;
                for (i = 0; i <= tbl3.Rows.Count - 1; i++)
                {
                    if (tbl3.Rows[i]["U_SFSH"].ToString() == "Y")
                    {
                        j = j + 1;
                    }
                }
                if (j == tbl3.Rows.Count)
                {
                    string sql4 = "UPDATE " + ConnModel.commonDB + ".. [@SBO_ZJD] SET U_DJZT = 'R' WHERE  DocEntry =@docentry";
                    SqlParameter ps4 = new SqlParameter("@docentry", tempcmbStockNo);
                    SqlHelper.ExecuteNonquery(sql4, CommandType.Text, ps4);
                }
            }
            QRCode.Focus();
            CusTraceNo.Text = "";
            txtSyb.Text = "";
            QRCode.Text = string.Empty;
            Number.Text = string.Empty;
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
       

       
    }
}