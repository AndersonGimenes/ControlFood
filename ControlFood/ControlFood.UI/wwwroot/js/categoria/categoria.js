$(document).ready(function () {
    var categoria = new Categoria();
    categoria.deletar();
});

class Categoria {

    deletar = function () {
        $(".btn-deletar").click(function () {

            var helper = new ComumHelper();
            var elementoTr = this.parentNode.parentNode;

            helper.ajaxDeletarItem("/Categoria/Deletar", elementoTr);

        });
    }
}