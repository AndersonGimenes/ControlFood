﻿using ControlFood.Domain.Entidades;
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
               new Categoria{IdentificadorUnico = 3, Tipo = "Sobremesa"},
               new Categoria{IdentificadorUnico = 4, Tipo = "CategoriaTeste"}
            };

        public static List<SubCategoria> MockListaSubCategoriasPersistidas() =>
            new List<SubCategoria>
            {
                 MockSubCategoria("Lanche", idCategoria: 1, idSubCategoria: 1),
                 MockSubCategoria("Pastel", idCategoria: 1, idSubCategoria: 2),
                 MockSubCategoria("Suco", idCategoria: 2, tipoCategoria: "Bebida", idSubCategoria: 3),
                 MockSubCategoria("Refrigerantes", idCategoria: 2, tipoCategoria: "Bebida", idSubCategoria: 4),
                 MockSubCategoria("Espetos", idCategoria: 1, idSubCategoria: 5),
                 MockSubCategoria("Sobremesas", idCategoria: 3, idSubCategoria: 6)
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
                MockProduto("spt2L", "Sprite 2 litros", idProduto: 3),
                MockProduto("XT001", "X-TUDO", idProduto: 4, idSubCategoria: 1),
                MockProduto("SP001", "Sorverte de palito", idProduto: 5, idSubCategoria: 6)
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

            produto.SubCategoria = new SubCategoria { IdentificadorUnico = idSubCategoria };

            return produto;
        }

        public static List<Estoque> MockListaEstoque()
        {
            var itemLoteHum = new Estoque { IdentificadorUnico = 1, Quantidade = 10, DataValidade = DateTime.Now.AddDays(-10), ValorCompraTotal = 50.00M, ValorCompraUnidade = 5.00M };
            itemLoteHum.AtribuirIdentificadorUnicoProduto(1);

            var itemLoteDois = new Estoque { IdentificadorUnico = 2, Quantidade = 10, DataValidade = DateTime.Now.AddDays(-7), ValorCompraTotal = 50.00M, ValorCompraUnidade = 5.00M };
            itemLoteDois.AtribuirIdentificadorUnicoProduto(1);

            var itemLoteTres = new Estoque { IdentificadorUnico = 3, Quantidade = 10, DataValidade = DateTime.Now.AddDays(150), ValorCompraTotal = 50.00M, ValorCompraUnidade = 5.00M };
            itemLoteTres.AtribuirIdentificadorUnicoProduto(1);

            var itemLoteQautro = new Estoque { IdentificadorUnico = 4, Quantidade = 10, DataValidade = DateTime.Now.AddDays(170), ValorCompraTotal = 50.00M, ValorCompraUnidade = 5.00M };
            itemLoteQautro.AtribuirIdentificadorUnicoProduto(1);

            var itemLoteCinco = new Estoque { IdentificadorUnico = 5, Quantidade = 10, DataValidade = DateTime.Now.AddDays(190), ValorCompraTotal = 50.00M, ValorCompraUnidade = 5.00M };
            itemLoteCinco.AtribuirIdentificadorUnicoProduto(1);

            return new List<Estoque>
            {
                itemLoteHum,
                itemLoteDois,
                itemLoteTres,
                itemLoteQautro,
                itemLoteCinco
            };
        }

        public static Cliente MockCliente(string cpf, int identificadorUnico = default)
        {
            var cliente = new Cliente
            {
                IdentificadorUnico = identificadorUnico,
                Nome = "Jose do teste",
                Cpf = cpf,
                DataNascimento = new DateTime(1983, 06, 14),
                Email = "nd@nd.com",
                TelefoneCelular = "19989898989",
                TelefoneFixo = "1932323232"
            };

            cliente.Endereco = new Endereco
            {
                Logradouro = "Rua hum",
                Numero = 2,
                Bairro = "Maria bonita",
                Cep = "13010020",
                Cidade = "São José",
                Estado = "SP"
            };

            return cliente;
        }

        public static List<Cliente> MockListaCliente() =>
            new List<Cliente>
            {
                MockCliente("12345678909", 1),
                MockCliente("32123145646", 2),
                MockCliente("12131332213", 3),
                MockCliente("41745697789", 4)
            };
    }
}
