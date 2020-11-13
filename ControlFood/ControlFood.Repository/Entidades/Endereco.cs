namespace ControlFood.Repository.Entidades
{
    public class Endereco : Comum
    {
        public int IndetificadorUnicoCliente { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Cliente Cliente { get; set; }
    }
}
