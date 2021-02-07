using System.Collections.Generic;

namespace ControlFood.Api.Helpers.Interface
{
    public interface IProdutoHelper
    {
        List<Models.Produto> CacheProdutos(bool renovaCache = false);
    }
}
