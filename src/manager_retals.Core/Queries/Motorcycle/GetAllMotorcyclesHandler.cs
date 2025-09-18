using manager_retals.Core.Repositories;
using MediatR;

namespace manager_retals.Core.Queries.Motorcycle
{
    public class GetAllMotorcyclesHandler : IRequestHandler<GetAllMotorcyclesQuery, IEnumerable<Entities.Motorcycle>>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public GetAllMotorcyclesHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<IEnumerable<Entities.Motorcycle>> Handle(GetAllMotorcyclesQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Plate))
            {
                return await _motorcycleRepository.FindAsync(m => m.Plate == request.Plate);
            }

            return await _motorcycleRepository.GetAllAsync();
        }
    }
}
