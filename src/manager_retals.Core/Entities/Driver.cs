namespace manager_retals.Core.Entities
{
    public class Driver : BaseEntity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string? Phone { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
