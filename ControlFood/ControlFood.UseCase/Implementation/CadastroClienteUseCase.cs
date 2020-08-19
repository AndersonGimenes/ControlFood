using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroClienteUseCase : CadastroBaseUseCase<Cliente>, ICadastroClienteUseCase
    {
        public CadastroClienteUseCase(IGenericRepository<Cliente> genericRepository)
           : base(genericRepository)
        {
        }
    }
}
