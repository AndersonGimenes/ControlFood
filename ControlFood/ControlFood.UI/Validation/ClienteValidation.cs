using ControlFood.UI.Models;
using ControlFood.UseCase.Exceptions;
using FluentValidation;
using System.Linq;

namespace ControlFood.UI.Validation
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(Constantes.Mensagem.Validacao.CampoVazio);

            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.TelefoneCelular) || !string.IsNullOrWhiteSpace(x.TelefoneFixo))
                .WithMessage(Constantes.Mensagem.Cliente.TelefoneObrigatorio);

            RuleFor(x => x.Enderecos)
                .Must(e => e.Count > 0)
                .WithMessage(Constantes.Mensagem.Cliente.EnderecoSemPreenchimento);

            RuleForEach(x => x.Enderecos)
                .Must(x => x != null)
                .SetValidator(new EnderecoValidation());
        }

        public void Validar(Cliente cliente)
        {
            var valida = this.Validate(cliente);

            if (!valida.IsValid)
            {
                var mensagensErros = valida.Errors.Select(x => x.ErrorMessage);
                throw new ArgumentoInvalidoDomainException(mensagensErros.FirstOrDefault());
            }
        }
    }

}
