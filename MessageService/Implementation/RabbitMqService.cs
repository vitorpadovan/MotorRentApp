using MessageService.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace MessageService.Implementation
{
    public class RabbitMqService : IMessageService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMqService> _logger;
        private readonly string _connectionString;

        public RabbitMqService(IConfiguration configuration, ILogger<RabbitMqService> logger)
        {
            _configuration = configuration;
            var conString = _configuration.GetConnectionString("RabbitMqServiceConn");
            if (conString != null)
                this._connectionString = conString;
            _logger = logger;
        }

        public bool Publish<T>(T v, Func<T, Guid> databaseResilience, string queue = "default", string routingKey = "default")
        {
            var asd = databaseResilience.Invoke(v);
            this.Publish(v, queue, routingKey);
            _logger.LogInformation("Publicando na fila o valor {valor} ", v);
            return true;
        }

        private void Publish<T>(T obj, string queue, string routingKey)
        {
            ConnectionFactory factory = new();
            if (_connectionString != null)
                factory.Uri = new(this._connectionString);
            else
                factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queue,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            const string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
