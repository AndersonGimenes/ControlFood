using AutoMapper;
using ControlFood.UI.Models;
using ControlFood.UseCase.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using Dominio = ControlFood.Domain.Entidades;

namespace ControlFood.UI.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICadastroCategoriaUseCase _cadastroCategoriaUseCase;

        public CategoriaController(IMapper mapper, ICadastroCategoriaUseCase cadastroCategoriaUseCase)
        {
            _mapper = mapper;
            _cadastroCategoriaUseCase = cadastroCategoriaUseCase;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            ViewBag.Categorias = _cadastroCategoriaUseCase.BuscarTodos();

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoriaDominio = _mapper.Map<Dominio.Categoria>(categoria);
                    
                    _cadastroCategoriaUseCase.Inserir(categoriaDominio);

                    ViewBag.Categorias = _cadastroCategoriaUseCase.BuscarTodos();
                    return View();
                }
                else
                {
                    ViewBag.Categorias = _cadastroCategoriaUseCase.BuscarTodos();
                    return View();
                }
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
