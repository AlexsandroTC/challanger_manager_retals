using manager_retals.Core.Enums;
using MediatR;

namespace manager_retals.Core.Commands.Driver
{
    public class CreateDriverCommand : IRequest<int>
    {
        public string Identifier { get; set; }
        public string Name { get; set; }

        // TODO Criar um value object para CNPJ
        public string CompanyNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string DriverLicenseNumber { get; set; }
        public DriverLicenseType DriverLicenseType { get; set; }
        public string DriverLicenseImage { get; set; }

        public CreateDriverCommand(string identifier, string name, string companyNumber, DateTime birthDate, string driverLicenseNumber, DriverLicenseType? driverLicenseTypehType, string driverLicenseImage)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(identifier)) throw new ArgumentNullException(nameof(identifier));
            if (DateTime.MinValue == birthDate) throw new ArgumentNullException(nameof(birthDate));
            if (string.IsNullOrWhiteSpace(companyNumber)) throw new ArgumentNullException(nameof(companyNumber));
            if (driverLicenseTypehType == null) throw new ArgumentNullException(nameof(driverLicenseTypehType));
            if (string.IsNullOrWhiteSpace(driverLicenseImage)) throw new ArgumentNullException(nameof(driverLicenseImage));

            Identifier = identifier;
            Name = name;
            CompanyNumber = companyNumber;
            BirthDate = birthDate;
            DriverLicenseNumber = driverLicenseNumber;
            DriverLicenseType = (DriverLicenseType)driverLicenseTypehType;
            DriverLicenseImage = driverLicenseImage;
        }
    }
}
