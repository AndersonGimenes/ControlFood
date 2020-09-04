using ControlFood.Domain.Enuns;
using System.Collections.Generic;

namespace ControlFood.Domain.Entidades
{
    public class Pedido
    {
        // numero do pedido
        public int IdentificadorUnico { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public bool PedidoPago { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public List<Produto> Items { get; set; }
        
    }
}
