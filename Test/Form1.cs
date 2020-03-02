using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace Test
{
    public partial class Form1 : Form
    {
        string m_Token = "";
        private delegate void SendMsgHandler(string keyValue, object entity);

        public Form1()
        {
            InitializeComponent();

            try
            {
                string s = "ADDRESS:1  NAME:00009702  FTOTAL:000001748M3  RTOTAL:000000010M3  FLOW:+002.67M3/h  SPEED:00.042m/s  TUB:00032%  ALARM:55000  HFLOW:+000.00GJ/h  HTOTAL:000000.000GJ  CTOTAL:000000.000GJ  PRESS:00.125MPa  CSQ:14  TIME:18-09-18 00:00:00  ";
                //string s = "ADDRESS:1 NAME:00007428 FTOTAL:000000055M3 RTOTAL:000000002M3 FLOW:+000.00M3/h SPEED:00.021m/s TUB:00001% ALARM:540000100 CSQ:16 TIME:2018-09-17 23:59:45 PRESS:00.056MPa";
                string tmp = "";
                tmp = s.Substring(s.IndexOf("NAME:"));
                string a = tmp.Substring(5, tmp.IndexOf(" ") - 5).PadLeft(10, '0');

                tmp = s.Substring(s.IndexOf("TIME:"));
                tmp = tmp.Substring(5, 19).Replace(',', ' ');
                DateTime Tm = DateTime.Parse(DateTime.Parse(tmp).ToString("yyyy-MM-dd HH:mm")).AddMinutes(1);

                tmp = s.Substring(s.IndexOf("FTOTAL:"));
                tmp = tmp.Substring(7, tmp.IndexOf(" ") - 7).Replace("M3", "").Replace("m3", "");
                double P01 = double.Parse(tmp);

                tmp = s.Substring(s.IndexOf("RTOTAL:"));
                tmp = tmp.Substring(7, tmp.IndexOf(" ") - 7).Replace("M3", "").Replace("m3", "");
                double P02 = double.Parse(tmp);

                tmp = s.Substring(s.IndexOf("FLOW:"));
                tmp = tmp.Substring(5, tmp.IndexOf(" ") - 5).Replace("M3/h", "").Replace("M3/H", "").Replace("m3/h", "").Replace("m3/H", "");
                double A01 = double.Parse(tmp);

                //tmp = s.Substring(s.IndexOf("A02:"));
                //tmp = tmp.Substring(4, tmp.IndexOf(";") - 4);
                //data.A02 = double.Parse(tmp);

                tmp = s.Substring(s.IndexOf("PRESS:"));
                tmp = tmp.Substring(6, tmp.IndexOf(" ") > 0 ? (tmp.IndexOf(" ") - 6) : tmp.Length-6).Replace("MPa", "").Replace("KPa", "").Replace('\0', ' ').Trim();
                double A03 = double.Parse(tmp);
            }
            catch (Exception e)
            {
            }
        }

        public static string PostString(string url, string send)
        {
            string result = "";
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                string postData = send;

                byte[] data = encoding.GetBytes(postData);

                HttpWebRequest myRequest =
                (HttpWebRequest)WebRequest.Create(url);
                // myRequest.KeepAlive = false;
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                //System.GC.Collect();
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
                result = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
                int status = (int)myResponse.StatusCode;
                reader.Close();
            }
            catch (Exception ep)
            {
                result = "error:" + ep.Message;
            }
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string url = "http://223.100.201.186:82/DataAdapter.asmx/Login";
            string Results = PostString(url, "{ param: {LoginName: 'admin', Password: 'Fxhd12345'} }");
            
            JObject json1 = (JObject)JsonConvert.DeserializeObject(Results);
            string dd = json1["d"].ToString();

            m_Token = dd.Substring(dd.IndexOf("Token") + "Token".Length + 5, dd.IndexOf("UserId") - dd.IndexOf("Token") - "Token".Length - 10);

            this.label1.Text = m_Token;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://223.100.201.186:82/DataAdapter.asmx/GetUsers";
            string Results = PostString(url, "{ token: '" + m_Token + "' }");
            this.label1.Text = Results;

            try
            {
                int IndexofA = Results.IndexOf("[");
                int IndexofB = Results.IndexOf("]");
                string Ru = Results.Substring(IndexofA, IndexofB - IndexofA + 1).Replace("\\\"", "'");

                //Ru = "[{\"UserId\":1,\"Name\":\"admin\",\"LoginName\":\"admin\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":2,\"Name\":\"海州营业公司\",\"LoginName\":\"海州营业公司\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":3,\"Name\":\"太平营业公司\",\"LoginName\":\"太平营业公司\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":4,\"Name\":\"新邱分公司\",\"LoginName\":\"新邱分公司\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":5,\"Name\":\"清河门营业公司\",\"LoginName\":\"清河门营业公司\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":6,\"Name\":\"和达电子\",\"LoginName\":\"hddz\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":7,\"Name\":\"计量考核办\",\"LoginName\":\"计量考核办\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":8,\"Name\":\"营业处\",\"LoginName\":\"营业处\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":9,\"Name\":\"大用户室\",\"LoginName\":\"大用户室\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":10,\"Name\":\"002\",\"LoginName\":\"002\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":11,\"Name\":\"003 \",\"LoginName\":\"003 \",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":12,\"Name\":\"004\",\"LoginName\":\"004\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":13,\"Name\":\"001\",\"LoginName\":\"001\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0},{\"UserId\":14,\"Name\":\"水质处\",\"LoginName\":\"水质处\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0}]";

                JavaScriptSerializer Serializer = new JavaScriptSerializer();
                List<Test> objs = Serializer.Deserialize<List<Test>>(Ru);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://223.100.201.186:82/DataAdapter.asmx/GetRealTimeData";
            string send = "{ token: '" + m_Token + "',param:{DivisionIds:'',StationIds:'',SensorIds:'',DatakindIds:'',Timewindow:''} }";
            var data = new
            {
                token = m_Token,
                param = new
                {
                    DivisionIds = "",
                    StationIds = "",
                    SensorIds = "",
                    DatakindIds = "",
                    Timewindow = ""
                }
            };
            string d = ObjectToJSON(data);
            string Results = PostString(url, d);

            try
            {
                int IndexofA = Results.IndexOf("[");
                int IndexofB = Results.IndexOf("]");
                string Ru = Results.Substring(IndexofA, IndexofB - IndexofA + 1).Replace("\\\"", "'");
                
                JavaScriptSerializer Serializer = new JavaScriptSerializer();
                List<RealTimeData> objs = Serializer.Deserialize<List<RealTimeData>>(Ru);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

         public static string ObjectToJSON(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Serialize(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string a = "1";
            SendMsgHandler handler = new SendMsgHandler(SendMsg);
            handler.BeginInvoke(a, null, null, null);
            string b = "";
        }

        private void SendMsg(string keyValue, object entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                System.Threading.Thread.Sleep(60 * 1000);
            }
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            txtAn.Text = fun(int.Parse(txtN.Text.Trim())).ToString();
        }
        private int fun(int n)
        {
            if (n == 0) return int.Parse(txtA0.Text.Trim());
            if (n == 1) return int.Parse(txtA1.Text.Trim());
            return fun(n - 1) + fun(n - 2);
        }
    }

    /// <summary>     /// 对象转JSON     /// </summary>     /// <param name="obj">对象</param>     /// <returns>JSON格式的字符串</returns>     publicstaticstringObjectToJSON(object obj)    {        JavaScriptSerializer jss =newJavaScriptSerializer();        try        {            return jss.Serialize(obj);        }        catch(Exception ex)        {            thrownewException("JSONHelper.ObjectToJSON(): "+ ex.Message);        }    }
    /// <summary>
    /// [{\"UserId\":1,\"Name\":\"admin\",\"LoginName\":\"admin\",\"Email\":\"\",\"Phone\":\"\",\"DepartmentId\":0}]
    /// </summary>
    public class Test
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
    }

    //DivisionId：直属分区Id；DivisionName:直属分区名；StationId：站点Id；StationName：站点名称；SensorId：传感器Id；SensorName：传感器名称；DatakindId：数据类型Id；Time：最后一次的时间；Value：最后一次的数值；Min：最小值及时间；Max：最大值及时间		
    //{\"DivisionId\":12,\"DivisionName\":\"海州营业公司\",\"StationId\":4,\"StationName\":\"双跃康大花苑180#(进)\",\"SensorId\":8,\"SensorName\":\"正向累计\",
    //\"DataType\":3,\"Time\":\"2017-02-16 16:00:00\",\"Value\":\"2248355\",\"Min\":{\"Time\":null,\"Value\":null},\"Max\":{\"Time\":null,\"Value\":null},\"State\":0,\"DataKindName\":\"正向累计\",\"DataKindCode\":\"ZXLJ\",\"Unit\":\"T\"}
    public class RealTimeData
    {
        //直属分区Id
        public int DivisionId { get; set; }
        //直属分区名
        public string DivisionName { get; set; }
        //站点Id
        public string StationId { get; set; }
        //站点名称
        public string StationName { get; set; }
        //传感器Id
        public string SensorId { get; set; }
        //传感器名称
        public string SensorName { get; set; }
        //数据类型Id
        public int DataType { get; set; }
        //最后一次的时间
        public string Time { get; set; }
        //最后一次的数值
        public string Value { get; set; }
        //最小值及时间
        public object Min { get; set; }
        //最大值及时间
        public object Max { get; set; }
        public string State { get; set; }
        public string DataKindName { get; set; }
        public string DataKindCode { get; set; }
        public string Unit { get; set; }
    }
}
