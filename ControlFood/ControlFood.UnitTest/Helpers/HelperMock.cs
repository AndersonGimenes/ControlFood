using ControlFood.Domain.Entidades;
using System.Collections.Generic;

namespace ControlFood.UnitTest.UseCase.Helpers
{
    public static class HelperMock
    {
        public static List<Categoria> MockListaCategoriasPersistidas()
        {
            return new List<Categoria>
            {
                new Categoria{IdentificadorUnico = 1, Tipo = "Alimento"},
                new Categoria{IdentificadorUnico = 2, Tipo = "Bebida"},
                new Categoria{IdentificadorUnico = 3, Tipo = "Sobremesa"}
            };
        }

        public static List<SubCategoria> MockListaSubCategoriasPersistidas() =>
           new List<SubCategoria> {
                MockSubCategoria("Lanche", 1, idSubCategoria: 1),
                MockSubCategoria("Pastel", 1, idSubCategoria: 2),
                MockSubCategoria("Suco", 2, tipoCategoria: "Bebida", idSubCategoria: 3)
           };

        public static SubCategoria MockSubCategoria(string tipo, int idCategoria, string tipoCategoria = "Alimento", int idSubCategoria = 0)
        {
            var subCategoria = new SubCategoria
            {
                IdentificadorUnico = idSubCategoria,
                Tipo = tipo,
                IndicadorItemCozinha = true
            };

            subCategoria.Categoria = new Categoria { Tipo = tipoCategoria, IdentificadorUnico = idCategoria };

            return subCategoria;
        }

    }
}
