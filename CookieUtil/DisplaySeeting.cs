using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CookieUtil
{
    public class DisplaySeeting
    {
        public int Style;
        public int Size;
        public override string ToString()
        {
            return string.Format("Style={0},Size={1}",Style,Size);
        }
    }
}