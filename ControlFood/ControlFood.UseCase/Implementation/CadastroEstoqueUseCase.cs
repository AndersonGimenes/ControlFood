using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroEstoqueUseCase : CadastroBaseUseCase<Estoque>, ICadastroEstoqueUseCase
    {
        private readonly IProdutoRepository _produtoRepository;

        public CadastroEstoqueUseCase(IEstoqueRepository estoqueRepository, IProdutoRepository produtoRepository) 
            : base(estoqueRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Produto InserirEstoque(Produto produto)
        {
            var produtos = _produtoRepository.BuscarTodos();

            CadastroEstoqueUseCaseValidation.ValidarRegrasParaInserir(produto, produtos);

            produto.Estoque.AtribuirDataDeEntrada();

            base.Inserir(produto.Estoque);

            // ajustar 
            return default;
        }

        
    }
}
