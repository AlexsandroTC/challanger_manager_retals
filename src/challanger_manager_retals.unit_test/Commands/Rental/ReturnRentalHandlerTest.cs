using FluentAssertions;
using manager_retals.Core.Commands.Rental;
using manager_retals.Core.Entities;
using manager_retals.Core.Enums;
using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using Moq;

namespace manager_retals.Unit_test.Commands.Rental
{
    public class ReturnRentalHandlerTest
    {
        private readonly Mock<IRentalRepository> _rentalRepositoryMock;
        private readonly ReturnRentalHandler _handler;

        private Core.Entities.Rental _rentalStub = 
            new Core.Entities.Rental 
            { 
                Id = 99,
                EndDate = DateTime.UtcNow.AddDays(-3),
                TotalPrice = 100,
                Plan = RentalPlan.SevenDays
            };


        public ReturnRentalHandlerTest()
        {
            _rentalRepositoryMock = new Mock<IRentalRepository>();
            _handler = new ReturnRentalHandler(_rentalRepositoryMock.Object);
        }

        [Fact]
        public async Task WhenRentalMotocycle_ShouldUpdateRental_WhenDataIsValid()
        {
            var command = new ReturnRentalCommand(99, DateTime.UtcNow);

            _rentalRepositoryMock.Setup(r => r.GetByIdAsync(command.Id))
                                 .ReturnsAsync(_rentalStub);

            _rentalRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Core.Entities.Rental>()))
                                 .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(command.Id);
            _rentalRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
            _rentalRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Core.Entities.Rental>()), Times.Once);
        }

        [Fact]
        public async Task WhenRentalMotocycle_ShouldThrowExepction_WhenRentalNotFound()
        {
            var command = new ReturnRentalCommand(99, DateTime.UtcNow);

            _rentalRepositoryMock.Setup(r => r.GetByIdAsync(command.Id))
                                 .ReturnsAsync((Core.Entities.Rental?)null);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<RentalDriverIsNotFoundException>();

            _rentalRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Core.Entities.Rental>()), Times.Never);
        }
    }
}
