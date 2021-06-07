using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.Domain.Entidades.Produto;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Validation.Comum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroProdutoUseCaseValidation
    {
        internal static void ValidarRegrasParaInserir(ProdutoVenda produto, IEnumerable<ProdutoVenda> produtos, IEnumerable<Categoria> categorias, IEnumerable<Adicional> adcionais)
        {
            // Verifica se existe outro produto cadastrado com mesmo nome
            ComumValidation<ProdutoVenda>
                .VerificarDuplicidade(produto, produtos, nameof(produto.Nome), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorNome, produto.Nome)));

            // Verifica se existe outro produto cadastrado com mesmo codigo interno
            ComumValidation<ProdutoVenda>
                .VerificarDuplicidade(produto, produtos, nameof(produto.CodigoInterno), () => throw new ProdutoIncorretoUseCaseException(string.Format(Mensagem.Validacao.Produto.ProdutoDuplicadoPorCodigo, produto.CodigoInterno)));

            // Verifica se existe categoria vinculada para inserir o produto
            ComumValidation<Categoria>
                .VerificarVinculoInserir(produto.CategoriaIdentificadorUnico, categorias, nameof(Categoria.IdentificadorUnico), () => throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.CategoriaNaoVinculadaAoProduto));

            // Verifica se algum adicional não existe na base
            if(produto.AdicionaisIdentificadoresUnico.Count > 0 && !produto.AdicionaisIdentificadoresUnico.TrueForAll(p => adcionais.Any(a => a.IdentificadorUnico == p)))
                throw new ProdutoIncorretoUseCaseException(Mensagem.Validacao.Produto.ProdutoSemAdicional);
        }
    }
}
