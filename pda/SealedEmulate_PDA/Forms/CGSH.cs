using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA.Forms
{
    public partial class CGSH : Form
    {
        public CGSH()
        {
            InitializeComponent();
        }
        private void CGRK_Load(object sender, EventArgs e)
        {
            BindWhs();
            Grid3DataTable();
            txtdocEntry.Focus();
        }

        /// <summary>
        /// 质检单号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtdocEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                #region 置空
                DataGrid1.DataSource = null;
                DataGrid2.DataSource = null;
                DataGrid3.DataSource = null;
                Grid3DataTable();
                ClearControl();
                #endregion
                string docEntry = txtdocEntry.Text.Trim();
                if (System.Text.RegularExpressions.Regex.IsMatch(docEntry, @"^[1-9]\d*$"))
                {
                    if (!string.IsNullOrEmpty(docEntry))
                    {
                        #region 订单页
                        //根据输入的质检单号，查询 
                        string sql = string.Format(@"SELECT Row_Number() OVER ( ORDER BY A.LineId )-1 '#', C.ItemCode 物料编码,C.Dscription 物料名称,case when A.U_WQSL is null or A.U_WQSL = 0 then A.U_HGSL else A.U_WQSL end 数量,
D.WhsCode 仓库编码,D.WhsName 仓库名称,D.BinActivat 是否库位,B.U_CardCode 供应商编码,B.U_CardName 供应商名称,A.DocEntry 质检单号,A.LineId 质检行号,C.DocEntry 采购订单,C.LineNum 采购订单行号,C.Quantity 订单数量,
C.Price 单价,C.LineTotal 行合计,0 标记数量
FROM {1}..[@SBO_ZJD_H] A
JOIN {1}..[@SBO_ZJD] B ON A.DocEntry=B.DocEntry
JOIN {1}..POR1 C ON A.U_CGDD=C.DocEntry AND A.U_CGDDH=C.LineNum
JOIN {1}..OWHS D ON C.WhsCode=D.WhsCode
WHERE (A.U_ZJJG ='Y'or A.U_ZJJG ='J' or A.U_PSJG ='Y') AND A.U_SFSH ='N' AND A.DocEntry={0}", docEntry, ConnModel.commonDB);
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
                            mycol1["#"].Width = 0;
                            mycol1["是否库位"].Width = 0;
                            mycol1["供应商编码"].Width = 0;
                            mycol1["供应商名称"].Width = 0;
                            mycol1["质检单号"].Width = 0;
                            mycol1["质检行号"].Width = 0;
                            mycol1["采购订单"].Width = 0;
                            mycol1["采购订单行号"].Width = 0;
                            mycol1["订单数量"].Width = 0;
                            mycol1["单价"].Width = 0;
                            mycol1["行合计"].Width = 0;
                            tabControl1.SelectedIndex = 1;
                            txtBarcode.Focus();
                        }
                        else
                        {
                            MessageBox.Show("当前质检单不存在,请更换质检单", "提示");
                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show("输入订单编号不合法!", "错误");
                }

            }
        }
        /// <summary>
        /// 条码信息扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                labAbsEntry.Text = "0";
                txtBinCode.Text = "";
                txtQty.Text = "";
                if (txtdocEntry.Text.Trim() == "")
                {
                    this.txtBarcode.Text = "";
                    MessageBox.Show("请先输入订单,在扫描.", "错误");
                    tabControl1.SelectedIndex = 0;
                    txtdocEntry.Focus();
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
                        DataTable dt1 = (DataTable)DataGrid1.DataSource;//订单页 
                        foreach (DataRow row in dt1.Rows)
                        {
                            if (row["物料编码"].ToString() == itemCode)
                            {
                                if (double.Parse(row["数量"].ToString()) == double.Parse(row["标记数量"].ToString()))
                                {
                                    MessageBox.Show("该物料已完成收货.", "错误");
                                    return;
                                }
                                txtItemCode.Text = row["物料编码"].ToString();
                                txtItemName.Text = row["物料名称"].ToString();
                                txtBatch.Text = array[2].Trim();
                                comboWhs.Text = row["仓库编码"].ToString();
                                return;
                            }
                        }
                        ClearControl();
                        MessageBox.Show("扫描物料与订单物料不匹配", "错误");
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
        /// 当前仓库变更 - 是否启用库位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboWhs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string binActivat = comboWhs.SelectedValue.ToString();
            if (binActivat == "N")
            {
                txtQty.Text = "";
                txtBinCode.ReadOnly = true;
                txtQty.Focus();
            }
            else if (binActivat == "Y")
            {
                txtQty.Text = "";
                txtBinCode.ReadOnly = false;
                txtBinCode.Focus();
            }
        }
        /// <summary>
        /// 库位编码 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtItemCode.Text == "")
                {
                    MessageBox.Show("请先扫描物料", "提示");
                    txtBinCode.Text = "";
                    txtBarcode.Focus();
                    return;
                }
                string binCode = txtBinCode.Text.Trim();
                if (binCode == "")
                {
                    txtBinCode.Focus();
                }
                else
                {
                    string whsCode = comboWhs.Text.Trim();
                    bool b = false;
                    if (binCode.IndexOf(",") != -1)
                    {
                        if (whsCode == binCode.Split(',')[0])
                        {
                            //扫描的 需截取库位编码,查询absEntry 
                            labAbsEntry.Text = binCode.Split(',')[2];
                            b = true;
                        }
                        else
                        {
                            txtBinCode.Text = "";
                            MessageBox.Show("扫描库位与仓库不一致", "提示");
                            txtBinCode.Focus();
                        }
                    }
                    else
                    {
                        string sql = string.Format("select AbsEntry from {1}..OBIN where BinCode = '{0}' and WhsCode='{2}'", binCode, ConnModel.commonDB, whsCode);
                        DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            labAbsEntry.Text = dt.Rows[0]["AbsEntry"].ToString();
                            b = true;
                        }
                        else
                        {
                            MessageBox.Show("当前仓库不存在该库位", "提示");
                        }
                    }
                    if (b)
                    {
                        //数量赋值
                        DataTable dt1 = (DataTable)DataGrid1.DataSource;
                        DataRow row = dt1.Select("物料编码='" + txtItemCode.Text + "'")[0];
                        double txtqty = double.Parse(row["数量"].ToString()) - double.Parse(row["标记数量"].ToString());
                        txtQty.Text = txtqty.ToString();
                    }
                }
            }
        }
        /// <summary>
        /// 数量输入只能为数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 库位清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            txtBinCode.Text = "";
            txtBinCode.Focus();
        }
        /// <summary>
        /// 扫描页 - 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region 校验

            string docEntry = txtdocEntry.Text;
            if (docEntry == "")
            {
                MessageBox.Show("请先选择订单.", "提示");
                return;
            }
            string itemCode = txtItemCode.Text;
            string itemName = txtItemName.Text;
            if (itemCode == "")
            {
                MessageBox.Show("请先扫描物料.", "提示");
                return;
            }
            if (txtBinCode.ReadOnly == false && labAbsEntry.Text == "0")
            {
                MessageBox.Show("请设置库位", "提示");
                return;
            }
            DataTable dt1 = (DataTable)DataGrid1.DataSource;
            DataRow row = dt1.Select("物料编码='" + itemCode + "'")[0];
            int serNum = int.Parse(row["#"].ToString());
            double syQty = double.Parse(row["数量"].ToString()) - double.Parse(row["标记数量"].ToString());
            double srQty = double.Parse(txtQty.Text);
            if (srQty > syQty)
            {
                MessageBox.Show("入库数量剩余为" + syQty, "提示");
                return;
            }
            #endregion

            #region 需添加字段

            string cardCode = row["供应商编码"].ToString();
            string cardName = row["供应商名称"].ToString();
            // 采购订单 docEntry
            string lineNum = row["采购订单行号"].ToString();
            string zjd = row["质检单号"].ToString();
            string zjdhh = row["质检行号"].ToString();
            //物料编码 物料名称 itemCode itemName
            string batch = txtBatch.Text;
            string quantity = row["订单数量"].ToString();
            string price = row["单价"].ToString();
            string lineTotal = row["行合计"].ToString();
            string binActivat = row["是否库位"].ToString();
            string whsCode = comboWhs.Text;
            string whsName = row["仓库名称"].ToString();
            string absEntry = "";
            if (txtBinCode.ReadOnly == false)
            {
                absEntry = labAbsEntry.Text;
            }
            string binCode = "";
            if (txtBinCode.Text.IndexOf(',') == -1)
            {
                binCode = txtBinCode.Text;
            }
            else
            {
                binCode = txtBinCode.Text.Split(',')[3];
            }
            //入库数量 srQty
            string zdr = ConnModel.userName;
            #endregion

            DataTable dt2 = (DataTable)DataGrid2.DataSource;
            int sign = 0;

            #region 更新过账页
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (dt2.Rows[i]["物料编码"].ToString() == itemCode && dt2.Rows[i]["批次"].ToString() == batch && dt2.Rows[i]["仓库编码"].ToString() == whsCode && dt2.Rows[i]["库位标识"].ToString() == absEntry)
                {
                    //只需更新入库数量
                    DataGrid2[i, dt2.Columns.Count - 2] = double.Parse(dt2.Rows[i]["入库数量"].ToString()) + srQty;
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
                newRow["供应商编码"] = cardCode;
                newRow["供应商名称"] = cardName;
                newRow["采购订单"] = docEntry;
                newRow["采购订单行号"] = lineNum;
                newRow["质检单号"] = zjd;
                newRow["质检单行号"] = zjdhh;
                newRow["物料编码"] = itemCode;
                newRow["物料名称"] = itemName;
                newRow["批次"] = batch;
                newRow["订单数量"] = quantity;
                newRow["单价"] = price;
                newRow["行合计"] = lineTotal;
                newRow["是否库位"] = binActivat;
                newRow["仓库编码"] = whsCode;
                newRow["仓库名称"] = whsName;
                newRow["库位标识"] = absEntry;
                newRow["库位编码"] = binCode;
                newRow["入库数量"] = srQty;
                newRow["制单人"] = zdr;
                dt2.Rows.Add(newRow);
                DataGrid2.DataSource = dt2;
            }

            #endregion

            DataGrid1[serNum, dt1.Columns.Count - 1] = double.Parse(row["标记数量"].ToString()) + srQty;
        }
        /// <summary>
        /// 重置按钮清空扫描页值
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearControl();
        }















        /// <summary>
        /// 过账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInCome_Click(object sender, EventArgs e)
        {

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
                    if (dtInCome.Rows[i]["物料编码"].ToString() == dt.Rows[n]["物料编码"].ToString() && dtInCome.Rows[i]["采购订单行号"].ToString() == dt.Rows[n]["采购订单行号"].ToString() && dtInCome.Rows[i]["批次"].ToString() == dt.Rows[n]["批次"].ToString())
                    {
                        double qty = Convert.ToDouble(dtInCome.Rows[i]["标记数量"]);
                        dtInCome.Rows[i]["标记数量"] = qty - Convert.ToDouble(dt.Rows[n]["数量"]);
                        DataGrid1.DataSource = dtInCome;
                    }

                }
                //从数据集集合中删除行 
                dt.Rows.RemoveAt(n);
                //刷新Datagrid1显示删除后的数据    
                DataGrid2.Refresh();
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
            txtBinCode.Text = "";
            txtQty.Text = "";
            labAbsEntry.Text = "0";
        }
        /// <summary>
        /// 绑定所有仓库
        /// </summary>
        private void BindWhs()
        {
            string sql = "SELECT WhsCode,BinActivat FROM " + ConnModel.commonDB + "..OWHS WHERE Inactive='N'";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            comboWhs.DataSource = dt;
            //comboWhs.DisplayMember = "WhsCode";
            //comboWhs.ValueMember = "WhsName";
            comboWhs.DisplayMember = "WhsCode";
            comboWhs.ValueMember = "BinActivat";
        }
        /// <summary>
        /// 过账创建空table
        /// </summary>
        /// <returns></returns>
        public void Grid3DataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("供应商编码");
            table.Columns.Add("供应商名称");
            table.Columns.Add("采购订单");
            table.Columns.Add("采购订单行号");
            table.Columns.Add("质检单号");
            table.Columns.Add("质检单行号");
            table.Columns.Add("物料编码");//6
            table.Columns.Add("物料名称");
            table.Columns.Add("批次");
            table.Columns.Add("订单数量");
            table.Columns.Add("单价");
            table.Columns.Add("行合计");
            table.Columns.Add("是否库位");
            table.Columns.Add("仓库编码");
            table.Columns.Add("仓库名称");
            table.Columns.Add("库位标识");
            table.Columns.Add("库位编码");
            table.Columns.Add("入库数量");
            table.Columns.Add("制单人");

            DataGridTableStyle mydata2 = new DataGridTableStyle();
            GridColumnStylesCollection mycol2 = null;
            DataGrid2.DataSource = table;
            mydata2.MappingName = table.TableName;
            DataGrid2.TableStyles.Clear();
            DataGrid2.TableStyles.Add(mydata2);
            mycol2 = DataGrid2.TableStyles[0].GridColumnStyles;
            mycol2["供应商编码"].Width = 0;
            mycol2["供应商名称"].Width = 0;
            mycol2["采购订单"].Width = 0;
            mycol2["采购订单行号"].Width = 0;
            mycol2["质检单号"].Width = 0;
            mycol2["质检单行号"].Width = 0;
            mycol2["订单数量"].Width = 0;
            mycol2["单价"].Width = 0;
            mycol2["行合计"].Width = 0;
            mycol2["是否库位"].Width = 0;
            mycol2["库位标识"].Width = 0;
            mycol2["制单人"].Width = 0;
        }




    }
}