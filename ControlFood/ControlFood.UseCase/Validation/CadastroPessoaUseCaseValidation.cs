using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Validation.Comum;
using System;
using System.Collections.Generic;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroPessoaUseCaseValidation
    {
        internal static void ValidarRegrasParaInserir(Pessoa pessoa, List<Pessoa> pessoas)
        {
            // Verifica se existe outro cpf cadastrado
            if (pessoa.Cpf != null && pessoa.Cpf != string.Empty)
                ComumValidation<Pessoa>
                    .VerificarDuplicidade(pessoa, pessoas, nameof(pessoa.Cpf), () => throw new PessoaIncorretaUseCaseException(string.Format(Mensagem.Validacao.Pessoa.CpfDuplicado, pessoa.Cpf)));

            ComumValidation<Pessoa>
                .VerificarDuplicidade(pessoa, pessoas, nameof(pessoa.Nome), () => throw new PessoaIncorretaUseCaseException(string.Format(Mensagem.Validacao.Pessoa.NomeDuplicado, pessoa.Nome)));

            ValidarDataNascimento(pessoa);
        }

        internal static void ValidarRegrasParaAtualizar(Pessoa pessoa)
        {
            ValidarDataNascimento(pessoa);
        }

        private static void ValidarDataNascimento(Pessoa pessoa)
        {
            if (pessoa.DataNascimento >= DateTime.Today.AddYears(-10))
                throw new PessoaIncorretaUseCaseException(Mensagem.Validacao.Pessoa.DataNascimentoInavalida);
        }
    }
}
