using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;
using SealedEmulate_PDA.Dal;

namespace SealedEmulate_PDA.Forms
{
    public partial class XSJH : Form
    {
        private string serNum = "";
        public XSJH()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XSJH_Load(object sender, EventArgs e)
        {
            ClearControl();
            txtDocEntry.Focus();
        }
        /// <summary>
        /// 订单页 - 订单编号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDocEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region 置空
                dataGrid1.DataSource = null;
                dataGrid2.DataSource = null;
                ClearControl();
                #endregion
                if (System.Text.RegularExpressions.Regex.IsMatch(this.txtDocEntry.Text.Trim(), @"^[1-9]\d*$"))
                {
                    int docEntry = int.Parse(this.txtDocEntry.Text.Trim());
                    string sql = string.Format(@"SELECT Row_Number() OVER ( ORDER BY A.LineNum )-1 '#',A.ItemCode as 物料编码, A.Dscription as 物料名称,A.Quantity as 销售数量,(A.Quantity-isnull(A.U_XSQty,0)) as 剩余数量, 0 as 出库数量,A.LineNum as 行号,A.WhsCode as 仓库,B.CardCode AS 客户编码,B.CardName AS 客户名称 FROM {0}..RDR1 A JOIN {0}..ORDR AS B ON A.DocEntry=B.DocEntry
WHERE A.DocEntry = {1} AND  A.OpenQty != 0 AND isnull(A.U_XSQty,'0') < A.Quantity AND  A.LineStatus ='O' AND A.U_XSLineStatus = 'O'", ConnModel.commonDB, docEntry);
                    DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        #region 订单页
                        DataGridTableStyle mydata1 = new DataGridTableStyle();
                        GridColumnStylesCollection mycol1 = null;
                        dataGrid1.DataSource = dt;
                        mydata1.MappingName = dt.TableName;
                        dataGrid1.TableStyles.Clear();
                        dataGrid1.TableStyles.Add(mydata1);
                        mycol1 = dataGrid1.TableStyles[0].GridColumnStyles;
                        mycol1["#"].Width = 0;
                        mycol1["物料编码"].Width = 80;
                        mycol1["物料名称"].Width = 80;
                        #endregion

                        #region 扫描页
                        //库存信息页签 根据（物料编码,仓库） 
                        DataTable tb2 = SqlHelper.GetDataTable(string.Format(" exec {1}..SBO_OnHandQtyForOrdr {0},'' ", docEntry, ConnModel.commonDB), CommandType.Text);
                        //设置datagrid的列宽
                        DataGridTableStyle mydata2 = new DataGridTableStyle();
                        GridColumnStylesCollection mycol2 = null;
                        dataGrid2.DataSource = tb2;
                        mydata2.MappingName = tb2.TableName;
                        dataGrid2.TableStyles.Clear();
                        dataGrid2.TableStyles.Add(mydata2);
                        mycol2 = dataGrid2.TableStyles[0].GridColumnStyles;
                        mycol2["#"].Width = 0;
                        mycol2["物料编码"].Width = 80;
                        mycol2["物料名称"].Width = 80;
                        mycol2["仓库名称"].Width = 0;
                        mycol2["库位标识"].Width = 0;
                        mycol2["订单编号"].Width = 0;
                        mycol2["订单行号"].Width = 0;
                        mycol2["客户编码"].Width = 0;
                        mycol2["客户名称"].Width = 0;
                        #endregion

                        tabControl1.SelectedIndex = 1;
                    }
                    else
                    {
                        MessageBox.Show("该订单已交货或者不存在...", "提示");
                    }
                }
                else
                {
                    MessageBox.Show("输入订单编号不合法!", "错误");
                }
            }
        }
        /// <summary>
        /// 扫描页 - 条码框回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBrCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDocEntry.Text.Trim() == "")
                {
                    this.txtBrCode.Text = "";
                    MessageBox.Show("请先输入订单,在扫描.", "错误");
                    tabControl1.SelectedIndex = 0;
                    txtDocEntry.Focus();
                    return;
                }
                string barCode = this.txtBrCode.Text.Trim();
                if (barCode != "")
                {
                    if (barCode.IndexOf(',') == -1)
                    {
                        ClearControl();
                        MessageBox.Show("条码信息不符合规范!", "错误");
                        return;
                    }
                    string[] array = barCode.Split(',');
                    if (array.Length > 0)
                    {
                        string itemCode = array[0].Trim();
                        string batch = array[2].Trim();
                        DataTable dt1 = (DataTable)dataGrid1.DataSource;//订单页
                        DataTable dt2 = (DataTable)dataGrid2.DataSource;//库存页
                        foreach (DataRow row in dt2.Rows)
                        {
                            //判断物料
                            if (row["物料编码"].ToString() == itemCode)
                            {
                                double kcQty = double.Parse(row["库存数量"].ToString());//库存数量 100
                                double ckQty = double.Parse(row["出库数量"].ToString());//出库数量 0 
                                if (kcQty != ckQty)
                                {
                                    #region 强制先进先出

                                    if (!checkBoxBatch.Checked)
                                    {
                                        if (row["批次"].ToString() != batch)
                                        {
                                            MessageBox.Show("当前物料批次不是最早批次,应扫描批次" + row["批次"], "提示");
                                            ClearControl();
                                            return;
                                        }
                                    }
                                    #endregion

                                    #region 按照批次 进行库位自动选择出库

                                    if (row["批次"].ToString() == batch)
                                    {
                                        DataRow[] rows = dt2.Select("批次='" + batch + "'");
                                        double batchQty = 0;
                                        foreach (DataRow item in rows)
                                        {
                                            double val = double.Parse(item["库存数量"].ToString()) - double.Parse(item["出库数量"].ToString());
                                            batchQty += val;
                                        }
                                        labKcQty.Text = batchQty.ToString();
                                        serNum = row["#"].ToString();
                                        txtQuanTity.Focus();
                                        return;
                                    }                                  
                                    #endregion                                   
                                }
                            }
                        }
                        //表格循环完毕,未发现扫描物料与订单物料一致
                        ClearControl();
                        MessageBox.Show("扫描物料或批次与订单物料不匹配", "错误");
                    }
                    else
                    {
                        ClearControl();
                        MessageBox.Show("条码信息不符合规范!", "错误");
                    }
                }
                else
                {
                    serNum = "";
                    labKcQty.Text = "0";
                    MessageBox.Show("条码信息不得为空!", "提示");
                }
            }
        }
        /// <summary>
        /// 扫描页 - 交货数量回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuanTity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(this.txtDocEntry.Text.Trim(), @"^[1-9]\d*$"))
                {
                    if (serNum != "")
                    {
                        double batchQty = double.Parse(labKcQty.Text);
                        double txtQty = double.Parse(txtQuanTity.Text);
                        if (txtQty > batchQty)
                        {
                            MessageBox.Show("输入数量大于库存数量...", "提示");
                        }
                        else
                        {
                            string itemCode = this.txtBrCode.Text.Trim().Split(',')[0].Trim();
                            DataTable dt1 = (DataTable)dataGrid1.DataSource;//订单页
                            DataRow row1 = dt1.Select("物料编码='" + itemCode + "'")[0];
                            double qty1 = double.Parse(row1["剩余数量"].ToString()) - double.Parse(row1["出库数量"].ToString());
                            if (txtQty > qty1)
                            {
                                MessageBox.Show("输入数量大于待交货数量" + qty1, "提示");
                            }
                            else
                            {
                                int index1 = int.Parse(row1["#"].ToString());
                                int col1 = 5;//订单页出库数量列索引
                                int col2 = 9;//扫描页出库数量列索引
                                string batch = this.txtBrCode.Text.Trim().Split(',')[2].Trim();
                                DataTable dt2 = (DataTable)dataGrid2.DataSource;//库存页
                                int location = int.Parse(serNum);
                                for (int i = location; i < dt2.Rows.Count; i++)
                                {
                                    if (dt2.Rows[i]["批次"].ToString() == batch && txtQty > 0)
                                    {
                                        double lineQty = double.Parse(dt2.Rows[i]["库存数量"].ToString()) - double.Parse(dt2.Rows[i]["出库数量"].ToString());
                                        if (txtQty > lineQty)
                                        {
                                            dataGrid1[index1, col1] = (double.Parse(dataGrid1[index1, col1].ToString()) + lineQty).ToString();
                                            dataGrid2[i, col2] = (double.Parse(dataGrid2[i, col2].ToString()) + lineQty).ToString();
                                        }
                                        else
                                        {
                                            dataGrid1[index1, col1] = (double.Parse(dataGrid1[index1, col1].ToString()) + txtQty).ToString();
                                            dataGrid2[i, col2] = (double.Parse(dataGrid2[i, col2].ToString()) + txtQty).ToString();
                                            break;
                                        }
                                        txtQty -= lineQty;
                                    }
                                }
                                ClearControl();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先扫描条码", "提示");
                    }
                }
                else
                {
                    MessageBox.Show("输入数量不合法!", "错误");
                }
            }
        }
        /// <summary>
        /// 扫描页 - 清
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        /// <summary>
        /// 过账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确定要过账吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                DataTable dt2 = (DataTable)dataGrid2.DataSource;//库存页
                List<string> sqlList = new List<string>();
                foreach (DataRow row in dt2.Rows)
                {
                    double val = double.Parse(row["出库数量"].ToString());
                    if (val != 0)
                    {
                        string sql = string.Format("insert into XSJH(CardCode,CardName,DocEntry,LineNum,ItemCode,ItemName,ItemSpec,ItemBatch,Qty,Price,LineTotal,WhsCode,WhsName,AbsEntry,BinCode,InWhsQty,UserSign) 	values('{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}',{8},{9},{10},'{11}','{12}',{13},'{14}',{15},'{16}')", row["客户编码"].ToString(), row["客户名称"].ToString(), row["订单编号"].ToString(), row["订单行号"].ToString(), row["物料编码"].ToString(), row["物料名称"].ToString(), "", row["批次"].ToString(), row["订单数量"].ToString(), row["单价"].ToString(), row["行合计"].ToString(), row["仓库编码"].ToString(), row["仓库名称"].ToString(), row["库位标识"].ToString(), row["库位编码"].ToString(), row["出库数量"].ToString(), ConnModel.userName);
                        sqlList.Add(sql);
                    }
                }
                if (sqlList.Count > 0)
                {
                    DataTable dt1 = (DataTable)dataGrid1.DataSource;//订单页
                    foreach (DataRow row in dt1.Rows)
                    {
                        double val = double.Parse(row["出库数量"].ToString());
                        if (val != 0)
                        {
                            string sql = string.Format("UPDATE {0}.. RDR1 SET U_XSQty = {1},U_XSLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END WHERE DocEntry = {2} AND LineNum = {3}", ConnModel.commonDB, (double.Parse(row["销售数量"].ToString()) + double.Parse(row["出库数量"].ToString()) - double.Parse(row["剩余数量"].ToString())), txtDocEntry.Text, row["行号"].ToString());
                            sqlList.Add(sql);
                        }
                    }
                }
                if (SqlHelper.ExecuteSqlTran(sqlList) > 0)
                {
                    //API 按照订单查询
                    int docEntry = int.Parse(txtDocEntry.Text);

                    //过账成功 或者失败都得刷新页面
                    dataGrid1.DataSource = null;
                    dataGrid2.DataSource = null;
                    ClearControl();
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("连接网络失败,请稍候重试..", "提示");
                }
            }
        }








        /// <summary>
        /// 扫描页 - 控件置空
        /// </summary>
        private void ClearControl()
        {
            txtBrCode.Text = "";
            txtQuanTity.Text = "";
            serNum = "";
            labKcQty.Text = "0";
        }

    }
}