using System.Collections.Generic;
using ControlFood.Repository.Models;

namespace ControlFood.Repository.Entidades
{
    public class Categoria: Comum
    {
        public string Tipo { get; set; }
        public List<ProdutoVenda> Produtos { get; set; }

    }
}
