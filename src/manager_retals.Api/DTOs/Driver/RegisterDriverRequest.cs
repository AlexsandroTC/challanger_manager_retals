using manager_retals.Core.Enums;

namespace manager_retals.Api.DTOs.Driver
{
    public record RegisterDriverRequest
    {
        public string Identificador { get; set; }
        public string Name { get;  set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public DriverLicenseType CnhType { get; set; }
        public string CnhImage { get; set; }
    }
}
