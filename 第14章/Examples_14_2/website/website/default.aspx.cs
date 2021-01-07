using System;
using System.Web;

namespace website
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 为了方便测试和客户端取消网络操作，后台服务延迟2秒钟再执行相关的操作
            System.Threading.Thread.Sleep(2000);

            // 返回请求的内容
            Response.Write("客户端请求的数据内容：");
            // 获取Post请求传递过来的数据
            System.IO.Stream inputStream = Request.InputStream;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.InputStream))
            {
                string body = reader.ReadToEnd();
                Response.Write(body);
            }
            // Get请求设置请求缓存时间
            if (Request.QueryString["cacheable"] != null)
            {
                // 设置缓存时间
                Response.Expires = Int32.Parse(Request.QueryString["cacheable"]);
                Response.Write("Get请求，当前的服务器时间是：");
                Response.Write(DateTime.Now);
                Response.Write("请求缓存" + Response.Expires+"分钟");
            }
            // 获取请求的Cookies
            if (Request.Cookies.Count>0)
            {
                Response.Write("Cookies：");
                foreach (var cookie in Request.Cookies.AllKeys)
                {

                    Response.Write(cookie + ":" + Request.Cookies[cookie].Value);
                }
            }
            // 设置服务器相应请求的cookies
            if (Request.QueryString["setCookies"] != null)
            {
                // 创建一个持续3分钟的cookie
                HttpCookie myCookie1 = new HttpCookie("LastVisit");
                DateTime now = DateTime.Now;
                myCookie1.Value = now.ToString();
                myCookie1.Expires = now.AddMinutes(3);
                Response.Cookies.Add(myCookie1);
                // 创建一个http会话的cookie
                HttpCookie myCookie2 = new HttpCookie("SID");
                myCookie2.Value = "31d4d96e407aad42";
                myCookie2.HttpOnly = true;
                Response.Cookies.Add(myCookie2);
            }
            // 返回Stream数据
            if (Request.QueryString["extraData"] != null)
            {
                int streamLength = Int32.Parse(Request.QueryString["extraData"]);
                for (int i = 0; i < streamLength; i++)
                {
                    Response.Write("@");
                }
            }
        }
    }
}