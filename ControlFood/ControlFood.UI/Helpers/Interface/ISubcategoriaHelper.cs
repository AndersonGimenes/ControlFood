using ControlFood.UI.Models;
using System.Collections.Generic;

namespace ControlFood.UI.Helpers.Interface
{
    public interface ISubcategoriaHelper
    {
        List<SubCategoria> CacheSubCategorias(bool renovaCache = false);
    }
}
