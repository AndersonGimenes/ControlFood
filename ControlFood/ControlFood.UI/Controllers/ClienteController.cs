using ControlFood.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ControlFood.UI.Controllers
{
    public class ClienteController : Controller
    {
        [HttpGet]
        public IActionResult Cadastrar()
        {
            var clientes = new List<Cliente>();
            return View(clientes);
        }

        [HttpPost]
        public IActionResult Cadastrar(Cliente cliente)
        {
            return View(cliente);
        }
    }
}
