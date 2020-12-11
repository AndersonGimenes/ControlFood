class Produto {

    constructor() {

        this.produtoModel = new ProdutoModel();
        this.estoque = new Estoque();

        $("#produto-valor-venda").mask('#.##0,00', { reverse: true });
    }

    cadastrar(el) {

        let elemento = el.parentNode;

        let nome = $(elemento).find('#produto-nome');
        let codigoInterno = $(elemento).find('#produto-codigo-interno');
        let valorVenda = $(elemento).find('#produto-valor-venda');

        let arrayElementos = [nome, codigoInterno, valorVenda];
        let arraySpans = [$('#valida-produto-nome'), $('#valida-produto-codigo-interno'), $('#valida-produto-valor-venda')];

        if (!Helper.validarCamposObrigatorios(arrayElementos, arraySpans))
            return;

        this.produtoModel.codigoInterno = codigoInterno.val();
        this.produtoModel.nome = nome.val();
        this.produtoModel.valorVenda = valorVenda.val();
        this.produtoModel.subCategoria.identificadorUnico = $(elemento).find('#sub-categoria-identificador-unico').val();

        Helper.realizarChamadaAjax('Produto/Cadastrar', this.produtoModel, 'POST');
    }

    atualizar(el) {

        let elemento = el.parentNode.parentNode;

        let valorVenda = $(elemento).find('#produto-valor-venda-modal');

        this.produtoModel.identificadorUnico = $(elemento).find('#produto-identificador-unico-modal').val();
        this.produtoModel.valorVenda = valorVenda.val();

        if (!Helper.validarCamposObrigatorios([valorVenda], $(elemento).find('#valida-produto-valor-venda-modal')))
            return;

        Helper.realizarChamadaAjax('/Produto/Atualizar', this.produtoModel, 'PUT');
    }

    deletar(el) {

        let elemento = el.parentNode.parentNode;

        this.produtoModel.identificadorUnico = $(elemento).find('.produto-identificador-unico').val();

        Helper.realizarChamadaAjax('/Produto/Deletar', this.produtoModel, 'DELETE');
    }

    popularModalAtualizar(el) {

        let elemento = el.parentNode.parentNode;

        let valor = Helper.formatarValorOutput($(elemento).find('.produto-valor-venda').text());

        $('#produto-atualizar-modal').modal('show');

        $('#div-sub-categoria-tipo-modal')
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.sub-categoria-tipo').text()}'/>`);

        $('#div-produto-nome-modal')
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.produto-nome').text()}'/>`);

        $('#div-produto-codigo-interno-modal')
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.produto-codigo-interno').text()}'/>`);

        $('#div-produto-valor-venda-modal')
            .html(`<span id='valida-produto-valor-venda-modal'></span><input type='text' class='form-control' id='produto-valor-venda-modal' value='${valor}'/>`);

        $('#div-produto-identificador-unico-modal')
            .html(`<input type='hidden' class='form-control' id='produto-identificador-unico-modal' value='${$(elemento).find('.produto-identificador-unico').val()}'/>`);

        $('#produto-valor-venda-modal').mask('#.##0,00', { reverse: true });
    }

}