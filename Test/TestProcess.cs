using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Test
{
   public  class TestProcess
    {
        public void ProcessOpe() {

            Process[] proArray = Process.GetProcesses();
            foreach (var item in proArray)
            {
                Console.WriteLine(item.Id + "-" + item.ProcessName);
                item.Kill();
            }
            /*  Process p = new Process();
              p.Start("c");
              */
            //   Process.Start("calc");
            // Process.Start("ssms");
            //   Process.Start("iexplore","http://www.baidu.com");
            //打开一个文件
            /* Process p = new Process();
             ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Users\Administrator\Desktop\远程桌面.txt");
             p.StartInfo = startInfo;
             p.Start();*/


        }

    }
}
