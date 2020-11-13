namespace ControlFood.Repository.Entidades
{
    public class Endereco : Comum
    {
        public int IndetificadorUnicoCliente { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string InfoApartamentoCondominio { get; set; }
        public string Complemento { get; set; }
        public Cliente Cliente { get; set; }
    }
}
