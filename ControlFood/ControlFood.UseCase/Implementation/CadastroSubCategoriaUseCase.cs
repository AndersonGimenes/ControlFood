﻿using ControlFood.Domain.Constantes;
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
        public CadastroSubCategoriaUseCase(ISubCategoriaRepository subCategoriaRepository)
            : base(subCategoriaRepository)
        {
                
        }

        public void VerificarCategoriaVinculada(SubCategoria subCategoria, List<Categoria> categorias)
        {
            var existeCategoriaVinculada = categorias.Any(c => c.IdentificadorUnico == subCategoria.Categoria.IdentificadorUnico && c.Tipo == subCategoria.Categoria.Tipo);
            if (!existeCategoriaVinculada)
                throw new SubCategoriaIncorretaUseCaseException(Mensagem.Validacao.CategoriaNaoVinculadaASubCategoria);
        }

        public void VerificarDuplicidade(SubCategoria subCategoria, List<SubCategoria> subCategorias)
        {
            if (subCategorias.Any(s => s.Tipo == subCategoria.Tipo))
                throw new SubCategoriaIncorretaUseCaseException(string.Format(Mensagem.Validacao.SubCategoriaDuplicada, subCategoria.Tipo));
        }
    }
}
