using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


namespace IEnumerable
{
    public class EnumerableI : System.Collections.IEnumerable
    {
        private  string[] str = { "a","b","c","d"};
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < str.Length; i++)
            {
                yield  return str[i];
            }
        }
    }
}
