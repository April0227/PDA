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
    public partial class frmProcdut1 : Form
    {
        public frmProcdut1()
        {
            InitializeComponent();
        }

        private int selectRow = 0;
        private string syb = string.Empty;// 事业部
        private string gx = string.Empty; // 工序
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProcdut_Load(object sender, EventArgs e)
        {
            this.cmbPro.Focus();
        }
        /// <summary>
        /// 生产订单文本 失焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPro_LostFocus(object sender, EventArgs e)
        {
            syb = string.Empty;
            gx = string.Empty;
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                // 检索生产计划编号
                string sql = string.Format("SELECT U_SCJHDH,U_SYB,U_GX FROM " + ConnModel.commonDB + "..OWOR where DocEntry=@docentry");
                SqlParameter param = new SqlParameter("@docentry", textBox.Text.Trim());
                DataTable obj = SqlHelper.GetDataTable(sql, CommandType.Text, param);
                if (obj != null && obj.Rows.Count > 0)
                {
                    cmbProPlan.Text = obj.Rows[0]["U_SCJHDH"].ToString();
                    syb = obj.Rows[0]["U_SYB"].ToString();
                    gx = obj.Rows[0]["U_GX"].ToString();
                }
                else
                {
                    cmbProPlan.Text = string.Empty;
                }
                this.btnGo.Enabled = true;
            }
            else
            {
                this.btnGo.Enabled = false;
            }
        }
        /// <summary>
        /// 生产订单文本发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPro_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                this.btnGo.Enabled = true;
            }
            else
            {
                this.btnGo.Enabled = false;
            }
        }
        /// <summary>
        /// 数据检索，提取订单的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (cmbPro.Text.Trim() == "")
            {
                return;
            }
            this.btnGo.Enabled = false;
            string proStr = cmbPro.Text;
            string docType = SqlHelper.ExecuteScalar(string.Format("SELECT case Type when 'S' then '标准' when 'P' then '特殊' else '分拆' end AS Type  FROM {0}..OWOR WHERE Status='R' and DocEntry={1}", ConnModel.commonDB, proStr), CommandType.Text).ToString();
            this.labType.Text = docType;
            string sql = string.Empty;
            if (docType == "分拆")
            {
                //发料为表头 成品或半成品
                sql = string.Format("select a.ItemCode '物料编码',b.ItemName '物料名称',a.PlannedQty '计划',ISNULL(a.U_SHQty,0) '已发',(a.PlannedQty-ISNULL(a.U_SHQty,0)) AS '未发',a.wareHouse AS '仓库',B.DocEntry '订单',-1 '订单行号' from {0}..owor a join {0}..oitm b on a.ItemCode=b.ItemCode where a.Status='R' and a.U_SFWG='W'and a.DocEntry={1}", ConnModel.commonDB, proStr);
            }
            else
            {
                //发料为行明细
                sql = string.Format("SELECT B.ItemCode '物料编码',C.ItemName '物料名称',B.PlannedQty '计划',ISNULL(B.U_SHQty,0) '已发',(B.PlannedQty-ISNULL(B.U_SHQty,0)) AS '未发',B.wareHouse AS '仓库',B.DocEntry '订单',B.LineNum '订单行号' FROM {0}OWOR AS A JOIN {0}WOR1 AS B ON B.DocEntry = A.DocEntry JOIN {0}OITM AS C ON C.ItemCode = B.ItemCode WHERE A.Status='R' AND  B.U_SFWG='W' and b.IssueType='M' AND  A.DocEntry ={1}", ConnModel.commonDB + "..", proStr);
            }
            DGpro.DataSource = SqlHelper.GetDataTable(sql, CommandType.Text);

        }
        /// <summary>
        /// 根据条码信息 截取物料编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string qrCodeStr = textBox1.Text.Trim();
                if (qrCodeStr != "")
                {
                    string[] arry = qrCodeStr.Split(',');
                    txtTraceNo.Text = arry[0].Trim();
                    if (arry.Length == 5)
                    {
                        tb_Quantity.Text = arry[3].Trim().TrimEnd(new char[] { 'G', 'g' }).TrimEnd(new char[] { 'K', 'k' });
                    }
                }
            }
        }
        /// <summary>
        /// 根据条码信息 截取物料编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            string qrCodeStr = textBox1.Text.Trim();
            if (qrCodeStr != "")
            {
                string[] arry = qrCodeStr.Split(',');
                txtTraceNo.Text = arry[0].Trim();
                if (arry.Length == 5)
                {
                    tb_Quantity.Text = arry[3].Trim().TrimEnd(new char[] { 'G', 'g' }).TrimEnd(new char[] { 'K', 'k' });
                }
            }
        }
        /// <summary>
        /// 根据条码信息 截取物料编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string qrCodeStr = textBox1.Text.Trim();
            if (qrCodeStr != "")
            {
                string[] arry = qrCodeStr.Split(',');
                txtTraceNo.Text = arry[0].Trim();
                if (arry.Length == 5)
                {
                    tb_Quantity.Text = arry[3].Trim().TrimEnd(new char[] { 'G', 'g' }).TrimEnd(new char[] { 'K', 'k' });
                }
            }
        }
        /// <summary>
        /// 物料编码文本发生改变
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
            DataTable dt = ((DataTable)DGpro.DataSource);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (val == dt.Rows[i]["物料编码"].ToString())
                    {
                        selectRow = i;
                        DGpro.Select(i);
                        this.btnSave.Enabled = true;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 清空按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            txtTraceNo.Text = "";
            textBox1.Focus();
        }
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = DGpro.DataSource as DataTable;
            if (dt != null)
            {
                this.btnSave.Enabled = false;
                DataRow row = dt.Rows[selectRow];//获取选中的行
                string sql = string.Empty;
                if (this.ckwg.Checked) //完工
                {
                    if (labType.Text == "分拆")
                    {
                        sql = String.Format("UPDATE {0}OWOR SET U_SFWG = 'Y',U_SHQty = U_SHQty + {1} WHERE DocEntry={2}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text);
                    }
                    else
                    {
                        sql = String.Format("UPDATE {0}WOR1 SET U_SFWG = 'Y',U_SHQty = U_SHQty + {1} WHERE DocEntry={2} AND LineNum={3}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text, row["订单行号"]);
                    }
                    if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) <= 0)
                    {
                        MessageBox.Show("更新SAP数据超时,请稍后重试...");
                        this.btnSave.Enabled = true;
                        return;
                    }
                }
                else//未点选完工
                {
                    if (labType.Text == "分拆")
                    {
                        sql = String.Format("UPDATE {0}OWOR SET U_SHQty = U_SHQty + {1} WHERE DocEntry={2}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text);
                    }
                    else
                    {
                        sql = String.Format("UPDATE {0}WOR1 SET U_SHQty = U_SHQty + {1} WHERE DocEntry={2} AND LineNum={3}", ConnModel.commonDB + "..", Convert.ToDouble(tb_Quantity.Text.Trim()), cmbPro.Text, row["订单行号"]);
                    }

                    if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) <= 0)
                    {
                        MessageBox.Show("更新SAP数据超时,请稍后重试...");
                        this.btnSave.Enabled = true;
                        return;
                    }
                }

                //往临时库添加数据
                sql = string.Format("INSERT INTO ProduHair(PlanNum,ProOrderNum,LineNum,ItemCode,ItemName,Quantity,BatchNum,WhsCode,SYB,GX) VALUES ('{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}','{8}','{9}')", cmbProPlan.Text, cmbPro.Text, row["订单行号"], row["物料编码"], row["物料名称"], tb_Quantity.Text.Trim(), txtTraceNo.Text.Trim(), row["仓库"], syb, gx);
                if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) > 0)
                {
                    if (this.ckwg.Checked) //完工,移除行数据
                    {
                        dt.Rows.RemoveAt(selectRow);
                    }
                    else
                    {
                        row["已发"] = (Convert.ToDouble(row["已发"].ToString()) + (Convert.ToDouble(tb_Quantity.Text.Trim()))).ToString();
                        row["未发"] = (Convert.ToDouble(row["计划"].ToString()) - Convert.ToDouble(row["已发"].ToString())).ToString();
                    }
                    DGpro.DataSource = dt;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DGpro.UnSelect(i);
                    }
                    btnGo_Click(null, null);
                    labType.Text = "";
                    MessageBox.Show("添加成功！");
                    this.textBox1.Text = string.Empty;
                    this.textBox1.Focus();
                    this.txtTraceNo.Text = "";
                    this.tb_Quantity.Text = "";
                    ckwg.Checked = false;
                    selectRow = 0;
                }
                else
                {
                    MessageBox.Show("添加数据超时,请稍后重试...");
                    this.btnSave.Enabled = true;
                }
            }

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