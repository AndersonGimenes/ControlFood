using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroSubCategoriaUseCaseValidation
    {
        internal static void VerificarSubCategoriaVinculada(SubCategoria subCategoria, List<Produto> produtos)
        {
            if (produtos.Any(x => x.SubCategoria.IdentificadorUnico == subCategoria.IdentificadorUnico))
                throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.SubCategoria.ProdutoVinculadoASubCategoria, subCategoria.Tipo));
        }

        internal static void VerificarTiposAtaulizacao(SubCategoria subCategoriaPersistida, SubCategoria subCategoria)
        {
            if (subCategoria.Tipo != subCategoriaPersistida.Tipo)
                throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.Comum.EdicaoInvalida, nameof(subCategoria.Tipo)));

            if (subCategoria.Categoria.IdentificadorUnico != subCategoriaPersistida.Categoria.IdentificadorUnico)
                throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.Comum.EdicaoInvalida, nameof(subCategoria.Categoria.IdentificadorUnico)));
        }

        internal static void VerificarCategoriaVinculada(SubCategoria subCategoria, List<Categoria> categorias)
        {
            if(!categorias.Any(c => c.IdentificadorUnico == subCategoria.Categoria.IdentificadorUnico))            
                throw new SubCategoriaIncorretaUseCaseException(Mensagem.Validacao.SubCategoria.CategoriaNaoVinculadaASubCategoria);
        }

        internal static void VerificarDuplicidade(SubCategoria subCategoria, List<SubCategoria> subCategorias)
        {
            if (subCategorias.Any(s => s.Tipo == subCategoria.Tipo))
                throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.SubCategoria.SubCategoriaDuplicada, subCategoria.Tipo));
        }
    }
}
