using System.Collections.Generic;
using ControlFood.Repository.Entidades;

namespace ControlFood.Repository.Models
{
    public class ProdutoVenda : Comum
    {
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorVenda { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public List<ProdutoAdicional> Adicionais { get; set; }
    }
}
