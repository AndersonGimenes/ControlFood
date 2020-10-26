using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroEstoqueUseCase : CadastroBaseUseCase<Estoque>, ICadastroEstoqueUseCase
    {
        public CadastroEstoqueUseCase(IEstoqueRepository estoqueRepository) 
            : base(estoqueRepository)
        {
        }

        public Produto InserirEstoque(Produto produto)
        {
            // validar produto x estoque por id 

            produto.Estoque.AtribuirDataDeEntrada();

            base.Inserir(produto.Estoque);

            // ajustar 
            return default;
        }
    }
}
