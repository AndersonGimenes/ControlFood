using ControlFood.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControlFood.UI.Controllers
{
    public class SubCategoriaController : Controller
    {
        [HttpGet]
        public IActionResult Cadastrar()
        
        {
            ViewBag.Categorias = ListaCategoriaTesteMock();
            ViewBag.SubCategorias = ListasubCategoriaTesteMock(null);

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(SubCategoria subCategoria)
        {
            ViewBag.Categorias = ListaCategoriaTesteMock();
            ViewBag.SubCategorias = ListasubCategoriaTesteMock(subCategoria);

            subCategoria.Categoria.Tipo = "Sobremesa";
            return View(subCategoria);
        }


        // tirar mock ao implemnetar o fluxo
        private List<Categoria> ListaCategoriaTesteMock() => new List<Categoria>
        {
            new Categoria{IdentificadorUnico = 1, Tipo = "Alimento"},
            new Categoria{IdentificadorUnico = 2, Tipo = "Bebida"},
            new Categoria{IdentificadorUnico = 3, Tipo = "Sobremesa"}
        };

        private List<SubCategoria> ListasubCategoriaTesteMock(SubCategoria subCategoria)
        {
            var lst = new List<SubCategoria>();

            var subA = new SubCategoria { IdentificadorUnico = 1, Tipo = "Lanche" };
            subA.Categoria = new Categoria { IdentificadorUnico = 1, Tipo = "Alimento" };
            lst.Add(subA);

            var subB = new SubCategoria { IdentificadorUnico = 2, Tipo = "Pastel" };
            subB.Categoria = new Categoria { IdentificadorUnico = 1, Tipo = "Alimento" };
            lst.Add(subB);

            var subC = new SubCategoria { IdentificadorUnico = 3, Tipo = "Refrigerante" };
            subC.Categoria = new Categoria { IdentificadorUnico = 2, Tipo = "Bebida" };
            lst.Add(subC);

            var subD = new SubCategoria { IdentificadorUnico = 4, Tipo = "Cerveja" };
            subD.Categoria = new Categoria { IdentificadorUnico = 2, Tipo = "Bebida" };
            lst.Add(subD);

            var subE = new SubCategoria { IdentificadorUnico = 5, Tipo = "Sorvete" };
            subE.Categoria = new Categoria { IdentificadorUnico = 3, Tipo = "Sobremesa" };
            lst.Add(subE);

            if (subCategoria != null)
            {
                lst.Add(subCategoria);
            }

            return lst;

        }
    }
}
