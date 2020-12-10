class Produto {

    constructor() {

        this.produtoModel = new ProdutoModel();

        this.estoque = new Estoque();
        
        // input valor venda produto
        $("#valor-venda").mask('#.##0,00', { reverse: true });
    }

    cadastrar(el) {

        let elemento = el.parentNode;

        // validar campos obrigatorios
        let arrayElementos = [$('#nome'), $('#codigo-interno'), $('#valor-venda')];
        let arraySpans = [$('#span-valida-nome'), $('#span-valida-codigo-interno'), $('#span-valida-valor-venda')];

        if (!Helper.validarCamposObrigatorios(arrayElementos, arraySpans))
            return;

        // ajustar este ponto
        let subCategoria = new SubCategoria($(elemento).find('#identificador-unico-sub-categoria').val());

        this.produtoModel.codigoInterno = $(elemento).find('#codigo-interno').val();
        this.produtoModel.nome = $(elemento).find('#nome').val();
        this.produtoModel.valorVenda = $(elemento).find('#valor-venda').val();
        this.produtoModel.subCategoria = subCategoria;

        Helper.realizarChamadaAjax('Produto/Cadastrar', this.produtoModel, 'POST');
    }

    atualizar(el) {

        let elemento = el.parentNode.parentNode;

        let valorVenda = $(elemento).find("#valor-venda-modal");

        this.produtoModel.identificadorUnico = $(elemento).find('#identificador-unico-modal').val();
        this.produtoModel.valorVenda = valorVenda.val();

        if (!Helper.validarCamposObrigatorios([valorVenda], $(elemento).find("#span-valida-valor-venda-modal")))
            return;

        Helper.realizarChamadaAjax("/Produto/Atualizar", this.produtoModel, "PUT");
    }

    deletar(el) {

        let elemento = el.parentNode.parentNode;

        this.produtoModel.identificadorUnico = $(elemento).find(".identificador-unico").val();

        Helper.realizarChamadaAjax("/Produto/Deletar", this.produtoModel, "DELETE");
    }



    popularModalAtualizar(el) {

        let elemento = el.parentNode.parentNode;

        let valor = Helper.formatarValorOutput($(elemento).find('.valor-venda').text());

        // mostrar modal
        $("#modal-atualizar").modal("show");

        // preencher nome produto e identificador do produto
        $("#sub-categoria-tipo-modal")
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.tipo-sub-categoria').text()}'/>`);

        $("#produto-nome-modal")
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.nome').text()}'/>`);

        $("#produto-codigo-modal")
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.codigo-interno').text()}'/>`);

        $("#produto-valor-venda-modal")
            .html(`<span id='span-valida-valor-venda-modal'></span><input type='text' class='form-control' id='valor-venda-modal' value='${valor}'/>`);

        $("#produto-identificador-unico-modal")
            .html(`<input type='hidden' class='form-control' id='identificador-unico-modal' value='${$(elemento).find('.identificador-unico').val()}'/>`);

        // input valor venda produto modal
        $("#valor-venda-modal").mask('#.##0,00', { reverse: true });
    }

}