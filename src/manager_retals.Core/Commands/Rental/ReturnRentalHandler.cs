using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using manager_retals.Core.Services;
using MediatR;

namespace manager_retals.Core.Commands.Rental
{
    public class ReturnRentalHandler : IRequestHandler<ReturnRentalCommand, int>
    {
        private readonly IRentalRepository _rentalRepository;

        public ReturnRentalHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<int> Handle(ReturnRentalCommand request, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.GetByIdAsync(request.Id);
            if (rental == null)
                throw new RentalDriverIsNotFoundException();

            decimal finalPrice = RentalPlanCalculationServices.CalculateFinalPrice(rental.EndDate, request.ReturnDate, rental.Plan, rental.TotalPrice);

            rental.EndDate = request.ReturnDate;
            rental.TotalPrice = finalPrice;
            rental.UpdatedAt = DateTime.UtcNow;

            await _rentalRepository.UpdateAsync(rental);
            return rental.Id;
        }
    }
}
