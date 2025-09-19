using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manager_retals.Core.Exceptions
{
    public class RentalDriverIsNotFoundException : BusinessException
    {
        public RentalDriverIsNotFoundException() : base("Entregador não encontrado.") { }
    }

    public class RentalMotorcyleIsNotFoundException : BusinessException
    {
        public RentalMotorcyleIsNotFoundException() : base("Moto não encontrada.") { }
    }

    public class RentalDriverWithIncompatibleDriveLicenseException : BusinessException
    {
        public RentalDriverWithIncompatibleDriveLicenseException() : base("Entregador não habilitado na categoria A não pode alugar motos.") { }
    }

    public class RentalIsNotFoundException : BusinessException
    {
        public RentalIsNotFoundException() : base("Locação não encontrada.") { }
    }
}
