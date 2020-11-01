using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Validation.Comum
{
    internal static class ComumValidation<T> where T : class
    {
        internal static void VerificarDuplicidade(T entidade, List<T> entidades, string propertyName, Action lancarException)
        {
            var propriedadeValor = ObterValorReflection(entidade, propertyName).ToString().ToUpper();

            if (entidades.Any(e => ObterValorReflection(e, propertyName).ToString().ToUpper() == propriedadeValor))
                lancarException.Invoke();
        }

        internal static void VerificarVinculoInserir(T entidade, List<T> entidades, string propertyName, Action lancarException)
        {
            var propriedadeValor = (int)ObterValorReflection(entidade, propertyName);

            var existeVinculo = entidades.Any(e => (int)ObterValorReflection(e, propertyName) == propriedadeValor);

            if (!existeVinculo)
                lancarException.Invoke();
        }

        internal static void VerificarVinculoDeletar(T entidade, List<object> entidades, string className, string propertyName, Action lancarException)
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

        internal static void VerificarTiposAtaulizacao(T entidadePersistida, T entidade, string propertyName, Action lancarException)
        {
            var propriedadeValorPersistido = ObterValorReflection(entidadePersistida, propertyName);
            var propriedadeValor = ObterValorReflection(entidade, propertyName);

            var propriedade = entidade.GetType().GetProperties().First(p => p.Name == propertyName);

            if (propriedade.PropertyType.Name is string)
            {
                if (propriedadeValorPersistido.ToString() != propriedadeValor.ToString())
                    lancarException.Invoke();

                return;
            }
            
            if ((int)propriedadeValorPersistido != (int)propriedadeValor)
                    lancarException.Invoke();
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
