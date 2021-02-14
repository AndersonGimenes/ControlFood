﻿namespace ControlFood.Domain.Constantes
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

                public const string CategoriaVinculadaAProduto = "Existe produto(s) vinculado(s) a Categoria {0}.";
            }

            public static class SubCategoria
            {
                public const string SubCategoriaDuplicada = "A sub-categoria {0} ja existe no sistema";

                public const string CategoriaNaoVinculadaASubCategoria = "Sub-categoria precisa estar vinculada a uma categoria";

                public const string ProdutoVinculadoASubCategoria = "Existe Produto vinculado a Sub-Categoria {0}.";
            }

            public static class Produto
            {
                public const string ProdutoDuplicadoPorNome = "O produto com nome {0} ja existe no sistema";

                public const string CategoriaNaoVinculadaAoProduto = "Produto precisa estar vinculada a uma categoria";

                public const string ProdutoDuplicadoPorCodigo = "O produto com codigo {0} ja existe no sistema";

                public const string ProdutoInexistente = "Não é possivel cadastrar o estoque: Produto inexistente";

                public const string ValidadeIncorreta = "A data de validade do produto deve ser maior que {0}";

                public const string ValoresDivergentes = "A quantidade X valor unitario é diferente do valor total do lote.";

                public const string EstoqueVinculado = "Existe estoque vinculado ao Produto {0}";
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
