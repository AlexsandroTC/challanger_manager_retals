using FluentAssertions;
using manager_retals.Core.Commands.Rental;
using manager_retals.Core.Enums;

namespace manager_retals.Unit_test.Commands.Rental
{
    public class CreateRentalCommandTest
    {
        private readonly int _motorcycleId = 1;
        private readonly int _driverId = 2;
        private readonly RentalPlan? _plan = RentalPlan.SevenDays;
        private readonly DateTime _startDate = new DateTime(2025, 1, 1);
        private readonly DateTime _endDate = new DateTime(2025, 1, 7);
        private readonly DateTime _expectedEndDate = new DateTime(2025, 1, 8);

        [Fact]
        public void WhenCreateRental_ShouldSetProperties_WhenValidArguments()
        {
            var command = new CreateRentalCommand(
                _motorcycleId,
                _driverId,
                _plan,
                _startDate,
                _endDate,
                _expectedEndDate
            );

            command.MotorcycleId.Should().Be(_motorcycleId);
            command.DriverId.Should().Be(_driverId);
            command.Plan.Should().Be(_plan);
            command.StartDate.Should().Be(_startDate);
            command.EndDate.Should().Be(_endDate);
            command.ExpectedEndDate.Should().Be(_expectedEndDate);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenRentalMotocycle__ShouldThrowException_WhenMotorcycleIdIsInvalid(int motorcycleId)
        {
            Action act = () => new CreateRentalCommand(motorcycleId, _driverId, _plan, _startDate, _endDate, _expectedEndDate);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("motorcycleId");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenRentalMotocycle__ShouldThrowException_WhenDriverIdIsInvalid(int driverId)
        {
            Action act = () => new CreateRentalCommand(_motorcycleId, driverId, _plan, _startDate, _endDate, _expectedEndDate);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("driverId");
        }

        [Theory]
        [InlineData(null)]
        public void WhenRentalMotocycle__ShouldThrowException_WhenPlanIsInvalid(RentalPlan plan)
        {
            Action act = () => new CreateRentalCommand(_motorcycleId, _driverId, plan, _startDate, _endDate, _expectedEndDate); 
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("plan");
        }

        [Fact]
        public void WhenRentalMotocycle__ShouldThrowException_WhenDtartDateIsInvalid()
        {
            Action act = () => new CreateRentalCommand(_motorcycleId, _driverId, _plan, DateTime.MinValue, _endDate, _expectedEndDate);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("startDate");
        }

        [Fact]
        public void WhenRentalMotocycle__ShouldThrowException_WhenEndDateIsInvalid()
        {
            Action act = () => new CreateRentalCommand(_motorcycleId, _driverId, _plan, _startDate, DateTime.MinValue, _expectedEndDate);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("endDate");
        }

        [Fact]
        public void WhenRentalMotocycle__ShouldThrowException_WhenExpectedEndDateIsInvalid()
        {
            Action act = () => new CreateRentalCommand(_motorcycleId, _driverId, _plan, _startDate, _endDate, DateTime.MinValue);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("expectedEndDate");
        }
    }
}
