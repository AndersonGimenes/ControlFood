namespace ControlFood.Domain.Entidades
{
    public class Produto : Comum
    {
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        // inserir Categoria
        public Estoque Estoque { get; set; }


    }
}
