var carrinho = [];

$(document).ready(function () {
    var dados = localStorage.getItem('store.carrinho');
    if (dados) {
        carrinho = JSON.parse(dados);
        $('#QuantidadeTotalCarrinho').text(carrinho.length);
    }
});

function adicionarAoCarrinho(codigo, marca, modelo, valor, imagem) {
    var item = {
        Codigo: codigo,
        Marca: marca,
        Modelo: modelo,
        Valor: valor,
        Imagem: imagem
    };

    carrinho.push(item);
    $('#QuantidadeTotalCarrinho').text(carrinho.length);

    localStorage.setItem('store.carrinho', JSON.stringify(carrinho));
}

function finalizarCompra() {
    if (carrinho.length > 0) {
        $.ajax({
            url: "/loja/FinalizarCompra",
            data: { Produtos: carrinho },
            dataType: "json",
            type: "POST"
        })
        .done(function (data) {
            if (data == 1) {
                carrinho = [];
                localStorage.removeItem('store.carrinho');
                $('#QuantidadeTotalCarrinho').text("0");
                $('#ModalCompraFinalizada').addClass('is-active');
                $('#ListaCarrinho').html("<h2>Não há itens no carrinho</h2>");
            }

        });
    }
}

function FecharModal() {
    $('#ModalCompraFinalizada').removeClass('is-active');
}