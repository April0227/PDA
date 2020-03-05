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
        /// �ջ��� - �س�    
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtdocEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                #region �ÿ�
                DataGrid1.DataSource = null;

                ClearControl();
                #endregion
                string docEntry = txtdocEntry.Text.Trim();
                if (System.Text.RegularExpressions.Regex.IsMatch(docEntry, @"^[1-9]\d*$"))
                {
                    if (!string.IsNullOrEmpty(docEntry))
                    {
                        #region ����ҳ
                        //����������ʼ쵥�ţ���ѯ 
                        string sql = " SELECT Row_Number() OVER ( ORDER BY Id )-1 '#', CardCode ��Ӧ�̱���,CardName ��Ӧ������,DocEntry �ɹ�����,LineNum �ɹ������к�,ZJD �ʼ쵥,ZJDHH �ʼ쵥�к�,ItemCode ���ϱ���,ItemName ��������,ItemBatch ����,WhsFlag �Ƿ��λ,WhsCode �ֿ����,WhsName �ֿ�����,AbsEntry ��λ��ʶ,BinCode ��λ����,InWhsQty �������  FROM [dbo].[CGRK] WHERE IsImport=1";
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
                            mycol1["��Ӧ�̱���"].Width = 0;
                            mycol1["��Ӧ������"].Width = 0;
                            mycol1["�ɹ�����"].Width = 0;
                            mycol1["�ɹ������к�"].Width = 0;
                            mycol1["�ʼ쵥"].Width = 0;
                            mycol1["�ʼ쵥�к�"].Width = 0;
                            mycol1["�Ƿ��λ"].Width = 0;
                            tabControl1.SelectedIndex = 1;
                            txtBarcode.Focus();
                        }
                        else
                        {
                            MessageBox.Show("��ǰ�ջ���������,������ջ���", "��ʾ");
                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show("���붩����Ų��Ϸ�!", "����");
                }
            }
        }
        /// <summary>
        /// ������Ϣɨ��
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
                    MessageBox.Show("�������붩��,��ɨ��.", "����");
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
                        MessageBox.Show("������Ϣ�����Ϲ淶!", "����");
                        return;
                    }
                    string[] array = barCode.Split(',');
                    if (array.Length > 0)
                    {
                        string itemCode = array[0].Trim();
                        DataTable dt1 = (DataTable)DataGrid1.DataSource;//����ҳ 
                        foreach (DataRow row in dt1.Rows)
                        {
                            //�����ζ��λ���ϱ���  �ж�����

                        }
                        ClearControl();
                        MessageBox.Show("ɨ�������붩�����ϲ�ƥ��", "����");
                    }
                    else
                    {
                        ClearControl();
                        MessageBox.Show("������Ϣ�����Ϲ淶!", "����");
                    }
                }
                else
                {
                    ClearControl();
                    MessageBox.Show("������Ϣ����Ϊ��!", "��ʾ");
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
        /// ɨ��ҳ�������
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