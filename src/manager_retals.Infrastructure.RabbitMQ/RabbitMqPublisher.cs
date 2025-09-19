using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace manager_retals.Infrastructure.RabbitMQ
{
    public class RabbitMqPublisher
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqPublisher(string hostName, string userName, string password)
        {
            _factory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }

        public async Task PublishAsync<T>(T message, string queueName)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                mandatory: false,
                body: body);
        }
    }
}
