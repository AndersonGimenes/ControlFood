using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;

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

            VerifcarDuplicidade(produto, produtos);
            VerificarSubCategoriaVinculadada(subCategorias, produto);

            return base.Inserir(produto);
        }

        private void VerifcarDuplicidade(Produto produto, List<Produto> produtos)
        {
            if (produtos.Any(p => p.Nome == produto.Nome))
                throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorNome, produto.Nome));

            if (produtos.Any(p => p.CodigoInterno == produto.CodigoInterno))
                throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorCodigo, produto.CodigoInterno));
        }

        private void VerificarSubCategoriaVinculadada(List<SubCategoria> subCategorias, Produto produto)
        {
            var existeSubCategoriaVinculada = subCategorias.Any(s => s.IdentificadorUnico == produto.SubCategoria.IdentificadorUnico);
            if (!existeSubCategoriaVinculada)
                throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.SubCategoriaNaoVinculadaAoProduto);
        }
    }
}
