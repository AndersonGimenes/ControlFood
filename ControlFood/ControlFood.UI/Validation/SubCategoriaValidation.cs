using ControlFood.UI.Models;
using FluentValidation;

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
    }
}
