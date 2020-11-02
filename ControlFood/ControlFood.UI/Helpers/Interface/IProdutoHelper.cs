using System.Collections.Generic;

namespace ControlFood.UI.Helpers.Interface
{
    public interface IProdutoHelper
    {
        List<Models.Produto> CacheProdutos(bool renovaCache = false);
    }
}
