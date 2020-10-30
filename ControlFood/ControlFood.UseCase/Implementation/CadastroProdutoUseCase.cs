using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroProdutoUseCase : CadastroBaseUseCase<Produto>, ICadastroProdutoUseCase
    {        
        private readonly IProdutoRepository _produtoRepository;
        private readonly ISubCategoriaRepository _subCategoriaRepository;

        public CadastroProdutoUseCase(IProdutoRepository produtoRepository, ISubCategoriaRepository subCategoriaRepository)
            : base(produtoRepository)
        {
            _produtoRepository = produtoRepository;
            _subCategoriaRepository = subCategoriaRepository;
        }

        public override Produto Inserir(Produto produto)
        {
            var produtos = _produtoRepository.BuscarTodos();
            var subCategorias = _subCategoriaRepository.BuscarTodos();

            CadastroProdutoUseCaseValidation.ValidarRegrasParaInserir(produto, produtos, subCategorias);

            return base.Inserir(produto);
        }
    }
}
