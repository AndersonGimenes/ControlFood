using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroFuncionarioUseCase : CadastroBaseUseCase<Funcionario>, ICadastroFuncionarioUseCase
    {
        private readonly IGenericRepository<Funcionario> _genericRepository;

        public CadastroFuncionarioUseCase(IGenericRepository<Funcionario> genericRepository)
            : base(genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Funcionario BuscarPorIdentificacao(Funcionario funcionario)
        {
            return _genericRepository.BuscarPorId(funcionario.IdentificadorUnico);
        }

    }
}
