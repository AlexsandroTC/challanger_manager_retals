namespace manager_retals.Infrastructure.Entities
{
    public class Motorcycle : BaseEntity
    {
        public string Identifier { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
