using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using MediatR;

namespace manager_retals.Core.Commands.Motorcycle
{
    public class RemoveMotorcycleHandler : IRequestHandler<RemoveMotorcycleCommand, int>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public RemoveMotorcycleHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<int> Handle(RemoveMotorcycleCommand request, CancellationToken cancellationToken)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(request.Id);
            if (motorcycle == null)
                throw new NotFoundException("Placa não encontrada na base de dados.");

            if (motorcycle.Rentals.Any())
                throw new RemoveMotorcycleWithRentalsException("Não é possivel remover uma moto com registro de locação.");

            Console.WriteLine($"Deletando moto: {motorcycle.Model}, placa: {motorcycle.Plate}");

            await _motorcycleRepository.DeleteAsync(motorcycle);

            Console.WriteLine($"Moto deletada com sucesso.");

            return request.Id;
        }
    }
}