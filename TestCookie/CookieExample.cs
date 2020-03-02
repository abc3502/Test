using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TestCookie
{
   public  class CookieExample
    {
        public void GetCookiePro(string url) {
            var request = (HttpWebRequest)WebRequest.Create(url);
            
            request.CookieContainer = new CookieContainer();
            using (var response=(HttpWebResponse) request.GetResponse())
            {
                Console.WriteLine(response.Cookies.Count);
                foreach (Cookie cookie in response.Cookies)
                {
                    
                    Console.WriteLine("Cookie:");
                    Console.WriteLine($"{cookie.Name}={cookie.Value}");
                    Console.WriteLine($"Domain:{cookie.Domain}");
                    Console.WriteLine($"Path:{cookie.Path}");
                    Console.WriteLine($"Port:{cookie.Port}");
                    Console.WriteLine($"Secure:{cookie.Secure}");
                    Console.WriteLine($"When issued: {cookie.TimeStamp}");
                    Console.WriteLine($"Expires: {cookie.Expires} (expired? {cookie.Expired})");
                    Console.WriteLine($"Don't save: {cookie.Discard}");
                    Console.WriteLine($"Comment: {cookie.Comment}");
                    Console.WriteLine($"Uri for comments: {cookie.CommentUri}");
                    Console.WriteLine($"Version: RFC {(cookie.Version == 1 ? 2109 : 2965)}");

                    // Show the string representation of the cookie.
                    Console.WriteLine($"String: {cookie}");

                }
            }
        }
        
    }
}
