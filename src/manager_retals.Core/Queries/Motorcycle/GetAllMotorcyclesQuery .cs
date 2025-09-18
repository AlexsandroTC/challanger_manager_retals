using MediatR;

namespace manager_retals.Core.Queries.Motorcycle
{
    public class GetAllMotorcyclesQuery : IRequest<IEnumerable<Entities.Motorcycle>>
    {
        public string? Plate { get; set; }

        public GetAllMotorcyclesQuery(string? plate = null)
        {
            Plate = plate;
        }
    }
}
