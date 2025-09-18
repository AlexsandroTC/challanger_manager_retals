using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using MediatR;

namespace manager_retals.Core.Commands.Motorcycle
{
    public class CreateMotorcycleHandler : IRequestHandler<CreateMotorcycleCommand, int>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public CreateMotorcycleHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<int> Handle(CreateMotorcycleCommand command, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Criando moto: {command.Model}, placa: {command.Place}");
            var exists = await _motorcycleRepository.GetByPlateAsync(command.Place);
            if(exists != null)
                throw new MotorcyclePlateAlreadyRegisteredException(command.Place);

            var entity = new Entities.Motorcycle(command.Identifier,
                                                 command.Model,
                                                 command.Year,
                                                 command.Place);

            var motorcycleCreated = await _motorcycleRepository.AddAsync(entity);

            Console.WriteLine($"Criado moto: {motorcycleCreated.Identifier}, placa: {motorcycleCreated.Plate}");

            return motorcycleCreated.Id;
        }
    }
}