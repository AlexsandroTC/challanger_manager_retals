using MediatR;

namespace manager_retals.Core.Notification
{
    internal class MotorcycleCreatedNotification : INotification
    {
        public int MotorcycleId { get; set; }
        public string Identifier { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }
    }
}
