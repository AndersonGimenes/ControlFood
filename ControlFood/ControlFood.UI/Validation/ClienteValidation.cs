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

            RuleFor(x => x.Endereco)
                .NotNull()
                .WithMessage(Constantes.Mensagem.Cliente.EnderecoSemPreenchimento);

            RuleFor(x => x)
                .Must(x => x.TelefoneCelular != null && x.TelefoneCelular != string.Empty && x.TelefoneFixo != null && x.TelefoneFixo != string.Empty)
                .WithMessage(Constantes.Mensagem.Cliente.TelefoneObrigatorio);
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
