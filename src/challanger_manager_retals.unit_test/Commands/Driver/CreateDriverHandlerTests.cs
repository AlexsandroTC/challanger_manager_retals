using FluentAssertions;
using manager_retals.Core.Commands.Driver;
using manager_retals.Core.Enums;
using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using Moq;
using System.Linq.Expressions;

namespace manager_retals.Unit_test.Commands.Driver
{
    public class CreateDriverHandlerTests
    {
        private readonly Mock<IDriverRepository> _driverRepositoryMock;
        private readonly CreateDriverHandler _handler;

        private readonly string _identifier = "ID123";
        private readonly string _name = "Name unit test";
        private readonly string _companyNumber = "12345678912";
        private readonly string _driverLicenseNumber = "12345687";
        private readonly DateTime _birthDate = DateTime.Now.AddYears(-18);
        private readonly DriverLicenseType? _driverlicenseType = DriverLicenseType.AB;
        private readonly string _driveLicenseImage = "imageDriveLicense.png";
        private readonly Core.Entities.Driver _documentDuplicated;


        public CreateDriverHandlerTests()
        {
            _driverRepositoryMock = new Mock<IDriverRepository>();
            _handler = new CreateDriverHandler(_driverRepositoryMock.Object);
            _documentDuplicated = new Core.Entities.Driver(_identifier, _name, _companyNumber, _birthDate, _driverLicenseNumber, (DriverLicenseType)_driverlicenseType, _driveLicenseImage);
        }

        [Fact]
        public async Task WhenCreateDriver_ShoudCreateDriver_WhenThereIsNoDuplicity()
        {
            var command = new CreateDriverCommand(
                _identifier, 
                _name, 
                _companyNumber, 
                _birthDate, 
                _driverLicenseNumber, 
                _driverlicenseType, 
                _driveLicenseImage
            );

            var driverCreated = new Core.Entities.Driver(
                command.Identifier,
                command.Name,
                command.CompanyNumber,
                command.BirthDate,
                command.DriverLicenseNumber,
                command.DriverLicenseType,
                command.DriverLicenseImage
            )
            {
                Id = 1
            };

            _driverRepositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Core.Entities.Driver, bool>>>()))
                                 .ReturnsAsync(Enumerable.Empty<Core.Entities.Driver>());

            _driverRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Core.Entities.Driver>()))
                                 .Callback<Core.Entities.Driver>(d => d.Id = 1)
                                 .ReturnsAsync(driverCreated);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(1);
            _driverRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Core.Entities.Driver>()), Times.Once);
        }

        [Fact]
        public async Task WhenCreateDriver_ShoudThrowException_WhenDriveLicenseNumberIsDuplicated()
        {
            var command = new CreateDriverCommand(
                _identifier,
                _name,
                _companyNumber,
                _birthDate,
                _driverLicenseNumber,
                _driverlicenseType,
                _driveLicenseImage
            );

            _driverRepositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Core.Entities.Driver, bool>>>()))
                                 .ReturnsAsync(new[] { _documentDuplicated });

            var act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<CreateDriverCompanyNumberRegisteredException>();
        }

        [Fact]
        public async Task WhenCreateDriver_ShoudThrowException_WhenCompanyNumberIsDuplicated()
        {
            var command = new CreateDriverCommand(
                 _identifier,
                 _name,
                 _companyNumber,
                 _birthDate,
                 _driverLicenseNumber,
                 _driverlicenseType,
                 _driveLicenseImage
             );

            _driverRepositoryMock.SetupSequence(r => r.FindAsync(It.IsAny<Expression<Func<Core.Entities.Driver, bool>>>()))
                                 .ReturnsAsync(Enumerable.Empty<Core.Entities.Driver>())
                                 .ReturnsAsync(new[] { _documentDuplicated });

            var act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<CreateDriverDriveLicenseRegisteredException>();
        }
    }
}
