namespace manager_retals.Core.Exceptions
{
    public class MotorcyclePlateAlreadyRegisteredException : BusinessException
    {
        public string Place { get; }

        public MotorcyclePlateAlreadyRegisteredException(string place) : base($"A placa {place} já esta cadastrada no sistema.")
        {
            Place = place;
        }
    }

    public class DeleteMotorcyclePlateWithRentalsException : BusinessException
    {
        public DeleteMotorcyclePlateWithRentalsException(string message) : base(message) { }
    }
}
