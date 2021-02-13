using System.Collections.Generic;

namespace ControlFood.Api.Helpers.Interface
{
    public interface IProdutoHelper
    {
        IEnumerable<Models.Produto> CacheProdutos(bool renovaCache);
    }
}
