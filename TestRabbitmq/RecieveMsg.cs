using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestRabbitmq
{
   public class RecieveMsg
    {
        public static void recieveMsg() {
         
          
        }
        public static void recieveCusMsg()
        {
            var factory = new ConnectionFactory();
            
            factory.HostName = "8.142.68.93";
            factory.Port = 1883;
            factory.UserName = "wwkj";
            factory.Password = "wwkj123!";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("mqttJet", true, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("mqttJet", false, consumer);
                    consumer.Received += (model, ea) => {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("消费者2已接收：{0}", message);
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    Console.ReadLine();

                }
            }

        }

        public static void recieveMsg2() {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("task_queue", true, false, false, null);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("task_queue", false, consumer);
                    //while (true) 
                    //{
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);
                        Console.WriteLine("Received {0}", message);
                        Console.WriteLine("Done");
                        channel.BasicAck(ea.DeliveryTag, true);
                 //   }

                }

            }

        }

    }
}
