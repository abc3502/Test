using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRabbitmq
{
    class Program
    {
        static void Main(string[] args)
        {
            //  RecieveMsg.recieveCusMsg();
            //   RecieveMsg.recieveCusMsg();

            //  RecieveMsg.recieveMsg2();
            //MqttService service=  new MqttService();
            //service.StartMqtt();
            //  new M2MqttService().StartMqtt();
            var strk = "7/18/23";
            
          
            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = "M/dd/YY";
          //  DateTime aa = Convert.ToDateTime(strk, dtInfo);
            DateTime dt = DateTime.ParseExact(strk, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            Console.WriteLine(dt);
            Console.ReadKey();
        }
    }
}
