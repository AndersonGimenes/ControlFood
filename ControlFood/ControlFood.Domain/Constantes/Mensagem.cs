using System;

namespace ControlFood.Domain.Constantes
{
    public static class Mensagem
    {
        public static class Validacao
        {
            public static class Comum
            {
                public const string PedidoInexistente = "O pedido numero {0} não confere no sistema, por favor verifique o numero do pedido";

                public const string EdicaoInvalida = "O campo {0} não pode ser atualizado.";
            }
            
            public static class Categoria
            {
                public const string CategoriaDuplicada = "A categoria {0} ja existe no sistema";

                public const string CategoriaVinculadaASubCategoria = "Existe Sub-categoria vinculada a Categoria {0}.";
            }
            
            public static class SubCategoria
            {
                public const string SubCategoriaDuplicada = "A sub-categoria {0} ja existe no sistema";

                public const string CategoriaNaoVinculadaASubCategoria = "Sub-categoria precisa estar vinculada a uma categoria";

                public const string ProdutoVinculadoASubCategoria = "Existe Produto vinculada a Sub-Categoria {0}.";
            }

            public static class Produto
            {
                public const string ProdutoDuplicadoPorNome = "O produto com nome {0} ja existe no sistema";

                public const string SubCategoriaNaoVinculadaAoProduto = "Produto precisa estar vinculada a uma sub-categoria";

                public const string ProdutoDuplicadoPorCodigo = "O produto com codigo {0} ja existe no sistema";
            }
        }  
        
    }
}
