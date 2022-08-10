using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace customerSatis
{
    public partial class CSQ : System.Web.UI.Page
    {
        string code = string.Empty;
        string score = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //取得集合
            string query = Request.Url.Query;
            if (string.IsNullOrEmpty(query))
            {
                //測試Code 確認攜帶Query
                string testParam = "code=A123456789&score=1&";
                testParam = System.Web.HttpUtility.UrlEncode(testParam);
                testParam = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(testParam.Replace(" ", "+")));
                Response.Redirect("~/CSQ.aspx?" + testParam);
                error();
            }
            else
            {
                try
                {
                    code = System.Text.Encoding.Default.GetString(Convert.FromBase64String(query.Substring(1).Replace("+", " ")));
                    code = System.Web.HttpUtility.UrlDecode(code);
                    if (code.IndexOf("code") != -1)
                    {
                        string[] qureyArr = code.Split('&');
                        foreach (string value in qureyArr)
                        {
                            if (value.IndexOf("code") != -1)
                            {
                                hfcode.Value = System.Web.HttpUtility.UrlEncode(value);
                                hfcode.Value = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(value.Replace(" ", "+")));
                                code = value.Substring(value.IndexOf("code") + 8);
                            }
                            if (value.IndexOf("score") != -1)
                            {
                                score = value.Substring(value.IndexOf("score") + 6);
                                switch (score)
                                {
                                    case "1":
                                        scoreOne.Checked = true;
                                        break;
                                    case "2":
                                        scoreTwo.Checked = true;
                                        break;
                                    case "3":
                                        scoreThree.Checked = true;
                                        break;
                                    case "4":
                                        scoreFour.Checked = true;
                                        break;
                                    case "5":
                                        scoreFive.Checked = true;
                                        break;
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(code))
                        {
                            error();
                        }
                        else
                        {
                            //if (check(code, 1) == 0)
                            //{
                            //    error();
                            //}
                            //else
                            //{
                            //    if (check(code, 2) != 0)
                            //    {
                            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "closeCust", "alert('您已經填寫過滿意度調查囉!');window.close();", true);
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        error();
                    }
                }
                catch (Exception)
                {
                    error();
                    throw;
                }

            }
        }
        #region 連線資料庫
        //連線字串
        private string GetDBConnStr()
        {
            return System.Configuration.ConfigurationManager.AppSettings["SQLConnectionString"];
        }
        //確認資料筆數報修編號是否存在
        private int check(string ss_code,int sqlTableType)
        {
            int iCount = 0;
            string sSQL = string.Empty;
            string sErrmsg = string.Empty;
            string sqlTable = string.Empty;
            if (sqlTableType == 1)
            {
                //資料表類型
                sqlTable = string.Empty;
            }
            else if (sqlTableType == 2)
            {
                //資料表類型
                sqlTable = string.Empty;
            }
            else
            {
                error();
            }
            System.Data.SqlClient.SqlConnection ASConnection = null;
            System.Data.SqlClient.SqlCommand AScmd = null;
            try
            {
                ASConnection = new System.Data.SqlClient.SqlConnection(GetDBConnStr());
                ASConnection.Open();
                
                //SQL字串
                sSQL = string.Empty;

                AScmd = new System.Data.SqlClient.SqlCommand(sSQL, ASConnection);
                AScmd.Parameters.AddWithValue("code", ss_code);
                iCount = (int)AScmd.ExecuteScalar();
                return iCount;
            }
            catch (Exception errobj)
            {
                return iCount;
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
        }

      
        #endregion 
        private void error()
        {
            Context.Response.Clear();
            Context.Response.Status = "403 Forbidden";
            Context.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden; //403
            Context.Response.Write("<H1>403 Forbidden</H1>");
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
    }
}