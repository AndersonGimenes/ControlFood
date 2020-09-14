using System;

namespace ControlFood.UseCase.Exceptions
{
    public class SubCategoriaIncorretaUseCaseException : Exception
    {
        public SubCategoriaIncorretaUseCaseException()
        {

        }

        public SubCategoriaIncorretaUseCaseException(string message)
            : base(message)
        {

        }

        public SubCategoriaIncorretaUseCaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

}
