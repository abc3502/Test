using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.0.183:809/WebAPI/MeterReading/GetTasking"; //192.168.0.183:809/WebAPI/MeterReading/GetTasking
            string d = "1";
            string Results = PostString(url, d);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:56465/WebAPI/MeterReading/ReadTaskDetailById";
            string d = "20181224001";
            string Results = PostString(url, d);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = "http://www.fxzls.cn:902/WebAPI/MeterReading/BigMeterRealDataLoad";
            url = "http://121.18.11.178:1991/WebAPI/MeterReading/BigMeterRealDataLoad";
            url = "http://localhost:56465/WebAPI/MeterReading/UpoladTask";
            var data = new
            {
                StationId = "12345",
                Time = "2018-08-11 18:59",
                Value = 22,
                DataKindCode = "4",
                Unit = "M3",
                FactoryCode = "05"
            };
            string d = "";
            d = "[{\"warrant_no\":\"20181224001\",\"dev_no\":\"\",\"read_num\":\"22\",\"fault_state\":\"0\",\"fault_des\":\"\",\"bat_voltage\":\"3\"}]";
            string Results = PostString(url, d);
        }

        //请求数据
        public string PostString(string url, string send)
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
            catch (Exception ex)
            {
                result = "error:" + ex.Message;
                throw new Exception("JSONHelper.PostString(): " + ex.Message);
            }
            return result;
        }
    }
}
