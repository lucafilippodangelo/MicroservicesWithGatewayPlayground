using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;

namespace ASecondConsumerLd
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        
        public EventBusRabbitMQConsumer(IRabbitMQConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.SecondConsumerQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.SecondConsumerQueue, autoAck:true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.SecondConsumerQueue)
            {
                writeFile();
                Console.WriteLine("SECOND CONSUMER CONSUMED an event from the queue " + EventBusConstants.SecondConsumerQueue + " ->" + DateTimeOffset.UtcNow);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }

        public void writeFile()
        {
            string path = @"C:\Users\ldazu\Desktop\MyTest.txt";

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.OpenWrite(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("SECOND CONSUMER CONSUMED an event from the queue " + EventBusConstants.SecondConsumerQueue + " ->" + DateTimeOffset.UtcNow);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
