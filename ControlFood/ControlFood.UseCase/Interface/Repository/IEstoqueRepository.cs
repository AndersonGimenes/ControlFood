using ControlFood.Domain.Entidades;
using System.Collections.Generic;

namespace ControlFood.UseCase.Interface.Repository
{
    public interface IEstoqueRepository : IGenericRepository<Estoque>
    {
        List<Estoque> BuscarTodosPorProduto(Produto produto);
    }
}
