namespace ControlFood.Repository.Entidades
{
    public class ProdutoAdicional
    {
        public int ProdutoId { get; set; }
        public int AdicionalId { get; set; }
        public Produto Produto { get; set; }
        public Adicional Adicional { get; set; }
    }
}
