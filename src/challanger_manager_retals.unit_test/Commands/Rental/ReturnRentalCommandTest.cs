using FluentAssertions;
using manager_retals.Core.Commands.Rental;

namespace manager_retals.Unit_test.Commands.Rental
{
    public class ReturnRentalCommandTest
    {
        private readonly int _validId = 1;
        private readonly DateTime _validReturnDate = DateTime.UtcNow;

        [Fact]
        public void WhenReturnRental_ShouldSetProperties_WhenValidArguments()
        {
            var command = new ReturnRentalCommand(_validId, _validReturnDate);
            command.Id.Should().Be(_validId);
            command.ReturnDate.Should().Be(_validReturnDate);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenReturnRental_ShouldThrowEception_WhenIdIsInvalid(int id)
        {
            Action act = () => new ReturnRentalCommand(id, _validReturnDate);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("id");
        }

        [Fact]
        public void WhenReturnRental_ShouldThrowEception_WhenReturnDateIsInvalid()
        {
            Action act = () => new ReturnRentalCommand(_validId, DateTime.MinValue);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("returnDate");
        }
    }
}