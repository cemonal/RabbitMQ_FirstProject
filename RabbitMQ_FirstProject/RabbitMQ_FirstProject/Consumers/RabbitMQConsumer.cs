using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ_FirstProject.Services;

namespace RabbitMQ_FirstProject.Consumers
{
    /// <summary>
    /// Provides functionality to consume messages from a RabbitMQ queue.
    /// </summary>
    public class RabbitMQConsumer
    {
        private readonly string _queueName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMQConsumer"/> class.
        /// </summary>
        /// <param name="queueName">The name of the queue to consume messages from.</param>
        public RabbitMQConsumer(string queueName)
        {
            _queueName = queueName;
        }

        /// <summary>
        /// Begins consuming messages from the specified RabbitMQ queue.
        /// </summary>
        /// <param name="receivedHandler">Event handler to process received messages.</param>
        public void Consume(EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            using (var connection = RabbitMQService.Instance.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += receivedHandler;

                // Start consuming messages from the queue
                channel.BasicConsume(_queueName, autoAck: true, consumer: consumer);
            }
        }
    }
}
