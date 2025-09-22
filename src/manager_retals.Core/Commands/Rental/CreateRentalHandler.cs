using manager_retals.Core.Commands.Motorcycle;
using manager_retals.Core.Enums;
using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using manager_retals.Core.Services;
using MediatR;

namespace manager_retals.Core.Commands.Rental
{
    public class CreateRentalHandler : IRequestHandler<CreateRentalCommand, int>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly RentalPlanCalculationServices _rentalPlanCalculationServices;

        public CreateRentalHandler(IRentalRepository rentalRepository,
                                   IMotorcycleRepository motorcycleRepository,
                                   IDriverRepository driverRepository,
                                   RentalPlanCalculationServices rentalPlanCalculationServices)
        {
            _rentalRepository = rentalRepository;
            _motorcycleRepository = motorcycleRepository;
            _driverRepository = driverRepository;
            _rentalPlanCalculationServices = rentalPlanCalculationServices;
        }

        public async Task<int> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetByIdAsync(request.DriverId);
            if (driver == null)
                throw new RentalDriverIsNotFoundException();

            if (driver.DriverLicenseType == DriverLicenseType.B)
                throw new RentalDriverWithIncompatibleDriveLicenseException();

            var motorcycle = await _motorcycleRepository.GetByIdAsync(request.MotorcycleId);
            if (motorcycle == null)
                throw new RentalMotorcyleIsNotFoundException();

            var rentalPlanCalculationServices = _rentalPlanCalculationServices.GetStrategy(request.Plan);
            var totalPrice = rentalPlanCalculationServices.GetTotalPrice();

            var rental = new Entities.Rental
            {
                DriverId = request.DriverId,
                MotorcycleId = request.MotorcycleId,
                Plan = request.Plan,
                TotalPrice = totalPrice,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(1 + rentalPlanCalculationServices.GetDays()),
                Status = RentalStatus.Open
            };

            await _rentalRepository.AddAsync(rental);
            return rental.Id;
        }
    }
}
