using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;
using SealedEmulate_PDA._2020.Global;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA._2020
{
    public partial class SCMK_SCFL : Form
    {
        public SCMK_SCFL()
        {
            InitializeComponent();
        }

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SCMK_SCFL_Load(object sender, EventArgs e)
        {
            this.page1DocEntry.Focus();
        }
        #endregion



        #region 订单页
        /// <summary>
        /// 生产订单回车
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
                        this.page2BarCode.Text = "";
                        this.page2ItemCode.Text = "";
                        this.page2ItemName.Text = "";
                        this.page2ItemBatch.Text = "";
                        this.page2WhsCode.Text = null;
                        this.page2BinCode.Text = "";
                        this.page2Qty.Text = "";
                        this.page2Batch.Checked = false;
                        //3
                        this.page3Grid.DataSource = null;
                        #endregion
                        int page1DocEntry = int.Parse(this.page1DocEntry.Text.Trim());
                        string sql = "exec SAP_NEW_SCFL_LOAD " + page1DocEntry;
                        DataTable page1Data = SqlHelper.GetDataTable(sql, CommandType.Text);
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
                            #endregion

                            #region 过账页 根据订单加载出该
                            string sql3 = "exec SAP_NEW_SCFL_LOAD_GZ " + page1DocEntry;
                            DataTable page3Data = SqlHelper.GetDataTable(sql3, CommandType.Text);
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
                            page3Mycol["是否授权"].Width = 0;
                            page3Mycol["授权人"].Width = 0;
                            page3Mycol["订单编号"].Width = 0;
                            page3Mycol["订单行号"].Width = 0;
                            page3Mycol["订单数量"].Width = 0; 
                            #endregion
                            //跳转至扫描页
                            tabControl1.SelectedIndex = 2;//先初始化一下页签,否则页签3不能辅助;好像是bug
                            tabControl1.SelectedIndex = 1; 
                        }
                        else
                        {
                            MessageBox.Show("该订单已发料或者不存在...", "提示");
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
        /// 过账页 - 物料批次剩余库存数量合计
        /// </summary>
        private double page3QuanTity = 0;
        /// <summary>
        /// 过账页 - 物料批次所对应的库位编码
        /// </summary>
        private Dictionary<string, double> page3BinCode = new Dictionary<string, double>();
        /// <summary>
        /// 过账页 - 单一库位的库存
        /// </summary>
        private double page2BinCodeQty = 0;

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
                        MessageBox.Show("生产订单号为空", "错误");
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
                    page3QuanTity = 0;
                    page3BinCode.Clear();
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2WhsCode.Text = "";
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
                        if ((Convert.ToDouble(page1Row["剩余数量"].ToString()) - Convert.ToDouble(page1Row["出库数量"].ToString())) == 0)
                        {
                            MessageBox.Show("该物料已出库完毕", "提示");
                            this.page2BarCode.Text = "";
                            this.page2BarCode.Focus();
                            return;
                        }
                        //判断过账页物料批次是否为先进先出批次
                        DataTable page3Data = (DataTable)page3Grid.DataSource;//过账页  
                        #region 判断是否为强制先进先出

                        if (this.page2Batch.Checked == false)
                        {
                            DataRow[] page3Rows = page3Data.Select("物料编码='" + page2ItemCode + "'");
                            foreach (DataRow page3Row in page3Rows)
                            {
                                if ((Convert.ToDouble(page3Row["库存数量"].ToString()) - Convert.ToDouble(page3Row["出库数量"].ToString())) > 0)
                                {
                                    if (page3Row["批次"].ToString().Trim() != page2ItemBatch)
                                    {
                                        MessageBox.Show("依据先进先出原则,应该扫描该物料的批次为：" + page3Row["批次"].ToString().Trim(), "提示");
                                        this.page2BarCode.Text = "";
                                        this.page2BarCode.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion

                        //判断扫描的物料批次剩余库存
                        DataRow[] page3NewRows = page3Data.Select("物料编码='" + page2ItemCode + "' and 批次='" + page2ItemBatch + "' and 仓库编码='" + page1Row["仓库"].ToString() + "'");
                        foreach (DataRow page3Row in page3NewRows)
                        {
                            double qty = (Convert.ToDouble(page3Row["库存数量"].ToString()) - Convert.ToDouble(page3Row["出库数量"].ToString()));
                            page3QuanTity += qty;
                            page3BinCode.Add(page3Row["库位编码"].ToString(), qty);
                        }
                        if (page3QuanTity == 0)
                        {
                            page3BinCode.Clear();
                            MessageBox.Show("扫描物料所对应的批次库存为0", "提示");
                            this.page2BarCode.Text = "";
                            this.page2BarCode.Focus();
                            return;
                        }
                        //扫描页赋值
                        this.page2ItemCode.Text = page2ItemCode;
                        this.page2ItemName.Text = page2ItemName;
                        this.page2ItemBatch.Text = page2ItemBatch;
                        this.page2WhsCode.Text = page1Row["仓库"].ToString();
                        page2QuanTity = (Convert.ToDouble(page1Row["剩余数量"].ToString()) - Convert.ToDouble(page1Row["出库数量"].ToString()));
                        this.page2BinCode.Focus();
                    }
                    else
                    {
                        MessageBox.Show("扫描物料与销售订单不匹配", "提示");
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
        /// 授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2Batch_Click(object sender, EventArgs e)
        {
            if (this.page2Batch.Checked == true)
            {
                XSMK_XSJH_SQ frm = new XSMK_XSJH_SQ();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("授权成功.");
                }
                else
                {
                    this.page2Batch.Checked = false;
                    MessageBox.Show("授权失败.");
                }
            }
        }
        /// <summary>
        /// 校验库位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2BinCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string page2BinCodeText = "";
                string page2BinCode = this.page2BinCode.Text.Trim();
                if (page2BinCode == "")
                {
                    return;
                }
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
                //校验库位是否存在
                foreach (string binCode in page3BinCode.Keys)
                {
                    if (binCode == page2BinCodeText)
                    {
                        page2BinCodeQty = page3BinCode[binCode];
                        this.page2Qty.Focus();
                        return;
                    }
                }
                this.page2BinCode.Text = "";
                MessageBox.Show("扫描物料所对应的批次的该库位库存为0", "提示");
            }
        }
        /// <summary>
        /// 发料数量校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2Qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckPage2Qty();
            }
        }
        /// <summary>
        /// 发料数量校验
        /// </summary>
        private bool CheckPage2Qty()
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(this.page2Qty.Text.Trim(), @"^[1-9]\d*$"))
            {
                double page2Qty = Convert.ToDouble(this.page2Qty.Text.Trim());
                //校验扫描页 - 行物料剩余出库数量
                if (page2Qty > page2QuanTity)
                {
                    this.page2Qty.Text = "";
                    MessageBox.Show("交货数量不得大于剩余出库数量：" + page2QuanTity, "提示");
                    return false;
                }
                //判断是否输入库位
                string page2BinCode = this.page2BinCode.Text.Trim();
                if (page2BinCode == "")
                {
                    //校验批次的总库存
                    if (page2Qty > page3QuanTity)
                    {
                        this.page2Qty.Text = "";
                        MessageBox.Show("当前批次剩余数量：" + page3QuanTity, "提示");
                        return false;
                    }
                }
                else
                {
                    if (page2Qty > page2BinCodeQty)
                    {
                        this.page2Qty.Text = "";
                        MessageBox.Show("当前库位剩余数量：" + page2BinCodeQty, "提示");
                        return false;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("输入数量不合法!", "错误");
                return false;
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
                if ((this.page2BarCode.Text.Trim() != "") && (this.page2Qty.Text.Trim() != "") && CheckPage2Qty() == true)
                {
                    string page2ItemCode = this.page2ItemCode.Text.Trim();
                    string page2ItemBatch = this.page2ItemBatch.Text.Trim();
                    string page2WhsCode = this.page2WhsCode.Text.Trim();
                    string page2BinCode = this.page2BinCode.Text.Trim();
                    double page2Qty = Convert.ToDouble(this.page2Qty.Text.Trim());
                    //更新订单页
                    DataTable page1Data = (DataTable)page1Grid.DataSource;
                    DataRow[] page1Rows = page1Data.Select("物料编码='" + page2ItemCode + "'");
                    int page1RowIndex = int.Parse(page1Rows[0]["#"].ToString());
                    //出库数量=出库数量+交货数量
                    double qty1 = Convert.ToDouble(this.page1Grid[page1RowIndex, 5].ToString()) + page2Qty;
                    this.page1Grid[page1RowIndex, 5] = qty1.ToString();
                    //更新过账页
                    DataTable page3Data = (DataTable)page3Grid.DataSource;
                    if (page2BinCode == "")
                    {
                        //不指定库位出库
                        string queryStr = string.Format("物料编码='{0}' and 批次='{1}' and 仓库编码='{2}'", page2ItemCode, page2ItemBatch, page2WhsCode);
                        DataRow[] page3Rows = page3Data.Select(queryStr);
                        for (int i = 0; i < page3Rows.Length; i++)
                        {
                            double qty3 = Convert.ToDouble(page3Rows[i]["库存数量"].ToString()) - Convert.ToDouble(page3Rows[i]["出库数量"].ToString());
                            if (qty3 > 0 && page2Qty > 0)
                            {
                                int page3RowIndex = int.Parse(page3Rows[i]["#"].ToString());
                                if (qty3 > page2Qty)
                                {
                                    this.page3Grid[page3RowIndex, 9] = (Convert.ToDouble(page3Rows[i]["出库数量"].ToString()) + page2Qty).ToString();
                                    this.page3Grid[page3RowIndex, 10] = this.page2Batch.Checked == true ? "是" : "否";
                                    this.page3Grid[page3RowIndex, 11] = ConnModel.AuthorName;
                                    break;
                                }
                                page2Qty -= qty3;
                                this.page3Grid[page3RowIndex, 9] = page3Rows[i]["库存数量"].ToString();
                                this.page3Grid[page3RowIndex, 10] = this.page2Batch.Checked == true ? "是" : "否";
                                this.page3Grid[page3RowIndex, 11] = ConnModel.AuthorName;
                            }
                        }
                    }
                    else
                    {
                        //指定库位出库 
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
                        string queryStr = string.Format("物料编码='{0}' and 批次='{1}' and 仓库编码='{2}' and 库位编码='{3}'", page2ItemCode, page2ItemBatch, page2WhsCode, page2BinCodeText);
                        DataRow[] page3Rows = page3Data.Select(queryStr);
                        int page3RowIndex = int.Parse(page3Rows[0]["#"].ToString());
                        double qty3 = Convert.ToDouble(this.page3Grid[page3RowIndex, 9].ToString()) + page2Qty;
                        this.page3Grid[page3RowIndex, 9] = qty3.ToString();
                        this.page3Grid[page3RowIndex, 10] = this.page2Batch.Checked == true ? "是" : "否";
                        this.page3Grid[page3RowIndex, 11] = ConnModel.AuthorName;
                    }
                    #region 清空扫描页、变量
                    page2QuanTity = 0;
                    page3QuanTity = 0;
                    page3BinCode.Clear();
                    page2BinCodeQty = 0;
                    this.page2BarCode.Text = "";
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2WhsCode.Text = "";
                    this.page2BinCode.Text = "";
                    this.page2Qty.Text = "";
                    this.page2Batch.Checked = false;
                    #endregion

                }
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
                double page3Qty = Convert.ToDouble(this.page3Grid[page3RowSelectIndex, 9].ToString());
                if (page3Qty > 0)
                {
                    string page3ItemCode = this.page3Grid[page3RowSelectIndex, 1].ToString();
                    //更新订单页
                    DataTable page1Data = (DataTable)page1Grid.DataSource;
                    DataRow[] page1Rows = page1Data.Select("物料编码='" + page3ItemCode + "'");
                    int page1RowSelectIndex = int.Parse(page1Rows[0]["#"].ToString());
                    double page1Qty = (Convert.ToDouble(page1Rows[0]["出库数量"].ToString())) - page3Qty;
                    this.page1Grid[page1RowSelectIndex, 5] = page1Qty;
                    //更新过账页
                    this.page3Grid[page3RowSelectIndex, 9] = 0.00;
                    this.page3Grid[page3RowSelectIndex, 10] = "否";
                    this.page3Grid[page3RowSelectIndex, 11] = "";
                    MessageBox.Show("删除成功", "提示");
                }
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
            foreach (DataRow row in page3Data.Rows)
            {
                double page3Qty = Convert.ToDouble(row["出库数量"].ToString());
                if (page3Qty > 0)
                {
                    sql.Add(string.Format("insert into [dbo].[Bar_SCMK_SCFL] (DocEntry,LineNum,ItemCode,ItemName,ItemBatch,Qty,WhsCode,WhsName,AbsEntry,BinCode,InWhsQty,UserSign,DocEntryFlag,IsAuthor,AuthorName) values ({0},{1},'{2}','{3}','{4}',{5},'{6}','{7}',{8},'{9}',{10},'{11}',{12},'{13}','{14}')", row["订单编号"].ToString(), row["订单行号"].ToString(), row["物料编码"].ToString(), row["物料名称"].ToString(), row["批次"].ToString(), row["订单数量"].ToString(), row["仓库编码"].ToString(), row["仓库名称"].ToString(), row["库位标识"].ToString(), row["库位编码"].ToString(), row["出库数量"].ToString(), ConnModel.userName, dateTimeStr, row["是否授权"].ToString(), row["授权人"].ToString()));
                }
            }
            if (sql.Count > 0)
            {
                //更新SAP自定义字段
                DataTable page1Data = (DataTable)page1Grid.DataSource;//订单页
                foreach (DataRow row in page1Data.Rows)
                {
                    double page1Qty = Convert.ToDouble(row["出库数量"].ToString());
                    if (page1Qty > 0)
                    {
                        sql.Add(string.Format("UPDATE {0}.. WOR1 SET U_SHQty={1},U_SFWG = CASE WHEN PlannedQty = {1} THEN 'Y' ELSE 'W' END WHERE DocEntry = {2} AND LineNum = {3}", ConnModel.commonDB, (double.Parse(row["计划数量"].ToString()) + double.Parse(row["出库数量"].ToString()) - double.Parse(row["剩余数量"].ToString())), this.page1DocEntry.Text.Trim(), row["行号"].ToString()));
                    }

                }
                if (SqlHelper.ExecuteSqlTran(sql) > 0)
                {
                    #region 清空扫描页、变量、返回到第一个页签
                    //1
                    this.page1DocEntry.Text = "";
                    this.page1Grid.DataSource = null;
                    //2
                    page2QuanTity = 0;
                    page3QuanTity = 0;
                    page3BinCode.Clear();
                    page2BinCodeQty = 0;
                    this.page2BarCode.Text = "";
                    this.page2ItemCode.Text = "";
                    this.page2ItemName.Text = "";
                    this.page2ItemBatch.Text = "";
                    this.page2WhsCode.Text = "";
                    this.page2BinCode.Text = "";
                    this.page2Qty.Text = "";
                    this.page2Batch.Checked = false;
                    //3
                    this.page3Grid.DataSource = null;
                    //4 返回到第一个页签
                    tabControl1.SelectedIndex = 0;
                    #endregion


                    //调用API生成系统单据
                    //string url = "SCGL_SCFL/Import";
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