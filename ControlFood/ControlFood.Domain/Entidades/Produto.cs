using System;

namespace ControlFood.Domain.Entidades
{
    public class Produto
    {
        public Produto()
        {
            this.Categoria = new Categoria();
        }
        public int IdentificadorUnico { get; set; }
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }
        public DateTime Validade { get; set; }
        public Categoria Categoria { get; set; }
    }
}
