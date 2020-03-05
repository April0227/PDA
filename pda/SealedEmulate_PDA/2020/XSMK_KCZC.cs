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
                #region 置空
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
                        #region 订单页
                        //根据输入的库存转储请求单号，查询 
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
                            mycol1["单据编号"].Width = 0;
                            mycol1["单据行号"].Width = 0;
                            mycol1["#"].Width = 0;
                            tabControl1.SelectedIndex = 1;
                            txtBarcode.Focus();
                        }
                        else
                        {
                            txtDocEntry.Text = "";
                            MessageBox.Show("当前销售订单不存在,请更换单据", "提示");

                        }
                        #endregion
                    }
                }
                else
                {
                    txtDocEntry.Text = "";
                    MessageBox.Show("输入销售订单单号不合法!", "错误");
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
                    this.txtBarcode.Text = "";//清空条码信息控件
                    MessageBox.Show("请先输入销售订单单号,再扫描.", "错误");
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
                        MessageBox.Show("条码信息不符合规范!", "错误");
                        return;
                    }
                    string[] array = barCode.Split(',');
                    if (array.Length > 0)
                    {
                        string itemCode = array[0].Trim();
                        string batch = array[2].Trim();//批次
                        DataTable dt1 = (DataTable)DataGrid1.DataSource;//订单页 
                        foreach (DataRow row in dt1.Rows)
                        {
                            if (row["物料编码"].ToString() == itemCode)
                            {
                                //查询当前物料是否存在当前批次
                                string sql = string.Format(@"SELECT T1.AbsEntry FROM {1}..OBBQ T0 
INNER JOIN {1}..OBIN T1 ON T0.BinAbs  = T1.AbsEntry
INNER JOIN {1}..OBTN T2 ON T2.AbsEntry = T0.SnBMDAbs   
where t0.OnHandQty <>0  and t2.ItemCode ='{0}' AND T2.DistNumber ='{2}'", itemCode, ConnModel.commonDB, batch);
                                DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                                if (dt.Rows.Count > 0)
                                {
                                    if (double.Parse(row["数量"].ToString()) == double.Parse(row["标记数量"].ToString()))
                                    {
                                        MessageBox.Show("该物料已完成库存转储.", "错误");
                                        return;
                                    }
                                    txtItemCode.Text = row["物料编码"].ToString();
                                    txtItemName.Text = row["物料名称"].ToString();
                                    txtBatch.Text = batch;
                                    cmbFCK.SelectedValue = row["仓库"].ToString();
                                    serNum = row["#"].ToString();
                                    page1LineNum = row["单据行号"].ToString();
                                    txtFKW.Focus();
                                    return;
                                }
                                ClearControl();
                                MessageBox.Show("当前物料与当前批次不匹配", "错误");
                                return;

                            }
                        }
                        ClearControl();
                        MessageBox.Show("扫描物料与销售订单物料不匹配", "错误");

                    }
                    else
                    {
                        ClearControl();
                        MessageBox.Show("条码信息不符合规范!", "错误");
                    }
                }
                else
                {
                    ClearControl();
                    MessageBox.Show("条码信息不得为空!", "提示");
                }
            }
        }
        /// <summary>
        /// 当前从仓库变更
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
        /// 从库位编码 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFKW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtItemCode.Text == "")
                {
                    MessageBox.Show("请先扫描物料", "提示");
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
                        //将选择的仓库改为扫描的
                        lFKW.Text = binCode.Split(',')[2];
                        whsCode = binCode.Split(',')[0];
                        fbinCode = binCode.Split(',')[1];
                    }
                    string itemCode = txtItemCode.Text;
                    string batch = txtBatch.Text;
                    DataTable page3Data = (DataTable)DataGrid2.DataSource;//过账页
                    DataRow[] checkWhsRows = page3Data.Select("单据行号='" + page1LineNum + "'");
                    if (checkWhsRows.Length > 0)
                    {
                        string page2WhsCodeText = checkWhsRows[0]["从仓库"].ToString().Trim();
                        if (page2WhsCodeText != whsCode)
                        {
                            //在入库时单张单据,由于行物料不能选择两个及以上仓库
                            txtFKW.Text = "";
                            MessageBox.Show("只能选择此仓库：" + checkWhsRows[0]["从仓库名称"].ToString().Trim(), "提示");
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
                                MessageBox.Show("该物料的当前批次在当前库位不存在", "提示");
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
                            MessageBox.Show("该物料的当前批次在当前库位不存在", "提示");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 到库位编码 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTKW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtItemCode.Text == "")
                {
                    MessageBox.Show("请先扫描物料", "提示");
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
                      DataTable page3Data = (DataTable)DataGrid2.DataSource;//过账页
                    DataRow[] checkWhsRows = page3Data.Select("单据行号='" + page1LineNum + "'");
                    if (checkWhsRows.Length > 0)
                    {
                        string page2WhsCodeText = checkWhsRows[0]["到仓库"].ToString().Trim();
                        if (page2WhsCodeText != whsCode)
                        {
                            //在入库时单张单据,由于行物料不能选择两个及以上仓库
                            txtTKW.Text = "";
                            MessageBox.Show("只能选择此仓库：" + checkWhsRows[0]["到仓库名称"].ToString().Trim(), "提示");
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
                                    MessageBox.Show("收货仓库不得与发货仓库相同", "提示");
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
                                MessageBox.Show("当前仓库不存在该库位", "提示");
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
                                MessageBox.Show("收货仓库不得与发货仓库相同", "提示");
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
                            MessageBox.Show("当前仓库不存在该库位", "提示");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 数量输入只能为数字
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
            #region 校验

            string docEntry = txtDocEntry.Text;
            if (docEntry == "")
            {
                MessageBox.Show("请先选择销售订单.", "提示");
                return;
            }
            string itemCode = txtItemCode.Text.Trim();
            if (itemCode == "")
            {
                MessageBox.Show("请先扫描物料.", "提示");
                return;
            }
            if (txtFKW.ReadOnly == false && lFKW.Text == "0")
            {
                MessageBox.Show("请设置从库位", "提示");
                return;
            }
            if (txtTKW.ReadOnly == false && lTKW.Text == "0")
            {
                MessageBox.Show("请设置到库位", "提示");
                return;
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("请填写数量", "提示");
                return;
            }
            DataTable dt1 = (DataTable)DataGrid1.DataSource;
            DataRow[] row = dt1.Select("物料编码='" + itemCode + "'");
            int serNum = int.Parse(row[0]["#"].ToString());
            double syQty = double.Parse(row[0]["数量"].ToString()) - double.Parse(row[0]["标记数量"].ToString());
            double srQty = double.Parse(txtQty.Text);
            if (srQty > syQty)
            {
                MessageBox.Show("转储数量剩余为" + syQty, "提示");
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
                    MessageBox.Show(string.Format("库存数量为{0}", dt.Rows[0]["OnHandQty"].ToString()), "提示");
                    txtQty.Focus();
                    return;
                }
                #region 需添加字段

                string lineNum = row[0]["单据行号"].ToString();
                docEntry = row[0]["单据编号"].ToString();
                string itemName = row[0]["物料名称"].ToString();
                string quantity = row[0]["数量"].ToString();
                string fromAbsEntry = "";
                string toWhs = cmbTCK.SelectedValue.ToString();
                string toWhsName = cmbTCK.Text;
                string toAbsEntry = "";
                string cardCode = row[0]["客户编码"].ToString();
                string cardName = row[0]["客户名称"].ToString();
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
                //转储数量 srQty
                string zdr = ConnModel.userName;
                #endregion

                DataTable dt2 = (DataTable)DataGrid2.DataSource;
                int sign = 0;

                #region 更新过账页
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["物料编码"].ToString() == itemCode && dt2.Rows[i]["批次"].ToString() == batch && dt2.Rows[i]["从仓库"].ToString() == fromWhs && dt2.Rows[i]["从库位标识"].ToString() == fromAbsEntry && dt2.Rows[i]["到仓库"].ToString() == toWhs && dt2.Rows[i]["到库位标识"].ToString() == toAbsEntry)
                    {
                        //只需更新转储数量
                        DataGrid2[i, dt2.Columns.Count - 2] = double.Parse(dt2.Rows[i]["转储数量"].ToString()) + srQty;
                    }
                    else
                    {
                        sign++;
                    }
                }
                #endregion

                #region 添加过账页

                if (sign == dt2.Rows.Count)
                {
                    DataRow newRow = dt2.NewRow();
                    newRow["单据编号"] = docEntry;
                    newRow["单据行号"] = lineNum;
                    newRow["客户编码"] = cardCode;
                    newRow["客户名称"] = cardName;
                    newRow["物料编码"] = itemCode;
                    newRow["物料名称"] = itemName;
                    newRow["批次"] = batch;
                    newRow["数量"] = quantity;
                    newRow["从仓库"] = fromWhs;
                    newRow["从仓库名称"] = fromWhsName;
                    newRow["从库位标识"] = fromAbsEntry;
                    newRow["从库位"] = fBinCode;
                    newRow["到仓库"] = toWhs;
                    newRow["到仓库名称"] = toWhsName;
                    newRow["到库位标识"] = toAbsEntry;
                    newRow["到库位"] = tBinCode;
                    newRow["转储数量"] = srQty;
                    newRow["制单人"] = zdr;
                    dt2.Rows.Add(newRow);
                    DataGrid2.DataSource = dt2;
                }

                #endregion

                DataGrid1[serNum, dt1.Columns.Count - 1] = double.Parse(row[0]["标记数量"].ToString()) + srQty;
                ClearControl();
            }
            else
            {
                MessageBox.Show("库存数量为0", "提示");
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
                //获取当前选定的行号   
                n = DataGrid2.CurrentRowIndex;
                for (int i = 0; i < dtInCome.Rows.Count; i++)
                {
                    if (dtInCome.Rows[i]["物料编码"].ToString() == dt.Rows[n]["物料编码"].ToString() && dtInCome.Rows[i]["单据行号"].ToString() == dt.Rows[n]["单据行号"].ToString())
                    {
                        double qty = Convert.ToDouble(dtInCome.Rows[i]["标记数量"]);
                        dtInCome.Rows[i]["标记数量"] = qty - Convert.ToDouble(dt.Rows[n]["转储数量"]);
                        DataGrid1.DataSource = dtInCome;
                    }

                }
                //从数据集集合中删除行 
                dt.Rows.RemoveAt(n);
                //刷新Datagrid1显示删除后的数据    
                DataGrid2.Refresh();
            }
        }
        private void btnInCome_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)DataGrid2.DataSource;
            //DataView dv = dt.DefaultView;
            //DataTable DistTable = dv.ToTable("Dist", true, "订单编号");
            //string parameter = string.Empty;
            //for (int i = 0; i < DistTable.Rows.Count; i++)
            //{
            //    parameter += "{Id:'";
            //    parameter += DistTable.Rows[i]["订单编号"].ToString();
            //    parameter += "'},";
            //}
            //parameter = parameter.Substring(0, parameter.Length - 1);
            string datatimeflg = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double qty = Convert.ToDouble(dt.Rows[i]["转储数量"].ToString());
                    string sql = String.Format("INSERT INTO [Bar_XSMK_KCZC](DocEntry,LineNum,ItemCode,ItemName,Quantity,BatchNum,FromWhsCode,FromBinCode,FromAbsEntry,ToWhsCode,ToBinCode,ToAbsEntry,Creater,DocEntryFlag,FromWhsName,ToWhsName,CardCode,CardName) values ({0},{1},'{2}','{3}',{4},'{5}','{6}','{7}',{8},'{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}','{17}');",
                                    Convert.ToInt32(dt.Rows[i]["单据编号"].ToString()),
                                    Convert.ToInt32(dt.Rows[i]["单据行号"].ToString()),
                                    dt.Rows[i]["物料编码"].ToString(),
                                    dt.Rows[i]["物料名称"].ToString(),
                                    Convert.ToDouble(dt.Rows[i]["转储数量"].ToString()),
                                    dt.Rows[i]["批次"].ToString(),
                                    dt.Rows[i]["从仓库"].ToString(),
                                    dt.Rows[i]["从库位"].ToString(),
                                    Convert.ToInt32(dt.Rows[i]["从库位标识"].ToString()),
                                    dt.Rows[i]["到仓库"].ToString(),
                                    dt.Rows[i]["到库位"].ToString(),
                                    dt.Rows[i]["到库位标识"].ToString() != "" ? Convert.ToInt32(dt.Rows[i]["到库位标识"].ToString()) : 0,
                                    dt.Rows[i]["制单人"].ToString(),
                                    datatimeflg,
                                    dt.Rows[i]["从仓库名称"].ToString(),
                                    dt.Rows[i]["到仓库名称"].ToString(),
                                    dt.Rows[i]["客户编码"].ToString(),
                                    dt.Rows[i]["客户名称"].ToString());
                    list.Add(sql);
                    sql = string.Format("");
                    //更新SAP系统中 转储请求导入数量字段
                    sql = string.Format("UPDATE {0}..RDR1 SET U_ZCQty =isnull( U_ZCQty,0) + {1} ,U_XSLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END where DocEntry = {2} and LineNum ={3}", ConnModel.commonDB, qty, Convert.ToInt32(dt.Rows[i]["单据编号"].ToString()), Convert.ToInt32(dt.Rows[i]["单据行号"].ToString()));
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
                    MessageBox.Show("请手动到PC端完成过账操作！");
                    //if (SapHelper.isLogin)
                    //{
                    //    string url = "SalesReceive/Import";
                    //    string res = HttpHelper.HttpPost(url, parameter);
                    //    string mes = "";
                    //    if (res == "操作已超时。")
                    //    {
                    //        mes = "网络连接失败，请手动到PC端完成过账操作！";
                    //    }
                    //    else
                    //    {
                    //        mes = res;
                    //    }
                    //    MessageBox.Show(mes);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("请手动到PC端完成过账操作！");
                    //}
                }
                else
                {
                    MessageBox.Show("请检查数据的完整性，稍后重试！");
                }
            }
        }
        /// <summary>
        /// 扫描页数据清空
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
        /// 过账创建空table
        /// </summary>
        /// <returns></returns>
        public void Grid3DataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("单据编号");
            table.Columns.Add("单据行号");
            table.Columns.Add("客户编码");
            table.Columns.Add("客户名称");
            table.Columns.Add("物料编码");
            table.Columns.Add("物料名称");
            table.Columns.Add("数量");
            table.Columns.Add("从仓库");
            table.Columns.Add("从仓库名称");
            table.Columns.Add("从库位");
            table.Columns.Add("到仓库");
            table.Columns.Add("到仓库名称");
            table.Columns.Add("到库位");
            table.Columns.Add("批次");
            table.Columns.Add("从库位标识");
            table.Columns.Add("到库位标识");
            table.Columns.Add("转储数量");
            table.Columns.Add("制单人");

            DataGridTableStyle mydata2 = new DataGridTableStyle();
            GridColumnStylesCollection mycol2 = null;
            DataGrid2.DataSource = table;
            mydata2.MappingName = table.TableName;
            DataGrid2.TableStyles.Clear();
            DataGrid2.TableStyles.Add(mydata2);
            mycol2 = DataGrid2.TableStyles[0].GridColumnStyles;
            mycol2["单据编号"].Width = 0;
            mycol2["单据行号"].Width = 0;

        }
        /// <summary>
        /// 绑定所有仓库
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