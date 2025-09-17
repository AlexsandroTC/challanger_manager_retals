using MediatR;

namespace manager_retals.Core.Commands.Motorcycle
{
    public class CreateMotorcycleCommand : IRequest<int>
    {
        public string Identifier { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Place { get; set; }

        public CreateMotorcycleCommand(string indentificador, int ano, string model, string placa)
        {
            if (string.IsNullOrWhiteSpace(indentificador)) throw new ArgumentNullException(nameof(indentificador));
            if (ano <= 0) throw new ArgumentNullException(nameof(ano));
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(placa)) throw new ArgumentNullException(nameof(placa));

            Identifier = indentificador;
            Year = ano;
            Model = model;
            Place = placa;
        }
    }
}
