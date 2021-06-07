using ControlFood.Domain.Entidades.Produto;

namespace ControlFood.Domain.Entidades
{
    public class ProdutoPedido : Comum
    {
        public string Observacao { get; set; }
        public ProdutoVenda Produto { get; set; }
    }
}
