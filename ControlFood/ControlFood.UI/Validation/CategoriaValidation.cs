using ControlFood.UI.Models;
using FluentValidation;

namespace ControlFood.UI.Validation
{
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        public CategoriaValidation()
        {
            RuleFor(x => x.Tipo)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);
        }

    }
}
