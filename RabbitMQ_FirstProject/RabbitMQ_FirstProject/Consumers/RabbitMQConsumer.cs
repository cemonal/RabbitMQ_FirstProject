using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ_FirstProject.Services;

namespace RabbitMQ_FirstProject.Consumers
{
    public class RabbitMQConsumer
    {
        private readonly RabbitMQService _rabbitMQService;
        private readonly string _queueName;

        public RabbitMQConsumer(string queueName)
        {
            _queueName = queueName;
        }

        public void Consume(EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            using (var connection = RabbitMQService.Instance.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += receivedHandler;

                    channel.BasicConsume(_queueName, true, consumer);
                }
            }
        }
    }
}