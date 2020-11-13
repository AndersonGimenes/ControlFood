using System;

namespace ControlFood.UI.Models
{
    public abstract class Pessoa
    {
        public int IdentificadorUnico { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
