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
                MockSubCategoria("Lanche", 1),
                MockSubCategoria("Pastel", 1)
           };

        public static SubCategoria MockSubCategoria(string tipo, int idCategoria)
        {
            var subCategoria = new SubCategoria
            {
                Tipo = tipo,
                IndicadorItemCozinha = true
            };

            subCategoria.Categoria = new Categoria { Tipo = "Alimento", IdentificadorUnico = idCategoria };

            return subCategoria;
        }

    }
}
