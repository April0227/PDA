using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SealedEmulate_PDA.Model;
namespace SealedEmulate_PDA.Dal
{
    public class UserDal
    {
        /// <summary>
        /// 登陆判断
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public LoginState Login(string name, string pwd, out string msg)
        {
            string sql = "select UserName,UserPwd from BarUser where UserName=@name";
            SqlParameter ps = new SqlParameter("@name", name);

            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text, ps);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["UserPwd"].ToString().Equals(Md5Helper.EncryptString(pwd)))
                {
                    msg = "登陆成功";
                    return LoginState.Ok;
                }
                else
                {
                    msg = "密码错误";
                    return LoginState.PwdError;
                }
            }
            else
            {
                msg = "用户不存在";
                return LoginState.NameError;
            }
        }
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetUserModel()
        {
            List<UserModel> list = new List<UserModel>();
            string sql = "select Id,UserName,UserPwd from BarUser where Flag=@flag";
            SqlParameter ps = new SqlParameter("@flag", 1);

            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text, ps);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    UserModel model = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        model = new UserModel();
                        model.Id = int.Parse(row["Id"].ToString());
                        model.UserName = row["UserName"].ToString();
                        model.UserPwd = row["UserPwd"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取PDA用户
        /// </summary>
        /// <returns></returns>
        public List<string> GetUser()
        {
            string sql = "select UserName from BarUser where Flag=1 and UserType=4";
            DataTable dt = SqlHelper.GetDataTable(sql, CommandType.Text);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    List<string> list = new List<string>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(row["UserName"].ToString());
                    }
                    return list;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public LoginState ChangePwd(string name, string pwd)
        {
            int userType = 4;
            string sql = String.Format("UPDATE BarUser SET UserPwd = '{0}' WHERE UserName='{1}' and UserType={2}", Md5Helper.EncryptString(pwd), name, userType);
            if (SqlHelper.ExecuteNonquery(sql, CommandType.Text) > 0)
            {
                return LoginState.Ok;
            }
            else
            {
                return LoginState.PwdError;
            }
        }



    }
}
