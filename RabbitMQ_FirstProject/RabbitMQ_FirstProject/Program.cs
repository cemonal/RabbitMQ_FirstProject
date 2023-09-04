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

            try
            {
                // Publish the message
                PublishMessage(message);

                // Start consuming messages
                StartConsuming();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: {0}", ex.Message);
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Publishes a message to the RabbitMQ queue.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        private static void PublishMessage(string message)
        {
            try
            {
                _publisher = new RabbitMQPublisher(_queueName);
                _publisher.Publish(message);

                Console.WriteLine("Message published: \"{0}\"", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while publishing: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Starts consuming messages from the RabbitMQ queue.
        /// </summary>
        private static void StartConsuming()
        {
            try
            {
                _consumer = new RabbitMQConsumer(_queueName);
                _consumer.Consume((model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var receivedMessage = Encoding.UTF8.GetString(body);

                    Console.WriteLine("{0} - Incoming message from {1}: \"{2}\"", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), _queueName, receivedMessage);
                });

                Console.WriteLine("Listening for incoming messages...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while consuming: {0}", ex.Message);
            }
        }
    }
}
