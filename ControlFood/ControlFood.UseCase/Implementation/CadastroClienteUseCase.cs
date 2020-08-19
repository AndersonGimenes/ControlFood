using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroClienteUseCase : CadastroBaseUseCase<Cliente>, ICadastroClienteUseCase
    {
        private readonly IGenericRepository<Cliente> _genericRepository;

        public CadastroClienteUseCase(IGenericRepository<Cliente> genericRepository)
           : base(genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Cliente BuscarPorIdentificacao(Cliente cliente)
        {
            return _genericRepository.BuscarPorId(cliente.IdentificadorUnico);
        }

    }
}
