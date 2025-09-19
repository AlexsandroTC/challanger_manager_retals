using FluentAssertions;
using manager_retals.Core.Commands.Motorcycle;
using manager_retals.Core.Exceptions;
using manager_retals.Core.Notification;
using manager_retals.Core.Repositories;
using MediatR;
using Moq;

namespace manager_retals.Unit_test.Commands.Motorcycle
{
    public class CreateMotorcycleCommandHandler
    {
        private readonly Mock<IMotorcycleRepository> _motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private readonly CreateMotorcycleHandler _handler;

        public CreateMotorcycleCommandHandler()
        {
            _motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new CreateMotorcycleHandler(_motorcycleRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateMotorcycle_Sucessfully()
        {
            var command = new CreateMotorcycleCommand("identifier1", 2024, "ModelX", "ABC1234");

            _motorcycleRepositoryMock.Setup(r => r.GetByPlateAsync(command.Place))
                                     .ReturnsAsync((Core.Entities.Motorcycle?)null);

            var createdMotorcycle = new Core.Entities.Motorcycle(command.Identifier, command.Model, command.Year, command.Place)
            {
                Id = 1
            };

            _motorcycleRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Core.Entities.Motorcycle>()))
                .ReturnsAsync(createdMotorcycle);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(1);
            _motorcycleRepositoryMock.Verify(r => r.GetByPlateAsync(command.Place), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Core.Entities.Motorcycle>()), Times.Once);
            _mediatorMock.Verify(m => m.Publish(It.IsAny<MotorcycleCreatedNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPlateAlreadyExists()
        {
            var command = new CreateMotorcycleCommand("identifier2", 2023, "ModelY", "XYZ9876");
            var existingMotorcycle = new Core.Entities.Motorcycle(command.Identifier, command.Model, command.Year, command.Place);

            _motorcycleRepositoryMock.Setup(r => r.GetByPlateAsync(command.Place))
                .ReturnsAsync(existingMotorcycle);

            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<MotorcyclePlateAlreadyRegisteredException>();

            _motorcycleRepositoryMock.Verify(r => r.GetByPlateAsync(command.Place), Times.Once);
            _motorcycleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Core.Entities.Motorcycle>()), Times.Never);
            _mediatorMock.Verify(m => m.Publish(It.IsAny<MotorcycleCreatedNotification>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
