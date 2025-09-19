
using manager_retals.Core.Enums;

namespace manager_retals.Core.Entities
{
    public class Driver : BaseEntity
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string CompanyNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string DriverLicenseNumber { get; set; }

        //TODO: Criar um enum para DriverLicenseType
        public DriverLicenseType DriverLicenseType { get; set; }
        public string DriverLicenseImagePath { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();


        public Driver(string identifier, string name, string companyNumber, DateTime birthDate, string driverLicenseNumber, DriverLicenseType driverLicenseType, string driverLicenseImagePath)
        {
            Identifier = identifier;
            Name = name;
            CompanyNumber = companyNumber;
            BirthDate = birthDate;
            DriverLicenseNumber = driverLicenseNumber;
            DriverLicenseType = driverLicenseType;
            DriverLicenseImagePath = driverLicenseImagePath;
        }
    }
}
