using ControlFood.UI.Models;
using FluentValidation;

namespace ControlFood.UI.Validation
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            RuleFor(x => x.Bairro)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);

            RuleFor(x => x.Cep)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);

            RuleFor(x => x)
                .Must(x => x.Cep != null && x.Cep.Length == 8)
                .WithMessage(string.Format(Constantes.Mensagem.Validacao.CampoForaDoTamanho, nameof(Endereco.Cep), "8"));

            RuleFor(x => x.Cidade)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);
        }

    }
}
