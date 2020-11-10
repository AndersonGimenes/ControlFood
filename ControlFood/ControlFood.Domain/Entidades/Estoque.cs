using System;

namespace ControlFood.Domain.Entidades
{
    public class Estoque: Comum
    {
        public int IdentificadorUnicoProduto { get; private set; }
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public decimal ValorCompraUnidade { get; set; }
        public decimal ValorCompraTotal { get; set; }
        
        public void AtribuirIdentificadorUnicoProduto(int identificadorUnico)
        {
            IdentificadorUnicoProduto = identificadorUnico;
        }

    }
}
