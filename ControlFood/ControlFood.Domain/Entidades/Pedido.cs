using ControlFood.Domain.Enuns;
using System.Collections.Generic;

namespace ControlFood.Domain.Entidades
{
    public class Pedido
    {
        public int Numero { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public bool PedidoPago { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public List<Item> Items { get; set; }
        
    }
}
