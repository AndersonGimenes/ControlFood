using System;

namespace ControlFood.Api.Models
{
    public class Estoque
    {
        public int IdentificadorUnico { get; set; }
        public int IdentificadorUnicoProduto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public decimal ValorCompraUnidade { get; set; }
        public decimal ValorCompraTotal { get; set; }
    }
}
