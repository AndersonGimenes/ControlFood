using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using FluentValidation;
using System;
using System.Linq;

namespace ControlFood.Domain.Validation
{
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        public CategoriaValidation()
        {
            RuleFor(x => x.Tipo)
                .NotEmpty()
                .WithMessage(x => string.Format(Constantes.Mensagem.Validacao.CampoVazio, nameof(x.Tipo)));
        }

        public void Validacao(Categoria categoria)
        {
            var errors = this.Validate(categoria);
            if (!errors.IsValid)
            {
                var mensagemErros = errors.Errors.Select(x => x.ErrorMessage);
                throw new ArgumentoInvalidoDomainException(string.Join(Environment.NewLine, mensagemErros));
            }
        }
    }
}
