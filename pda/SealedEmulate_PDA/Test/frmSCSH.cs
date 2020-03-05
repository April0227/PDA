using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;
using SealedEmulate_PDA.Dal;
using System.Data.SqlClient;

namespace SealedEmulate_PDA
{
    public partial class frmSCSH : Form
    {
        public frmSCSH()
        {
            InitializeComponent();
        }
        private System.Data.DataTable tbl;
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSCSH_Load(object sender, EventArgs e)
        {
            try
            {
                LoadForm();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                DataTable dt = DGpro.DataSource as DataTable;
                if (dt != null)
                {
                    double qTy = Convert.ToDouble(tb_Quantity.Text.Trim());//收获数量
                    string sql = string.Empty;
                    string orderNum = cmbPro.Text.Trim();
                    foreach (DataRow row in dt.Rows)
                    {
                        double wfQty = Convert.ToDouble(row["未发"].ToString());
                        double tagNum = Math.Ceiling(qTy / 25);
                        double singleQty = qTy % 25;
                        if (singleQty == 0)
                        {
                            singleQty = 25;
                        }
                        string qrCode = row["物料编码"] + " " + DateTime.Now.ToString("yyyyMMdd");
                        if (ckwg.Checked)//已完工
                        {
                            sql = string.Format("INSERT INTO ProduReceipt (IsFinish,ItemCode,ItemName,OrderQty,QuanTity,UndoneQty,ProOrderNum,Shifts,TagNum,SingleQty,BatchNum,QRCode) VALUES ('1','{0}','{1}','{2}',{3},{4},'{5}','{6}',{7},{8},'{9}','{10}')", row["物料编码"], row["物料名称"], row["计划"], qTy, wfQty - qTy, orderNum, txtBC.Text.Trim().Length >= 8 ? txtBC.Text.Trim().Substring(0, 7) : txtBC.Text.Trim(), tagNum, singleQty, DateTime.Now.ToString("yyyyMMdd"), qrCode);
                        }
                        else
                        {
                            sql = string.Format("INSERT INTO ProduReceipt (IsFinish,ItemCode,ItemName,OrderQty,QuanTity,UndoneQty,ProOrderNum,Shifts,TagNum,BatchNum,SingleQty,QRCode) VALUES ('0','{0}','{1}',{2},{3},{4},'{5}','{6}',{7},'{8}',25,'{9}')", row["物料编码"], row["物料名称"], row["计划"], qTy, wfQty - qTy, orderNum, txtBC.Text.Trim().Length >= 8 ? txtBC.Text.Trim().Substring(0, 7) : txtBC.Text.Trim(), tagNum, DateTime.Now.ToString("yyyyMMdd"), qrCode);
                        }
                    }
                    if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) > 0)
                    {
                        if (ckwg.Checked) //已完工
                        {
                            sql = string.Format("UPDATE " + ConnModel.commonDB + "..OWOR SET U_SFWG='Y',U_SHQty+={0} WHERE DocEntry={1}", qTy, orderNum);
                        }
                        else
                        {
                            sql = string.Format("UPDATE " + ConnModel.commonDB + "..OWOR SET U_SHQty+={0} WHERE DocEntry={1}", qTy, orderNum);
                        }
                        if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) > 0)
                        {
                            MessageBox.Show("添加成功...");
                            LoadForm();
                        }
                        else
                        {
                            MessageBox.Show("更新B1数据失败...");
                            sql = "Delete from ProduReceipt where ProOrderNum=" + orderNum;
                            SqlHelper.ExecuteNonquery(sql, CommandType.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("添加失败...");
                    }
                }
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// 订单发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPro_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPro.Text != "")
                {
                    string sql = string.Format("SELECT A.DocEntry '订单',A.ItemCode '物料编码',B.ItemName '物料名称',A.PlannedQty '计划',ISNULL(A.U_SHQty,0) '已发',(A.PlannedQty-ISNULL(A.U_SHQty,0)) '未发',A.Warehouse '仓库' FROM {0}OWOR AS A JOIN {0}OITM AS B ON B.ItemCode = A.ItemCode WHERE A.DocEntry={1}", ConnModel.commonDB + "..", cmbPro.Text.Trim());
                    DGpro.DataSource = SqlHelper.GetDataTable(sql, CommandType.Text);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void LoadForm()
        {
            txtBC.Text = DateTime.Now.ToString("yyyyMMdd");
            tb_Quantity.Text = "";
            ckwg.Checked = false;
            string sql = "SELECT DocEntry FROM " + ConnModel.commonDB + "..OWOR WHERE Status = 'R' AND U_SFWG='W'";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            cmbPro.ValueMember = "DocEntry";
            cmbPro.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                DGpro.DataSource = null;
                btnSave.Enabled = false;
            }

        }
    }
}