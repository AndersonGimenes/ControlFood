using ControlFood.UI.Models;
using ControlFood.UseCase.Exceptions;
using FluentValidation;
using System.Linq;

namespace ControlFood.UI.Validation
{
    public class EstoqueValidation : AbstractValidator<Estoque>
    {
        public EstoqueValidation()
        {
            RuleFor(x => x.Quantidade)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);

            RuleFor(x => x.ValorCompraTotal)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);

            RuleFor(x => x.ValorCompraUnidade)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);

            RuleFor(x => x.DataValidade)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);
        }

        public void Validar(Estoque estoque)
        {
            var valida = this.Validate(estoque);

            if (!valida.IsValid)
            {
                var mensagensErros = valida.Errors.Select(x => x.ErrorMessage);
                throw new ArgumentoInvalidoDomainException(mensagensErros.FirstOrDefault());
            }
        }
    }
}
