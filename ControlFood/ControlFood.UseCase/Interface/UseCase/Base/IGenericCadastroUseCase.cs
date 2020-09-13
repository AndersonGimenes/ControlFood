using System.Collections.Generic;

namespace ControlFood.UseCase.Interface.UseCase.Base
{
    public interface IGenericCadastroUseCase<T> where T : class
    {
        T Inserir(T entidade);
        void Atualizar(T entidade);
        T BuscarPorIdentificacao(T entidade, string propertyName);
        List<T> BuscarTodos();
        void Deletar(T entidade);

    }
}
