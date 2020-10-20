$(document).ready(function () {
    var categoria = new Categoria();
    categoria.cadastrar(categoria);
    categoria.deletar(categoria);
});

class Categoria {

    constructor() {
        this._helper = new ComumHelper();
    }

    cadastrar = function (instanciaCategoria) {

        $("#btn-cadastrar").click(function () {
            var elemento = this.parentNode;

            if (!instanciaCategoria._helper.validarCamposObrigatorios([$("#tipo")], [$("#span-valida-tipo")]))
                return;

            var data = {
                Tipo: instanciaCategoria._helper.obterValorPorId(elemento, "tipo")
            }

            instanciaCategoria._helper.realizarChamadaAjax("Categoria/Cadastrar", data, "POST");
        });
    }

    deletar = function (instanciaCategoria) {
        $(".btn-deletar").click(function () {

            var elementoTr = this.parentNode.parentNode;

            var data = {
                IdentificadorUnico: instanciaCategoria._helper.obterValorPorClasse(elementoTr, "identificador-unico"),
                Tipo: instanciaCategoria._helper.obterTextoPorClasse(elementoTr, "tipo")
            }

            instanciaCategoria._helper.realizarChamadaAjax("/Categoria/Deletar", data, "DELETE");

        });
    }
}