using FluentAssertions;
using manager_retals.Core.Commands.Motorcycle;

namespace manager_retals.Unit_test.Commands.Motorcycle
{
    public class CreateMotorcycleCommandTest
    {
        private readonly string _identifierCase = "ID123";
        private readonly int _yearCase = 2020;
        private readonly string _modelCase = "Honda CG 160";
        private readonly string _placeCase = "ABC-1234";

        public CreateMotorcycleCommandTest() { }

        [Fact]
        public void When_Create_Motorycle_ShouldCreateMotorcycleCommand()
        {
            var command = new CreateMotorcycleCommand(_identifierCase, _yearCase, _modelCase, _placeCase);
            command.Identifier.Should().Be(_identifierCase);
            command.Year.Should().Be(_yearCase);
            command.Model.Should().Be(_modelCase);
            command.Place.Should().Be(_placeCase);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WhenCreateMotorycleNotPassingIdentifier__ShouldCreateMotorcycleCommand(string identifier)
        {
            Action act = () => new CreateMotorcycleCommand(identifier, _yearCase, _modelCase, _placeCase);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("identifier");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WhenCreateMotorycleNotPassingModel__ShouldCreateMotorcycleCommand(string model)
        {
            Action act = () => new CreateMotorcycleCommand(_identifierCase, _yearCase, model, _placeCase);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("model");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WhenCreateMotorycleNotPassingPlaceOrInvalidPlace__ShouldCreateMotorcycleCommand(string plate)
        {
            Action act = () => new CreateMotorcycleCommand(_identifierCase, _yearCase, _modelCase, plate);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("plate");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenCreateMotorycleNotPassingYear__ShouldCreateMotorcycleCommand(int year)
        {
            Action act = () => new CreateMotorcycleCommand(_identifierCase, year, _modelCase, _placeCase);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("year");
        }
    }
}
