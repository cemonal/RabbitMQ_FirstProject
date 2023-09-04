using System.Text;
using RabbitMQ.Client;
using RabbitMQ_FirstProject.Services;

namespace RabbitMQ_FirstProject.Publishers
{
    /// <summary>
    /// Provides functionality to publish messages to a RabbitMQ queue.
    /// </summary>
    public class RabbitMQPublisher
    {
        private readonly string _queueName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMQPublisher"/> class.
        /// </summary>
        /// <param name="queueName">The name of the queue to publish messages to.</param>
        public RabbitMQPublisher(string queueName)
        {
            _queueName = queueName;
        }

        /// <summary>
        /// Publishes a message to the specified RabbitMQ queue.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        public void Publish(string message)
        {
            using (var connection = RabbitMQService.Instance.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare the queue if it doesn't exist
                channel.QueueDeclare(_queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                // Convert message to bytes and publish it
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", _queueName, null, body);
            }
        }
    }
}