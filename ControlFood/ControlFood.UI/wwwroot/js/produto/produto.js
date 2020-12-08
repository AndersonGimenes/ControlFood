class Produto {

    constructor() {
        this.codigoInterno;
        this.nome;
        this.valorVenda;
        this.subCategoria;

        // input valor venda produto
        $("#valor-venda").mask('#.##0,00', { reverse: true });
    }

    cadastrar(el) {

        let elemento = el.parentNode;

        // validar campos obrigatorios
        var arrayElementos = [$("#nome"), $("#codigo-interno"), $("#valor-venda")];
        var arraySpans = [$("#span-valida-nome"), $("#span-valida-codigo-interno"), $("#span-valida-valor-venda")];

        if (!Helper.validarCamposObrigatorios(arrayElementos, arraySpans))
            return;

        let subCategoria = new SubCategoria($(elemento).find("#identificador-unico-sub-categoria").val());

        this.codigoInterno = $(elemento).find("#codigo-interno").val();
        this.nome = $(elemento).find("#nome").val();
        this.valorVenda = $(elemento).find("#valor-venda").val();
        this.subCategoria = subCategoria;

        Helper.realizarChamadaAjax("Produto/Cadastrar", this, "POST");
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

        $("#valor-venda-render-modal")
            .html(`<span id='span-valida-valor-venda'></span><input type='text' class='form-control' id='valor-venda-modal' value='${valor}'/>`);

        $("#identificador-unico-render-modal")
            .html(`<input type='hidden' class='form-control' id='identificador-unico-modal' value='${$(elemento).find('.identificador-unico').val()}'/>`);

        // input valor venda produto modal
        $("#valor-venda-modal").mask('#.##0,00', { reverse: true });
    }

    atualizar(el) {

        let elemento = el.parentNode.parentNode;

        let valorVenda = $(elemento).find("#valor-venda-modal");

        this.identificadorUnico = $(elemento).find('#identificador-unico-modal').val();
        this.valorVenda = valorVenda.val();

        if (!Helper.validarCamposObrigatorios([valorVenda], $(elemento).find("#span-valida-valor-venda")))
            return;

        Helper.realizarChamadaAjax("/Produto/Atualizar", this, "PUT");
    }

    deletar(el) {

        var elemento = el.parentNode.parentNode;

        this.identificadorUnico = $(elemento).find(".identificador-unico").val();

        Helper.realizarChamadaAjax("/Produto/Deletar", this, "DELETE");
    }
}