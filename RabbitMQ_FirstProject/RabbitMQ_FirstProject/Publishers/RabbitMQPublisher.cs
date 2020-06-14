using System.Text;
using RabbitMQ.Client;
using RabbitMQ_FirstProject.Services;

namespace RabbitMQ_FirstProject.Publishers
{
    public class RabbitMQPublisher
    {
        private readonly string _queueName;

        public RabbitMQPublisher(string queueName)
        {
            _queueName = queueName;
        }

        public void Publish(string message)
        {
            using (var connection = RabbitMQService.Instance.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(_queueName, false, false, false, null);
                    channel.BasicPublish("", _queueName, null, Encoding.UTF8.GetBytes(message));
                }
            }
        }
    }
}