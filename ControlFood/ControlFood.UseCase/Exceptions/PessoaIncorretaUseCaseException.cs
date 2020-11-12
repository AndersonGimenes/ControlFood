using System;

namespace ControlFood.UseCase.Exceptions
{
    public class PessoaIncorretaUseCaseException : Exception
    {
        public PessoaIncorretaUseCaseException()
        {

        }

        public PessoaIncorretaUseCaseException(string message)
            : base(message)
        {

        }

        public PessoaIncorretaUseCaseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
