using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace manager_retals.Infrastructure.RabbitMQ
{
    public class RabbitMqConsumer<T>
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqConsumer(string hostName, string userName, string password)
        {
            _factory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }

        public async Task StartConsumingAsync(string queueName, Action<T> onMessageReceived)
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);

                var message = JsonSerializer.Deserialize<T>(messageJson);

                if (message != null)
                    onMessageReceived(message);

                await Task.Yield();
            };

            await channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
    }
}
