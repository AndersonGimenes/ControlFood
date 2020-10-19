using ControlFood.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ControlFood.UI.Controllers
{
    public class ProdutoController : Controller
    {
        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.SubCategorias = TempMockSubCategorias();

            return View(TempMockProdutos());
        }

        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            try
            {
                // validar produto is valid

                // criar mapper para dominio 

                // chamar metodo para inserir

                // mapear retorno 

                // retornar lista de produtos
                return View();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        private List<Produto> TempMockProdutos()
        {
            var produtos = new List<Produto>();

            var cocacola = new Produto
            {
                CodigoInterno = "CC-600",
                IdentificadorUnico = 1,
                Nome = "Coca cola 600 ml",
                ValorVenda = 8.00m
            };

            cocacola.Estoque = new Estoque
            {
                DataValidade = new DateTime(2022, 5, 14),
                Quantidade = 10,
                ValorCompraTotal = 50.00m,
                ValorCompraUnidade = 5.00m
            };

            cocacola.SubCategoria = TempMockSub(2, 1, "Refrigerantes", 2, "Bebidas");

            var xTudo = new Produto
            {
                CodigoInterno = "XT-001",
                IdentificadorUnico = 2,
                Nome = "X tudo",
                ValorVenda = 12.00m
            };

            xTudo.Estoque = new Estoque();

            xTudo.Igredientes = new List<string>
            {
                "Pão",
                "Hamburguer",
                "Salada",
                "Ovo",
                "Bacon"
            };

            xTudo.SubCategoria = TempMockSub(1, 0, "Lanches", 1, "Alimentos");

            produtos.Add(cocacola);
            produtos.Add(xTudo);

            return produtos;
        }
        private List<SubCategoria> TempMockSubCategorias()
        {
            var lst = new List<SubCategoria>();

            var subLanche = TempMockSub(1, 0, "Lanches", 1, "Alimentos");
            var subRefri = TempMockSub(2, 1, "Refrigerantes", 2, "Bebidas");
            
            lst.Add(subRefri);
            lst.Add(subLanche);

            return lst;
        }

        private SubCategoria TempMockSub(int id, int indicador, string tipo , int idCategoria, string tipoCategoria)
        {
            var sub = new SubCategoria
            {
                IdentificadorUnico = id,
                Indicador = indicador,
                Tipo = tipo
            };

            sub.Categoria = new Categoria
            {
                IdentificadorUnico = idCategoria,
                Tipo = tipoCategoria
            };

            return sub;
        }
    }

}

