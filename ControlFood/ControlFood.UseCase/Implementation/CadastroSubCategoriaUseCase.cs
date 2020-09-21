using ControlFood.Domain.Constantes;
using ControlFood.Domain.Entidades;
using ControlFood.UseCase.Exceptions;
using ControlFood.UseCase.Implementation.Base;
using ControlFood.UseCase.Interface.Repository;
using ControlFood.UseCase.Interface.UseCase;
using System.Collections.Generic;
using System.Linq;

namespace ControlFood.UseCase.Implementation
{
    public class CadastroSubCategoriaUseCase : CadastroBaseUseCase<SubCategoria>,  ICadastroSubCategoriaUseCase
    {
        private ICategoriaRepository _categoriaRepository;

        public CadastroSubCategoriaUseCase(ISubCategoriaRepository subCategoriaRepository, ICategoriaRepository categoriaRepository)
            : base(subCategoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public override SubCategoria Inserir(SubCategoria subCategoria)
        {
            var subCategorias = base.BuscarTodos();

            this.VerificarCategoriaVinculada(subCategoria, _categoriaRepository.BuscarTodos());
            this.VerificarDuplicidade(subCategoria, subCategorias);

            return base.Inserir(subCategoria);
        }

        private void VerificarCategoriaVinculada(SubCategoria subCategoria, List<Categoria> categorias)
        {
            var existeCategoriaVinculada = categorias.Any(c => c.IdentificadorUnico == subCategoria.Categoria.IdentificadorUnico);
            if (!existeCategoriaVinculada)
                throw new SubCategoriaIncorretaUseCaseException(Mensagem.Validacao.CategoriaNaoVinculadaASubCategoria);
        }

        private void VerificarDuplicidade(SubCategoria subCategoria, List<SubCategoria> subCategorias)
        {
            if (subCategorias.Any(s => s.Tipo == subCategoria.Tipo))
                throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.SubCategoriaDuplicada, subCategoria.Tipo));
        }
    }
}
