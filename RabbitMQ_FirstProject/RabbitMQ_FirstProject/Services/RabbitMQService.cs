using RabbitMQ.Client;

namespace RabbitMQ_FirstProject.Services
{
    public sealed class RabbitMQService
    {
        private readonly string _hostName = "localhost";
        private static RabbitMQService instance = null;
        private static readonly object padlock = new object();

        public IConnection CreateConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName
            };

            return connectionFactory.CreateConnection();
        }

        public static RabbitMQService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new RabbitMQService();
                    }
                }

                return instance;
            }
        }
    }
}