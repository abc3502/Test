using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CookieUtil
{
    public static class Cookie1Extention
    {
        public static string toJosn(this object obj) {
            if (obj==null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(obj);
        }
        public static T ToObject<T>(this string json) {
            if (json==null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}