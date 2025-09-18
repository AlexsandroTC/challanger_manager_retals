namespace manager_retals.Infrastructure.Entities
{
    public class Rental : BaseEntity
    {
        public int MotorcycleId { get; set; }
        public Motorcycle Motorcycle { get; set; } = null!;

        public int DriverId { get; set; }
        public Driver Driver { get; set; } = null!;

        public DateTime RentalStart { get; set; }
        public DateTime? RentalEnd { get; set; }
        public decimal? TotalPrice { get; set; }

        public string Status { get; set; }

    }
}
