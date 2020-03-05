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
    public partial class YLZS : Form
    {
        public YLZS()
        {
            InitializeComponent();
        }
        /// <summary>
        /// github ≤‚ ‘”√
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page2ItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData();
            }
        }

        private void page2ItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData();
            }
        }

        private void page2Batch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData();
            }
        }
        private void BindData()
        {
            string sql1 = string.Format("exec SAP_NEW_YLZS '{0}','{1}','{2}'", this.page2ItemCode.Text.Trim(), this.page2ItemName.Text.Trim(), this.page2Batch.Text.Trim());
            DataTable page1Data = SqlHelper.GetDataTable(sql1, CommandType.Text);
            if (page1Data != null && page1Data.Rows.Count > 0)
            {
                this.page1Grid.DataSource = page1Data;
            }
            else
            {
                this.page1Grid.DataSource = null;
            }
        }


    }
}