using ControlFood.Api.Models;
using System.Collections.Generic;

namespace ControlFood.Api.Helpers.Interface
{
    public interface ISubCategoriaHelper
    {
        List<SubCategoria> CacheSubCategorias(bool renovaCache = false);
    }
}
