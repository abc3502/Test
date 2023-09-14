using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
  public  class SendMsg
    {
        public static void sendMsg() {

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection=factory.CreateConnection())
            {
                using (var channel=connection.CreateModel())
                {
                    channel.QueueDeclare("task_queue", true,false,false,null);
                  
                    for (int i = 0; i <100; i++)
                    {
                        String message = "{\"headers\":{ \"productId\":\"WWPhtoSubProduct\",\"deviceName\":\"00000022092102\"},\"messageType\":\"REPORT_PROPERTY\",\"deviceId\":\"00000022092102\",\"properties\":{\"thisPicPackNums\":17,\"PicNo\":0,\"thisPicPackNo\":0,\"dataTime\":\"2016 - 05 - 26 22:27:51\",\"picData\":\"ff d8 ff e0 00 10 4a 46 49 46 00 01 01 01 00 00 00 00 00 00 ff db 00 43 00 0c 08 09 0b 09 08 0c 0b 0a 0b 0e 0d 0c 0e 12 1e 14 12 11 11 12 25 1a 1c 16 1e 2c 26 2e 2d 2b 26 2a 29 30 36 45 3b 30 33 41 34 29 2a 3c 52 3d 41 47 4a 4d 4e 4d 2f 3a 55 5b 54 4b 5a 45 4c 4d 4a ff db 00 43 01 0d 0e 0e 12 10 12 23 14 14 23 4a 32 2a 32 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a 4a ff c4 00 1f 00 00 01 05 01 01 01 01 01 01 00 00 00 00 00 00 00 00 01 02 03 04 05 06 07 08 09 0a 0b ff c4 00 b5 10 00 02 01 03 03 02 04 03 05 05 04 04 00 00 01 7d 01 02 03 00 04 11 05 12 21 31 41 06 13 51 61 07 22 71 14 32 81 91 a1 08 23 42 b1 c1 15 52 d1 f0 24 33 62 72 82 09 0a 16 17 18 19 1a 25 26 27 28 29 2a 34 35 36 37 38 39 3a 43 44 45 46 47 48 49 4a 53 54 55 56 57 58 59 5a 63 64 65 66 67 68 69 6a 73 74 75 76 77 78 79 7a 83 84 85 86 87 88 89 8a 92 93 94 95 96 97 98 99 9a a2 a3 a4 a5 a6 a7 a8 a9 aa b2 b3 b4 b5 b6 b7 b8 b9 ba c2 c3 c4 c5 c6 c7 c8 c9 ca d2 d3 d4 d5 d6 d7 d8 d9 da e1 e2 e3 e4 e5 e6 e7 e8 e9 ea f1 f2 f3 f4 f5 f6 f7 f8 f9 fa ff c4 00 1f 01 00 03 01 01 01 01 01 01 01 01 01 00 00 00 00 00 00 01 02 03 04 05 06 07 08 09 0a 0b ff c4 00 b5 11 00 02 01 02 04 04 03 04 07 05 04 04 00 01 02 77 00 01 02 03 11 04 05 21 31 06 12 41 51 07 61 71 13 22 32 81 08 14 42 91 a1 b1 c1 09 23 33 52 f0 15 62 72 d1 0a 16 24 34 e1 25 f1 17 18 19 1a 26 27 28 29 2a 35 36 37 38 39 3a 43 44 45 46 47 48 49 4a 53 54 55 56 57 58 59 5a 63 64 65 66 67 68 69 6a 73 74 \",\"allPicNums\":1},\"timestamp\":1673234258618}";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish("", "task_queue", null, body);
                        Console.WriteLine("已发送：{0}", message);
                    }
                  
                    Console.WriteLine();
                }
            }

        }
        /// <summary>
        /// 工作线程
        /// </summary>
        /// <param name=\"args\"></param>
        public static void sendMsg2(String[] args) {

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    bool durable = true;
                    channel.QueueDeclare("task_queue", durable, false, false, null);
                    string message = GetMessage(args);
                    var properties = channel.CreateBasicProperties();
                    properties.SetPersistent(true);

                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", "task_queue", properties, body);
                    Console.WriteLine("已发送：{0}", message);
                    Console.WriteLine();
                }
            }
            Console.ReadKey();


        }
        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }

    }
}
