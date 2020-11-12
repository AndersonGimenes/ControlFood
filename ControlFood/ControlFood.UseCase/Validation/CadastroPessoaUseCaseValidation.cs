using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Validation.Comum;
using System.Collections.Generic;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroPessoaUseCaseValidation
    {
        internal static void ValidarRegrasParaInserir(Pessoa pessoa, List<Pessoa> pessoas)
        {
            // Verifica se existe outro cpf cadastrado
            ComumValidation<Pessoa>
                .VerificarDuplicidade(pessoa, pessoas, nameof(pessoa.Cpf), () => throw new PessoaIncorretaUseCaseException(string.Format(Mensagem.Validacao.Pessoa.CpfDuplicado, pessoa.Cpf)));
        }
    }
}
