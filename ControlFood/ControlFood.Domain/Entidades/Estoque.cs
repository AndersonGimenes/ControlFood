using System;

namespace ControlFood.Domain.Entidades
{
    public class Estoque
    {
        public int IdentificadorUnico { get; set; }
        public int IdentificadorUnicoProduto { get; private set; }
        public int Quantidade { get; set; }
        public DateTime DataValidade { get; set; }
        public DateTime DataEntrada { get; private set; }
        public decimal ValorCompraUnidade { get; set; }
        public decimal ValorCompraTotal { get; set; }
        public DateTime? DataAlteracao { get; private set; }

        public void AtribuirIdentificadorUnicoProduto(int identificadorUnico)
        {
            IdentificadorUnicoProduto = identificadorUnico;
        }

        public void AtribuirDataDeEntrada()
        {
            DataEntrada = DateTime.Now;
        }

        public void AtribuirDataDeAlteracao()
        {
            DataAlteracao = DateTime.Now;
        }
    }
}
