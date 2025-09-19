using manager_retals.Infrastructure.RabbitMQ;
using MediatR;

namespace manager_retals.Core.Notification
{
    internal class MotorcycleCreatedNotificationHandler : INotificationHandler<MotorcycleCreatedNotification>
    {
        private const string ExchangeName = "motorcycle_created";
        private readonly RabbitMqPublisher _publisher;

        public MotorcycleCreatedNotificationHandler(RabbitMqPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Handle(MotorcycleCreatedNotification notification, CancellationToken cancellationToken)
        {
            await _publisher.PublishAsync(notification, ExchangeName);
        }
    }
}
