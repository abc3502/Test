using MQTTnet;
using MQTTnet.Core.Adapter;
using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRabbitmq
{
   public  class MqttService
    {
        private MqttClient mqttClient = new MqttClientFactory().CreateMqttClient() as MqttClient;
        public MqttService() {
            Task.Run(async () => { await ConnectMqttServerAsync(); });
        }
        public  void StartMqtt()
        {
            
           // SubscribeAsync();
        }
        private void SubscribeAsync() {
            String topic = "mqttJet";

            if (!mqttClient.IsConnected)
            {
                Console.WriteLine("MQTT客户端尚未连接！");
                return;
            }
            mqttClient.SubscribeAsync(new List<TopicFilter> {
                new TopicFilter(topic, MqttQualityOfServiceLevel.AtMostOnce)
            });


        }
        private async Task ConnectMqttServerAsync()
        {
            if (mqttClient == null)
            {
                mqttClient = new MqttClientFactory().CreateMqttClient() as MqttClient;
                mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
                mqttClient.Connected += MqttClient_Connected;
                mqttClient.Disconnected += MqttClient_Disconnected;

            }

            try
            {
                var options = new MqttClientTcpOptions
                {
                    Server = "8.142.68.93",
                    Port = 1883,
                    ClientId = Guid.NewGuid().ToString().Substring(0, 5),
                    UserName = "wwkj",
                    Password = "wwkj123!",
                    CleanSession = true
                };

                await mqttClient.ConnectAsync(options);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"连接到MQTT服务器失败！" + Environment.NewLine + ex.Message + Environment.NewLine);
              
            }
        }
        /// <summary>
        /// 客户端连接关闭事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private void MqttClient_Disconnected(object sender, EventArgs arg)
        {
            Console.WriteLine($"客户端已断开与服务端的连接……");
        }

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private void MqttClient_Connected(object sender, EventArgs arg)
        {
            Console.WriteLine($"客户端已连接服务端……\n");

        }

        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine($"ApplicationMessageReceivedAsync：客户端ID=接收到消息。 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】\n");
        }


    }
}
