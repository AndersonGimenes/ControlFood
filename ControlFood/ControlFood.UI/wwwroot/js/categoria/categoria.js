class Categoria {

    constructor() {
        this.categoriaModel = new CategoriaModel();
    }

    cadastrar(el) {

        let elemento = el.parentNode;

        if (!Helper.validarCamposObrigatorios([$('#categoria-tipo')], [$('#valida-categoria-tipo')]))
            return;

        this.categoriaModel.tipo = $(elemento).find('#categoria-tipo').val();

        Helper.realizarChamadaAjax('Categoria/Cadastrar', this.categoriaModel, 'POST');
    }

    deletar(el) {

        let elemento = el.parentNode.parentNode;

        this.categoriaModel.identificadorUnico = $(elemento).find('.categoria-identificador-unico').val();
        
        Helper.realizarChamadaAjax('/Categoria/Deletar', this.categoriaModel, 'DELETE');
    }
}