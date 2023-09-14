using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TestRabbitmq
{
  public  class M2MqttService
    {
        private  MqttClient mqClient;
        static String ip= "8.142.68.93";
        static Int32 port = 1883;
        static String userName = "wwkj";
        static String pwd = "wwkj123!";
        String topic = "mqttJet";
        public  void StartMqtt() {
         
            String clientId = Guid.NewGuid().ToString();
            // 实例化Mqtt客户端 
            mqClient = new MqttClient(ip,port,false,null,null, MqttSslProtocols.TLSv1_2);
            mqClient.Connect(clientId,userName,pwd);
            Console.WriteLine("mqtt连接成功");
            mqClient.Subscribe(new string[] {topic},new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            Console.WriteLine("订阅主题成功");

            mqClient.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

        }
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string topic = e.Topic.ToString();
            string message = System.Text.Encoding.GetEncoding("utf-8").GetString(e.Message);
           
            //同时订阅两个或者以上主题时，分类收集收到的信息
            if (topic == "mqttJet")
            {
                Console.WriteLine(message);
            }
          
          

        }

    }
}
