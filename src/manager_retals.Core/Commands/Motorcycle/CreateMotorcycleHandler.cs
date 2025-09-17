using MediatR;

namespace manager_retals.Core.Commands.Motorcycle
{
    public class CreateMotorcycleHandler : IRequestHandler<CreateMotorcycleCommand, int>
    {
        public Task<int> Handle(CreateMotorcycleCommand command, CancellationToken cancellationToken)
        {
            // Aqui você faria a lógica para salvar no banco
            Console.WriteLine($"Criando moto: {command.Model}, placa: {command.Place}");
            int newId = new Random().Next(1, 1000); // Mock do ID
            return Task.FromResult(newId);
        }
    }
}
