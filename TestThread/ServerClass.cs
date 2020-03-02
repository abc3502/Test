using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestThread
{
    /// <summary>
    /// 创建线程
    /// </summary>
   public  class ServerClass
    {
        public void InstanceMethod() {
            Console.WriteLine("ServerClass.InstanceMethod is running on another thread.");
            Thread.Sleep(3000);
            Console.WriteLine("The instance method called by the worker thread has ended.");
        
        }
        public static void StaticMethod() {
            Console.WriteLine("ServerClass.StaticMethod is running on another thread.");
            Thread.Sleep(5000);
            Console.WriteLine("The static method called by the worker thread has ended.");
        }
    }
}
