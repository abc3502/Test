using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            //string a = "abcd";
            //Console.WriteLine(a.PadLeft(10,'0'));
            //Console.ReadKey();
            //测试四舍五入
            //float a =(float) Math.Round(0.555,0);
            //Console.WriteLine(a); 
            //Console.ReadKey();
            //string a = DateTime.Now.ToString("yyyyMMdd");
            //string b = string.Format("{0:yyyy-MM-dd}",null);
            //Console.WriteLine();
            //Console.ReadLine();
            //string a = "b+大|小";
            //a = a.Substring(a.LastIndexOf("+")+1);
            //Console.WriteLine(a);

            //Console.WriteLine();
            //decimal c = Convert.ToDecimal("3.256") + Convert.ToDecimal("-"+"3.269");
            //string d = Convert.ToDateTime("2018-05-03").ToString("yyyy-MM-dd HH:mm:ss");
            // DataTable dt = new DataTable();
            // dt.Columns.Add("标识",typeof(System.String));
            // dt.Columns.Add("用量",typeof(System.String));
            // DataColumn column = new DataColumn("求和",typeof(System.Int32));
            // dt.Columns.Add(column);
            // dt.Rows.Add(new object[]{1,10});
            // dt.Rows.Add(new object[]{2,11});
            // column.Expression = "Convert(用量,'System.Int32')";
            //// object total = dt.Compute("Sum(Convert(Child.用量,'System.Int32'))","");
            // object total = dt.Compute("Sum(用量)","");
            //  object num = dt.Rows[0]["求和"];
            //16进制转换
            //int a = 30;
            //Console.WriteLine(a.ToString("X0"));
            //正则表达式
            //    string regex = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";
            //string a = "2013//02//01";
            //string txt = "2019-08-11  15:03";

            //string re1 = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";	// YYYYMMDD 1
            //string re2 = "(\\s+)";	// White Space 1
            //string re3 = "((?:(?:[0-1][0-9])|(?:[2][0-3])|(?:[0-9])):(?:[0-5][0-9])(?::[0-5][0-9])?(?:\\s?(?:am|AM|pm|PM))?)";	// HourMinuteSec 1
            // Console.WriteLine(Regex.IsMatch(txt, re1 + re2 + re3));
            //16进制转换2进制
            // 00
            // int x = 0x01;
            // string a = Convert.ToString(x, 2).PadLeft(8,'0').Substring(6,2);
            //// byte a = Convert.ToByte("0x01", 16);
            // Console.WriteLine(a);
            //测试Linq中的select
            //List<int> list = new List<int>();
            //list.Add(1);
            //list.Add(2);
            //list.Add(3);
            //list.Add(4);
            //list.Add(5);

            //var result = from x in list
            //             select new Student()
            //             {
            //                 Name=x.ToString(),
            //                 sex=x.ToString()

            //             };
            //foreach (var item in result)
            //{
            //    Console.WriteLine(item.Name+item.sex);
            //}
            //  Console.WriteLine(result);
            //
            //var json = "{\"data\":[{\"a\":3,\"b\":4,\"c\":5},{\"a\":1,\"b\":10,\"c\":11},{\"a\":2,\"b\":8,\"c\":12}]}";
            //JObject obj = JObject.Parse(json);
            //var query = from str in obj["data"].Children() orderby str["a"] ascending select str ;
            //foreach (var item in query)
            //{
            // //   Console.WriteLine(item);
            //}
            //Console.WriteLine(JsonConvert.SerializeObject(query));
            //Console.ReadKey();
            /*  TestDictionary dic = new TestDictionary();
              dic.addSpace();
              Console.ReadKey();*/
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(System.Int32));
            dt.Columns.Add("name", typeof(System.String));
            dt.Rows.Add(new object[] { 1, "lubert" });
            dt.Rows.Add(new object[] { 2, "xiaoming" });
            //  string json = ToJson(dt);
            //   Console.WriteLine(json);
            // DataTable   dt1 = JsonConvert.DeserializeObject<DataTable>(json);
            //    Console.WriteLine(dt1.GetType());
            //    Console.ReadKey();
            //}
            //public static string ToJson(object obj) {
            //    if (obj==null)
            //    {
            //        return null;
            //    }
            //    var timeConverter = new IsoDateTimeConverter{ DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            //    return JsonConvert.SerializeObject(obj,timeConverter);
            //}
            new TestProcess().ProcessOpe();

        }
    }
    class Student { 
        public string Name { get; set; }
        public string sex { get; set;}
    }
}
