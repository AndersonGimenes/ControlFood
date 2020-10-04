$(document).ready(function () {
    var subCategoria = new SubCategoria();
    subCategoria.deletar();
    subCategoria.editar();
    subCategoria.atualizar();
});

class SubCategoria {

    deletar = function () {
        $(".btn-deletar").click(function () {

            var helper = new ComumHelper();
            var elementoTr = this.parentNode.parentNode;

            helper.ajaxDeletarItem("/SubCategoria/Deletar", elementoTr);           
        });
    }

    editar = function () {
        $(".btn-editar").click(function () {

            var helper = new ComumHelper();
            var elementoTr = this.parentNode.parentNode;
            var indicadorCozinha = "0";
           
            var data = {
                Tipo: helper.obterTextoPorClasse(elementoTr, "tipo"),
                TipoCategoria: helper.obterTextoPorClasse(elementoTr, "tipo-categoria"),
                Indicador: helper.obterValorPorClasse(elementoTr, "indicador"),
                IdentificadorUnico: helper.obterValorPorClasse(elementoTr, "identificador-unico"),
                IdentificadorUnicoCategoria: helper.obterValorPorClasse(elementoTr, "identificador-unico-categoria")
            }

            $("#modal-atualizar").modal("show");

            $("#categoria-tipo").html("<input type='text' class='form-control tipo-categoria' disabled='disabled' value='" + data.TipoCategoria + "'/>");
            $("#sub-categoria-tipo").html("<input type='text' class='form-control tipo' disabled='disabled' value='" + data.Tipo + "'/>");
            $("#identificador-unico").html("<input type='hidden' class='identificador-unico' value='" + data.IdentificadorUnico + "'/>");
            $("#identificador-unico-categoria").html("<input type='hidden' class='identificador-unico-categoria' value='" + data.IdentificadorUnicoCategoria + "'/>");
            
            if (data.Indicador === indicadorCozinha) {
                $("#indicador-cozinha").prop("checked", true);
                $("#indicador-bar").prop("checked", false);
            }
            else {
                $("#indicador-cozinha").prop("checked", false);
                $("#indicador-bar").prop("checked", true);
            }

            console.log(data);
        });
    }

    atualizar = function () {
        $("#btn-atualizar").click(function () {

            var helper = new ComumHelper();
            var elementoTr = this.parentNode.parentNode;

            var categoria = {
                IdentificadorUnico : helper.obterValorPorClasse(elementoTr, "identificador-unico-categoria"),
                Tipo: helper.obterValorPorClasse(elementoTr, "tipo-categoria")
            }

            var data = {
                Tipo: helper.obterValorPorClasse(elementoTr, "tipo"),
                Categoria: categoria,
                Indicador: $(elementoTr).find("input[name='indicador']:checked").val(),
                IdentificadorUnico: helper.obterValorPorClasse(elementoTr, "identificador-unico"),
                IdentificadorUnicoCategoria: helper.obterValorPorClasse(elementoTr, "identificador-unico-categoria")
            }

            $.ajax({
                url: "SubCategoria/Atualizar",
                type: 'PUT',
                data: data,
                success: function (response) {
                    console.log(response);
                    alert('Atualização concluida com sucesso.');
                    window.location.reload();                   
                },
                error: function (XMLHttpRequest) {
                    alert('Erro: ' + XMLHttpRequest.responseText)
                    console.log(XMLHttpRequest.responseText);
                }
            });

        });

    }

    atualizarCheckedModal = function () {

    }
}
