using MediatR;

namespace manager_retals.Core.Commands.Motorcycle
{
    public class UpdateMotorcycleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Plate { get; set; }

        public UpdateMotorcycleCommand(int id, string plate)
        {
            if (string.IsNullOrWhiteSpace(plate)) throw new ArgumentNullException(nameof(plate));
            if (id <= 0) throw new ArgumentNullException(nameof(id));

            Id = id;
            Plate = plate;
        }
    }
}
