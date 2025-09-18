using MediatR;
using System.Numerics;

namespace manager_retals.Core.Commands.Motorcycle
{
    public class RemoveMotorcycleCommand : IRequest<int>
    {
        public int Id { get; set; }

        public RemoveMotorcycleCommand(int id)
        {
            if (id < 0) throw new ArgumentNullException(nameof(id));

            Id = id;
        }
    }
}
