namespace ControlFood.Api.Models
{
    public class Produto
    {
        public int IdentificadorUnico { get; set; }
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public string Mensagem { get; set; }
    }
}
