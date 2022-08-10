using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace customerSatis
{
    /// <summary>
    /// saveSQl 的摘要描述
    /// </summary>
    public class saveSQl : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string resultcodeimg = string.Empty;
            var reader = new System.IO.StreamReader(context.Request.InputStream);
            var result = reader.ReadToEnd();
            System.Collections.Specialized.NameValueCollection NVCQueryString = HttpUtility.ParseQueryString(result);
            //接收傳遞內容
            string resultStr = string.Empty;
            string code = "";
            string scoreStr = string.Empty;
            int score = 0;
            string CustSay = string.Empty;
            if (NVCQueryString.AllKeys.Contains("code"))
            {
                code = NVCQueryString["code"];
                code = System.Text.Encoding.Default.GetString(Convert.FromBase64String(code.Replace("+", " ")));
                code = System.Web.HttpUtility.UrlDecode(code);
                code = code.Substring(code.IndexOf("code") + 8);
            }
            if (NVCQueryString.AllKeys.Contains("score"))
            {
                scoreStr = NVCQueryString["score"];
                bool isInt = int.TryParse(scoreStr, out score);
            }
            if (NVCQueryString.AllKeys.Contains("CustSay"))
            {
                CustSay = NVCQueryString["CustSay"];
            }
            resultStr = execute(code, score, CustSay);
        }
        #region 連線資料庫
        //連線字串
        public string GetDBConnStr()
        {
            return System.Configuration.ConfigurationManager.AppSettings["SQLConnectionString"];
        }

        public string execute(string code, int score, string CustSay)
        {
            int resultcount = 0;
            string sSQL = string.Empty;
            string sErrmsg = string.Empty;
            System.Data.SqlClient.SqlConnection ASConnection = null;
            System.Data.SqlClient.SqlCommand AScmd = null;
            try
            {
                ASConnection = new System.Data.SqlClient.SqlConnection(GetDBConnStr());
                ASConnection.Open();

                //SQL字串
                sSQL = String.Empty;


                AScmd = new System.Data.SqlClient.SqlCommand(sSQL, ASConnection);
                AScmd.Parameters.AddWithValue("code", code);
                AScmd.Parameters.AddWithValue("score", score);
                AScmd.Parameters.AddWithValue("CustSay", CustSay);

                resultcount = AScmd.ExecuteNonQuery();
            }
            catch (Exception errobj)
            {
                sErrmsg = "滿意填寫調查失敗";
            }
            finally
            {
                if (ASConnection != null)
                {
                    ASConnection.Close();
                    ASConnection.Dispose();
                    ASConnection = null;
                }
                if (AScmd != null)
                {
                    AScmd = null;
                }
            }
            return sErrmsg;

        }
        #endregion 
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}