using ControlFood.Domain.Entidades.Produto;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;
using System.Collections.Generic;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroProdutoUseCase : CadastroBaseUseCase<ProdutoVenda>, ICadastroProdutoUseCase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IAdicionalRepository _adicionalRepository;

        public CadastroProdutoUseCase(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IAdicionalRepository adicionalRepository)
            : base(produtoRepository)
        {
            _categoriaRepository = categoriaRepository;
            _adicionalRepository = adicionalRepository;
        }

        public override ProdutoVenda Inserir(ProdutoVenda produto)
        {
            var produtos = base.BuscarTodos();
            var categorias = _categoriaRepository.BuscarTodos();
            var adicionais = _adicionalRepository.BuscarTodos();

            CadastroProdutoUseCaseValidation.ValidarRegrasParaInserir(produto, produtos, categorias, adicionais);

            return base.Inserir(produto);
        }

        // public override void Deletar(Produto produto)
        // {
        //     //[TODO]: Implementar regras para deleção do produto

        //     //base.Deletar(produto);
        // }

        public void AtualizarProduto(ProdutoVenda produto)
        {
            base.Atualizar(produto, new List<string> { nameof(produto.ValorVenda), nameof(produto.Descricao)});
        }
    }
}
