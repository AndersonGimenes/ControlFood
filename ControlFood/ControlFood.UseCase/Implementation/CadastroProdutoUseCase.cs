using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroProdutoUseCase : CadastroBaseUseCase<Produto>, ICadastroProdutoUseCase
    {
        private readonly IGenericRepository<Produto> _genericRepository;
        public CadastroProdutoUseCase(IGenericRepository<Produto> genericRepository)
            : base(genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Produto BuscarPorIdentificacao(Produto produto)
        {
            return _genericRepository.BuscarPorId(produto.IdentificadorUnico);
        }
    }
}
