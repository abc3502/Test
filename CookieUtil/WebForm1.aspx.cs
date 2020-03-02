using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CookieUtil
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*   //cookie写入浏览器
               HttpCookie cookie = new HttpCookie("MyFirstCookie","cookieValue");
               Request.Cookies.Add(cookie);
               HttpContext.Current.Response.Cookies.Add(cookie);
               //读取每次响应中的cookie值
               HttpCookie cookie1 = Request.Cookies["MyFirstCookie"];
               if (cookie1 != null)
               {
                   Label1.Text = cookie1.Value;
               }
               else
               {
                   Label1.Text = "未定义";
               }
               //设置cookie过期
               //cookie1 = new HttpCookie("MyFirstCookie", "secondCookie");
               cookie.Expires = DateTime.Now.AddYears(-1);
               Response.Cookies.Add(cookie);*/
            /*  WriteCookie();
              ReadCookie();*/
            //数据转化为json,放入cookie传输
            WriteCookie1();
            ReadCookie1();
        }
        private void WriteCookie() {
            DisplaySeeting setting = new DisplaySeeting { Style=5,Size=24 };
            HttpCookie cookie = new HttpCookie("settings");
            cookie["Style"] = setting.Style.ToString();
            cookie["Size"] = setting.Size.ToString();
            Response.Cookies.Add(cookie);
        
        }
        private void ReadCookie() {
            DisplaySeeting setting = new DisplaySeeting();
            HttpCookie cookie = Request.Cookies["settings"];
            if (cookie==null)
            {
                Label1.Text = "未定义";
            }
            else
            {
                setting.Style =Convert.ToInt32( cookie["Style"]);
                setting.Size = Convert.ToInt32( cookie["Size"]);
                Label1.Text = setting.ToString();
            }
        }

        //将参数包含在json中，由cookie携带
        public void WriteCookie1() {
            DisplaySeeting setting = new DisplaySeeting() { Style = 5, Size = 36 };
            string json = HttpUtility.UrlEncode(setting.toJosn(), Encoding.Default);
            HttpCookie cookie = new HttpCookie("settings", json);
            Response.Cookies.Add(cookie);
        }
        public void ReadCookie1() {
            HttpCookie cookie = Request.Cookies["settings"];
            if (cookie == null)
            {
                Label1.Text = "未定义";
            }
            else
            {
                   string json = HttpUtility.UrlDecode(cookie.Value,Encoding.Default);
                 Label1.Text = json.ToObject<DisplaySeeting>().ToString();
            }
        }
    }
}