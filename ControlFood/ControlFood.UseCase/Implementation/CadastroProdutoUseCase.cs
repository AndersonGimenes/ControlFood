using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;
using System.Collections.Generic;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroProdutoUseCase : CadastroBaseUseCase<Produto>, ICadastroProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public CadastroProdutoUseCase(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
            : base(produtoRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public override Produto Inserir(Produto produto)
        {
            var produtos = base.BuscarTodos();
            var categorias = _categoriaRepository.BuscarTodos();

            CadastroProdutoUseCaseValidation.ValidarRegrasParaInserir(produto, produtos, categorias);

            return base.Inserir(produto);
        }

        public override void Deletar(Produto produto)
        {
            //var estoques = _estoqueRepository.BuscarTodos();

            //CadastroProdutoUseCaseValidation.ValidarRegrasParaDeletar(produto, estoques);

            //base.Deletar(produto);
        }

        public void AtualizarProduto(Produto produto)
        {
            base.Atualizar(produto, new List<string> { nameof(produto.ValorVenda) });
        }
    }
}
