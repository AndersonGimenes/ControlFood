class SubCategoria {

    constructor(identificadorUnico = 0) {
        this.identificadorUnico = identificadorUnico;
        this.tipo;
        this.indicador;
        this.categoria;
    }

    cadastrar(el) {

        let elemento = el.parentNode;

        if (!Helper.validarCamposObrigatorios([$("#tipo")], [$("#span-valida-tipo")]))
            return;

        let categoria = new Categoria($(elemento).find("#identificador-unico-categoria").val());

        this.tipo = $(elemento).find("#tipo").val();
        this.categoria = categoria;
        this.indicador = $(elemento).find("input[name='indicador']:checked").val();

        Helper.realizarChamadaAjax("SubCategoria/Cadastrar", this, "POST");
    }

    deletar(el) {

        let elemento = el.parentNode.parentNode;

        this.identificadorUnico = $(elemento).find(".identificador-unico").val();

        Helper.realizarChamadaAjax("/SubCategoria/Deletar", this, "DELETE");

    }

    popularModalAtualizar(el) {

        let elemento = el.parentNode.parentNode;
        let indicadorCozinha = "0";

        $("#modal-atualizar").modal("show");

        $("#categoria-tipo")
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find(".tipo-categoria").text()}'/>`);

        $("#sub-categoria-tipo")
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find(".tipo").text()}'/>`);

        $("#identificador-unico")
            .html(`<input type='hidden' class='identificador-unico' value='${$(elemento).find(".identificador-unico").val()}'/>`);

        let indicador = $(elemento).find(".indicador").val();

        if (indicador === indicadorCozinha) {
            $("#indicador-cozinha").prop("checked", true);
            $("#indicador-bar").prop("checked", false);
        }
        else {
            $("#indicador-cozinha").prop("checked", false);
            $("#indicador-bar").prop("checked", true);
        }
    }

    atualizar(el) {

        let elemento = el.parentNode.parentNode;

        this.indicador = $(elemento).find("input[name='indicador']:checked").val()
        this.identificadorUnico = $(elemento).find(".identificador-unico").val();

        Helper.realizarChamadaAjax("/SubCategoria/Atualizar", this, "PUT");
    }
}
