using FluentAssertions;
using manager_retals.Core.Commands.Driver;
using manager_retals.Core.Commands.Motorcycle;
using manager_retals.Core.Enums;

namespace manager_retals.Unit_test.Commands.Driver
{
    public class CreateDriverCommandTests
    {
        private readonly string _identifier = "ID123";
        private readonly string _name = "Name unit test";
        private readonly string _companyNumber = "12345678912";
        private readonly string _driverLicenseNumber = "12345687";
        private readonly DateTime _birthDate = DateTime.Now.AddYears(-18);
        private readonly DriverLicenseType? _driverlicenseType = DriverLicenseType.AB;
        private readonly string _driveLicenseImage = "imageDriveLicense.png";

        [Fact]
        public void WhenCreateDriver_ShouldSetProprerties_WhenValidArgunments()
        {
            var command = new CreateDriverCommand(_identifier, _name, _companyNumber, _birthDate, _driverLicenseNumber, _driverlicenseType, _driveLicenseImage );
            command.Identifier.Should().Be(_identifier);
            command.Name.Should().Be(_name);
            command.CompanyNumber.Should().Be(_companyNumber);
            command.DriverLicenseNumber.Should().Be(_driverLicenseNumber);
            command.DriverLicenseType.Should().Be(_driverlicenseType);
            command.DriverLicenseImage.Should().Be(_driveLicenseImage);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WhenCreateDriver__ShouldThrowException_WhenIdentifierIsInvalid(string identifier)
        {
            Action act = () => new CreateDriverCommand(identifier, _name, _companyNumber, _birthDate, _driverLicenseNumber, _driverlicenseType, _driveLicenseImage);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("identifier");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WhenCreateDriver__ShouldThrowException_WhenNameIsInvalid(string name)
        {
            Action act = () => new CreateDriverCommand(_identifier, name, _companyNumber, _birthDate, _driverLicenseNumber, _driverlicenseType, _driveLicenseImage);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("name");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WhenCreateDriver__ShouldThrowException_WhenCompanyNumberIsInvalid(string companyNumber)
        {
            Action act = () => new CreateDriverCommand(_identifier, _name, companyNumber, _birthDate, _driverLicenseNumber, _driverlicenseType, _driveLicenseImage);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("companyNumber");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void WhenCreateDriver__ShouldThrowException_WhenDriverLicenseNumberIsInvalid(string driverLicenseNumber)
        {
            Action act = () => new CreateDriverCommand(_identifier, _name, _companyNumber, _birthDate, driverLicenseNumber, _driverlicenseType, _driveLicenseImage);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("driverLicenseNumber");
        }

        [Theory]
        [InlineData(null)]
        public void WhenCreateDriver__ShouldThrowException_WhenDriverlicenseTypeInvalid(DriverLicenseType driverlicenseType)
        {
            Action act = () => new CreateDriverCommand(_identifier, _name, _companyNumber, _birthDate, _driverLicenseNumber, driverlicenseType, _driveLicenseImage);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("driverlicenseType");
        }

        [Fact]
        public void WhenCreateDriver__ShouldThrowException_WhenBirthDateIsInvalid()
        {
            Action act = () => new CreateDriverCommand(_identifier, _name, _companyNumber, DateTime.MinValue, _driverLicenseNumber, _driverlicenseType, _driveLicenseImage);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("birthDate");
        }
    }
}
