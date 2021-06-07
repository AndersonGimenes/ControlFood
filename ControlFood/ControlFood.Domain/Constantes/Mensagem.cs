namespace ControlFood.Domain.Constantes
{
    public static class Mensagem
    {
        public const string CampoNaoInformado = "Não informado.";

        public static class Validacao
        {
            public static class Comum
            {
                public const string PedidoInexistente = "O pedido numero {0} não confere no sistema, por favor verifique o numero do pedido";
            }

            public static class Categoria
            {
                public const string CategoriaDuplicada = "A categoria {0} ja existe no sistema";

                public const string CategoriaVinculadaAProduto = "Existe produto(s) vinculado(s) a Categoria selecionada.";
            }

            public static class Produto
            {
                public const string ProdutoDuplicadoPorNome = "O produto com nome {0} ja existe no sistema";

                public const string CategoriaNaoVinculadaAoProduto = "Produto precisa estar vinculada a uma categoria.";

                public const string ProdutoDuplicadoPorCodigo = "O produto com codigo {0} ja existe no sistema";
                
                public const string ProdutoSemAdicional = "Um dos adicionais especificados não esta cadastrado no sistema";
            }

            public static class Adicional
            {
                public const string AdicionalDuplicada = "O adicional {0} ja existe no sistema";
            }

            public static class Pessoa
            {
                public const string CpfDuplicado = "O CPF {0} ja existe no sistema";

                public const string NomeDuplicado = "O Nome {0} ja existe no sistema";

                public const string DataNascimentoInavalida = "O Data de nascimento esta invalida. Cliente deve ter ao menos 10 anos";
            }
        }

    }
}
