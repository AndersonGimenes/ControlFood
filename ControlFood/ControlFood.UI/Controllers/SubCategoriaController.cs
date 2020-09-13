using AutoMapper;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ControlFood.UI.Controllers
{
    public class SubCategoriaController : Controller
    {
        public SubCategoriaController(ICadastroSubCategoriaUseCase cadastroSubCategoriaUseCase, IMapper mapper, IMemoryCache cache)
        {

        }

        [HttpGet]
        public IActionResult Cadastrar()
        
        {
            //ViewBag.Categorias = ListaCategoriaTesteMock();
            //ViewBag.SubCategorias = ListasubCategoriaTesteMock(null);

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(SubCategoria subCategoria)
        {
            //ViewBag.Categorias = ListaCategoriaTesteMock();
            //ViewBag.SubCategorias = ListasubCategoriaTesteMock(subCategoria);

            //subCategoria.Categoria.Tipo = "Sobremesa";
            return View(subCategoria);
        }
             
    }
}
