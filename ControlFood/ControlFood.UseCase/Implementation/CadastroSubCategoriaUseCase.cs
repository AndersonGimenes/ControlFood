using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using ControlFood.UseCase.Validation;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroSubCategoriaUseCase : CadastroBaseUseCase<SubCategoria>,  ICadastroSubCategoriaUseCase
    {
        private ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CadastroSubCategoriaUseCase(ISubCategoriaRepository subCategoriaRepository, ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository)
            : base(subCategoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
        }

        public override SubCategoria Inserir(SubCategoria subCategoria)
        {
            var subCategorias = base.BuscarTodos();

            CadastroSubCategoriaUseCaseValidation.VerificarCategoriaVinculada(subCategoria, _categoriaRepository.BuscarTodos());
            CadastroSubCategoriaUseCaseValidation.VerificarDuplicidade(subCategoria, subCategorias);

            return base.Inserir(subCategoria);
        }

        public override void Atualizar(SubCategoria subCategoria)
        {
            var subCategoriaPersistida = base.BuscarPorIdentificacao(subCategoria, nameof(subCategoria.IdentificadorUnico));
            CadastroSubCategoriaUseCaseValidation.VerificarTiposAtaulizacao(subCategoriaPersistida, subCategoria);

            base.Atualizar(subCategoria);
        }

        public override void Deletar(SubCategoria subCategoria)
        {
            var produtos = _produtoRepository.BuscarTodos();
            CadastroSubCategoriaUseCaseValidation.VerificarSubCategoriaVinculada(subCategoria, produtos);

            base.Deletar(subCategoria);
        }

        
    }
}
