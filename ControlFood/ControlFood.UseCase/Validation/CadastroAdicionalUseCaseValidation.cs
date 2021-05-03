using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Validation.Comum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControlFood.UseCase.Validation
{
    internal class CadastroAdicionalUseCaseValidation
    {
        internal static void ValidarRegrasParaInserir(Adicional adicional, IEnumerable<Adicional> adicionais)
        {
            // Verifica se existe outro adicional cadastrado com mesmo nome
            ComumValidation<Adicional>
                .VerificarDuplicidade(adicional, adicionais, nameof(adicional.Tipo), () => throw new AdicionalIncorretoUseCaseException(string.Format(Mensagem.Validacao.Adicional.AdicionalDuplicada, adicional.Tipo)));
        }
    }
}
