using manager_retals.Core.Enums;

namespace manager_retals.Api.DTOs.Rental
{
    public record RentMotorcycleRequest
    {
        public int DriverId { get;set; }
        public int MotorcycleId { get;set; }
        public DateTime StartDate { get;set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public RentalPlan Plan { get; set; }
    }
}
