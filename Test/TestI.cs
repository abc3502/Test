using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class TestI : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {

            throw new NotImplementedException();
        }
    }
}
