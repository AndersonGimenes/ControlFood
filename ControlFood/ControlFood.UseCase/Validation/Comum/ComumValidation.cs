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

        internal static void VerificarVinculoInserir(T entidade, IEnumerable<T> entidades, string propertyName, Action lancarException)
        {
            var propriedadeValor = (int)ObterValorReflection(entidade, propertyName);

            var existeVinculo = entidades.Any(e => (int)ObterValorReflection(e, propertyName) == propriedadeValor);

            if (!existeVinculo)
                lancarException.Invoke();
        }

        internal static void VerificarVinculoParaDeletar(T entidade, List<object> entidades, string className, string propertyName, Action lancarException)
        {
            var propriedadeValor = (int)ObterValorReflection(entidade, propertyName);

            entidades.ForEach(e =>
            {
                var objeto = ObterValorReflection(e, className);
                var valor = (int)ObterValorReflection(objeto, propertyName);

                if (valor == propriedadeValor)
                    lancarException.Invoke();
            });

        }

        #region[ PRIVADOS ]
        private static object ObterValorReflection(object entidade, string propertyName) => entidade
                                                                                                .GetType()
                                                                                                .GetProperties()
                                                                                                .First(p => p.Name == propertyName)
                                                                                                .GetValue(entidade);
                                                                                                
        #endregion
    }
}
