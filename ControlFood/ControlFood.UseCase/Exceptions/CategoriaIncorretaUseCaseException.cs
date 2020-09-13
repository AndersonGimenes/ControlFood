using System;

namespace ControlFood.UseCase.Exceptions
{
    public class CategoriaIncorretaUseCaseException : Exception
    {
        public CategoriaIncorretaUseCaseException()
        {

        }

        public CategoriaIncorretaUseCaseException(string message)
            : base(message)
        {

        }

        public CategoriaIncorretaUseCaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

}
