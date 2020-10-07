$(document).ready(function () {
    var subCategoria = new SubCategoria();
    subCategoria.cadastrar();
    subCategoria.deletar();
    subCategoria.editar();
    subCategoria.atualizar();
});

class SubCategoria {

    cadastrar = function () {
        $("#btn-cadastrar").click(function () {
            var helper = new ComumHelper();
            var elemento = this.parentNode;

            if (!helper.validarCamposObrigatorios())
                return;

            var categoria = {
                IdentificadorUnico: helper.obterValorPorId(elemento, "identificador-unico-categoria-nova")
            }

            var data = {
                Tipo: helper.obterValorPorId(elemento, "tipo"),
                Categoria: categoria,
                Indicador: $(elemento).find("input[name='indicador']:checked").val()
            }

            helper.realizarChamadaAjax("SubCategoria/Cadastrar", data, "POST");
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

            helper.realizarChamadaAjax("/SubCategoria/Deletar", data, "DELETE");           
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
            }
                        
            helper.realizarChamadaAjax("/SubCategoria/Atualizar", data, "PUT");  

        });
    }
}
