using FluentAssertions;
using manager_retals.Core.Commands.Rental;
using manager_retals.Core.Enums;
using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using Moq;

namespace manager_retals.Unit_test.Commands.Rental
{
    public class CreateRentalHandlerTest
    {
        private readonly Mock<IRentalRepository> _rentalRepositoryMock;
        private readonly Mock<IMotorcycleRepository> _motorcycleRepositoryMock;
        private readonly Mock<IDriverRepository> _driverRepositoryMock;
        private readonly CreateRentalHandler _handler;

        public readonly Core.Entities.Driver _driverStub = new Core.Entities.Driver("id", "name", "12345678901", DateTime.UtcNow.AddYears(-20), "9999999", DriverLicenseType.A, "img.png");
        public readonly Core.Entities.Motorcycle _motocycleStub = new Core.Entities.Motorcycle("Moto123", "Model", 2023, "ABC1234");

        public CreateRentalHandlerTest()
        {
            _rentalRepositoryMock = new Mock<IRentalRepository>();
            _motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            _driverRepositoryMock = new Mock<IDriverRepository>();
            _handler = new CreateRentalHandler(_rentalRepositoryMock.Object, _motorcycleRepositoryMock.Object, _driverRepositoryMock.Object);
        }


        [Fact]
        public async Task WhenRentalMotocycle_ShouldCreateRental_WhenDataIsValid()
        {
            var command = new CreateRentalCommand(1, 1, RentalPlan.SevenDays, DateTime.Now, DateTime.Now, DateTime.Now);

            _driverRepositoryMock.Setup(x => x.GetByIdAsync(command.DriverId))
                                 .ReturnsAsync(_driverStub);

            _motorcycleRepositoryMock.Setup(x => x.GetByIdAsync(command.MotorcycleId))
                                     .ReturnsAsync(_motocycleStub);

            _rentalRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Core.Entities.Rental>()))
                                 .ReturnsAsync((Core.Entities.Rental rental) =>
                                 {
                                     rental.Id = 42;
                                     return rental;
                                 });

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(42);
            _rentalRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Core.Entities.Rental>()), Times.Once);
        }

        [Fact]
        public async Task WhenRentalMotocycle_ShouldThrowException_WhenDriverIsNotFound()
        {
            var command = new CreateRentalCommand(1, 99, RentalPlan.SevenDays, DateTime.Now, DateTime.Now, DateTime.Now);
            _driverRepositoryMock.Setup(x => x.GetByIdAsync(command.DriverId))
                                 .ReturnsAsync((Core.Entities.Driver)null);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<RentalDriverIsNotFoundException>();
        }

        [Fact]
        public async Task WhenRentalMotocycle_ShouldThrowException_WhenDriverHasLicenseTypeB()
        {
            var command = new CreateRentalCommand(1, 1, RentalPlan.SevenDays, DateTime.Now, DateTime.Now, DateTime.Now);

            _driverStub.DriverLicenseType = DriverLicenseType.B;

            _driverRepositoryMock.Setup(x => x.GetByIdAsync(command.DriverId))
                                 .ReturnsAsync(_driverStub);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<RentalDriverWithIncompatibleDriveLicenseException>();
        }

        [Fact]
        public async Task WhenRentalMotocycle_ShouldThrowException_WhenMotorcycleIsNotFound()
        {
            var command = new CreateRentalCommand(1, 1, RentalPlan.SevenDays, DateTime.Now, DateTime.Now, DateTime.Now);

            _driverRepositoryMock.Setup(x => x.GetByIdAsync(command.DriverId))
                                 .ReturnsAsync(_driverStub);

            _motorcycleRepositoryMock.Setup(x => x.GetByIdAsync(command.MotorcycleId))
                                     .ReturnsAsync((Core.Entities.Motorcycle)null);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<RentalMotorcyleIsNotFoundException>();
        }
    }
}
