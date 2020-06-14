using System;
using System.Text;
using RabbitMQ_FirstProject.Consumers;
using RabbitMQ_FirstProject.Publishers;

namespace RabbitMQ_FirstProject
{
    class Program
    {
        private static readonly string _queueName = "cemonal";
        private static RabbitMQPublisher _publisher;
        private static RabbitMQConsumer _consumer;

        static void Main(string[] args)
        {
            Console.WriteLine("Please write your message:");
            var message = Console.ReadLine();

            _publisher = new RabbitMQPublisher(_queueName);
            _publisher.Publish(message);

            _consumer = new RabbitMQConsumer(_queueName);
            _consumer.Consume((model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine("{0} - Incoming message from {1}: \"{2}\"", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), _queueName, message);
            });

            Console.ReadLine();
        }
    }
}