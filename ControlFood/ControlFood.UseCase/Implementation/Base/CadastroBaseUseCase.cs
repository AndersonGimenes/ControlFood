using ControlFood.UseCase.Interface.Repository;
using System;
using System.Collections.Generic;

namespace ControlFood.UseCase.Implementation.Base
{
    public abstract class CadastroBaseUseCase<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;

        public CadastroBaseUseCase(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public virtual T Inserir(T entidade) => _genericRepository.Inserir(entidade);

        public void Atualizar(T entidade)
        {
            _genericRepository.Atualizar(entidade);
        }

        public T BuscarPorIdentificacao(T entidade, string propertyName)
        {
            var propriedades = entidade.GetType().GetProperties();

            foreach (var prop in propriedades)
            {
                if (prop.Name.Equals(propertyName))
                    return _genericRepository.BuscarPorId((int)prop.GetValue(entidade));
            }

            return default;
        }

        public List<T> BuscarTodos() => _genericRepository.BuscarTodos();

        public void Deletar(T entidade)
        {
            _genericRepository.Deletar(entidade);
        }

    }
}
