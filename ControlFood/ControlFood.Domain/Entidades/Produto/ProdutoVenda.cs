using System.Collections.Generic;

namespace ControlFood.Domain.Entidades.Produto
{
    public class ProdutoVenda : Comum
    {
        public string CodigoInterno { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal ValorVenda { get; set; }
        public int CategoriaIdentificadorUnico { get; set; }
        public string ImagemBase64 { get; set; }
        public List<int> AdicionaisIdentificadoresUnico { get; set; }
        public List<Adicional> Adicionais { get; set; }
    }
}
