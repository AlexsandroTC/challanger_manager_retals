using FluentAssertions;
using manager_retals.Core.Commands.Motorcycle;

namespace manager_retals.Unit_test.Commands.Motorcycle
{
    public class RemoveMotorcycleCommandTest
    {
        private readonly int _id = 1;

        public RemoveMotorcycleCommandTest() { }

        [Fact]
        public void When_Remove_Motorycle_ShouldRemoveMotorcycleCommand()
        {
            var command = new RemoveMotorcycleCommand(_id);
            command.Id.Should().Be(_id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenCreateMotorycleNotPassingID__ShouldThowExpection(int id)
        {
            Action act = () => new RemoveMotorcycleCommand(id);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("id");
        }
    }
}
