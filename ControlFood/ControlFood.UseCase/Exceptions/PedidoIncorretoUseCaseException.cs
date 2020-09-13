using System;

namespace ControlFood.UseCase.Exceptions
{
    public class PedidoIncorretoUseCaseException : Exception
    {
        public PedidoIncorretoUseCaseException()
        {

        }

        public PedidoIncorretoUseCaseException(string message)
            : base(message)
        {

        }

        public PedidoIncorretoUseCaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

}
