using RabbitMQ.Client;

namespace RabbitMQ_FirstProject.Services
{
    /// <summary>
    /// Provides a singleton service for creating connections to RabbitMQ.
    /// </summary>
    public sealed class RabbitMQService
    {
        private readonly string _hostName = "localhost";
        private static readonly RabbitMQService _instance = new RabbitMQService();

        // Private constructor to prevent external instantiation.
        private RabbitMQService() { }

        /// <summary>
        /// Gets the singleton instance of the <see cref="RabbitMQService"/>.
        /// </summary>
        public static RabbitMQService Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Creates and returns a new connection to the RabbitMQ server.
        /// </summary>
        /// <returns>An instance of <see cref="IConnection"/>.</returns>
        public IConnection CreateConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName
            };

            return connectionFactory.CreateConnection();
        }
    }
}
