using System.Collections.Generic;

namespace ControlFood.Repository.Entidades
{
    public class Categoria: Comum
    {
        public string Tipo { get; set; }
        public List<Produto> Produtos { get; set; }

    }
}
