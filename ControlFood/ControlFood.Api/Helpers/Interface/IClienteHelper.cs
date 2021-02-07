using System.Collections.Generic;

namespace ControlFood.Api.Helpers.Interface
{
    public interface IClienteHelper
    {
        List<Models.Cliente> CacheClientes(bool renovaCache = false);
    }
}
