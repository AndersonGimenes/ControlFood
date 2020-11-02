using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UnitTest.Helpers
{
    internal static class HelperComum<T> where T : class
    {
        internal static int DeletarRegistro(T entidade, List<T> listaEntidades, string propertyName)
        {
            int indice;
            var propriedadeValor = (int)entidade
                                        .GetType()
                                        .GetProperties()
                                        .First(p => p.Name == propertyName)
                                        .GetValue(entidade);

            indice = listaEntidades.FindIndex((x) => (int)x
                                                        .GetType()
                                                        .GetProperties()
                                                        .First(p => p.Name == propertyName)
                                                        .GetValue(x) == propriedadeValor);

            listaEntidades.RemoveAt(indice);
            
            return listaEntidades.Count;
        }
    }
}
