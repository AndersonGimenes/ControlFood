using System.Collections.Generic;
using ControlFood.Api.Models.Categoria;

namespace ControlFood.Api.Helpers.Interface
{
    public interface ICategoriaHelper
    {
        IEnumerable<CategoriaResponse> CacheCategorias(bool renovaCache);
    }
}