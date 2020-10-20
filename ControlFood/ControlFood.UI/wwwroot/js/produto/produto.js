$(document).ready(function () {
    var produto = new Produto();
    produto.checkIgrediente(produto);
    produto.checkEstoque(produto);
    produto.cadastrar(produto);
});

class Produto {

    constructor(){
        this._helper = new ComumHelper();
    }

    checkIgrediente = function (instanciaProduto) {
        instanciaProduto._helper.checkHiddenOnOff("#especifico-igredientes", "#span-especifico-igredientes");
    }

    checkEstoque = function (instanciaProduto) {
        instanciaProduto._helper.checkHiddenOnOff("#especifico-estoque", "#span-especifico-estoque");
    }

    cadastrar = function (instanciaProduto) {

        $("#btn-cadastrar").click(function () {
            var elemento = this.parentNode;

            if (!instanciaProduto._validarCampos(elemento))
                return;

            var subCategoria = {
                IdentificadorUnico: instanciaProduto._helper.obterValorPorId(elemento, "identificador-unico-sub-categoria-nova")
            }

            var estoque = {
                ValorCompraUnidade: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-unitario"),
                ValorCompraTotal: instanciaProduto._helper.obterValorPorId(elemento, "valor-compra-total"),
                DataValidade: instanciaProduto._helper.obterValorPorId(elemento, "data-validade"),
                Quantidade: instanciaProduto._helper.obterValorPorId(elemento, "quantidade")
            }

            var data = {
                CodigoInterno: instanciaProduto._helper.obterValorPorId(elemento, "codigo-interno"),
                Nome: instanciaProduto._helper.obterValorPorId(elemento, "nome"),
                ValorVenda: instanciaProduto._helper.obterValorPorId(elemento, "valor-venda"),
                SubCategoria: subCategoria,
                Estoque: estoque
            }

            instanciaProduto._helper.realizarChamadaAjax("Produto/Cadastrar", data, "POST");
        });
    }

    _validarCampos = function (elemento) {

        var arrayElementos = [$("#nome"), $("#codigo-interno"), $("#valor-venda")];
        var arraySpans = [$("#span-valida-nome"), $("#span-valida-codigo-interno"), $("#span-valida-valor-venda")];


        if (!this._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
            return false;

        var className = $(elemento).find("#span-especifico-estoque")[0].className;

        if (className != "d-none") {
            arrayElementos = [$("#data-validade"), $("#quantidade")];
            arraySpans = [$("#span-valida-data-validade"), $("#span-valida-quantidade")];

            if (!this._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
                return false;
        }

        return true;
    }
}