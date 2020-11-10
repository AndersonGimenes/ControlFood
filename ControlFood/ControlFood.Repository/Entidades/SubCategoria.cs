using System.Collections.Generic;

namespace ControlFood.Repository.Entidades
{
    public class SubCategoria: Comum
    {
        public string Tipo { get; set; }
        public bool IndicadorItemCozinha { get; set; }
        public bool IndicadorItemBar { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public List<Produto> Produtos { get; set; }

    }
}