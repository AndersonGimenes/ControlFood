namespace ControlFood.Domain.Entidades
{
    public class SubCategoria: Comum
    {
        public string Tipo { get; set; }
        public bool IndicadorItemCozinha { get; set; }
        public bool IndicadorItemBar { get; set; }
        public Categoria Categoria { get; set; }
    }
}
