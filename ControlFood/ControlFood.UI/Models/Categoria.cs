using System.ComponentModel.DataAnnotations;

namespace ControlFood.UI.Models
{
    public class Categoria
    {
        public int IdentificadorUnico { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo não pode ser vazio ou com espaço no começo")]
        public string Tipo { get; set; }
    }
}
