using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;
using System.Linq;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroClienteUseCase : CadastroBaseUseCase<Cliente>, ICadastroClienteUseCase
    {
        public CadastroClienteUseCase(IClienteRepository clienteRepository)
           : base(clienteRepository)
        {
        }

        public override Cliente Inserir(Cliente cliente)
        {
            var clientes = base.BuscarTodos();

            var clientesCast = clientes
                                .Cast<Pessoa>()
                                .Where(x => x.Cpf != null)
                                .ToList();

            CadastroPessoaUseCaseValidation.ValidarRegrasParaInserir(cliente, clientesCast);
            cliente.Endereco.AtribuirDataCadastro();

            return base.Inserir(cliente);
        }
    }
}
