namespace manager_retals.Api.DTOs.Rental
{
    public record ReturnRentalRequest
    {
        public DateTime ReturnDate { get; set; }
    }
}
