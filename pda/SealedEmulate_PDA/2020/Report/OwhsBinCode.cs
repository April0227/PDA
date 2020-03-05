using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SealedEmulate_PDA.Dal;

namespace SealedEmulate_PDA._2020.Report
{
    public partial class OwhsBinCode : Form
    {
        public OwhsBinCode()
        {
            InitializeComponent();
        }


        private void OwhsBinCode_Load(object sender, EventArgs e)
        {
            #region 加载所有库位仓库
            string sql2 = "EXEC SAP_NEW_OWHS_BIN";
            DataTable page2Data = SqlHelper.GetDataTable(sql2, CommandType.Text);
            this.page2Whs.DataSource = page2Data;
            this.page2Whs.DisplayMember = "WhsName";
            this.page2Whs.ValueMember = "WhsCode";
            #endregion
            string val = this.page2Whs.SelectedValue.ToString();
            string sql = "exec SAP_NEW_OWHS_BinCode '" + val + "'";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            this.page1Grid.DataSource = dt;
        }

        private void page2Whs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string val = this.page2Whs.SelectedValue.ToString();
            string sql = "exec SAP_NEW_OWHS_BinCode '" + val + "'";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            this.page1Grid.DataSource = dt;
        }
    }
}