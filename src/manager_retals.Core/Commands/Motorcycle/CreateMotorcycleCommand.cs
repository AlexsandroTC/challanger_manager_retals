using MediatR;

namespace manager_retals.Core.Commands.Motorcycle
{
    public class CreateMotorcycleCommand : IRequest<int>
    {
        public string Identifier { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }

        //TODO Criar um value object para placa
        public string Place { get; set; }

        public CreateMotorcycleCommand(string identifier, int year, string model, string plate)
        {
            if (string.IsNullOrWhiteSpace(identifier)) throw new ArgumentNullException(nameof(identifier));
            if (year <= 0) throw new ArgumentNullException(nameof(year));
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(plate)) throw new ArgumentNullException(nameof(plate));

            Identifier = identifier;
            Year = year;
            Model = model;
            Place = plate;
        }
    }
}
