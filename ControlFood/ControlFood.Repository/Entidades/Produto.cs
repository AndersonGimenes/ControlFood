using System.Collections.Generic;

namespace ControlFood.Repository.Entidades
{
    public class Produto
    {
        public Produto()
        {
            Estoques = new List<Estoque>();
        }
        public int Id { get; set; }
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public int SubCategoriaId { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public List<Estoque> Estoques { get; set; }
    }
}
