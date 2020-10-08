using ControlFood.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace ControlFood.UnitTest.UseCase.Helpers
{
    public static class HelperMock
    {
        public static List<Categoria> MockListaCategoriasPersistidas() =>
            new List<Categoria>
            {
               new Categoria{IdentificadorUnico = 1, Tipo = "Alimento"},
               new Categoria{IdentificadorUnico = 2, Tipo = "Bebida"},
               new Categoria{IdentificadorUnico = 3, Tipo = "Sobremesa"}
            };

        public static List<SubCategoria> MockListaSubCategoriasPersistidas() =>
            new List<SubCategoria>
            {
                 MockSubCategoria("Lanche", 1, idSubCategoria: 1),
                 MockSubCategoria("Pastel", 1, idSubCategoria: 2),
                 MockSubCategoria("Suco", 2, tipoCategoria: "Bebida", idSubCategoria: 3),
                 MockSubCategoria("Refrigerantes", 2, tipoCategoria: "Bebida", idSubCategoria: 4)
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

        public static List<Produto> MockListaProdutosPersistidos() => 
            new List<Produto>
            {
                MockProduto("cc350", "Coca-cola lata 350ml", idProduto: 1),
                MockProduto("cc1L", "Coca-cola 1 litro", idProduto: 2),
                MockProduto("spt2L", "Sprite 2 litros", idProduto: 3)
            };
        

        public static Produto MockProduto(string codigo, string nome, int idProduto = 0, int idSubCategoria = 4)
        {
            var produto = new Produto
            {
                CodigoInterno = codigo,
                IdentificadorUnico = idProduto,
                Nome = nome,
                ValorVenda = 5
            };

            produto.SubCategoria.IdentificadorUnico = idSubCategoria;
           
            return produto;
        }
    }
}
