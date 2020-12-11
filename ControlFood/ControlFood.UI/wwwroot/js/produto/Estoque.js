class Estoque {

    constructor() {

        $('#estoque-valor-compra-unitario-modal').mask('#.##0,00', { reverse: true });
        $('#estoque-valor-compra-total-modal').mask('#.##0,00', { reverse: true });

        $('#estoque-valor-compra-unitario-modal-filho').mask('#.##0,00', { reverse: true });
        $('#estoque-valor-compra-total-modal-filho').mask('#.##0,00', { reverse: true });
    }

    cadastrar(el) {

        let elemento = el.parentNode.parentNode;

        let quantidade = $(elemento).find('#estoque-quantidade-modal');
        let dataValidade = $(elemento).find('#estoque-data-validade-modal');
        let valorCompraUnitario = $(elemento).find('#estoque-valor-compra-unitario-modal');
        let valorCompraTotal = $(elemento).find('#estoque-valor-compra-total-modal');

        let arrayElementos = [quantidade, valorCompraUnitario, valorCompraTotal, dataValidade];
        let arraySpans = [$("#valida-estoque-quantidade-modal"), $("#valida-estoque-valor-compra-unitario-modal"), $("#valida-estoque-valor-compra-total-modal"), $("#valida-estoque-data-validade-modal")];

        if (!Helper.validarCamposObrigatorios(arrayElementos, arraySpans))
            return;

        produto.produtoModel.identificadorUnico = $(elemento).find('#produto-identificador-unico-modal').val();
        produto.produtoModel.estoque.quantidade = quantidade.val();
        produto.produtoModel.estoque.dataValidade = dataValidade.val();
        produto.produtoModel.estoque.valorCompraUnidade = valorCompraUnitario.val();
        produto.produtoModel.estoque.valorCompraTotal = valorCompraTotal.val();

        Helper.realizarChamadaAjax('Produto/CadastrarEstoque', produto.produtoModel, 'POST');
    }

    consultar(el) {

        let elemento = el.parentNode.parentNode;

        produto.produtoModel.estoque.identificadorUnico = $(elemento).find('.produto-identificador-unico').val();
        EstoqueModel.nomeProduto = $(elemento).find('.produto-nome').text();

        Helper.realizarChamadaAjax('Produto/BuscarEstoque', produto.produtoModel.estoque, 'GET', this._acaoSucesso);
    }

    atualizar(el) {

        let elemento = el.parentNode.parentNode;

        let quantidade = $(elemento).find('#estoque-quantidade-modal-filho');
        let dataValidade = $(elemento).find('#estoque-data-validade-modal-filho');
        let valorCompraUnitario = $(elemento).find('#estoque-valor-compra-unitario-modal-filho');
        let valorCompraTotal = $(elemento).find('#estoque-valor-compra-total-modal-filho');

        let arrayElementos = [quantidade, valorCompraUnitario, valorCompraTotal, dataValidade];
        let arraySpans = [$("valida-estoque-quantidade-modal-filho"), $("#valida-estoque-valor-compra-unitario-modal-filho"), $("#valida-estoque-valor-compra-total-modal-filho"), $("#span-valida-data-validade-modal")];

        if (!Helper.validarCamposObrigatorios(arrayElementos, arraySpans))
            return;

        produto.produtoModel.estoque.identificadorUnico = $(elemento).find('#estoque-identificador-unico-modal-filho').val();
        produto.produtoModel.estoque.quantidade = quantidade.val();
        produto.produtoModel.estoque.dataValidade = dataValidade.val();
        produto.produtoModel.estoque.valorCompraUnidade = valorCompraUnitario.val();
        produto.produtoModel.estoque.valorCompraTotal = valorCompraTotal.val();

        Helper.realizarChamadaAjax('Produto/AtualizarEstoque', produto.produtoModel, 'PUT');
    }

    deletar(el) {

        let elemento = el.parentNode.parentNode;

        produto.produtoModel.estoque.identificadorUnico = $(elemento).find('.estoque-identificador-unico-modal').val();

        Helper.realizarChamadaAjax('/Produto/DeletarEstoque', produto.produtoModel, 'DELETE');

    }

    popularModalCadastro(el) {

        let elemento = el.parentNode.parentNode;

        $('#estoque-cadastrar-modal').modal('show');

        $('#div-produto-nome-modal')
            .html(`<input type='text' class='form-control' disabled='disabled' value='${$(elemento).find('.produto-nome').text()}'/>`);

        $('#div-produto-identificador-unico-modal')
            .html(`<input id='produto-identificador-unico-modal' type='hidden' value='${$(elemento).find('.produto-identificador-unico').val()}'/>`);

        this._ajustarValorCompra($('#estoque-valor-compra-unitario-modal'), $('#estoque-valor-compra-total-modal'), $('#estoque-quantidade-modal'));
    }

    popularModalAtualizar(el) {

        let elemento = el.parentNode.parentNode;

        let valorUnitario = Helper.formatarValorOutput($(elemento).find('.estoque-valor-compra-unitario-modal').text());
        let valorTotal = Helper.formatarValorOutput($(elemento).find('.estoque-valor-compra-total-modal').text());
        let dataValidade = Helper.formatarDataOutput($(elemento).find('.estoque-data-validade-modal').text());

        $('#estoque-atualizar-modal').modal('show');

        $('#produto-nome-modal-filho').text(EstoqueModel.nomeProduto)
        $('#estoque-quantidade-modal-filho').val(parseInt($(elemento).find('.estoque-quantidade-modal').text()));
        $('#estoque-valor-compra-unitario-modal-filho').val(valorUnitario);
        $('#estoque-valor-compra-total-modal-filho').val(valorTotal);
        $('#estoque-data-validade-modal-filho').val(dataValidade);

        $('#div-estoque-identificador-unico-modal-filho')
            .html(`<input id='estoque-identificador-unico-modal-filho' type='hidden' value='${$(elemento).find('.estoque-identificador-unico-modal').val()}'/>`);

        this._ajustarValorCompra($('#estoque-valor-compra-unitario-modal-filho'), $('#estoque-valor-compra-total-modal-filho'), $('#estoque-quantidade-modal-filho'));
    }

    limparCampoInput = function () {
        $("#btn-close").click(function () {
            $("#quantidade-cadastro").val('');
            $("#data-validade-cadastro").val('');
            $("#valor-compra-unitario-cadastro").val('');
            $("#valor-compra-total-cadastro").val('');
        });
    }

    // metodos privados
    _acaoSucesso(response) {

        $('#estoque-consultar-modal').modal('show');

        $('#produto-nome-modal').text(`Consulta estoque ${EstoqueModel.nomeProduto}`);

        // monta html modal consultar estoque
        let html = response.map(produto =>
            `<tr>
                <td class='estoque-quantidade-modal'> ${produto.estoque.quantidade}</td>
                <td class='estoque-valor-compra-unitario-modal'>R$ ${produto.estoque.valorCompraUnidade.toFixed(2).toString().replace('.', ',')}</td>
                <td class='estoque-valor-compra-total-modal'>R$ ${produto.estoque.valorCompraTotal.toFixed(2).toString().replace('.', ',')}</td>
                <td class='estoque-data-validade-modal'>${Helper.formatarDataOutput(produto.estoque.dataValidade)}</td>
                <td><button type='button' class='btn btn-primary' onclick='produto.estoque.popularModalAtualizar(this);'>Editar</button></td>
                <td><button type='button' class='btn btn-danger' onclick='produto.estoque.deletar(this);'> Deletar</button></td>
                <td><input type = 'hidden' class='estoque-identificador-unico-modal' value='${produto.estoque.identificadorUnico}'/></td>
            </tr>`
        ).join('');

        $('#render-lista-estoque').html(html);

        let somaQuantidade = response.reduce((total, produto) => total += produto.estoque.quantidade, 0);
        $('#estoque-quantidade-total-modal').text(somaQuantidade);

    }

    _ajustarValorCompra(valorUnitario, valorTotal, quantidade) {

        // autocompleta o valor de compra total com base no valor unitario
        valorUnitario.blur(() => {

            let valorFormatado = valorUnitario.val().replace('.', '').replace(',', '.');

            let resultado = parseInt(quantidade.val()) * valorFormatado;

            valorTotal.val(resultado.toFixed(2).toString().replace('.', ','));

        });

        // autocompleta o valor de compra unitario com base no valor total
        valorTotal.blur(() => {

            let valorFormatado = valorTotal.val().replace('.', '').replace(',', '.');

            let resultado = valorFormatado / parseInt(quantidade.val());

            valorUnitario.val(resultado.toFixed(2).toString().replace('.', ','));
        });
    }
}