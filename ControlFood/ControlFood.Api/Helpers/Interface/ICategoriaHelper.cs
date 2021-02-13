using System.Collections.Generic;

namespace ControlFood.Api.Helpers.Interface
{
    public interface ICategoriaHelper
    {
        IEnumerable<Models.Categoria> CacheCategorias(bool renovaCache);
    }
}