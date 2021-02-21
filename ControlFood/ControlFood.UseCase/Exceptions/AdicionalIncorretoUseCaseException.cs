using System;

namespace ControlFood.UseCase.Exceptions
{
    public class AdicionalIncorretoUseCaseException : Exception
    {
        public AdicionalIncorretoUseCaseException()
        {

        }

        public AdicionalIncorretoUseCaseException(string message)
            : base(message)
        {

        }

        public AdicionalIncorretoUseCaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
