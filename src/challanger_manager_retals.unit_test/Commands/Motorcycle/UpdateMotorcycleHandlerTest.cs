using FluentAssertions;
using manager_retals.Core.Commands.Motorcycle;
using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using Moq;

namespace manager_retals.Unit_test.Commands.Motorcycle
{
    public class UpdateMotorcycleHandlerTest
    {
        private readonly Mock<IMotorcycleRepository> _motorcycleRepositoryMock;
        private readonly UpdateMotorcycleHandler _handler;

        public UpdateMotorcycleHandlerTest()
        {
            _motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            _handler = new UpdateMotorcycleHandler(_motorcycleRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdatePlate_WhenDataIsValid()
        {
            var motorcycle = new Core.Entities.Motorcycle("identifier", "ModelX", 2024, "OLD1234")
            {
                Id = 1
            };

            var command = new UpdateMotorcycleCommand(1, "NEW1234");

            _motorcycleRepositoryMock.Setup(r => r.GetByIdAsync(command.Id))
                                     .ReturnsAsync(motorcycle);

            _motorcycleRepositoryMock.Setup(r => r.GetByPlateAsync(command.Plate))
                                     .ReturnsAsync((Core.Entities.Motorcycle?)null);

            _motorcycleRepositoryMock.Setup(r => r.UpdateAsync(motorcycle))
                                     .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(1);
            motorcycle.Plate.Should().Be("NEW1234");
            _motorcycleRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.GetByPlateAsync(command.Plate), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.UpdateAsync(motorcycle), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenMotorcycleDoesNotExist()
        {
            var command = new UpdateMotorcycleCommand(2, "NEW1234");
            _motorcycleRepositoryMock.Setup(r => r.GetByIdAsync(command.Id))
                                     .ReturnsAsync((Core.Entities.Motorcycle?)null);

            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("Placa não encontrada na base de dados.");

            _motorcycleRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Core.Entities.Motorcycle>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowMotorcyclePlateAlreadyRegisteredException_WhenPlateAlreadyExists()
        {
            var motorcycle = new Core.Entities.Motorcycle("identifier", "ModelX", 2024, "OLD1234")
            {
                Id = 3
            };
            var command = new UpdateMotorcycleCommand(3, "DUPL1234");
            var existing = new Core.Entities.Motorcycle("identifier2", "ModelY", 2023, "DUPL1234")
            {
                Id = 99
            };

            _motorcycleRepositoryMock.Setup(r => r.GetByIdAsync(command.Id))
                                     .ReturnsAsync(motorcycle);

            _motorcycleRepositoryMock.Setup(r => r.GetByPlateAsync(command.Plate))
                                     .ReturnsAsync(existing);

            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<MotorcyclePlateAlreadyRegisteredException>();
            
            _motorcycleRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.GetByPlateAsync(command.Plate), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Core.Entities.Motorcycle>()), Times.Never);
        }
    }
}
