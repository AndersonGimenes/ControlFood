$(document).ready(function () {
    var subCategoria = new SubCategoria();
    subCategoria.deletar();
    subCategoria.editar();
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
                    IdentificadorUnico: helper.obterValorPorClasse(elementoTr, "identificador-unico"),
                    Tipo: helper.obterTextoPorClasse(elementoTr, "tipo")
                },
                success: function () {
                    window.location.reload();
                    $("#tipo").val('');
                },
                error: function (XMLHttpRequest) {
                    alert('Erro: ' + XMLHttpRequest.responseText)
                    console.log(XMLHttpRequest.responseText);

                }
            });
        });
    }

    editar = function () {
        $(".btn-editar").click(function () {

            var helper = new ComumHelper();
            var elementoTr = this.parentNode.parentNode

            var data = {
                Tipo: helper.obterTextoPorClasse(elementoTr, "tipo"),
                TipoCategoria: helper.obterTextoPorClasse(elementoTr, "tipo-categoria"),
                Indicador: helper.obterValorPorClasse(elementoTr, "indicador"),
                IdentificadorUnico: helper.obterValorPorClasse(elementoTr, "identificador-unico"),
                IdentificadorUnicoCategoria: helper.obterValorPorClasse(elementoTr, "identificador-unico-categoria")
            }

            $("#Categoria_IdentificadorUnico").val(data.IdentificadorUnicoCategoria).prop("disabled", true);
            $("#tipo").val(data.Tipo).prop("disabled", true);
            $("#btn-cadastrar").text("Atualizar").prop("type", "button");

            console.log(data);
        });
    };
}
