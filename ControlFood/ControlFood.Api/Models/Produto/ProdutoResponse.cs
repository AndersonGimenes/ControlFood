using System.Collections.Generic;

namespace ControlFood.Api.Models.Produto
{
    public class ProdutoResponse
    {
        public int IdentificadorUnico { get; set; }
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public string Descricao { get; set; }
        public string ImagemBase64 { get; set; }
        public IEnumerable<Adicional> Adicionais { get; set; }
    }
}
