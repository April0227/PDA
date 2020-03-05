using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Sockets;
using SealedEmulate_PDA.Model;

namespace SealedEmulate_PDA.Dal
{
    /// <summary>
    /// ����api������
    /// </summary>
    public class HttpHelper
    {
        private static string api = ConnModel.Api;//  "http://192.168.1.104:20555/api/";
        /// <summary>
        /// ����api
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="data">����</param>
        /// <param name="type">��������</param>
        /// <returns></returns>
        public static string HttpPost(string url, string data)
        {
            try
            {
                string msg = TcpClient();
                if (msg == "OK")
                {

                    Encoding encoding = Encoding.GetEncoding("utf-8");
                    string res = string.Empty;
                    //����
                    WebRequest webRequest = null;
                    Stream postStream = null;

                    //��Ӧ
                    WebResponse webResponse = null;
                    StreamReader streamReader = null;

                    //����
                    string currUrl = api + url;
                    webRequest = WebRequest.Create(currUrl);
                    webRequest.Proxy = new WebProxy();
                    webRequest.Method = "Post";
                    webRequest.Timeout = 5000;
                    webRequest.ContentType = "application/json";

                    //��������д����
                    byte[] postData = encoding.GetBytes(data);
                    webRequest.ContentLength = postData.Length;
                    postStream = webRequest.GetRequestStream();
                    postStream.Write(postData, 0, postData.Length);

                    //�������Ĺؼ�
                    postStream.Flush();
                    postStream.Close();
                    postStream = null;

                    //��Ӧ
                    webResponse = webRequest.GetResponse();//1���˴���winceƽ̨�ϱ�����д����������ǰ�����ܼ������������Ӧ��
                    streamReader = new StreamReader(webResponse.GetResponseStream(), encoding);
                    res = streamReader.ReadToEnd();
                    string jsonRes = "[" + res + "]";
                    JArray array = (JArray)JsonConvert.DeserializeObject(jsonRes);
                    return array[0]["ResultCode"].ToString() + ':' + array[0]["Msg"].ToString();
                }
                else
                {
                    return "-1:�ͻ�������ʧ��," + msg;
                }
            }
            catch (Exception ex)
            {
                return "-1:�����쳣" + ex.Message;
            }

        }
        /// <summary> 
        /// GET�������ȡ��� 
        /// </summary> 
        public static string HttpGet(string Url, string postDataStr)
        {
            string currUrl = api + Url + (postDataStr == "" ? "" : "?") + postDataStr;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(currUrl);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        public static string TcpClient()
        {
            TcpClient client = new TcpClient();
            IPAddress ipaddress = IPAddress.Parse("192.168.1.104");//IP�Ͷ˿�
            IPEndPoint endpoint = new IPEndPoint(ipaddress, 20555);

            try
            {
                client.Connect(endpoint);
                client.Close();
                return "OK";
            }
            catch (Exception)
            {
                return "�����ѳ�ʱ��";
            }
        }
    }
}
