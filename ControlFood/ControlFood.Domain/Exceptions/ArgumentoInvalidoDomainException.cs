using System;

namespace ControlFood.UseCase.Exceptions
{
    public class ArgumentoInvalidoDomainException : Exception
    {
        public ArgumentoInvalidoDomainException()
        {

        }

        public ArgumentoInvalidoDomainException(string message)
            : base(message)
        {

        }

        public ArgumentoInvalidoDomainException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

}
