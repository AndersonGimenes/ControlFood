using System;

namespace ControlFood.Domain.Entidades
{
    public abstract class Pessoa: Comum
    {
        public Pessoa()
        {
            this.Endereco = new Endereco();
        }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string TelefoneResidencial { get; set; }
        public string TelefoneCelular { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public Endereco Endereco { get; set; }
    }
}
