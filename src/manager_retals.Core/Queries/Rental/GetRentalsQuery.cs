using MediatR;

namespace manager_retals.Core.Queries.Rental
{
    public class GetRentalsQuery : IRequest<Entities.Rental>
    {
        public int Id { get; set; }

        public GetRentalsQuery(int id)
        {
            Id = id;
        }
    }
}
