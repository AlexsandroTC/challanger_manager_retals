using manager_retals.Core.Enums;
using MediatR;

namespace manager_retals.Core.Commands.Rental
{
    public class CreateRentalCommand : IRequest<int>
    {
        public int MotorcycleId { get; set; }
        public int DriverId { get; set; }
        public RentalPlan Plan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }

        public CreateRentalCommand(int motorcycleId, int driverId, RentalPlan? plan, DateTime startDate, DateTime endDate, DateTime expectedEndDate)
        {
            if (motorcycleId <= 0) throw new ArgumentNullException(nameof(motorcycleId));
            if (driverId <= 0) throw new ArgumentNullException(nameof(driverId));
            if (plan == null) throw new ArgumentNullException(nameof(plan));
            if (DateTime.MinValue == startDate) throw new ArgumentNullException(nameof(startDate));
            if (DateTime.MinValue == endDate) throw new ArgumentNullException(nameof(endDate));
            if (DateTime.MinValue == expectedEndDate) throw new ArgumentNullException(nameof(expectedEndDate));

            MotorcycleId = motorcycleId;
            DriverId = driverId;
            Plan = (RentalPlan)plan;
            StartDate = startDate;
            EndDate = endDate;
            ExpectedEndDate = expectedEndDate;
        }
    }
}