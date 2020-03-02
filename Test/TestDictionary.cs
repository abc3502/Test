using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
   public  class TestDictionary
    {
        public void addSpace() {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("","");
            Console.WriteLine("键可以为空:"+dic.Keys.Count);
        }
    }
}
