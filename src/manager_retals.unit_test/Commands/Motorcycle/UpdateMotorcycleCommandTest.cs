using FluentAssertions;
using manager_retals.Core.Commands.Motorcycle;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using manager_retals.Core.Commands.Motorcycle;
using manager_retals.Core.Entities;
using manager_retals.Core.Exceptions;
using manager_retals.Core.Repositories;
using Moq;
using Xunit;

namespace manager_retals.Unit_test.Commands.Motorcycle
{
    public class UpdateMotorcycleCommandTest
    {
        private readonly int _id = 1;
        private readonly string _plateCase = "ABC-1234";

        [Fact]
        public void WhenUpdateMotorcycle_ShouldSetProperties_WhenValidArguments()
        {
            var command = new UpdateMotorcycleCommand(_id, _plateCase);
            command.Id.Should().Be(_id);
            command.Plate.Should().Be(_plateCase);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenUpdateMotorcycle_ShouldThrowArgumentNullException_WhenPlateIsNullOrWhiteSpace(int id)
        {

            Action act = () => new UpdateMotorcycleCommand(id, _plateCase);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("id");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void WhenUpdateMotorcycle_ShouldThrowArgumentNullException_WhenIdIsNegative(string plate)
        {
            Action act = () => new UpdateMotorcycleCommand(_id, plate);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("plate");
        }
    }
}