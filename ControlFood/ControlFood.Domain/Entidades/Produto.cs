namespace ControlFood.Domain.Entidades
{
    public class Produto: Comum
    {
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public Estoque Estoque { get; set; }

        
    }
}
