using System.Collections.Generic;

namespace ControlFood.Api.Models
{
    public class Cliente : Pessoa
    {
        public Cliente()
        {
            Enderecos = new List<Endereco>();
        }
        public List<Endereco> Enderecos { get; set; }
    }
}
