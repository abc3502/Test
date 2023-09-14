using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PartialClass
{
    class Program
    {
        private static string connStr = "Data Source=.;Initial Catalog=阳曲;uid=sa;password=123456";
        static void Main(string[] args)
        {
            //DataTable dt = GetDataTable();
            //if (dt !=null)
            //{
            //    if (dt.Rows.Count>0)
            //    {
            //        foreach (DataRow row in dt.Rows)
            //        {
            //            try
            //            {
            //                GetAddrFromStr(row["表地址"].ToString());
            //                Console.WriteLine(row["表地址"]);
            //            }
            //            catch (Exception ex)
            //            {

            //                Console.WriteLine(ex.Message+":"+row["表地址"]); ;
            //            }

            //        }
            //        Console.ReadKey();    
            //    }

            //}



            //GetStr getStr = new GetStr();
            //Console.WriteLine( getStr.getStr1()+getStr.getStr2());
            //List<Int32> list = new List<int>() { 1,2,3,99,55,3,28};
            //var query = from x in list select new { id=x,index=list.IndexOf(x) };
            //foreach (var item in query)
            //{
            //    Console.WriteLine("排序："+item.index);
            //}

            //Console.ReadKey();
            //List<String> str = new List<string> { "2021-08","2022-09","2022-08"};
            //Console.WriteLine(str.Min());
            //Console.WriteLine(str.Max());
            //Base64编码
            //String fileName = "F:\\wwkj\\营业收费\\中牟收费系统\\首页.png";
            //FileInfo fileInfo = new FileInfo(fileName);
            //FileStream fileStream= fileInfo.Open(FileMode.OpenOrCreate);
            //byte[] arr = new byte[fileInfo.Length];

            //fileStream.Read(arr,0,arr.Length);
            //String str = Convert.ToBase64String(arr);
            //Console.WriteLine(str);
            //Byte[] toArr = Convert.FromBase64String(str);
            //String path = "E:\\aaa.png";
            //FileStream stream = new FileStream(path,FileMode.OpenOrCreate);
            //stream.Write(toArr,0,toArr.Length);
            ////位图
            //Bitmap bitmap = new Bitmap("aaa");
            //bitmap.Save(path);
            //正则表达式
            //string except_chars = ": ‘ ！ @ # % … & * （  ^  &  ￥  ， 。 , .）$";
            //string src = "就是包含: 这些‘字符 包含空格）都要$去掉么？,,,,,";
            //string result = Regex.Replace(src, "[" + Regex.Escape(except_chars) + "]", "");
            //Console.WriteLine(result);
            //src = "123456789";
            //result= Regex.Replace(src, @"[^a-zA-Z0-9\u4e00-\u9fa5\s]", "");
            //Console.WriteLine(result);
            //
            //测试try...catch
            // int aaa= new Random().Next(100, 1000);
          //  Console.WriteLine(aaa);
            try
            {
                string a= "fadsf15";
                int b = Int32.Parse(a);
            }
            catch (Exception)
            {
                Console.WriteLine("数据转换出错");
            }
            Console.WriteLine("程序正常执行");
            Console.WriteLine("程序走完了");







        }

        private static  DataTable GetDataTable() {
            DataTable dt = null;
            DataSet dataSet = new DataSet();
            SqlConnection conn = new SqlConnection(connStr);
            string sql = " Select 表地址, 表通道, b.类型代码 From Bsc_MeterInfo a left join Bsc_MeterType b on a.MeterTypeID = b.MeterTypeID Where 设备ID =7";
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            adapter.Fill(dataSet);
            dt = dataSet.Tables[0];
            return dt;
        
            
        }
        private static byte[] GetAddrFromStr(string address)
        {
            address = address.PadLeft(14, '0');
            byte[] addr = new byte[7];
            for (int i = 0; i < address.Length / 2; i++)
            {
                addr[i] = Convert.ToByte(address.Substring(i * 2, 2), 16);
            }

            return addr;
        }
    }
    
    //public partial class GetStr {

    //    public string getStr1() {
    //        return "用一生下载你";
    //    }
    //}
    //public partial class GetStr1 {
        
    //    public string getStr2() {

    //        return "云烨香风";
    //    }
    //}
    //public static class GetMethod {
    //    /// <summary>
    //    /// 拓展方法
    //    /// </summary>
    //    /// <param name=""></param>
    //    /// <returns></returns>
    //    public static string getStr2(this GetStr getStr)
    //    {
    //        return "云烨香风";
    //    }
    //} 


   
}
