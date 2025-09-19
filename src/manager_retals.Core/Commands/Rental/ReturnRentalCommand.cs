using MediatR;

namespace manager_retals.Core.Commands.Rental
{
    public class ReturnRentalCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime ReturnDate { get; set; }

        public ReturnRentalCommand(int id, DateTime returnDate)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));
            if (DateTime.MinValue == returnDate) throw new ArgumentNullException(nameof(returnDate));

            Id = id;
            ReturnDate = returnDate;
        }
    }
}
