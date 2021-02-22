using System.Collections.Generic;

namespace ControlFood.Repository.Entidades
{
    public class Adicional : Comum
    {
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public List<ProdutoAdicional> Produtos { get; set; }
    }
}
