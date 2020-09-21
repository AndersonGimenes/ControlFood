$(document).ready(function () {
    var subCategoria = new SubCategoria();
    subCategoria.deletar();
});

class SubCategoria {

    deletar = function () {
        $(".btn-deletar").click(function () {

            var helper = new ComumHelper();
            var elementoTr = this.parentNode.parentNode;

            $.ajax({
                url: "/SubCategoria/Deletar",
                type: 'DELETE',
                data: {
                    IdentificadorUnico: helper.obterId(elementoTr),
                    Tipo: helper.obterTipo(elementoTr)
                },
                success: function () {
                    window.location.reload();
                    $("#Tipo").val('');
                },
                error: function (XMLHttpRequest) {
                    alert('Erro: ' + XMLHttpRequest.responseText)
                    console.log(XMLHttpRequest.responseText);

                }
            });
        });
    }
}
