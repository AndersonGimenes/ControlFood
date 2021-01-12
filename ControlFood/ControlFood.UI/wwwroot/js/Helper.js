class Helper {

    static validarCamposObrigatorios(arrayElementos, arraySpan) {

        let valido = true;
        
        for (let i = 0; i < arrayElementos.length; i++) {
            let valor = arrayElementos[i].val();
            let span = arraySpan[i];

            if ($.trim(valor) === "") {
                $(span).html("<p class='text-danger'>Campo obrigatório !<p/>");
                valido = false;
            }
            else {
                $(span).html("");
            }
        }

        return valido;
    }

    static realizarChamadaAjax(url, data, acao, acaoSucesso) {

        $.ajax({
            url: url,
            type: acao,
            data: data,
            success: acaoSucesso == null ? Helper._acaoSucessoDefault : acaoSucesso,

            error: (XMLHttpRequest) => {
                $.confirm({
                    title: 'Erro',
                    type: 'red',
                    content: XMLHttpRequest.responseText,
                    buttons: {
                        confirm: () => {
                            console.log(XMLHttpRequest.responseText);
                        }
                    }
                });
            }
        });
    }

    static formatarValorOutput(valorString) {
        let valorSemCifrao = valorString.replace('R$', '').replace(' ', '');

        if (valorSemCifrao.length <= 6)
            return valorSemCifrao;

        let valorFormatado = valorSemCifrao
            .split('') // transforma string em array
            .reverse() // inverte o array
            .map((valor, index) => { // formatata no padrão
                if (index == 5) {
                    valor = '.' + valor;
                }

                return valor;

            }).reverse() // inverte o array(volta posição original)
            .join(''); // concatena array para string

        return valorFormatado;
    }

    static formatarDataOutput(data) {
        let novaData = new Date(data);
        let dia = novaData.getDate() < 10 ? `0${novaData.getDate()}` : novaData.getDate();
        let mes = novaData.getMonth() + 1 < 10 ? `0${novaData.getMonth() + 1}` : novaData.getMonth() + 1;

        return `${dia}/${mes}/${novaData.getFullYear()}`;
    }

    //metodos privados
    static _acaoSucessoDefault(response) {
        // inclusao de novo cadastro cai no if
        if (response.mensagem === '' || response.mensagem === undefined) {
            window.location.reload();
            return;
        }

        $.confirm({
            title: 'Sucesso',
            type: 'blue',
            content: response.mensagem,
            buttons: {
                confirm: () => window.location.reload()
            }
        });
    }
}