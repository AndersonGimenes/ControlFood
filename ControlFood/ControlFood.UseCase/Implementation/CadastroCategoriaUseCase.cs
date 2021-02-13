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

        public CadastroCategoriaUseCase(ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository)
           : base(categoriaRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public override Categoria Inserir(Categoria categoriaRequisicao)
        {
            var categoriasPersistidas = base.BuscarTodos();

            CadastroCategoriaUseCaseValidation.ValidarRegrasParaInserir(categoriaRequisicao, categoriasPersistidas);

            return base.Inserir(categoriaRequisicao);
        }

        public override void Deletar(Categoria categoria)
        {
            // substituir por produto
            //var subCategorias = _subCategoriaRepository.BuscarTodos();

            //CadastroCategoriaUseCaseValidation.ValidarRegrasParaDeletar(categoria, subCategorias);

            base.Deletar(categoria);
        }
    }
}
