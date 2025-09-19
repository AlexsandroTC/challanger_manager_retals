namespace manager_retals.Core.Exceptions
{
    public class DriverDocumentRegisteredException : BusinessException
    {
        public DriverDocumentRegisteredException(string place) : base($"A placa {place} já esta cadastrada no sistema.") { }
    }
}
