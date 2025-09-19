using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using MediatR;

namespace manager_retals.Core.Commands.Driver
{
    public class CreateDriverHandler : IRequestHandler<CreateDriverCommand, int>
    {
        private readonly IDriverRepository _driverRepository;
        public CreateDriverHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<int> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            var driverWithCnpj = await _driverRepository.FindAsync(d => d.CompanyNumber == request.CompanyNumber);
            if (driverWithCnpj.Any())
                throw new DriverDocumentRegisteredException("CNPJ ja cadastrado.");

            var driverWithCnh = await _driverRepository.FindAsync(d => d.DriverLicenseNumber == request.DriverLicenseNumber);
            if (driverWithCnh.Any())
                throw new DriverDocumentRegisteredException("CNH ja cadastrado.");

            var driver = new Entities.Driver(
                request.Identifier,
                request.Name,
                request.CompanyNumber,
                request.BirthDate,
                request.DriverLicenseNumber,
                request.DriverLicenseType,
                request.DriverLicenseImage
            );

            await _driverRepository.AddAsync(driver);
            return driver.Id;
        }
    }
}
