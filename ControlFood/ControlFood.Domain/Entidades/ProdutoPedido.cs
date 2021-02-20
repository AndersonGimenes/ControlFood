namespace ControlFood.Domain.Entidades
{
    public class ProdutoPedido : Comum
    {
        public string Observacao { get; set; }
        public Produto Produto { get; set; }
    }
}
