namespace ControlFood.Domain.Entidades
{
    public class Categoria
    {
        public Categoria()
        {
            this.SubCategoria = new SubCategoria();
        }
        public string Tipo { get; set; }

        public SubCategoria SubCategoria { get; set; }
    }
}