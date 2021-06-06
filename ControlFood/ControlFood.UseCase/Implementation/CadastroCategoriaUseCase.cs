using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroCategoriaUseCase : CadastroBaseUseCase<Categoria>, ICadastroCategoriaUseCase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public CadastroCategoriaUseCase(ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository)
           : base(categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public override Categoria Inserir(Categoria categoriaRequisicao)
        {
            var categoriasPersistidas = base.BuscarTodos();

            CadastroCategoriaUseCaseValidation.ValidarRegrasParaInserir(categoriaRequisicao, categoriasPersistidas);

            return base.Inserir(categoriaRequisicao);
        }

        public void DeletarCategoria(int idCategoria)
        {
            var produtosPersistidos = _produtoRepository.BuscarTodos();

            CadastroCategoriaUseCaseValidation.ValidarRegrasParaDeletar(idCategoria, produtosPersistidos);

            var categoria = _categoriaRepository.BuscarPorId(idCategoria);  

            base.Deletar(categoria);
        }
    }
}
