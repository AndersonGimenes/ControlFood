using System;
using System.Collections.Generic;

namespace ControlFood.Domain.Entidades
{
    public abstract class Pessoa: Comum
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public List<Endereco> Enderecos { get; set; }
    }
}
