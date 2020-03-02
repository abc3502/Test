using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PartialClass
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetStr getStr = new GetStr();
            //Console.WriteLine( getStr.getStr1()+getStr.getStr2());
            List<Int32> list = new List<int>() { 1,2,3,99,55,3,28};
            var query = from x in list select new { id=x,index=list.IndexOf(x) };
            foreach (var item in query)
            {
                Console.WriteLine("排序："+item.index);
            }

            Console.ReadKey();
        }
    }
    public partial class GetStr {

        public string getStr1() {
            return "用一生下载你";
        }
    }
    public partial class GetStr1 {
        
        public string getStr2() {

            return "云烨香风";
        }
    }
    public static class GetMethod {
        /// <summary>
        /// 拓展方法
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string getStr2(this GetStr getStr)
        {
            return "云烨香风";
        }
    } 
   
}
