using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA.Dal
{
    public class SqlHelper
    {
        private static readonly string ConnStr = ConnModel.P_ConnectString; 
        public static DataTable GetDataTable(string sql, CommandType type, params SqlParameter[] pars)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn))
                    {
                        if (pars != null && pars.Length > 0)
                        {
                            foreach (SqlParameter p in pars)
                            {
                                apter.SelectCommand.Parameters.Add(p);
                            }
                        }
                        apter.SelectCommand.CommandType = type;
                        DataTable da = new DataTable();
                        apter.Fill(da);
                        return da;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        public static DataTable GetDataTable(string sql, CommandType type)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {

                    using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn))
                    {
                        apter.SelectCommand.CommandType = type;
                        DataTable da = new DataTable();
                        apter.Fill(da);
                        return da;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        public static DataSet GetDataSet(string sql, CommandType type, params SqlParameter[] pars)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn))
                    {
                        if (pars != null && pars.Length > 0)
                        {
                            foreach (SqlParameter p in pars)
                            {
                                apter.SelectCommand.Parameters.Add(p);
                            }
                        }
                        apter.SelectCommand.CommandType = type;
                        DataSet da = new DataSet();
                        apter.Fill(da);
                        return da;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

        }

        public static int ExecuteNonquery(string sql, CommandType type, params SqlParameter[] pars)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (pars != null && pars.Length > 0)
                        {
                            foreach (SqlParameter p in pars)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }
                        cmd.CommandType = type;
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

        }

        /// <summary>  
        /// 执行多条SQL语句，实现数据库事务。  
        /// </summary>  
        /// <param name="list">多条SQL语句</param>       
        public static int ExecuteSqlTran(List<String> list)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        int count = 0;
                        for (int i = 0; i < list.Count; i++)
                        {
                            string sql = list[i];
                            if (sql.Trim().Length > 1)
                            {
                                cmd.CommandText = sql;
                                count += cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                        return count;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.StackTrace);
                        tran.Rollback();
                        return -1;
                    }
                }
            }
        }
        public static object ExecuteScalar(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (pars != null && pars.Length > 0)
                    {
                        foreach (SqlParameter p in pars)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                    cmd.CommandType = type;
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
