using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestThread
{
    //类型安全的跨线程传输数据
    public class ThreadState
    {
        // State information used in the task.
        private string boilerplate;
        private int numberValue;

        // The constructor obtains the state information.
        public ThreadState(string text, int number)
        {
            boilerplate = text;
            numberValue = number;
        }

        // The thread procedure performs the task, such as formatting
        // and printing a document.
        public void ThreadProc()
        {
            Console.WriteLine(boilerplate, numberValue);
        }

    }
}
