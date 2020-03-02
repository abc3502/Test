using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestThread
{
   public  class ThreadStateWithCallBack
    {
        // State information used in the task.
        private string boilerplate;
        private int numberValue;

         // Delegate used to execute the callback method when the
        // task is complete.
        private ExampleCallBack callback;


        // The constructor obtains the state information and the
        // callback delegate.
        public ThreadStateWithCallBack(string text,int num,ExampleCallBack callBackDelegate) {
            boilerplate = text;
            numberValue = num;
            callback = callBackDelegate;
        }

        // The thread procedure performs the task, such as
        // formatting and printing a document, and then invokes
        // the callback delegate with the number of lines printed.
        public void ThreadProc() {
            Console.WriteLine(boilerplate,numberValue);
            if (callback !=null)
            {
                callback(1);
            }
        }

        //delegate that defines the signature for the callback method
        public delegate void ExampleCallBack(int lineCount);

    }
}
