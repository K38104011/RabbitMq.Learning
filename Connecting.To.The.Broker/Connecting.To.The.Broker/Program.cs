using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;

namespace Connecting.To.The.Broker
{
    class Program
    {
        static void Main()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            // remember start service when exception happens
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var myQueue = "myFirstQueue";
                    channel.QueueDeclare(myQueue, true, false, false, null);

                    var message = "My message to myFirstQueue";
                    var messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", myQueue, null, messageBytes);

                    var props = new BasicProperties
                    {
                        Persistent = true
                    };
                    channel.BasicPublish("", myQueue, props, messageBytes);
                }
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
