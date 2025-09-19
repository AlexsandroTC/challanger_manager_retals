using manager_retals.Core.Enums;

namespace manager_retals.Core.Entities
{
    public class Rental : BaseEntity
    {
        public int MotorcycleId { get; set; }
        public Motorcycle Motorcycle { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public RentalPlan Plan { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public RentalStatus Status { get; set; }
    }
}
