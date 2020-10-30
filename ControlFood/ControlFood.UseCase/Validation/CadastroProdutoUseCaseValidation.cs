using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroProdutoUseCaseValidation
    {
        internal static void ValidarRegrasParaInserir(Produto produto, List<Produto> produtos, List<SubCategoria> subCategorias)
        {
            VerifcarDuplicidade(produto, produtos);
            VerificarSubCategoriaVinculadada(subCategorias, produto);
        }

        #region[ PRIVADOS ]
        private static void VerifcarDuplicidade(Produto produto, List<Produto> produtos)
        {
            if (produtos.Any(p => p.Nome == produto.Nome))
                throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorNome, produto.Nome));

            if (produtos.Any(p => p.CodigoInterno == produto.CodigoInterno))
                throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorCodigo, produto.CodigoInterno));
        }

        private static void VerificarSubCategoriaVinculadada(List<SubCategoria> subCategorias, Produto produto)
        {
            if (!subCategorias.Any(s => s.IdentificadorUnico == produto.SubCategoria.IdentificadorUnico))
                throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.SubCategoriaNaoVinculadaAoProduto);
        }
        #endregion
    }
}
