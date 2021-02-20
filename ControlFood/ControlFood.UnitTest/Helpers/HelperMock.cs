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
               new Categoria{IdentificadorUnico = 1, Tipo = "Lanches"},
               new Categoria{IdentificadorUnico = 2, Tipo = "Cervejas"},
               new Categoria{IdentificadorUnico = 3, Tipo = "Porções"},
               new Categoria{IdentificadorUnico = 4, Tipo = "Refrigerantes"}
            };

        public static List<Produto> MockListaProdutosPersistidos() =>
            new List<Produto>
            {
                MockProduto("cc350", "Coca-cola lata 350ml", idProduto: 1, idCategoria: 4, adicionais: null),
                MockProduto("cc1L", "Coca-cola 1 litro", idProduto: 2, idCategoria: 4, adicionais: null),
                MockProduto("spt2L", "Sprite 2 litros", idProduto: 3, idCategoria: 4, adicionais: null),
                MockProduto("XT001", "X-TUDO", idProduto: 4, idCategoria: 1, ListaMockAdicionaisPersistidos())
            };

        public static Produto MockProduto(string codigo, string nome, int idProduto, int idCategoria, List<Adicional> adicionais)
        {
            var produto = new Produto
            {
                CodigoInterno = codigo,
                IdentificadorUnico = idProduto,
                Nome = nome,
                ValorVenda = 5
            };

            produto.Categoria = new Categoria { IdentificadorUnico = idCategoria };
            if (!(adicionais is null))
            {
                produto.Adicionais = new List<Adicional>();
                adicionais.AddRange(adicionais);
            }

            return produto;
        }

        private static List<Adicional> ListaMockAdicionaisPersistidos() =>
            new List<Adicional>
            {
                new Adicional
                {
                    IdentificadorUnico = 1,
                    Tipo = "Bacon",
                    Valor = 2.00m
                },
                new Adicional
                {
                    IdentificadorUnico = 2,
                    Tipo = "Hamburguer",
                    Valor = 7.00m
                },
                new Adicional
                {
                    IdentificadorUnico = 3,
                    Tipo = "Mussarela",
                    Valor = 2.00m
                }
            };
    }
}
