using System;

namespace ControlFood.Domain.Entidades
{
    public class Endereco : Comum
    {
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string InfoApartamentoCondominio { get; set; }
        public string Complemento { get; set; }

        public void AtribuirDataCadastro()
        {
            DataCadastro = DateTime.Now;
        }
    }
}
