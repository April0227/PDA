using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA
{
    public partial class frmXSJH2 : Form
    {
        public frmXSJH2()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        private string cardCode = string.Empty;
        /// <summary>
        /// 订单信息页签 - 行号
        /// </summary>
        private int rowXS = 0;
        /// <summary>
        /// 物料扫描后 - 批次
        /// </summary>
        private string pc = string.Empty;
        /// <summary>
        /// 物料明细页签 - 存储过程名字
        /// </summary>
        private const string currSql = "SBO_OnHandQtyForOrdr";
        /// <summary>
        /// 物料明细页签 - 行号
        /// </summary>
        private int rowNum;
        /// <summary>
        /// 物料明细页签 - 序号行剩余出库数量(库存数量-出库数量)
        /// </summary>
        private double rowNumQty = 0;
        /// <summary>
        /// 物料明细页签 - 是否刷新
        /// </summary>
        private int currDocEntry;











        /// <summary>
        /// 销售订单 - 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSaleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            { 
                cmbSaleNoChangeOrKeyPress();
            }
        }
        /// <summary>
        /// 销售交货--条码信息 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1KeyPress();
            }
        }
        /// <summary>
        /// 销售交货 - 物料编码文本框 发生改变
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
        /// 数量文本框 - 回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    string itemCode = txtTraceNo.Text.Trim();
                    double currQty = double.Parse(tb_Quantity.Text.Trim());
                    double xsSyQty = double.Parse(DGsale[rowXS, 3].ToString());
                    double xsQty = double.Parse(DGsale[rowXS, 4].ToString());

                    //判断订单信息页签 (本次扫描出库数量 <= 剩余出库数量)
                    if (currQty > (xsSyQty - xsQty))
                    {
                        MessageBox.Show(string.Format("本次扫描信息:\r\n物料编码:{0}\r\n扫描批次:{1}\r\n 销售数量:{2}\r\n【订单信息页签 - 剩余出库数量{3}】", itemCode, pc, currQty, xsSyQty - xsQty));
                        return;
                    }

                    //判断 物料明细页签 (本次扫描出库数量 <= 剩余出库数量) 
                    DataTable dt = (DataTable)DG2.DataSource;
                    if (dt != null)
                    {
                        double sumQty = 0;
                        for (int i = rowNum; i < dt.Rows.Count; i++)
                        {
                            if (radioButton1.Checked == true)
                            {
                                if (dt.Rows[i][1].ToString() == itemCode)//"物料编码"
                                {
                                    double kcQty = double.Parse(DG2[i, 6].ToString());
                                    double currRowQty = double.Parse(DG2[i, 7].ToString());
                                    sumQty += (kcQty - currRowQty);
                                }
                            }
                            else
                            {
                                if (dt.Rows[i][1].ToString() == itemCode && dt.Rows[i][3].ToString() == pc)//物料编码 批次
                                {
                                    double kcQty = double.Parse(DG2[i, 6].ToString());
                                    double currRowQty = double.Parse(DG2[i, 7].ToString());
                                    sumQty += (kcQty - currRowQty);
                                }
                            }

                        }
                        if (currQty > sumQty)
                        {
                            MessageBox.Show(string.Format("本次扫描信息:\r\n物料编码:{0}\r\n扫描批次:{1}\r\n 销售数量:{2}\r\n【物料明细页签 - 剩余出库总数量{3}】", itemCode, pc, currQty, sumQty));
                            return;
                        }
                        //订单信息页签 - 出库数量赋值 
                        DGsale[rowXS, 4] = xsQty + currQty;
                        //物料明细页签 - 出库数量赋值
                        for (int i = rowNum; i < dt.Rows.Count; i++)
                        {
                            if (currQty != 0)
                            {
                                if (dt.Rows[i][1].ToString() == itemCode)//物料编码
                                {
                                    double kcQty = double.Parse(DG2[i, 6].ToString());//20
                                    double currRowQty = double.Parse(DG2[i, 7].ToString());//0
                                    double ckQty = kcQty - currRowQty;//20
                                    if (ckQty != 0)
                                    {
                                        //ckQty:20 currQty:30
                                        if (currQty < ckQty)
                                        {
                                            DG2[i, 7] = currQty + currRowQty;
                                            break;
                                        }
                                        else
                                        {
                                            DG2[i, 7] = kcQty.ToString();
                                            currQty = currQty - ckQty;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
                System.Data.DataTable tbl = (DataTable)DGsale.DataSource;
                if (tbl.Rows.Count < 0)
                {
                    MessageBox.Show("明细行为空!");
                    return;
                }
                List<string> list = new List<string>();
                string salesNum = cmbSaleNo.Text.ToString();
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    string itemCode = tbl.Rows[i]["物料编码"].ToString();
                    string itemName = tbl.Rows[i]["物料名称"].ToString();
                    double xsQuanTity = double.Parse(tbl.Rows[i]["销售数量"].ToString());
                    double syQuanTity = double.Parse(tbl.Rows[i]["剩余数量"].ToString());
                    double quanTity = double.Parse(tbl.Rows[i]["出库数量"].ToString());
                    string docEntry = cmbSaleNo.Text.ToString();
                    int salesLineNum = int.Parse(tbl.Rows[i]["行号"].ToString());
                    string whsCode = tbl.Rows[i]["仓库"].ToString();
                    if (quanTity > 0)
                    {
                        //自定义库 添加或者更新判断
                        string sql = string.Format("SELECT ID FROM [dbo].[SalesDelivery] WHERE IsImport=0 AND SalesNum={0} and SalesNumLine={1}", salesNum, salesLineNum);
                        DataTable salesDt = SqlHelper.GetDataTable(sql, CommandType.Text);
                        if (salesDt.Rows.Count > 0)
                        {
                            list.Add(string.Format("UPDATE [dbo].[SalesDelivery] SET Quantity=Quantity+{2} WHERE IsImport=0 AND SalesNum={0} and SalesNumLine={1}", salesNum, salesLineNum, quanTity));
                        }
                        else
                        {
                            list.Add(string.Format("INSERT INTO SalesDelivery (SalesNum,ItemCode,ItemName,Quantity,SalesNumLine,CardCode,WhsCode)VALUES ({0},'{1}','{2}',{3},{4},'{5}','{6}')", salesNum, itemCode, itemName, quanTity, salesLineNum, cardCode, whsCode));
                        }
                        //SAP
                        list.Add(string.Format(@"UPDATE {0}.. RDR1 SET U_XSQty = {1},U_XSLineStatus = CASE WHEN Quantity = {1} THEN 'C' ELSE 'O' END WHERE DocEntry = {2} AND LineNum = {3} ", ConnModel.commonDB, (xsQuanTity + quanTity - syQuanTity), docEntry, salesLineNum));
                        DataTable dt = SqlHelper.GetDataTable(string.Format("SELECT U_XSLineStatus FROM {0}..RDR1 WHERE U_XSLineStatus='O' and DocEntry ={1}", ConnModel.commonDB, docEntry), CommandType.Text);
                        if (dt.Rows.Count > 0)
                        {
                            list.Add(string.Format("UPDATE {0}.. ORDR SET U_XSStatus = 'C' WHERE  DocEntry ={1}", ConnModel.commonDB, docEntry));
                        }
                        else
                        {
                            list.Add(string.Format("UPDATE {0}.. ORDR SET U_XSStatus = 'O' WHERE  DocEntry ={1}", ConnModel.commonDB, docEntry));
                        }
                    }
                }
                if (SqlHelper.ExecuteSqlTran(list) > 0)
                {
                    MessageBox.Show("添加成功！");
                    cmbSaleNoChangeOrKeyPress();
                }
                else
                {
                    MessageBox.Show("添加失败！");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }








        //----------------------------------------------------------标准项--------------------------------------------------//
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmXSJH2_Load(object sender, EventArgs e)
        {
            //订单号文本框获得焦点
            cmbSaleNo.Focus();
        }
        private void cmbSaleNoChangeOrKeyPress()
        {
            try
            {
                textBox1.Text = "";
                txtTraceNo.Text = "";
                tb_Quantity.Text = "";
                cmbKH.Text = string.Empty;
                cardCode = string.Empty;
                if (cmbSaleNo.Text != "")
                {
                    int docEntry = int.Parse(cmbSaleNo.Text.ToString().Trim());
                    //检索客户
                    DataTable dtkh = SqlHelper.GetDataTable(string.Format("SELECT CardCode,CardName from {0}..ORDR WHERE DocEntry = {1} ", ConnModel.commonDB, docEntry), CommandType.Text);
                    if (dtkh != null && dtkh.Rows.Count > 0)
                    {
                        cmbKH.Text = dtkh.Rows[0]["CardName"].ToString();
                        cardCode = dtkh.Rows[0]["CardCode"].ToString();
                    }
                    LoadOrdrPage(docEntry);
                    LoadItemPage(docEntry);
                    textBox1.Focus();
                }

              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// 订单信息页签
        /// </summary>
        /// <param name="docEntry"></param>
        private void LoadOrdrPage(int docEntry)
        {
            // 订单信息       根据销售订单检索销售订单明细行 AND U_XSQty < Quantity AND  LineStatus ='O' AND U_XSLineStatus = 'O'
            DataTable tbl = SqlHelper.GetDataTable(string.Format(@"SELECT A.ItemCode as 物料编码, A.Dscription as 物料名称,A.Quantity as 销售数量,(A.Quantity-isnull(A.U_XSQty,0)) as 剩余数量, 0 as 出库数量,A.LineNum as 行号,A.WhsCode as 仓库 FROM {0}..RDR1 A
WHERE A.DocEntry = {1} AND  A.OpenQty != 0 AND isnull(A.U_XSQty,'0') < A.Quantity AND  A.LineStatus ='O' AND A.U_XSLineStatus = 'O'", ConnModel.commonDB, docEntry), CommandType.Text);
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
        /// <summary>
        /// 物料明细页签
        /// </summary>
        /// <param name="docEntry"></param>
        private void LoadItemPage(int docEntry)
        {
            //物料明细 根据（物料编码,仓库）
            DataTable tb2 = SqlHelper.GetDataTable(string.Format(" exec {1}..{2} {0},'' ", docEntry, ConnModel.commonDB, currSql), CommandType.Text);
            //设置datagrid的列宽
            DataGridTableStyle mydata2 = new DataGridTableStyle();
            GridColumnStylesCollection mycol2 = null;
            DG2.DataSource = tb2;
            mydata2.MappingName = tb2.TableName;
            DG2.TableStyles.Clear();
            DG2.TableStyles.Add(mydata2);
            mycol2 = DG2.TableStyles[0].GridColumnStyles;
            mycol2[1].Width = 80;
            mycol2[2].Width = 80;

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
        /// <summary>
        /// 销售交货--清除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            txtTraceNo.Text = "";
            tb_Quantity.Text = "";
            textBox1.Focus();
        }
        /// <summary>
        /// 条码信息 - 回车事件
        /// </summary>
        private void textBox1KeyPress()
        {
            try
            {
                if (DGsale.IsSelected(rowXS))
                {
                    DGsale.UnSelect(rowXS);
                    txtTraceNo.Text = "";
                    int docEntry = int.Parse(cmbSaleNo.Text.ToString().Trim());
                    //LoadItemPage(docEntry);//忘记为什么添加这行代码了
                }
                string qrCodeStr = textBox1.Text.Trim();
                if (qrCodeStr != "")
                {
                    string[] array = qrCodeStr.Split(',');
                    if (array.Length == 5 && !qrCodeStr.EndsWith(","))
                    {
                        string itemCode = qrCodeStr.Split(',')[0].Trim();
                        string qty = qrCodeStr.Split(',')[4].Trim();
                        pc = qrCodeStr.Split(',')[2].Trim();
                        DataTable dt = (DataTable)DG2.DataSource;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //判断当前扫描的物料批次是否为第一个
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][1].ToString() == itemCode)//物料编码
                                {
                                    double kcQty = double.Parse(dt.Rows[i][6].ToString());//库存数量
                                    double syQty = double.Parse(dt.Rows[i][7].ToString());//出库数量
                                    if (kcQty - syQty > 0)
                                    {
                                        string currPc = dt.Rows[i][3].ToString();//批次
                                        if (currPc == pc)
                                        {
                                            txtTraceNo.Text = itemCode;
                                            tb_Quantity.Text = qty;
                                            rowNum = i;//行号
                                            rowNumQty = kcQty - syQty;
                                            tb_Quantity.Focus();//数量文本框获得焦点  
                                            return;
                                        }
                                        else
                                        {
                                            MessageBox.Show(string.Format("本次扫描批次不匹配\r\n扫描批次【{0}】\r\n应扫描批次【{1}】", pc, currPc));
                                            textBox1.Text = "";
                                            textBox1.Focus();
                                            return;
                                        }
                                    }
                                }
                            }
                            MessageBox.Show(string.Format("物料明细页签不存在本次扫描\r\n物料编码【{0}】", itemCode));
                            textBox1.Text = "";
                            textBox1.Focus();
                        }
                        else
                        {
                            MessageBox.Show("当前物料明细页签数据为空....");
                            textBox1.Text = "";
                            textBox1.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox1.Text = "";
                textBox1.Focus();
            }
        }





    }
}