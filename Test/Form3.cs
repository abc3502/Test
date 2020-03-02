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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            string url = txtURL.Text.Trim();
            string d = txtPara.Text.Trim();
            string Results = PostString(url, d);
            txtResult.Text = Results;
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
