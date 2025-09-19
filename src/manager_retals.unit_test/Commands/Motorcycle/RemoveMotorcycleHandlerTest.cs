using FluentAssertions;
using manager_retals.Core.Commands.Motorcycle;
using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using Moq;

namespace manager_retals.Unit_test.Commands.Motorcycle
{
    public class RemoveMotorcycleHandlerTest
    {
        private readonly Mock<IMotorcycleRepository> _motorcycleRepositoryMock;
        private readonly RemoveMotorcycleHandler _handler;

        public RemoveMotorcycleHandlerTest()
        {
            _motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            _handler = new RemoveMotorcycleHandler(_motorcycleRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldRemoveMotorcycle_SucessfullCase()
        {
            var motorcycle = new Core.Entities.Motorcycle("identifier", "ModelX", 2024, "ABC1234")
            {
                Id = 1,
                Rentals = new List<Core.Entities.Rental>()
            };

            _motorcycleRepositoryMock.Setup(r => r.GetByIdAsync(1))
                                     .ReturnsAsync(motorcycle);

            _motorcycleRepositoryMock.Setup(r => r.DeleteAsync(motorcycle))
                                     .Returns(Task.CompletedTask);

            var command = new RemoveMotorcycleCommand(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(1);
            _motorcycleRepositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.DeleteAsync(motorcycle), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenMotorcycleDoesNotExist()
        {

            _motorcycleRepositoryMock.Setup(r => r.GetByIdAsync(2))
                                     .ReturnsAsync((Core.Entities.Motorcycle?)null);

            var command = new RemoveMotorcycleCommand(2);

            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Placa não encontrada na base de dados.");

            _motorcycleRepositoryMock.Verify(r => r.GetByIdAsync(2), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Core.Entities.Motorcycle>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowDeleteMotorcyclePlateWithRentalsException_WhenRentalsExist()
        {
            var motorcycle = new Core.Entities.Motorcycle("identifier", "ModelY", 2023, "XYZ9876")
            {
                Id = 3,
                Rentals = new List<Core.Entities.Rental> { new Core.Entities.Rental() }
            };

            _motorcycleRepositoryMock.Setup(r => r.GetByIdAsync(3))
                                     .ReturnsAsync(motorcycle);

            var command = new RemoveMotorcycleCommand(3);

            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<RemoveMotorcycleWithRentalsException>();

            _motorcycleRepositoryMock.Verify(r => r.GetByIdAsync(3), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Core.Entities.Motorcycle>()), Times.Never);
        }
    }
}