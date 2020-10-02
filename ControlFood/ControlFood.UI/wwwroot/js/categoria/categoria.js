$(document).ready(function () {
    var categoria = new Categoria();
    categoria.deletar();
});

class Categoria {

    deletar = function () {
        $(".btn-deletar").click(function () {

            var helper = new ComumHelper();
            var elementoTr = this.parentNode.parentNode;

            $.ajax({
                url: "/Categoria/Deletar",
                type: 'DELETE',
                data: {
                    IdentificadorUnico: helper.obterValorPorClasse(elementoTr, "identificador-unico"),
                    Tipo: helper.obterTextoPorClasse(elementoTr, "tipo")
                },
                success: function () {
                    window.location.reload();
                    $("#tipo").val('');
                },
                error: function (XMLHttpRequest) {
                    alert('Erro: ' + XMLHttpRequest.responseText )
                    console.log(XMLHttpRequest.responseText);
                    
                }
            });
        });
    }
}