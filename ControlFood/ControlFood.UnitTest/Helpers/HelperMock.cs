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
               new Categoria{IdentificadorUnico = 1, Tipo = "Lanches"},
               new Categoria{IdentificadorUnico = 2, Tipo = "Cervejas"},
               new Categoria{IdentificadorUnico = 3, Tipo = "Porções"},
               new Categoria{IdentificadorUnico = 4, Tipo = "Refrigerantes"}
            };

        public static List<Produto> MockListaProdutosPersistidos() =>
            new List<Produto>
            {
                MockProduto("cc350", "Coca-cola lata 350ml", idProduto: 1, idCategoria: 4),
                MockProduto("cc1L", "Coca-cola 1 litro", idProduto: 2, idCategoria: 4),
                MockProduto("spt2L", "Sprite 2 litros", idProduto: 3, idCategoria: 4),
                MockProduto("XT001", "X-TUDO", idProduto: 4, idCategoria: 1)
            };


        public static Produto MockProduto(string codigo, string nome, int idProduto, int idCategoria)
        {
            var produto = new Produto
            {
                CodigoInterno = codigo,
                IdentificadorUnico = idProduto,
                Nome = nome,
                ValorVenda = 5
            };

            produto.Categoria = new Categoria { IdentificadorUnico = idCategoria };

            return produto;
        }

        public static Cliente MockCliente(string cpf = default, int identificadorUnico = default)
        {
            var cliente = new Cliente
            {
                IdentificadorUnico = identificadorUnico,
                Nome = "Jose do teste",
                Cpf = cpf,
                DataNascimento = new DateTime(1983, 06, 14),
                Email = "nd@nd.com",
                TelefoneCelular = "19989898989"
            };

            cliente.Enderecos = new List<Endereco>
            {
                new Endereco
                {
                    Logradouro = "Rua hum",
                    Numero = "1",
                    Bairro = "Maria bonita",
                    Cep = "13010020",
                    Cidade = "Campinas",
                    Estado = "SP"
                },
                new Endereco
                {
                    Logradouro = "Rua dois",
                    Numero = "2",
                    Bairro = "Maria bonita",
                    Cep = "13010020",
                    Cidade = "São José",
                    Estado = "SP"
                }
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
