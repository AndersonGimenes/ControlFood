using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UnitTest.Helpers
{
    internal static class HelperComum<T> where T : class
    {
        internal static int DeletarRegistro(int id, List<T> listaEntidades, string propertyName)
        {
            int indice;
            
            indice = listaEntidades
                        .FindIndex((x) => 
                            (int)x
                                .GetType()
                                .GetProperties()
                                .First(p => p.Name == propertyName)
                                .GetValue(x) == id
                        );

            listaEntidades.RemoveAt(indice);

            return listaEntidades.Count;
        }
    }
}
