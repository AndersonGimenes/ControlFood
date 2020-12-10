class SubCategoria {

    constructor() {
        this.subCategoriaModel = new SubCategoriaModel();
    }

    cadastrar(el) {

        let elemento = el.parentNode;

        let tipo = $(elemento).find('#sub-categoria-tipo');

        if (!Helper.validarCamposObrigatorios([tipo], [$(elemento).find('#valida-sub-categoria-tipo')]))
            return;

        this.subCategoriaModel.tipo = tipo.val();
        this.subCategoriaModel.categoria.identificadorUnico = $(elemento).find('#categoria-identificador-unico').val();
        this.subCategoriaModel.indicador = $(elemento).find('input[name="sub-categoria-indicador"]:checked').val();

        Helper.realizarChamadaAjax('SubCategoria/Cadastrar', this.subCategoriaModel, 'POST');
    }

    deletar(el) {

        let elemento = el.parentNode.parentNode;

        this.subCategoriaModel.identificadorUnico = $(elemento).find(".sub-categoria-identificador-unico").val();

        Helper.realizarChamadaAjax("/SubCategoria/Deletar", this.subCategoriaModel, "DELETE");

    }

    popularModalAtualizar(el) {

        let elemento = el.parentNode.parentNode;

        $('#sub-categoria-atualizar-modal').modal('show');

        $('#div-categoria-tipo-modal')
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.categoria-tipo').text()}'/>`);

        $('#div-sub-categoria-tipo-modal')
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.sub-categoria-tipo').text()}'/>`);

        $('#div-sub-categoria-identificador-unico-modal')
            .html(`<input type='hidden' id='sub-categoria-identificador-unico-modal' value='${$(elemento).find(".sub-categoria-identificador-unico").val()}'/>`);

        let valorIndicador = $(elemento).find('.sub-categoria-indicador').val();
        let indicadorCozinha = $('#sub-categoria-indicador-cozinha-modal');
        let indicadorBar = $('#sub-categoria-indicador-bar-modal');

        if (valorIndicador === '0') {
            indicadorCozinha.prop('checked', true);
            indicadorBar.prop('checked', false);
            return;
        }

        indicadorCozinha.prop("checked", false);
        indicadorBar.prop("checked", true);
    }

    atualizar(el) {

        let elemento = el.parentNode.parentNode;

        this.subCategoriaModel.indicador = $(elemento).find('input[name="sub-categoria-indicador-modal"]:checked').val()
        this.subCategoriaModel.identificadorUnico = $(elemento).find('#sub-categoria-identificador-unico-modal').val();

        Helper.realizarChamadaAjax('/SubCategoria/Atualizar', this.subCategoriaModel, 'PUT');
    }
}
