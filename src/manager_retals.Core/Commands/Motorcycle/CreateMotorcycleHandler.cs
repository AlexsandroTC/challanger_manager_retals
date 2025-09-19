using manager_retals.Core.Entities;
using manager_retals.Core.Exceptions;
using manager_retals.Core.Notification;
using manager_retals.Core.Repositories;
using MediatR;

namespace manager_retals.Core.Commands.Motorcycle
{
    public class CreateMotorcycleHandler : IRequestHandler<CreateMotorcycleCommand, int>
    {
        private readonly IMediator _mediator;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public CreateMotorcycleHandler(IMotorcycleRepository motorcycleRepository, IMediator mediator)
        {
            _motorcycleRepository = motorcycleRepository;
            _mediator = mediator;
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

            await _mediator.Publish(new MotorcycleCreatedNotification
            {
                MotorcycleId = motorcycleCreated.Id,
                Identifier = motorcycleCreated.Identifier,
                Model = motorcycleCreated.Model,
                Year = motorcycleCreated.Year,
                Plate = motorcycleCreated.Plate
            }, cancellationToken);

            Console.WriteLine($"Criado moto: {motorcycleCreated.Identifier}, placa: {motorcycleCreated.Plate}");

            return motorcycleCreated.Id;
        }
    }
}