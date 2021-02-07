namespace ControlFood.Api.Constantes
{
    public static class Mensagem
    {
        public static class Validacao
        {
            public const string CampoVazio = "O campo {PropertyName} não pode ser vazio ou com espaço no começo";
            public const string CampoForaDoTamanho = "O campo {0} deve ser preenchido com {1} caracteres";
        }

        public static class Comum
        {
            public const string ItemDeletado = "Registro deletado.";
            public const string ItemAtualizado = "Item atualizado.";
        }

        public static class EStoque
        {
            public const string EStoqueCadastrado = "Estoque cadastrado";
        }

        public static class Cliente
        {
            public const string EnderecoSemPreenchimento = "O Endereço deve ser preenchido";
            public const string TelefoneObrigatorio = "Ao Menos um telefone deve ser preenchido";
        }
    }
}
