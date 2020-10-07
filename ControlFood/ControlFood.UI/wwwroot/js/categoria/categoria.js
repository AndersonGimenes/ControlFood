$(document).ready(function () {
    var categoria = new Categoria();
    categoria.cadastrar(categoria);
    categoria.deletar();
});

class Categoria {

    cadastrar = function () {

        $("#btn-cadastrar").click(function () {

            var helper = new ComumHelper();
            var elemento = this.parentNode;

            if (!helper.validarCamposObrigatorios())
                return;

            var data = {
                Tipo: helper.obterValorPorId(elemento, "tipo")
            }

            helper.realizarChamadaAjax("Categoria/Cadastrar", data, "POST");
        });
    }

    deletar = function () {
        $(".btn-deletar").click(function () {

            var helper = new ComumHelper();
            var elementoTr = this.parentNode.parentNode;

            var data = {
                IdentificadorUnico: helper.obterValorPorClasse(elementoTr, "identificador-unico"),
                Tipo: helper.obterTextoPorClasse(elementoTr, "tipo")
            }

            helper.realizarChamadaAjax("/Categoria/Deletar", data, "DELETE");

        });
    }
}