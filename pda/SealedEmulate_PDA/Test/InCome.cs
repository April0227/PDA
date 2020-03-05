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
    public partial class InCome : Form
    {
        public InCome()
        {
            InitializeComponent();
        }
        private DataTable tbl;
        private string cardcode;
        private void btnDelrow_Click(object sender, EventArgs e)
        {
            int n = 0;
            //获取当前选定的行号   
            n = DG.CurrentRowIndex;
            //从数据集集合中删除行 
            tbl.Rows.RemoveAt(n);
            //刷新Datagrid1显示删除后的数据    
            DG.Refresh();

        }

        private void cmbStockNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //处理由动态窗口加载时产生的不必要的变化
            if (cmbStockNo.SelectedValue.Equals("System.Data.DataRowView"))
            {
                return;
            }
            //检索质检单
            string sql = "SELECT U_ItemCode as 物料编码,U_ItemName as 物料名称,U_HGSL as 合格数量, LineId as 质检单行号,U_CGDD as 采购订单,U_CGDDH as 采购订单行号 FROM " + ConnModel.commonDB + ".. [@SBO_ZJD_H] WHERE DocEntry = @docentry AND U_ZJJG ='y'AND U_SFSH = 'N'";
            SqlParameter ps = new SqlParameter("@docentry", Convert.ToString(cmbStockNo.SelectedValue));
            tbl = SqlHelper.GetDataTable(sql, CommandType.Text, ps);
            DG.DataSource = tbl;

            string sql2 = "SELECT U_SYB,U_CardCode FROM " + ConnModel.commonDB + ".. [@SBO_ZJD] WHERE DocEntry =@docentry";
            SqlParameter ps2 = new SqlParameter("@docentry", Convert.ToString(cmbStockNo.SelectedValue));
            DataTable tbl2 = SqlHelper.GetDataTable(sql2, CommandType.Text, ps2);
            if (tbl2.Rows.Count > 0)
            {
                txtSyb.Text = Convert.ToString(tbl2.Rows[0]["U_SYB"]);
                cardcode = Convert.ToString(tbl2.Rows[0]["U_CardCode"]);

            }

        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                CheckIsInfo();

                //添加数据
                string str = Convert.ToString(cmbStockNo.SelectedValue);
                char[] separator = { '-' };
                string tempcmbStockNo = str.Split(separator)[0];
                if (tbl.Rows.Count > 0)
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        string sqlStr = string.Empty;
                        string itemcode = tbl.Rows[i]["物料编码"].ToString();
                        string itemname = tbl.Rows[i]["物料名称"].ToString();
                        double quantity = Convert.ToDouble(tbl.Rows[i]["合格数量"].ToString());
                        int checknumline = Convert.ToInt32(tbl.Rows[i]["质检单行号"].ToString());
                        string buynum = tbl.Rows[i]["采购订单"].ToString();
                        int buynumline = Convert.ToInt32(tbl.Rows[i]["采购订单行号"].ToString());
                        string custraceno = CusTraceNo.Text.ToString();
                        string nowtime = System.DateTime.Now.ToString("yyyyMMdd");
                        string qrcode = itemcode + " " + nowtime;
                        sqlStr = String.Format("INSERT INTO InCome(CheckNum,ItemCode,ItemName,QuanTity,CheckNumLine,BuyNum,BuyNumLine,CardCode,CusTraceNo,BatchNum,QRCode) values ('{0}','{1}','{2}',{3},{4},'{5}',{6},'{7}','{8}','{9}','{10}')", tempcmbStockNo, itemcode, itemname, quantity, checknumline, buynum, buynumline, cardcode, custraceno, nowtime, qrcode);
                        list.Add(sqlStr);
                        sqlStr = String.Format("UPDATE {0}.. [@SBO_ZJD_H] SET U_SFSH = 'Y' WHERE  DocEntry ='{1}' and LineId = {2}", ConnModel.commonDB, tempcmbStockNo, checknumline);
                        list.Add(sqlStr);
                    }
                    if (SqlHelper.ExecuteSqlTran(list) > 0)
                    {
                        MessageBox.Show("添加成功...");
                        tbl = null;
                        Init(tempcmbStockNo);
                    }
                    else
                    {
                        MessageBox.Show("添加失败...");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误：" + ex.Message);
            }

        }

        private void InCome_Load(object sender, EventArgs e)
        {
            string sql = "select DocEntry,cast(DocEntry as varchar)+'--'+U_CardCode as cardname from " + ConnModel.commonDB + "..[@SBO_ZJD] where U_DJZT = 'W'  order by docnum desc";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            cmbStockNo.DataSource = dt;
            cmbStockNo.ValueMember = "DocEntry";
            cmbStockNo.DisplayMember = "cardname";
            DG.Focus();
            if (cmbStockNo.Text != "")
            {
                //取出入库员 OHEM 员工表   2017.06.09 新需求中不需要在PDA显示入库员
                //string sql2 = "SELECT empID , ISNULL(T1.firstName,'')+ISNULL(T1.middleName,'')+ISNULL(T1.lastName,'') as name FROM " + ConnModel.commonDB + "..OHEM T1";
                //DataTable tbl2 = SqlHelper.GetDataTable(sql2, CommandType.Text);
                //cmbRky.DataSource = tbl2;
                //cmbRky.ValueMember = "empID";
                //cmbRky.DisplayMember = "name";
            }
            else
            {
                MessageBox.Show("请先生成质检单！");
                this.Close();
            }

        }
        //检查输入相关信息准确性
        private void CheckIsInfo()
        {
            if (tbl.Rows.Count == 0)
            {
                cmbStockNo.Focus();
                throw new Exception("请先选择质检单号！");

            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Init(string tempcmbStockNo)
        {
            //检索该单据的行明细是否全部收货，如果全部收货，则更新表头 单据状态为R 已入库
            string sql3 = "SELECT U_SFSH FROM " + ConnModel.commonDB + " ..[@SBO_ZJD_H] WHERE DocEntry =@docentry";
            SqlParameter ps3 = new SqlParameter("@docentry", tempcmbStockNo);

            DataTable tbl3 = SqlHelper.GetDataTable(sql3, CommandType.Text, ps3);
            if (tbl3.Rows.Count > 0)
            {
                int i = 0;
                int j = 0;
                for (i = 0; i <= tbl3.Rows.Count - 1; i++)
                {
                    if (tbl3.Rows[i]["U_SFSH"].ToString() == "Y")
                    {
                        j = j + 1;
                    }
                }
                if (j == tbl3.Rows.Count)
                {
                    string sql4 = "UPDATE " + ConnModel.commonDB + ".. [@SBO_ZJD] SET U_DJZT = 'R' WHERE  DocEntry =@docentry";
                    SqlParameter ps4 = new SqlParameter("@docentry", tempcmbStockNo);
                    SqlHelper.ExecuteNonquery(sql4, CommandType.Text, ps4);
                }
            }

            cmbStockNo.Focus();
            //重新绑定 combox的数据源
            string sql = "select DocEntry,cast(DocEntry as varchar)+'--'+U_CardCode as cardname from " + ConnModel.commonDB + "..[@SBO_ZJD] where U_DJZT = 'W'  order by docnum desc";
            System.Data.DataTable tbl1 = SqlHelper.GetDataTable(sql, CommandType.Text);
            cmbStockNo.DataSource = tbl1;
            cmbStockNo.ValueMember = "DocEntry";
            cmbStockNo.DisplayMember = "cardname";

            if (cmbStockNo.Items.Count == 0)
            {
                DG.DataSource = null;
                //cmbRky.Text = "";
            }
            CusTraceNo.Text = "";
            txtSyb.Text = "";
        }

        private void CusTraceNo_GotFocus(object sender, EventArgs e)
        {
            if (CusTraceNo.Text != "")
            {
                CusTraceNo.Text = "";
            }
        }
    }
}