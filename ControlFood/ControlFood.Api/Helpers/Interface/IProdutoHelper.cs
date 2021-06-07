using System.Collections.Generic;
using ControlFood.Api.Models.Produto;

namespace ControlFood.Api.Helpers.Interface
{
    public interface IProdutoHelper
    {
        IEnumerable<ProdutoResponse> CacheProdutos(bool renovaCache);
    }
}
