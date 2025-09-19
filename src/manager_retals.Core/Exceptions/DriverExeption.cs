namespace manager_retals.Core.Exceptions
{
    public class CreateDriverDriveLicenseRegisteredException : BusinessException
    {
        public CreateDriverDriveLicenseRegisteredException(string message) : base($"A CNH já cadastrada no sistema") { }
    }

    public class CreateDriverCompanyNumberRegisteredException : BusinessException
    {
        public CreateDriverCompanyNumberRegisteredException(string message) : base($"O CNPJ já cadastrada no sistema") { }
    }
}
