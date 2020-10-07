using ControlFood.UI.Models;
using ControlFood.UseCase.Exceptions;
using FluentValidation;
using System.Linq;

namespace ControlFood.UI.Validation
{
    public class SubCategoriaValidation : AbstractValidator<SubCategoria>
    {
        public SubCategoriaValidation()
        {
            RuleFor(x => x.Tipo)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);
        }

        public void Validar(SubCategoria subCategora)
        {
            var valida = this.Validate(subCategora);

            if (!valida.IsValid)
            {
                var mensagensErros = valida.Errors.Select(x => x.ErrorMessage);
                throw new ArgumentoInvalidoDomainException(mensagensErros.FirstOrDefault());
            }
        }
    }
}
