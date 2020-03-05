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
    public partial class CGMK_CGSH : Form
    {
        public CGMK_CGSH()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 过账页 - 初始化DataTable
        /// </summary>
        private DataTable Page3Data = new DataTable();
        /// <summary>
        /// 订单页 - 客户编码
        /// </summary>
        private string CardCode = "";
        /// <summary>
        /// 订单页 - 客户名称
        /// </summary>
        private string CardName = "";

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CGMK_CGSH_Load(object sender, EventArgs e)
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
            Page3Data.Columns.Add("订单行号");
            Page3Data.Columns.Add("订单数量");
            Page3Data.Columns.Add("单价");
            Page3Data.Columns.Add("行合计");
            Page3Data.Columns.Add("供应商编码");
            Page3Data.Columns.Add("供应商名称");
            Page3Data.Columns.Add("采购订单单号");
            Page3Data.Columns.Add("采购订单行号");
            Page3Data.Rows.Clear();
            #region 加载所有库位仓库
            string sql2 = "EXEC SAP_NEW_OWHS_BIN";
            DataTable page2Data = SqlHelper.GetDataTable(sql2, CommandType.Text);
            this.page2Whs.DataSource = page2Data;
            this.page2Whs.DisplayMember = "WhsName";
            this.page2Whs.ValueMember = "WhsCode";
            #endregion
        }
        #endregion



        #region 订单页
        /// <summary>
        /// 采购质检单回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page1DocEntry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(this.page1DocEntry.Text.Trim(), @"^[1-9]\d*$"))
                    {
                        #region 订单页、扫描页、过账页数据清空
                        //1
                        this.page1Grid.DataSource = null;
                        //2 
                        CardCode = "";
                        CardName = "";
                        page2QuanTity = 0;
                        page1LineNum = "";
                        page2AbsEntry = "";

                        this.page2BarCode.Text = "";
                        this.page2ItemCode.Text = "";
                        this.page2ItemName.Text = "";
                        this.page2ItemBatch.Text = "";
                        this.page2BinCode.Text = "";
                        this.page2Qty.Text = "";
                        //3
                        Page3Data.Rows.Clear();
                        this.page3Grid.DataSource = null;
                        #endregion
                        int page1DocEntry = int.Parse(this.page1DocEntry.Text.Trim());
                        string sql1 = "exec SAP_NEW_CGSH_LOAD " + page1DocEntry;
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
                            page1Mycol["#"].Width = 0;
                            page1Mycol["物料编码"].Width = 80;
                            page1Mycol["物料名称"].Width = 80;
                            page1Mycol["行号"].Width = 0;
                            page1Mycol["供应商编码"].Width = 0;
                            page1Mycol["供应商名称"].Width = 0;

                            page1Mycol["仓库名称"].Width = 0;
                            page1Mycol["是否库位"].Width = 0;
                            page1Mycol["订单编号"].Width = 0;
                            page1Mycol["采购订单单号"].Width = 0;
                            page1Mycol["采购订单行号"].Width = 0;
                            page1Mycol["单价"].Width = 0;
                            page1Mycol["行合计"].Width = 0;

                            CardCode = page1Data.Rows[0]["供应商编码"].ToString();
                            CardName = page1Data.Rows[0]["供应商名称"].ToString();
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
                            page3Mycol["订单行号"].Width = 0;
                            page3Mycol["订单数量"].Width = 0;
                            page3Mycol["单价"].Width = 0;
                            page3Mycol["行合计"].Width = 0;
                            page3Mycol["供应商编码"].Width = 0;
                            page3Mycol["供应商名称"].Width = 0;
                            page3Mycol["采购订单单号"].Width = 0;
                            page3Mycol["采购订单行号"].Width = 0;
                            #endregion

                            //跳转至扫描页
                            tabControl1.SelectedIndex = 2;//先初始化一下页签,否则页签3不能辅助;好像是bug
                            tabControl1.SelectedIndex = 1;
                        }
                        else
                        {
                            MessageBox.Show("该订单已关闭或者不存在...", "提示");
                        }
                    }
                    else
                    {
                        MessageBox.Show("输入订单编号不合法!", "错误");
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
        /// 扫描页 - 行物料剩余出库数量标记
        /// </summary>
        private double page2QuanTity = 0;
        /// <summary>
        /// 订单页 - 记录物料行号
        /// </summary>
        private string page1LineNum = "";
        /// <summary>
        /// 订单页 - 记录采购订单单号
        /// </summary>
        private string page1PurDocEntry = "";
        /// <summary>
        /// 订单页 - 记录采购订单行号
        /// </summary>
        private string page1PurLineNum = "";
        /// <summary>
        /// 库位标识号
        /// </summary>
        private string page2AbsEntry = "";

        /// <summary>
        /// 条码信息回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2BarCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.page1DocEntry.Text.Trim() == "")
                    {
                        MessageBox.Show("采购质检单号为空", "错误");
                        return;
                    }
                    string page2BarCode = this.page2BarCode.Text.Trim();
                    if (page2BarCode == "")
                    {
                        return;
                    }
                    if (page2BarCode.IndexOf(',') == -1)
                    {
                        MessageBox.Show("二维码编码格式不正确", "错误");
                        return;//匹配条件为必须包含一个,号
                    }
                    #region 扫描页清空
                    page2QuanTity = 0;
                    page1LineNum = "";
                    page2AbsEntry = "";
                    page1PurDocEntry = "";
                    page1PurLineNum = "";
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2BinCode.Text = "";
                    this.page2Qty.Text = "";
                    #endregion
                    string page2ItemCode = page2BarCode.Split(',')[0].Trim();
                    string page2ItemName = page2BarCode.Split(',')[1].Trim();
                    string page2ItemBatch = page2BarCode.Split(',')[2].Trim();
                    //判断订单页物料和出库数量
                    DataTable page1Data = (DataTable)page1Grid.DataSource;//订单页
                    DataRow[] page1Rows = page1Data.Select("物料编码='" + page2ItemCode + "'");
                    if (page1Rows.Length > 0)
                    {
                        DataRow page1Row = page1Rows[0];
                        page2QuanTity = (Convert.ToDouble(page1Row["数量"].ToString()) - Convert.ToDouble(page1Row["入库数量"].ToString()));
                        if (page2QuanTity == 0)
                        {
                            MessageBox.Show("该物料已收货完毕", "提示");
                            this.page2BarCode.Text = "";
                            this.page2BarCode.Focus();
                            return;
                        }
                        //校验批次是否为交货批次
                        //string sql = "SELECT COUNT(*) Num FROM [dbo].[Bar_CGMK_CGSH] WHERE IsImport=1 AND SalesRecDoc=" + this.page1DocEntry.Text.Trim() + " AND ItemCode='" + page2ItemCode + "' AND ItemBatch='" + page2ItemBatch + "'";
                        //int num = int.Parse(SqlHelper.ExecuteScalar(sql, CommandType.Text).ToString());
                        //if (num == 0)
                        //{
                        //    MessageBox.Show("批次与采购质检单批次不匹配.", "提示");
                        //    this.page2BarCode.Text = "";
                        //    this.page2BarCode.Focus();
                        //    return;
                        //}
                        //扫描页赋值
                        this.page2ItemCode.Text = page2ItemCode;
                        this.page2ItemName.Text = page2ItemName;
                        this.page2ItemBatch.Text = page2ItemBatch;
                        this.page2Whs.SelectedValue = page1Row["仓库"].ToString();
                        this.page2BinCode.Focus();
                        page1LineNum = page1Row["行号"].ToString();
                        page1PurDocEntry = page1Row["采购订单单号"].ToString();
                        page1PurLineNum = page1Row["采购订单行号"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("扫描物料与采购质检单不匹配", "提示");
                        this.page2BarCode.Text = "";
                        this.page2BarCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }
        /// <summary>
        /// 仓库发生变化 - 清空库位
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
                        DataRow[] checkWhsRows = page3Data.Select("订单行号='" + page1LineNum + "'");
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
                    else
                    {
                        this.page2BinCode.Text = "";
                        MessageBox.Show("输入库位二维码不正确", "提示");
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
                        DataRow[] checkWhsRows = page3Data.Select("订单行号='" + page1LineNum + "'");
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
            if ((this.page2BarCode.Text.Trim() == "") && (this.page2Qty.Text.Trim() == ""))
            {
                return;
            }

            string page1DocEntry = this.page1DocEntry.Text.Trim();
            string page2ItemCode = this.page2ItemCode.Text.Trim();
            string page2ItemName = this.page2ItemName.Text.Trim();
            string page2ItemBatch = this.page2ItemBatch.Text.Trim();
            string page2WhsCode = this.page2Whs.SelectedValue.ToString();
            string page2WhsName = this.page2Whs.Text.Trim();
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
            if (CardCode == "" || page2ItemCode == "" || page2AbsEntry == "")
            {
                MessageBox.Show("请先扫描物料或扫描库位", "错误");
                return;
            }
            else if (page2Qty > page2QuanTity)
            {
                MessageBox.Show("剩余收货数量为：" + page2QuanTity, "错误");
                return;
            }
            else
            {
                DataTable page3Data = (DataTable)page3Grid.DataSource;//过账页
                DataRow[] checkWhsRows = page3Data.Select("订单行号='" + page1LineNum + "'");
                if (checkWhsRows.Length > 0)
                {
                    string whsCode = checkWhsRows[0]["仓库编码"].ToString().Trim();
                    if (page2WhsCode != whsCode)
                    {
                        //在入库时单张单据,由于行物料不能选择两个及以上仓库
                        MessageBox.Show("只能选择此仓库：" + checkWhsRows[0]["仓库名称"].ToString().Trim(), "提示");
                    }
                    else
                    {
                        DataRow[] page3Rows = page3Data.Select("订单行号='" + page1LineNum + "' and 批次='" + page2ItemBatch + "' and 库位标识='" + page2AbsEntry + "'");
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
                            page3Row["物料编码"] = page2ItemCode;
                            page3Row["物料名称"] = page2ItemName;
                            page3Row["批次"] = page2ItemBatch;
                            page3Row["仓库编码"] = page2WhsCode;
                            page3Row["仓库名称"] = page2WhsName;
                            page3Row["库位标识"] = page2AbsEntry;
                            page3Row["库位编码"] = page2BinCodeText;
                            page3Row["入库数量"] = page2Qty.ToString();
                            page3Row["订单编号"] = page1DocEntry;
                            page3Row["订单行号"] = page1LineNum;
                            page3Row["采购订单单号"] = page1PurDocEntry;
                            page3Row["采购订单行号"] = page1PurLineNum;
                            page3Row["供应商编码"] = CardCode;
                            page3Row["供应商名称"] = CardName;
                            page3Data.Rows.Add(page3Row);
                            page3Grid.DataSource = page3Data;
                        }
                    }
                }
                else
                {
                    DataRow page3Row = page3Data.NewRow();
                    //是否为首行
                    int count = page3Data.Rows.Count == 0 ? 0 : page3Data.Rows.Count;
                    page3Row["#"] = count.ToString();
                    page3Row["物料编码"] = page2ItemCode;
                    page3Row["物料名称"] = page2ItemName;
                    page3Row["批次"] = page2ItemBatch;
                    page3Row["仓库编码"] = page2WhsCode;
                    page3Row["仓库名称"] = page2WhsName;
                    page3Row["库位标识"] = page2AbsEntry;
                    page3Row["库位编码"] = page2BinCodeText;
                    page3Row["入库数量"] = page2Qty.ToString();
                    page3Row["订单编号"] = page1DocEntry;
                    page3Row["订单行号"] = page1LineNum;
                    page3Row["采购订单单号"]= page1PurDocEntry;
                    page3Row["采购订单行号"] = page1PurLineNum;
                    page3Row["供应商编码"] = CardCode;
                    page3Row["供应商名称"] = CardName;
                    page3Data.Rows.Add(page3Row);
                    page3Grid.DataSource = page3Data;
                } 
                //更新订单页剩余入库数量
                DataTable page1Data = (DataTable)page1Grid.DataSource;//过账页
                int page1RowLine = int.Parse(page1Data.Select("行号='" + page1LineNum + "'")[0]["#"].ToString());
                double page1Qty = Convert.ToDouble(page1Grid[page1RowLine, 16].ToString()) + page2Qty;
                page1Grid[page1RowLine, 16] = page1Qty.ToString(); 
                #region 扫描页清空
                page2QuanTity = 0;
                page1LineNum = "";
                page2AbsEntry = "";
                this.page2BarCode.Text = "";
                this.page2ItemCode.Text = "";
                this.page2ItemName.Text = "";
                this.page2ItemBatch.Text = "";
                this.page2BinCode.Text = "";
                this.page2Qty.Text = "";
                #endregion
                this.page2BarCode.Focus();
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
                DataRow[] page1Rows = page1Data.Select("物料编码='" + page3ItemCode + "'");
                int page1RowSelectIndex = int.Parse(page1Rows[0]["#"].ToString());
                double page1Qty = (Convert.ToDouble(page1Rows[0]["入库数量"].ToString())) - page3Qty;
                this.page1Grid[page1RowSelectIndex, 16] = page1Qty;
                //更新过账页
                page3Data.Rows.RemoveAt(page3RowSelectIndex);
                page3Grid.Refresh();
                MessageBox.Show("删除成功", "提示");
            }
        }
        /// <summary>
        /// 过账
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
                    sql.Add(string.Format("insert into [dbo].[Bar_CGMK_CGSH] (CardCode,CardName,DocEntry,LineNum,ItemCode,ItemName,ItemBatch,Qty,Price,LineTotal,WhsCode,WhsName,AbsEntry,BinCode,InWhsQty,UserSign,DocEntryFlag,PurDocEntry,PurLineNum) values ('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},{8},{9},'{10}','{11}',{12},'{13}',{14},'{15}','{16}',{17},{18})", row["供应商编码"].ToString(), row["供应商名称"].ToString(), row["订单编号"].ToString(), row["订单行号"].ToString(), row["物料编码"].ToString(), row["物料名称"].ToString(), row["批次"].ToString(), 0, 0, 0, row["仓库编码"].ToString(), row["仓库名称"].ToString(), row["库位标识"].ToString(), row["库位编码"].ToString(), row["入库数量"].ToString(), ConnModel.userName, dateTimeStr, Convert.ToInt32(row["采购订单单号"].ToString()), Convert.ToInt32(row["采购订单行号"].ToString())));
                }
                //更新SAP自定义字段
                DataTable page1Data = (DataTable)page1Grid.DataSource;//订单页
                foreach (DataRow row in page1Data.Rows)
                {
                    double page1Qty = Convert.ToDouble(row["入库数量"].ToString());
                    if (page1Qty > 0)
                    {
                        sql.Add(string.Format("UPDATE {0}.. [@SBO_ZJD_H] SET U_WQSL =ISNULL(U_WQSL,0) + {1} WHERE DocEntry = {2} AND LineId = {3}", ConnModel.commonDB, double.Parse(row["入库数量"].ToString()), this.page1DocEntry.Text.Trim(), row["行号"].ToString()));                       
                    }
                }
                if (SqlHelper.ExecuteSqlTran(sql) > 0)
                {
                    #region 清空扫描页、变量、返回到第一个页签
                    //1
                    this.page1DocEntry.Text = "";
                    this.page1Grid.DataSource = null;
                    CardCode = "";
                    CardName = "";
                    //2
                    page2QuanTity = 0;
                    page1LineNum = "";
                    page2AbsEntry = "";

                    this.page2BarCode.Text = "";
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2BinCode.Text = "";
                    this.page2Qty.Text = "";
                    //3
                    this.page3Grid.DataSource = null;
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