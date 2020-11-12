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
        public CadastroClienteUseCase(IGenericRepository<Cliente> genericRepository)
           : base(genericRepository)
        {
        }

        public override Cliente Inserir(Cliente cliente)
        {
            // validar CPF um para cada cliente
            var clientes = base.BuscarTodos();

            var clientesCast = clientes
                                .Cast<Pessoa>()
                                .ToList();

            CadastroPessoaUseCaseValidation.ValidarRegrasParaInserir(cliente, clientesCast);

            return base.Inserir(cliente);
        }
    }
}
