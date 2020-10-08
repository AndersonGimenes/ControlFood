using System;

namespace ControlFood.UseCase.Exceptions
{
    public class ProdutoIncorretoUseCaseException : Exception
    {
        public ProdutoIncorretoUseCaseException()
        {

        }

        public ProdutoIncorretoUseCaseException(string message)
            : base(message)
        {

        }

        public ProdutoIncorretoUseCaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

}
