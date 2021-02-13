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
        public CadastroProdutoUseCase(IProdutoRepository produtoRepository)
            : base(produtoRepository)
        {
            //_subCategoriaRepository = subCategoriaRepository;
        }
        public override Produto Inserir(Produto produto)
        {
            var produtos = base.BuscarTodos();
            //var subCategorias = _subCategoriaRepository.BuscarTodos();

            //CadastroProdutoUseCaseValidation.ValidarRegrasParaInserir(produto, produtos, subCategorias);

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
