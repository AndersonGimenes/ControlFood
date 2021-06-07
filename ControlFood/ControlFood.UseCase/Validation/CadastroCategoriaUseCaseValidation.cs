using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.Domain.Entidades.Produto;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Validation.Comum;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroCategoriaUseCaseValidation
    {
       
        internal static void ValidarRegrasParaInserir(Categoria categoria, IEnumerable<Categoria> categorias)
        {
            // Verifica se existe outra categoria cadastrada com mesmo nome
            ComumValidation<Categoria>
                .VerificarDuplicidade(categoria, categorias, nameof(categoria.Tipo), () => throw new CategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.Categoria.CategoriaDuplicada, categoria.Tipo)));
        }

        internal static void ValidarRegrasParaDeletar(int idCategoria, IEnumerable<ProdutoVenda> produtos)
        {
            var produtosCast = produtos.Cast<object>().ToList();

            // Verfica se existe algum produto vinculado a categoria a ser deletada
            ComumValidation<Categoria>
                .VerificarVinculoParaDeletar(
                    idCategoria,
                    produtosCast, 
                    nameof(ProdutoVenda.CategoriaIdentificadorUnico), 
                    () => throw new CategoriaIncorretaUseCaseException(Mensagem.Validacao.Categoria.CategoriaVinculadaAProduto)
                );
        }
    }
}
