using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;

namespace SealedEmulate_PDA.Forms
{
    public partial class CGTH : Form
    {
        public CGTH()
        {
            InitializeComponent();
        }



        /// <summary>
        /// 收货单 - 回车    
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtdocEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                #region 置空
                DataGrid1.DataSource = null;

                ClearControl();
                #endregion
                string docEntry = txtdocEntry.Text.Trim();
                if (System.Text.RegularExpressions.Regex.IsMatch(docEntry, @"^[1-9]\d*$"))
                {
                    if (!string.IsNullOrEmpty(docEntry))
                    {
                        #region 订单页
                        //根据输入的质检单号，查询 
                        string sql = " SELECT Row_Number() OVER ( ORDER BY Id )-1 '#', CardCode 供应商编码,CardName 供应商名称,DocEntry 采购订单,LineNum 采购订单行号,ZJD 质检单,ZJDHH 质检单行号,ItemCode 物料编码,ItemName 物料名称,ItemBatch 批次,WhsFlag 是否库位,WhsCode 仓库编码,WhsName 仓库名称,AbsEntry 库位标识,BinCode 库位编码,InWhsQty 入库数量  FROM [dbo].[CGRK] WHERE IsImport=1";
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
                            mycol1["供应商编码"].Width = 0;
                            mycol1["供应商名称"].Width = 0;
                            mycol1["采购订单"].Width = 0;
                            mycol1["采购订单行号"].Width = 0;
                            mycol1["质检单"].Width = 0;
                            mycol1["质检单行号"].Width = 0;
                            mycol1["是否库位"].Width = 0;
                            tabControl1.SelectedIndex = 1;
                            txtBarcode.Focus();
                        }
                        else
                        {
                            MessageBox.Show("当前收货单不存在,请更换收货单", "提示");
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
                            //单批次多库位物料编码  判断数量

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

        private void txtBinCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {

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
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }

        private void btnInCome_Click(object sender, EventArgs e)
        {

        }

        private void comboWhs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}