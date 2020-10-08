using System;

namespace ControlFood.Domain.Entidades
{
    public class Estoque
    {
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public DateTime DataEntrada { get; set; }
        public decimal ValorCompraUnidade { get; set; }
        public decimal ValorCompraTotal { get; set; }
    }
}
