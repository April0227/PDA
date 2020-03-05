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
        /// 过账页 - 初始化DataTable
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
            Page3Data.Columns.Add("物料编码");
            Page3Data.Columns.Add("物料名称");
            Page3Data.Columns.Add("批次");
            Page3Data.Columns.Add("仓库编码");
            Page3Data.Columns.Add("仓库名称");
            Page3Data.Columns.Add("库位标识");
            Page3Data.Columns.Add("库位编码");
            Page3Data.Columns.Add("入库数量");
            Page3Data.Columns.Add("订单编号");
            Page3Data.Columns.Add("订单数量");
            Page3Data.Rows.Clear();

            #region 加载所有库位仓库
            string sql2 = "EXEC SAP_NEW_OWHS_BIN";
            DataTable page2Data = SqlHelper.GetDataTable(sql2, CommandType.Text);
            this.page2Whs.DataSource = page2Data;
            this.page2Whs.DisplayMember = "WhsName";
            this.page2Whs.ValueMember = "WhsCode";
            #endregion

            page1BarCode.Focus();
        }
        #endregion


        #region 订单页
        /// <summary>
        /// 条码信息回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page1BarCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    #region 条码框验证
                    if (this.page1BarCode.Text.Trim() == "")
                    {
                        MessageBox.Show("条码信息不能为空", "错误");
                        return;
                    }
                    string page1BarCode = this.page1BarCode.Text.Trim();
                    if (page1BarCode.IndexOf(',') == -1)
                    {
                        MessageBox.Show("二维码编码格式不正确", "错误");
                        return;//匹配条件为必须包含一个,号
                    }
                    #endregion

                    #region 订单页、扫描页、过账页数据清空
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
                        #region 订单页设置表格样式及隐藏列
                        DataGridTableStyle page1Mydata = new DataGridTableStyle();
                        GridColumnStylesCollection page1Mycol = null;
                        this.page1Grid.DataSource = page1Data;
                        page1Mydata.MappingName = page1Data.TableName;
                        this.page1Grid.TableStyles.Clear();
                        this.page1Grid.TableStyles.Add(page1Mydata);
                        page1Mycol = this.page1Grid.TableStyles[0].GridColumnStyles;
                        page1Mycol["物料编码"].Width = 80;
                        page1Mycol["物料名称"].Width = 80;
                        #endregion

                        #region 过账页 根据订单加载出该
                        DataTable page3Data = Page3Data;
                        DataGridTableStyle page3Mydata = new DataGridTableStyle();
                        GridColumnStylesCollection page3Mycol = null;
                        this.page3Grid.DataSource = page3Data;
                        page3Mydata.MappingName = page3Data.TableName;
                        this.page3Grid.TableStyles.Clear();
                        this.page3Grid.TableStyles.Add(page3Mydata);
                        page3Mycol = this.page3Grid.TableStyles[0].GridColumnStyles;
                        page3Mycol["#"].Width = 0;
                        page3Mycol["物料编码"].Width = 80;
                        page3Mycol["物料名称"].Width = 80;
                        page3Mycol["仓库名称"].Width = 0;
                        page3Mycol["库位标识"].Width = 0;
                        page3Mycol["订单编号"].Width = 0;
                        page3Mycol["订单数量"].Width = 0;
                        #endregion

                        //跳转至扫描页
                        tabControl1.SelectedIndex = 2;//先初始化一下页签,否则页签3不能辅助;好像是bug
                        tabControl1.SelectedIndex = 1;
                        //扫描页赋值
                        page2DocEntry.Text = docEntry;
                        page2ItemCode.Text = itemCode;
                        page2ItemName.Text = itemName;
                        page2ItemBatch.Text = itemBatch;
                        this.page2Whs.SelectedValue = page1Data.Rows[0]["仓库"].ToString();
                        this.page2BinCode.Focus();
                    }
                    else
                    {
                        MessageBox.Show("该订单已关闭或者不存在...", "提示");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }
        #endregion

        #region 扫描页

        /// <summary>
        /// 库位标识号
        /// </summary>
        private string page2AbsEntry = "";


        /// <summary>
        /// 仓库发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2Whs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.page2BinCode.Text = "";
            this.page2BinCode.Focus();
        }
        /// <summary>
        /// 库位回车
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
                    MessageBox.Show("请先扫描二维码,再选择库位", "提示");
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

                    #region 手动输入
                    page2BinCodeText = page2BinCode;
                    page2WhsCodeText = this.page2Whs.SelectedValue.ToString();
                    string sql = "exec SAP_NEW_OWHS_OnBinCode '" + page2WhsCodeText + "','" + page2BinCodeText + "'";
                    DataTable whsBinDt = SqlHelper.GetDataTable(sql, CommandType.Text);
                    if (whsBinDt != null && whsBinDt.Rows.Count > 0)
                    {
                        DataTable page3Data = (DataTable)page3Grid.DataSource;//过账页
                        DataRow[] checkWhsRows = page3Data.Select("订单编号='" + page2DocEntryText + "'");
                        if (checkWhsRows.Length > 0)
                        {
                            string whsCode = checkWhsRows[0]["仓库编码"].ToString().Trim();
                            if (page2WhsCodeText != whsCode)
                            {
                                //在入库时单张单据,由于行物料不能选择两个及以上仓库
                                this.page2BinCode.Text = "";
                                MessageBox.Show("只能选择此仓库：" + checkWhsRows[0]["仓库名称"].ToString().Trim(), "提示");
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
                    #region 扫描
                    page2BinCodeText = page2BinCode.Split(',')[1];
                    page2WhsCodeText = page2BinCode.Split(',')[0];
                    string sql = "exec SAP_NEW_OWHS_OnBinCode '" + page2WhsCodeText + "','" + page2BinCodeText + "'";
                    DataTable whsBinDt = SqlHelper.GetDataTable(sql, CommandType.Text);
                    if (whsBinDt != null && whsBinDt.Rows.Count > 0)
                    {
                        DataTable page3Data = (DataTable)page3Grid.DataSource;//过账页
                        DataRow[] checkWhsRows = page3Data.Select("订单编号='" + page2DocEntryText + "'");
                        if (checkWhsRows.Length > 0)
                        {
                            string whsCode = checkWhsRows[0]["仓库编码"].ToString().Trim();
                            if (page2WhsCodeText != whsCode)
                            {
                                //在入库时单张单据,由于行物料不能选择两个及以上仓库
                                this.page2BinCode.Text = "";
                                MessageBox.Show("只能选择此仓库：" + checkWhsRows[0]["仓库名称"].ToString().Trim(), "提示");
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
                        MessageBox.Show("库位二维码在系统中不存在此库位", "提示");
                    }
                    #endregion
                }
            }
        }
        /// <summary>
        /// 添加至过账页
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
                    //手动输入
                    page2BinCodeText = page2BinCode;
                }
                else
                {
                    //扫描
                    page2BinCodeText = page2BinCode.Split(',')[1];
                }
                double page2Qty = Convert.ToDouble(this.page2Qty.Text.Trim());
                if (page2AbsEntry == "")
                {
                    MessageBox.Show("请先扫描物料或扫描库位", "错误");
                    return;
                }
                //校验输入数量
                DataTable page1Data = (DataTable)page1Grid.DataSource;//过账页
                DataRow page1Row = page1Data.Select("生产订单=" + docEntry)[0];
                double page1Qty = Convert.ToDouble(page1Row["剩余数量"].ToString()) - Convert.ToDouble(page1Row["入库数量"].ToString());
                if (page2Qty > page1Qty)
                {
                    MessageBox.Show("剩余收货数量为：" + page1Qty, "错误");
                    return;
                }
                DataTable page3Data = (DataTable)page3Grid.DataSource;//过账页
                if (page3Data.Rows.Count > 0)
                {
                    //再次判断仓库
                    string whsCodeCheck = page3Data.Rows[0]["仓库编码"].ToString().Trim();
                    if (whsCodeCheck != whsCode)
                    {
                        //在入库时单张单据,由于行物料不能选择两个及以上仓库
                        MessageBox.Show("只能选择此仓库：" + page3Data.Rows[0]["仓库名称"].ToString().Trim(), "提示");
                        return;
                    }
                }

                //添加操作
                DataRow[] page3Rows = page3Data.Select("订单编号='" + docEntry + "' and 批次='" + itemBatch + "' and 库位标识='" + page2AbsEntry + "'");
                if (page3Rows.Length > 0)
                {
                    //入库数量索引 8
                    int rowLine = int.Parse(page3Rows[0]["#"].ToString());
                    double qty = double.Parse(page3Rows[0]["入库数量"].ToString()) + page2Qty;
                    page3Grid[rowLine, 8] = qty.ToString();
                }
                else
                {
                    DataRow page3Row = page3Data.NewRow();
                    //是否为首行
                    int count = page3Data.Rows.Count == 0 ? 0 : page3Data.Rows.Count;
                    page3Row["#"] = count.ToString();
                    page3Row["物料编码"] = itemCode;
                    page3Row["物料名称"] = itemName;
                    page3Row["批次"] = itemBatch;
                    page3Row["仓库编码"] = whsCode;
                    page3Row["仓库名称"] = whsName;
                    page3Row["库位标识"] = page2AbsEntry;
                    page3Row["库位编码"] = page2BinCodeText;
                    page3Row["入库数量"] = page2Qty.ToString();
                    page3Row["订单编号"] = docEntry;
                    page3Data.Rows.Add(page3Row);
                    page3Grid.DataSource = page3Data;
                }
                //更新订单页
                double updateQty = Convert.ToDouble(page1Grid[0, 4].ToString()) + page2Qty;
                page1Grid[0, 4] = updateQty.ToString();
                //清空
                this.page2AbsEntry = "";
                this.page2BinCode.Text = "";
                this.page2Qty.Text = "";
                this.page2BinCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }
        #endregion



        #region 过账页

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page3BtnDel_Click(object sender, EventArgs e)
        {
            DataTable page3Data = (DataTable)page3Grid.DataSource;//过账页  
            int page3RowSelectIndex = this.page3Grid.CurrentRowIndex;
            if (page3RowSelectIndex != -1)
            {
                double page3Qty = Convert.ToDouble(this.page3Grid[page3RowSelectIndex, 8].ToString());
                string page3ItemCode = this.page3Grid[page3RowSelectIndex, 1].ToString();
                //更新订单页
                DataTable page1Data = (DataTable)page1Grid.DataSource;
                double page1Qty = (Convert.ToDouble(page1Data.Rows[0]["入库数量"].ToString())) - page3Qty;
                this.page1Grid[0, 4] = page1Qty;
                //更新过账页
                page3Data.Rows.RemoveAt(page3RowSelectIndex);
                page3Grid.Refresh();
                MessageBox.Show("删除成功", "提示");
            }
        }
        /// <summary>
        /// 过账到系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page3BtnAdd_Click(object sender, EventArgs e)
        {
            string dateTimeStr = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            List<string> sql = new List<string>();
            //添加至中间库
            DataTable page3Data = (DataTable)page3Grid.DataSource;//过账页
            if (page3Data.Rows.Count > 0)
            {
                foreach (DataRow row in page3Data.Rows)
                {
                    sql.Add(string.Format("insert into [dbo].[Bar_SCMK_SCSH] (DocEntry,ItemCode,ItemName,ItemBatch,Qty,WhsCode,WhsName,AbsEntry,BinCode,InWhsQty,UserSign,DocEntryFlag) values ({0},'{1}','{2}','{3}',{4},'{5}','{6}',{7},'{8}',{9},'{10}','{11}')", this.page2DocEntry.Text.Trim(), row["物料编码"].ToString(), row["物料名称"].ToString(), row["批次"].ToString(), this.page1Grid[0, 2].ToString(), row["仓库编码"].ToString(), row["仓库名称"].ToString(), row["库位标识"].ToString(), row["库位编码"].ToString(), row["入库数量"].ToString(), ConnModel.userName, dateTimeStr));
                }
                //更新SAP自定义字段
                DataTable page1Data = (DataTable)page1Grid.DataSource;//订单页
                foreach (DataRow row in page1Data.Rows)
                {
                    double page1Qty = Convert.ToDouble(row["入库数量"].ToString());
                    if (page1Qty > 0)
                    {
                        sql.Add(string.Format("UPDATE {0}.. OWOR SET U_SHQty ={1},U_SFWG = CASE WHEN PlannedQty = {1} THEN 'Y' ELSE 'W' END WHERE DocEntry = {2}", ConnModel.commonDB, (double.Parse(row["生产数量"].ToString()) + double.Parse(row["入库数量"].ToString()) - double.Parse(row["剩余数量"].ToString())), this.page2DocEntry.Text.Trim()));
                    }
                }
                if (SqlHelper.ExecuteSqlTran(sql) > 0)
                {
                    #region 清空扫描页、变量、返回到第一个页签
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
                    //4 返回到第一个页签
                    tabControl1.SelectedIndex = 0;
                    #endregion


                    //调用API生成系统单据
                    //string url = "XSGL_XSTH/Import";
                    //string ps = "{DocEntryFlag:'" + dateTimeStr + "'}";
                    //string valStr = HttpHelper.HttpPost(url, ps);
                    //if (valStr.Split(':')[0].ToString() == "0")
                    //{
                    //    MessageBox.Show("OK");
                    //}
                    //else
                    //{
                    //    MessageBox.Show(valStr.Split(':')[1].ToString(), "错误");
                    //}
                }
                else
                {
                    MessageBox.Show("添加至中间库失败,请检查数据完整性.", "提示");
                }
            }
        }
        #endregion









    }
}