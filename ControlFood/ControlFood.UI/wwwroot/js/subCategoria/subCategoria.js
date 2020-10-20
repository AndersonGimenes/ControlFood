$(document).ready(function () {
    var subCategoria = new SubCategoria();
    subCategoria.cadastrar(subCategoria);
    subCategoria.deletar(subCategoria);
    subCategoria.editar(subCategoria);
    subCategoria.atualizar(subCategoria);
});

class SubCategoria {

    constructor() {
        this._helper = new ComumHelper();
    }

    cadastrar = function (instanciaSubCategoria) {
        $("#btn-cadastrar").click(function () {
            var elemento = this.parentNode;

            if (!instanciaSubCategoria._helper.validarCamposObrigatorios([$("#tipo")], [$("#span-valida-tipo")]))
                return;

            var categoria = {
                IdentificadorUnico: instanciaSubCategoria._helper.obterValorPorId(elemento, "identificador-unico-categoria-nova")
            }

            var data = {
                Tipo: instanciaSubCategoria._helper.obterValorPorId(elemento, "tipo"),
                Categoria: categoria,
                Indicador: $(elemento).find("input[name='indicador']:checked").val()
            }

            instanciaSubCategoria._helper.realizarChamadaAjax("SubCategoria/Cadastrar", data, "POST");
        });
    }

    deletar = function (instanciaSubCategoria) {
        $(".btn-deletar").click(function () {
            var elementoTr = this.parentNode.parentNode;

            var data = {
                IdentificadorUnico: instanciaSubCategoria._helper.obterValorPorClasse(elementoTr, "identificador-unico"),
                Tipo: instanciaSubCategoria._helper.obterTextoPorClasse(elementoTr, "tipo")
            }

            instanciaSubCategoria._helper.realizarChamadaAjax("/SubCategoria/Deletar", data, "DELETE");           
        });
    }

    editar = function (instanciaSubCategoria) {
        $(".btn-editar").click(function () {
            var elementoTr = this.parentNode.parentNode;
            var indicadorCozinha = "0";
           
            var data = {
                Tipo: instanciaSubCategoria._helper.obterTextoPorClasse(elementoTr, "tipo"),
                TipoCategoria: instanciaSubCategoria._helper.obterTextoPorClasse(elementoTr, "tipo-categoria"),
                Indicador: instanciaSubCategoria._helper.obterValorPorClasse(elementoTr, "indicador"),
                IdentificadorUnico: instanciaSubCategoria._helper.obterValorPorClasse(elementoTr, "identificador-unico"),
                IdentificadorUnicoCategoria: instanciaSubCategoria._helper.obterValorPorClasse(elementoTr, "identificador-unico-categoria")
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

    atualizar = function (instanciaSubCategoria) {
        $("#btn-atualizar").click(function () {
            var elementoTr = this.parentNode.parentNode;

            var categoria = {
                IdentificadorUnico: instanciaSubCategoria._helper.obterValorPorClasse(elementoTr, "identificador-unico-categoria"),
                Tipo: instanciaSubCategoria._helper.obterValorPorClasse(elementoTr, "tipo-categoria")
            }

            var data = {
                Tipo: instanciaSubCategoria._helper.obterValorPorClasse(elementoTr, "tipo"),
                Categoria: categoria,
                Indicador: $(elementoTr).find("input[name='indicador']:checked").val(),
                IdentificadorUnico: instanciaSubCategoria._helper.obterValorPorClasse(elementoTr, "identificador-unico"),
            }
                        
            instanciaSubCategoria._helper.realizarChamadaAjax("/SubCategoria/Atualizar", data, "PUT");  

        });
    }
}
