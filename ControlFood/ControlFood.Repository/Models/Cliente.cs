using System.Collections.Generic;

namespace ControlFood.Repository.Entidades
{
    public class Cliente : Pessoa
    {
        public List<Endereco> Enderecos { get; set; }
    }
}
