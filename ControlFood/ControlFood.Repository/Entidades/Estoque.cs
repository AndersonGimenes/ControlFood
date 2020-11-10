using System;

namespace ControlFood.Repository.Entidades
{
    public class Estoque : Comum
    {
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public decimal ValorCompraUnidade { get; set; }
        public decimal ValorCompraTotal { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
    }
}
