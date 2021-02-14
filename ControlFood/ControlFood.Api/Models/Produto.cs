using System;

namespace ControlFood.Api.Models
{
    public class Produto
    {
        public int IdentificadorUnico { get; set; }
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCompra { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataValidade { get; set; }
        public Categoria Categoria { get; set; }
    }
}
