using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using MediatR;

namespace manager_retals.Core.Commands.Motorcycle
{
    public class UpdateMotorcycleHandler : IRequestHandler<UpdateMotorcycleCommand, int>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public UpdateMotorcycleHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<int> Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(request.Id);
            if (motorcycle == null)
                throw new NotFoundException("Placa não encontrada na base de dados.");

            var hasPlaceRegistered = await _motorcycleRepository.GetByPlateAsync(request.Plate);
            if(hasPlaceRegistered != null)
                throw new MotorcyclePlateAlreadyRegisteredException(request.Plate);

            motorcycle.Plate = request.Plate;
            motorcycle.UpdatedAt = DateTime.Now;

            await _motorcycleRepository.UpdateAsync(motorcycle);

            Console.WriteLine($"Atualizado placa da moto: {motorcycle.Identifier}, placa: {request.Plate}");
            return motorcycle.Id;
        }
    }
}