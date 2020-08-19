namespace ControlFood.UseCase.Interface.UseCase.Base
{
    public interface ICadastroBaseUseCase<T> where T : class
    {
        T Inserir(T entidade);
        void Atualizar(T entidade);
        T BuscarPorIdentificacao(T entidade, string propertyName);
    }
}
