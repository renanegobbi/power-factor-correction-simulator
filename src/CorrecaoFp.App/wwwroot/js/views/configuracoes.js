$(document).ready(function () {

    $("#msg_box").fadeOut(3000);

    $(document).on("input", ".somenteNumero", function () { this.value = this.value.replace(/[^0-9|,]/g, ''); });

    atualizarEstagiosAutomaticos(false);

    atualizarEstagiosFixos(false);
    $("#sQuantidadeEstagios").change(function () {
        atualizarEstagiosAutomaticos(true);
    });

    $("#sQuantidadeEstagiosFixos").change(function () {
        atualizarEstagiosFixos(true);
    });

    addPoppover();
});

function aplicarSelect2() {
    $("#sQuantidadeEstagios, #sQuantidadeEstagiosFixos").select2({
        minimumResultsForSearch: Infinity,
        placeholder: " ",
        allowClear: false
    });

    $("#iSelectBancoFixo1, #iSelectBancoFixo2, #iSelectBancoFixo3, #iSelectBancoFixo4").select2({
        minimumResultsForSearch: Infinity,
        placeholder: " ",
        allowClear: false
    });

    $("#iSelectEstagio1, #iSelectEstagio2, #iSelectEstagio3, #iSelectEstagio4, #iSelectEstagio5, #iSelectEstagio6").select2({
        minimumResultsForSearch: Infinity,
        placeholder: " ",
        allowClear: false
    });

    $("#iSelectEstagio7, #iSelectEstagio8, #iSelectEstagio9, #iSelectEstagio10, #iSelectEstagio11, #iSelectEstagio12").select2({
        minimumResultsForSearch: Infinity,
        placeholder: " ",
        allowClear: false
    });

    $("#iSelectEstagio13, #iSelectEstagio14, #iSelectEstagio15, #iSelectEstagio16, #iSelectEstagio17, #iSelectEstagio18").select2({
        minimumResultsForSearch: Infinity,
        placeholder: " ",
        allowClear: false
    });

    $("#iSelectEstagio19, #iSelectEstagio20, #iSelectEstagio21, #iSelectEstagio22, #iSelectEstagio23, #iSelectEstagio24").select2({
        minimumResultsForSearch: Infinity,
        placeholder: " ",
        allowClear: false
    });

    $("#iSelectEstagioFixo1, #iSelectEstagioFixo2, #iSelectEstagioFixo3, #iSelectEstagioFixo4").select2({
        minimumResultsForSearch: Infinity,
        placeholder: " ",
        allowClear: false
    });
}

function atualizarEstagiosAutomaticos(_change) {
    var selectQuantidadeEstagios = $('#sQuantidadeEstagios').val();
    $.ajax({
        url: "/Configuracao/ListarEstagiosAutomaticos",
        type: "POST",
        datatype: "json",
        data: {
            sQuantidadeEstagios: selectQuantidadeEstagios,
            change: _change
        },
        success: function (data) {
            $('#estagiosBancoAutomatico').html(data);
            aplicarSelect2();
        }
    });
}

function atualizarEstagiosFixos(_change) {
    var selectQuantidadeEstagiosFixos = $('#sQuantidadeEstagiosFixos').val();
    $.ajax({
        url: "/Configuracao/ListarEstagiosFixos",
        type: "POST",
        datatype: "json",
        data: {
            sQuantidadeEstagiosFixos: selectQuantidadeEstagiosFixos,
            change: _change
        },
        success: function (data) {
            $('#estagiosBancoFixo').html(data);
            aplicarSelect2();
        }
    });
}

function addPoppover() {
    $('#infoTensaoLinha').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Tensão de Linha",
        content: "Tensão de Linha (V)."
    });

    $('#infoRelacaoTc').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Relação TC",
        content: "Relação de transformação do transformador de corrente (TC)."
    });

    $('#infoRelacaoCk').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Relação C/k",
        content: "C/k é a sensibilidade do controlador de fator de potência. Normalmente, "
            + "esse parâmetro é ajustado para 2/3 "
            + "da corrente do primeiro passo do capacitor. "
            + "Ele representa o valor limite da corrente para o "
            + "controlador de fator de potência ativar ou desativar um estágio de capacitor."
    });
}