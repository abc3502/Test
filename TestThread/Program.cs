using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace TestThread
{
    class Program
    {
        static void Main(string[] args)
        {
            //开启线程
            /*  ServerClass serverClass = new ServerClass();
              //实例方法
              Thread intanceThread = new Thread(new ThreadStart(serverClass.InstanceMethod));
              intanceThread.Start();
              Console.WriteLine("The Main() thread calls this after "
             + "starting the new InstanceCaller thread.");
              //静态方法
              Thread staticThread = new Thread(new ThreadStart(ServerClass.StaticMethod));
              staticThread.Start();
              Console.WriteLine("The Main() thread calls this after "
             + "starting the new StaticCaller thread.");*/

            /*   //跨线程传输数据
               ThreadState ts = new ThreadState("This report displays the number {0}.",42);
               //创建线程执行任务
               Thread t = new Thread(new ThreadStart(ts.ThreadProc));
               //开始线程
               t.Start();
               Console.WriteLine("Main thread does some work. then waits");
               //阻止主线程的调用,直到实例线程终止
               t.Join();
               Console.WriteLine("Independent task has completed; main thread starts.");*/

            //使用回调方法检索线程中的数据
            ThreadStateWithCallBack twsb = new ThreadStateWithCallBack("This report displays the number {0}.",42,new ThreadStateWithCallBack.ExampleCallBack(ResultCallback));
            //创建线程
            Thread t = new Thread(new ThreadStart(twsb.ThreadProc));
            t.Start();
            Console.WriteLine("Main thread does some work, then waits.");
            t.Join();
            Console.WriteLine("Independent task has completed; main thread starts.");
        }
        public static void ResultCallback(int lineCount) {
            Console.WriteLine("Independent task printed {0} lines.",lineCount);
        }
    }
}
