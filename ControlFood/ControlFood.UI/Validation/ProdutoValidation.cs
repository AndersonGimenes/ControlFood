using ControlFood.UI.Models;
using ControlFood.UseCase.Exceptions;
using FluentValidation;
using System.Linq;

namespace ControlFood.UI.Validation
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);

            RuleFor(x => x.CodigoInterno)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);

            RuleFor(x => x.ValorVenda)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);
        }

        public void Validar(Produto produto)
        {
            var valida = this.Validate(produto);

            if (!valida.IsValid)
            {
                var mensagensErros = valida.Errors.Select(x => x.ErrorMessage);
                throw new ArgumentoInvalidoDomainException(mensagensErros.FirstOrDefault());
            }
        }
    }
}
