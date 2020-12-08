class Categoria {

    constructor(identificadorUnico = 0) {
        this.identificadorUnico = identificadorUnico;
        this.tipo;
    }

    cadastrar(el) {

        let elemento = el.parentNode;

        if (!Helper.validarCamposObrigatorios([$("#tipo")], [$("#span-valida-tipo")]))
            return;

        this.tipo = $(elemento).find("#tipo").val();

        Helper.realizarChamadaAjax("Categoria/Cadastrar", this, "POST");
    }

    deletar(el) {

        let elemento = el.parentNode.parentNode;

        this.identificadorUnico = $(elemento).find(".identificador-unico").val();
        
        Helper.realizarChamadaAjax("/Categoria/Deletar", this, "DELETE");
    }
}