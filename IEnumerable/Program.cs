using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft;
using System.Data;
using Newtonsoft.Json;
using System.Linq;

namespace IEnumerable
{
    class Program
    {
        public class A{
            public List<int> _numArray { get; set; }
            public A() {
                _numArray = new List<int>();
                for (int i = 0; i <= 1; i++)
                {
                    _numArray.Add(i);
                }

            }
            public void testMethod1() {
                foreach (int  num in getEventNumber1())
                {
                    Console.WriteLine(num);
                }

            }
            public void testMethod() {
                foreach ( int num in getEventNumber())
                {
                    Console.WriteLine(num);
                }

            }
            public IEnumerable<int> getEventNumber() {
                foreach (int item in _numArray)
                {
                    if (item %2==0)
                    {
                        yield return item;
                    }
                }
                //可有可无
                yield break;
            }
            public IEnumerable<int> getEventNumber1() {
                List<int> list = new List<int>();
                foreach (int num in _numArray)
                {
                    if (num %2 == 0)
                    {
                        list.Add(num);
                    }
                }
                return list;
            }




        }

        static void Main(string[] args)
        {
            ////测量yield return 与集合List<>的运行时间
            //A a = new A();
            //Stopwatch stop = new Stopwatch();
            //stop.Start();
            //a.testMethod();
            //stop.Stop();
            //Console.WriteLine( "第一种方法用时："+stop.ElapsedMilliseconds);
            //stop.Reset();
            //stop.Start();
            //a.testMethod1();
            //a.getEventNumber1();
            //stop.Stop();
            //Console.WriteLine("第二种方法用时："+stop.ElapsedMilliseconds);
           
            ////简单迭代器
            //EnumerableI enumerable = new EnumerableI();
            //foreach (var item in enumerable)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.ReadKey();
            //DataTable转换成linq
            DataTableToLinq dtToLinq = new DataTableToLinq();
            //string json = dtToLinq.DataTalbeToLinq();
            //Console.WriteLine(json);
            //Console.ReadKey();
            //两个DataTable转换成一个DataTable 
            DataTable dt1 = dtToLinq.GetDataTable();
            DataTable dt2 = dtToLinq.GetDataTable().AsEnumerable().Where(x=>x.Field<int>("ID")>=5).Select(x=>x).CopyToDataTable();
            DataTable dt3 = (from a in dt1.AsEnumerable() join b in dt2.AsEnumerable() on a.Field<int>("ID") equals b.Field<int>("ID") select b).CopyToDataTable();

            //   Console.WriteLine(JsonConvert.SerializeObject(result));

            // DataTable 转换成list
            List<Product> products;
            var query    = from a in dt1.AsEnumerable()
                        from b in dt2.AsEnumerable()  
                        where b.Field<int>("ID") >= 5 && a.Field<int>("ID") == b.Field<int>("ID")
                select new
                {
                    productID = b.Field<int>("ID"),
                    productName = a.Field<string>("Name")

                } ;
            Console.WriteLine(JsonConvert.SerializeObject(query));
            Console.ReadKey();



        }
        

    }
    public class Product {
        public int productID { get; set; }
        public string productName { get; set; }
    }
}
