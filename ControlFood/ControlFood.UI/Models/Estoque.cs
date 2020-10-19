using System;

namespace ControlFood.UI.Models
{
    public class Estoque
    {
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public decimal ValorCompraUnidade { get; set; }
        public decimal ValorCompraTotal { get; set; }
    }
}
