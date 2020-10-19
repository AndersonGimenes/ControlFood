using ControlFood.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControlFood.UI.Controllers
{
    public class ProdutoController : Controller
    {
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
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
    }
}
