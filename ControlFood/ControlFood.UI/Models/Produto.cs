using ControlFood.UI.Validation;

namespace ControlFood.UI.Models
{
    public class Produto
    {
        public int IdentificadorUnico { get; set; }
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public Estoque Estoque { get; set; }
        public string Mensagem { get; set; }

        public void IsValid()
        {
            var valida = new ProdutoValidation();
            valida.Validar(this);
        }
    }
}
