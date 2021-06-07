using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation.Comum
{
    internal static class ComumValidation<T> where T : class
    {
        internal static void VerificarDuplicidade(T entidade, IEnumerable<T> entidades, string propertyName, Action lancarException)
        {
            var propriedadeValor = ObterValorReflection(entidade, propertyName).ToString().ToUpper();

            if (entidades.Any(e => ObterValorReflection(e, propertyName).ToString().ToUpper() == propriedadeValor))
                lancarException.Invoke();
        }

        internal static void VerificarVinculoInserir(int id, IEnumerable<T> entidades, string propertyName, Action lancarException)
        {
            var existeVinculo = entidades.Any(e => (int)ObterValorReflection(e, propertyName) == id);

            if (!existeVinculo)
                lancarException.Invoke();
        }

        internal static void VerificarVinculoParaDeletar(int id, List<object> entidades, string propertyName, Action lancarException)
        {
            entidades.ForEach(e =>
            {
                var valor = (int)ObterValorReflection(e, propertyName);

                if (valor == id)
                    lancarException.Invoke();
            });

        }

        #region[ PRIVADOS ]
        private static object ObterValorReflection(object entidade, string propertyName) => entidade
                                                                                                .GetType()
                                                                                                .GetProperty(propertyName)
                                                                                                .GetValue(entidade);
                                                                                                
        #endregion
    }
}
