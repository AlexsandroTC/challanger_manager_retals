using manager_retals.Core.Repositories;
using MediatR;

namespace manager_retals.Core.Queries.Rental
{
    internal class GetRentalsHandler : IRequestHandler<GetRentalsQuery, Entities.Rental>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetRentalsHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<Entities.Rental> Handle(GetRentalsQuery request, CancellationToken cancellationToken)
        {
            return await _rentalRepository.GetByIdAsync(request.Id);
        }
    }
}
