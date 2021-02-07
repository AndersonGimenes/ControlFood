using System.Collections.Generic;

namespace ControlFood.Api.Helpers.Interface
{
    public interface ICategoriaHelper
    {
        List<Models.Categoria> CacheCategorias(bool renovaCache = false);
    }
}