using System.Collections.Generic;

namespace ControlFood.Repository.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public List<SubCategoria> SubCategorias { get; set; }

    }
}
