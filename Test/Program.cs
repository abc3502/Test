using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            //String base_url = "http://8.142.68.93:8844/api/v1/token";
            //String xTimestamp =Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds).ToString();

            ////openApi客户端id
            //String xClientId = "6SFrjy858X6WQFKj"; 
            ////密钥
            //String secureKey = "hpThMbPf84DTbDMDyHb6ktw7";
            //String body = "{\"expires\":7200}"; 
            //MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            ////byte[] data = Encoding.UTF8.GetBytes(body);
            ////byte[] data1 = Encoding.UTF8.GetBytes(xTimestamp); 
            ////byte[] data2 = Encoding.UTF8.GetBytes(secureKey);
            ////byte[] buffer = new byte[data.Length+data1.Length+data2.Length];
            ////Array.Copy(data, 0,buffer,0,data.Length);
            ////Array.Copy(data1,0, buffer,data.Length, data1.Length);
            ////Array.Copy(data2, 0, buffer, data.Length+data1.Length, data2.Length);
            //byte[] buffer = Encoding.UTF8.GetBytes(body+ xTimestamp+ secureKey);

            //byte[] secret = md5Hasher.ComputeHash(buffer);
            //StringBuilder sBuilder = new StringBuilder();
            //for (int i = 0; i < secret.Length; i++)
            //{
            //    sBuilder.Append(secret[i].ToString("x2"));
            //}
            //WebHeaderCollection webHeader = new WebHeaderCollection();
            //webHeader.Add("X-Sign",sBuilder.ToString());
            //webHeader.Add("X-Client-Id", xClientId);
            //webHeader.Add("X-Timestamp", xTimestamp);
            //String result = WebsHelper.HttpWebRequestPlat(base_url,body,Encoding.GetEncoding("utf-8"),webHeader, "POST", "application/json");
            //Console.WriteLine(result);
            //  Test1();
            //   BytesToFloat();
            //  ObjectToStrings();
            string hexString = "524946460488010057415645666d7420100000000100010088130000102700000200100064617461E0870100";

            byte[] aaa= ToBytesFromHexString(hexString);
            //  GetRegex();
            //合并table
          //  mergeTable();

        }
        public static void Test1() {
            String a = "aaa";
            for (int i = 0; i < 100; i++)
            {
                switch (a)
                {
                    case "aaa":
                        continue;
                    default:
                        int cc = 35;
                        break;


                }
                int zzz = 25;
            }

        }
        public static byte[] GetAddrFromStr(string address)
        {
            Int32 length = Convert.ToInt32(Math.Ceiling((decimal)address.Length / 2));
            byte[] addr = new byte[length];
            byte[] buffer = new byte[7];
            for (int i = 0; i < length; i++)
            {
                addr[i] = Convert.ToByte(address.Substring(i * 2, 2), 16);
            }
            addr.CopyTo(buffer, 7 - addr.Length);
            return buffer;
        }
        private static byte[] GetAddrFromStr1(string address)
        {
            byte[] addr = new byte[7];
            for (int i = 0; i < address.Length / 2; i++)
            {
                addr[i] = Convert.ToByte(address.Substring(i * 2, 2), 16);
            }
            return addr;
        }
        public static DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("type");
            dt.Columns.Add("age");
            dt.Rows.Add(new object[] { "xiaoming", "1", "15" });
            dt.Rows.Add(new object[] { "xiaoming", "1", "9" });
            dt.Rows.Add(new object[] { "xiaohong", "2", "11" });
            dt.Rows.Add(new object[] { "xiaohong", "2", "12" });
            dt.Rows.Add(new object[] { "linyang", "3", "10" });
            return dt;
        }
        /// <summary>
        /// 字节数组转为float
        /// </summary>
        public static void BytesToFloat() {
            //  byte[] aaa = new byte[] { 1, 23, 25, 26 }; 
            //有无
            String str = "C1533333";
            byte[] bbb = new byte[str.Length / 2];
            for (int i = 0; i < str.Length; i += 2)
            {
                bbb[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            }
            int cc = 0;
            for (int i = 0; i < bbb.Length; i++)
            {
                cc = (cc << 8) + bbb[i];
            }
            // Int32 cc= BitConverter.ToInt32(bbb,0);
            Console.WriteLine(cc);
            uint num = uint.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier);
            Console.WriteLine(num);
            
            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);
            Console.WriteLine(f);
            float a = 10f;
            byte[] dd = BitConverter.GetBytes(a);
            // float f = BitConverter.ToSingle(aaa,0);
            f = BitConverter.ToSingle(dd, 0);
            Console.WriteLine(f);
            Console.ReadKey();

        }
        /// <summary>
        /// object To string[]
        /// </summary>
        public static void ObjectToStrings(){
            var obj = new { a = "333", b = "444" };
            Console.WriteLine(obj.ToString());
          
            string[] strArr = Array.ConvertAll(obj.GetType().GetProperties(), p => (string)p.GetValue(obj,null));
            string[] propArr = Array.ConvertAll(obj.GetType().GetProperties(), p => (string)p.Name);
            Console.ReadKey();


        }
        public static byte[] ToBytesFromHexString(String hexString)
        {
           

            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length / 2; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            string str = BitConverter.ToString(returnBytes);
            return returnBytes;
        }
        public static string GetRegex() {

            string input = "abc123def456";
            string result = Regex.Replace(input, "[^0-9]", "");
            string letters = Regex.Replace(input, "[a-zA-Z]+","");
            return result + "," + letters;

        }

        public static void mergeTable() {
            // 创建两个DataTable  
            DataTable table1 = new DataTable();
            table1.Columns.Add("ID", typeof(int));
            table1.Columns.Add("Name", typeof(string));
            table1.Rows.Add(1, "John");
            table1.Rows.Add(2, "Mary");

            DataTable table2 = new DataTable();
            table2.Columns.Add("ID", typeof(int));
            table2.Columns.Add("Name", typeof(string));
            table2.Rows.Add(3, "Peter");
            table2.Rows.Add(4, "Linda");

            // 创建一个DataSet并添加这两个DataTable  
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(table1);
            dataSet.Tables.Add(table2);

            // 使用Merge方法合并两个DataTable，并保留新数据和已存在的数据（默认行为）  
            dataSet.Merge(table2);

            // 打印合并后的DataTable  
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine(row["ID"] + ": " + row["Name"]);
                }
            }
        }


}


    
    class Student
    {
        public string Name { get; set; }
        public string sex { get; set; }
    }
}
