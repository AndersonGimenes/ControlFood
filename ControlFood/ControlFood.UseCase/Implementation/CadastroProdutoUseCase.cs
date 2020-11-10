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
        private readonly ISubCategoriaRepository _subCategoriaRepository;
        private readonly IEstoqueRepository _estoqueRepository;

        public CadastroProdutoUseCase(IProdutoRepository produtoRepository, ISubCategoriaRepository subCategoriaRepository, IEstoqueRepository estoqueRepository)
            : base(produtoRepository)
        {
            _produtoRepository = produtoRepository;
            _subCategoriaRepository = subCategoriaRepository;
            _estoqueRepository = estoqueRepository;
        }

        public override Produto Inserir(Produto produto)
        {
            var produtos = _produtoRepository.BuscarTodos();
            var subCategorias = _subCategoriaRepository.BuscarTodos();

            CadastroProdutoUseCaseValidation.ValidarRegrasParaInserir(produto, produtos, subCategorias);

            return base.Inserir(produto);
        }

        public override void Deletar(Produto produto)
        {
            var estoques = _estoqueRepository.BuscarTodos();

            CadastroProdutoUseCaseValidation.ValidarRegrasParaDeletar(produto, estoques);
            
            base.Deletar(produto);
        }

        public void AtualizarProduto(Produto produto)
        {
            base.Atualizar(produto, new List<string> { nameof(produto.ValorVenda) });
        }
    }
}
