using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Test
{
    public static class WebsHelper
    {
        #region Host(获取主机名)

        /// <summary>
        /// 获取主机名,即域名，
        /// 范例：用户输入网址http://www.a.com/b.htm?a=1&amp;b=2，
        /// 返回值为: www.a.com
        /// </summary>
        public static string Host
        {
            get
            {
                return HttpContext.Current.Request.Url.Host;
            }
        }
        #endregion

        #region ResolveUrl(解析相对Url)

        /// <summary>
        /// 解析相对Url
        /// </summary>
        /// <param name="relativeUrl">相对Url</param>
        public static string ResolveUrl(string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(relativeUrl))
                return string.Empty;
            relativeUrl = relativeUrl.Replace("\\", "/");
            if (relativeUrl.StartsWith("/"))
                return relativeUrl;
            if (relativeUrl.Contains("://"))
                return relativeUrl;
            return VirtualPathUtility.ToAbsolute(relativeUrl);
        }

        #endregion

        #region HtmlEncode(对html字符串进行编码)

        /// <summary>
        /// 对html字符串进行编码
        /// </summary>
        /// <param name="html">html字符串</param>
        public static string HtmlEncode(string html)
        {
            return HttpUtility.HtmlEncode(html);
        }
        /// <summary>
        /// 对html字符串进行解码
        /// </summary>
        /// <param name="html">html字符串</param>
        public static string HtmlDecode(string html)
        {
            return HttpUtility.HtmlDecode(html);
        }

        #endregion

        #region UrlEncode(对Url进行编码)

        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode(string url, bool isUpper = false)
        {
            return UrlEncode(url, Encoding.UTF8, isUpper);
        }

        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode(string url, Encoding encoding, bool isUpper = false)
        {
            var result = HttpUtility.UrlEncode(url, encoding);
            if (!isUpper)
                return result;
            return GetUpperEncode(result);
        }

        /// <summary>
        /// 获取大写编码字符串
        /// </summary>
        private static string GetUpperEncode(string encode)
        {
            var result = new StringBuilder();
            int index = int.MinValue;
            for (int i = 0; i < encode.Length; i++)
            {
                string character = encode[i].ToString();
                if (character == "%")
                    index = i;
                if (i - index == 1 || i - index == 2)
                    character = character.ToUpper();
                result.Append(character);
            }
            return result.ToString();
        }

        #endregion

        #region UrlDecode(对Url进行解码)

        /// <summary>
        /// 对Url进行解码,对于javascript的encodeURIComponent函数编码参数,应使用utf-8字符编码来解码
        /// </summary>
        /// <param name="url">url</param>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        /// <summary>
        /// 对Url进行解码,对于javascript的encodeURIComponent函数编码参数,应使用utf-8字符编码来解码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码,对于javascript的encodeURIComponent函数编码参数,应使用utf-8字符编码来解码</param>
        public static string UrlDecode(string url, Encoding encoding)
        {
            return HttpUtility.UrlDecode(url, encoding);
        }

        #endregion





        #region GetFileControls(获取客户端文件控件集合)

        /// <summary>
        /// 获取有效客户端文件控件集合,文件控件必须上传了内容，为空将被忽略,
        /// 注意:Form标记必须加入属性 enctype="multipart/form-data",服务器端才能获取客户端file控件.
        /// </summary>
        public static List<HttpPostedFile> GetFileControls()
        {
            var result = new List<HttpPostedFile>();
            var files = HttpContext.Current.Request.Files;
            if (files.Count == 0)
                return result;
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file.ContentLength == 0)
                    continue;
                result.Add(files[i]);
            }
            return result;
        }

        #endregion

        #region GetFileControl(获取第一个有效客户端文件控件)

        /// <summary>
        /// 获取第一个有效客户端文件控件,文件控件必须上传了内容，为空将被忽略,
        /// 注意:Form标记必须加入属性 enctype="multipart/form-data",服务器端才能获取客户端file控件.
        /// </summary>
        public static HttpPostedFile GetFileControl()
        {
            var files = GetFileControls();
            if (files == null || files.Count == 0)
                return null;
            return files[0];
        }

        #endregion

        #region HttpWebRequest(请求网络资源)

        /// <summary>
        /// 请求网络资源,返回响应的文本
        /// </summary>
        /// <param name="url">网络资源地址</param>
        public static string HttpWebRequest(string url)
        {
            return HttpWebRequest(url, string.Empty, Encoding.GetEncoding("utf-8"));
        }

        /// <summary>
        /// 请求网络资源,返回响应的文本
        /// </summary>
        /// <param name="url">网络资源Url地址</param>
        /// <param name="parameters">提交的参数,格式：参数1=参数值1&amp;参数2=参数值2</param>
        public static string HttpWebRequest(string url, string parameters)
        {
            return HttpWebRequest(url, parameters, Encoding.GetEncoding("utf-8"), "POST");
        }

        /// <summary>
        /// 请求网络资源,返回响应的文本
        /// </summary>
        /// <param name="url">网络资源Url地址</param>
        /// <param name="parameters"></param>
        public static string HttpWebRequest(string url, string parameters, string contentType, string Authorization, string app_key)
        {
            return HttpWebRequest(url, parameters, Encoding.GetEncoding("utf-8"), "POST", contentType, Authorization, app_key);
        }

        /// <summary>
        /// 请求网络资源,返回响应的文本
        /// </summary>
        /// <param name="url">网络资源Url地址</param>
        /// <param name="parameters"></param>
        public static string HttpWebRequest(string url, string parameters, string mehtod, string contentType, string Authorization, string app_key)
        {
            return HttpWebRequest(url, parameters, Encoding.GetEncoding("utf-8"), mehtod, contentType, Authorization, app_key);
        }

        /// <summary>
        /// 请求网络资源,返回响应的文本
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">提交的参数,格式：参数1=参数值1&amp;参数2=参数值2</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isPost">是否Post提交</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="cookie">Cookie容器</param>
        /// <param name="timeout">超时时间</param>
        public static string HttpWebRequest(string url, string parameters, Encoding encoding, string mehtod = "POST",
             string contentType = "application/x-www-form-urlencoded", string Authorization = null, string app_key = null, CookieContainer cookie = null, int timeout = 120000)
        {
            HttpWebRequest request = null;
            try
            {
                //如果是发送HTTPS请求  
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls;
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    //X509Certificate cerCaiShang = new X509Certificate(System.Web.HttpContext.Current.Server.MapPath(Config.GetValue("NB_PfxPath")), Config.GetValue("NB_PfxKey"));
                    //X509Certificate2 cerCaiShang = GetSentosaCertificate();
                    X509Certificate2 cerCaiShang = new X509Certificate2();
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ClientCertificates.Add(cerCaiShang);
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }

                request.Timeout = timeout;
                if (!string.IsNullOrEmpty(Authorization))
                {
                    request.Headers["Authorization"] = Authorization;
                }
                if (!string.IsNullOrEmpty(app_key))
                {
                    request.Headers["app_key"] = app_key;
                }
                request.CookieContainer = cookie;
                request.ContentType = contentType;
                request.Method = mehtod;
                if (mehtod == "POST")
                {
                    byte[] postData = encoding.GetBytes(parameters);
                    request.Method = "POST";
                    request.ContentType = contentType;
                    request.ContentLength = postData.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(postData, 0, postData.Length);
                    }
                }
                if (mehtod == "PUT")
                {
                    using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
                    {
                        requestStream.Write(parameters);
                    }
                }

                var response = (HttpWebResponse)request.GetResponse();
                string result;
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream == null)
                        return string.Empty;
                    using (var reader = new StreamReader(stream, encoding))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string HttpWebRequestPlat(string url, string parameters, Encoding encoding, WebHeaderCollection webHeader = null, string mehtod = "POST",
           string contentType = "application/x-www-form-urlencoded",  CookieContainer cookie = null, int timeout = 120000)
        {
            HttpWebRequest request = null;
            try
            {
                //如果是发送HTTPS请求  
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls;
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    //X509Certificate cerCaiShang = new X509Certificate(System.Web.HttpContext.Current.Server.MapPath(Config.GetValue("NB_PfxPath")), Config.GetValue("NB_PfxKey"));
                    //X509Certificate2 cerCaiShang = GetSentosaCertificate();
                    X509Certificate2 cerCaiShang = new X509Certificate2();
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ClientCertificates.Add(cerCaiShang);
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Timeout = timeout;
                if (webHeader !=null )
                {
                    request.Headers = webHeader;
                }
                request.CookieContainer = cookie;
                request.ContentType = contentType;
                request.Method = mehtod;
                if (mehtod == "POST")
                {
                    byte[] postData = encoding.GetBytes(parameters);
                    request.Method = "POST";
                    request.ContentType = contentType;
                    request.ContentLength = postData.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(postData, 0, postData.Length);
                    }
                }
                if (mehtod == "PUT")
                {
                    using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
                    {
                        requestStream.Write(parameters);
                    }
                }

                var response = (HttpWebResponse)request.GetResponse();
                string result;
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream == null)
                        return string.Empty;
                    using (var reader = new StreamReader(stream, encoding))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        private static X509Certificate2 GetSentosaCertificate()
        {
            X509Store userCaStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                userCaStore.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificatesInStore = userCaStore.Certificates;
                X509Certificate2Collection findResult = certificatesInStore.Find(X509FindType.FindBySubjectName, "server", true);
                X509Certificate2 clientCertificate = null;
                if (findResult.Count == 1)
                {
                    clientCertificate = findResult[0];
                }
                else
                {
                    throw new Exception("Unable to locate the correct client certificate.");
                }
                return clientCertificate;
            }
            catch
            {
                throw;
            }
            finally
            {
                userCaStore.Close();
            }
        }
        #endregion

        #region 去除HTML标记
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="NoHTML">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHtml(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&hellip;", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&mdash;", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;

        }
        #endregion

        #region 格式化文本（防止SQL注入）
        /// <summary>
        /// 格式化文本（防止SQL注入）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Formatstr(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex10 = new System.Text.RegularExpressions.Regex(@"select", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex11 = new System.Text.RegularExpressions.Regex(@"update", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex12 = new System.Text.RegularExpressions.Regex(@"delete", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex10.Replace(html, "s_elect");
            html = regex11.Replace(html, "u_pudate");
            html = regex12.Replace(html, "d_elete");
            html = html.Replace("'", "’");
            html = html.Replace("&nbsp;", " ");
            return html;
        }
        #endregion

        public static string HttpWebRequestByFormData(string url, List<DataItem> list, string mehtod = "POST", string Authorization = null, string app_key = null, CookieContainer cookie = null, int timeout = 120000)
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            string boundary = "----WebKitFormBoundary" + DateTime.Now.Ticks.ToString("x");
            string contentType = "multipart/form-data; boundary ="+ boundary;
            HttpWebRequest request = null;
            try
            {

                request = WebRequest.Create(url) as HttpWebRequest;


                request.Timeout = timeout;
                if (!string.IsNullOrEmpty(Authorization))
                {
                    request.Headers["Authorization"] = Authorization;
                }
                if (!string.IsNullOrEmpty(app_key))
                {
                    request.Headers["app_key"] = app_key;
                }
                request.CookieContainer = cookie;
                request.ContentType = "";
                request.Method = mehtod;
                if (mehtod == "POST")
                {
                    var postStream = new MemoryStream();
                    request.Method = "POST";
                    request.ContentType = contentType;
                    setFormData(postStream, list, boundary);
                    //string hahah = setFormData( list, boundary);
                    //byte[] strData = Encoding.UTF8.GetBytes(hahah);
                    //postData.Write(strData, 0, strData.Length);
                    request.ContentLength = postStream.Length;
                    //Stream stream = request.GetRequestStream();
                    #region 输入二进制流

                    if (postStream != null)
                    {
                        postStream.Position = 0;
                        //直接写入流
                        Stream requestStream = request.GetRequestStream();

                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                        }

                        postStream.Close();//关闭文件访问
                    }

                    #endregion

                  
                }
                

                var response = (HttpWebResponse)request.GetResponse();
                string result;
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream == null)
                        return string.Empty;
                    using (var reader = new StreamReader(stream, encoding))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void setFormData(Stream st,List<DataItem> list,string boundary)
        {
            StringBuilder sb = new StringBuilder(); 
            string fileFormData =
                "\r\n--" + boundary + "--" +
                "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                "\r\nContent-Type: application/octet-stream" +
                "\r\n\r\n";
            string dataFormData =
                "\r\n--" + boundary + "--" +
                "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                "\r\n\r\n{1}";
            foreach (DataItem item in list)
            {
                string formdata = null;
                if (item.type == "text")
                {
                    formdata = string.Format(dataFormData, item.key, item.name);
                }
                else
                {
                    formdata = string.Format(dataFormData, item.key, item.name);
                }
                byte[] formDataBytes = null;
                if (st.Length == 0)
                {
                    formDataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                }
                else
                {
                    formDataBytes = Encoding.UTF8.GetBytes(formdata);
                }
                st.Write(formDataBytes, 0, formDataBytes.Length);

                if (item.fileContent != null && item.fileContent.Length > 0)
                {
                    using (var stream = item.fileContent)
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            st.Write(buffer, 0, bytesRead);
                        }

                    }  
                }
            }
            byte[] footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            st.Write(footer, 0, footer.Length);
        }



        public static string setFormData(List<DataItem> list, string boundary)
        {
            int num = 0;
            StringBuilder sb = new StringBuilder();
            string fileFormData =
                "\r\n--" + boundary + "--" +
                "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                //"\r\nContent-Type: application/octet-stream" +
                "\r\n\r\n";
            string dataFormData =
                "\r\n--" + boundary + "--" +
                "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                "\r\n\r\n{1}";
            foreach (DataItem item in list)
            {
                string formdata = null;
                if (item.type == "text")
                {
                    formdata = string.Format(dataFormData, item.key, item.name);
                }
                else
                {
                    formdata = string.Format(dataFormData, item.key, item.name);
                }
                byte[] formDataBytes = null;
                if (num == 0)
                {
                    formDataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
                    
                }
                else
                {
                    formDataBytes = Encoding.UTF8.GetBytes(formdata);
                }
                sb.Append(formdata);
                //st.Write(formDataBytes, 0, formDataBytes.Length);

                if (item.fileContent != null && item.fileContent.Length > 0)
                {
                    sb.Append(item.fileC);
                    //st.Write(item.fileContent, 0, item.fileContent.Length);
                }
                num++;
            }
            //byte[] footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            sb.Append("\r\n--" + boundary + "--\r\n");
            //st.Write(footer, 0, footer.Length);
            return sb.ToString();
        }
    }
    public class DataItem
    {
        public string type { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public Stream fileContent { get; set; }
        public string fileC { get; set; }
    }
}
