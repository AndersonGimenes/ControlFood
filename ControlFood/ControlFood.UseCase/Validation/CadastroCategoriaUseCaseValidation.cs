using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation
{
    internal static class CadastroCategoriaUseCaseValidation
    {
        internal static void VerificarDuplicidade(Categoria categoria, List<Categoria> categorias)
        {
            if (categorias.Any(c => c.Tipo == categoria.Tipo))
                throw new CategoriaIncorretaUseCaseException(string.Format(Domain.Constantes.Mensagem.Validacao.Categoria.CategoriaDuplicada, categoria.Tipo));
        }

        internal static void VerificarSubCategoriaVinculada(Categoria categoria, List<SubCategoria> subCategorias)
        {
            if (subCategorias.Any(x => x.Categoria.IdentificadorUnico == categoria.IdentificadorUnico))
                throw new CategoriaIncorretaUseCaseException(string.Format(Domain.Constantes.Mensagem.Validacao.Categoria.CategoriaVinculadaASubCategoria, categoria.Tipo));
        }

    }
}
