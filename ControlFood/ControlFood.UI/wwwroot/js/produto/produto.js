$(document).ready(function () {
    var produto = new Produto();
    produto.cadastrar(produto);
    produto.cadastrarEstoque(produto);
});

class Produto {

    constructor() {
        this._helper = new ComumHelper();
    }

    cadastrar = function (instanciaProduto) {

        $("#btn-cadastrar").click(function () {
            var elemento = this.parentNode;

            var arrayElementos = [$("#nome"), $("#codigo-interno"), $("#valor-venda")];
            var arraySpans = [$("#span-valida-nome"), $("#span-valida-codigo-interno"), $("#span-valida-valor-venda")];

            if (!instanciaProduto._helper.validarCamposObrigatorios(arrayElementos, arraySpans))
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

    cadastrarEstoque = function (instanciaProduto) {
        $(".btn-cadastro-estoque").click(function () {
            var elementoTr = this.parentNode.parentNode;

            var produto = {
                Nome: instanciaProduto._helper.obterTextoPorClasse(elementoTr, "nome"),
                IdentificadorUnico: instanciaProduto._helper.obterValorPorClasse(elementoTr, "identificador-unico")
            }

            $("#modal-cadastro-estoque").modal("show");

            $("#produto-nome").html("<input type='text' class='form-control' disabled='disabled' value='" + produto.Nome + "'/>");
            $("#produto-identificador-unico").html("<input type='hidden' value='" + produto.IdentificadorUnico + "'/>");
            
        });
    }
}