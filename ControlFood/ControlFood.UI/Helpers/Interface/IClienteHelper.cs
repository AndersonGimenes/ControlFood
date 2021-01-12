using System.Collections.Generic;

namespace ControlFood.UI.Helpers.Interface
{
    public interface IClienteHelper
    {
        List<Models.Cliente> CacheClientes(bool renovaCache = false);
    }
}
