$(document).ready(function () {

    const checked = 1;
    checkTemBancoCapacitor(checked);
    $("#checkTemBancoCapacitor").click(function () {
        checkTemBancoCapacitor(checked);

        _isChecked = isChecked();
        if (_isChecked) {
            selecaoModoCompensacao = $('#sModoCompensacaoSemBancoCapacitor').val();
            recarregarTabelaEre();
        } else {
            selecaoModoCompensacao = $('#sModoCompensacao').val();
            recarregarTabelaEre();
        }
    });

    $(document).on("input", ".somenteNumero", function () { this.value = this.value.replace(/\D/g, ''); });
    $(document).on("input", ".fatorPotencia", function () { this.value = this.value.replace(/^[2-9][0-9]{0,2}$/g, ''); });

    $("#iProcurarFP").inputmask({
        mask: ["9", "9,9", "9,99"],
        keepStatic: true,
        clearEmpty: true
    });

    $("#sProcurarIndMedia").select2({
        minimumResultsForSearch: Infinity,
        allowClear: false
    });

    $("#sProcurarAcao").select2({
        minimumResultsForSearch: Infinity,
        allowClear: false
    });

    $("#sProcurarIndMediaFPCorrigido").select2({
        minimumResultsForSearch: Infinity,
        allowClear: false
    });

    $(".campo-data").inputmask("99/99/9999");

    $.extend($.fn.datepicker.defaults, {
        autoclose: true,
        language: 'pt-BR',
        format: 'dd/mm/yyyy',
        todayBtn: 'linked',
        todayHighlight: true,
        orientation: "auto left"
    });

    $('.datepicker').datepicker({
        autoclose: true
    })

    $("#msg_box").fadeOut(60000);
    $(document).on("input", ".somenteNumero", function () { this.value = this.value.replace(/\D/g, ''); });
    $('input[type="file"]').on("change", function () {
        let filenames = [];
        let files = this.files;
        if (files.length > 1) {
            filenames.push("Total Files (" + files.length + ")");
        } else {
            for (let i in files) {
                if (files.hasOwnProperty(i)) {
                    filenames.push(files[i].name);
                }
            }
        }
        $(this)
            .next(".custom-file-label")
            .html(filenames.join(","));
    });

    $('#tbtDadosUpload').DataTable({
        "processing": true,
        "serverSide": true,
        columnDefs: [{ width: '20%', targets: 0 }],
        scrollCollapse: true,
        scrollX: true,
        ajax: {
            url: ("/Dados/Listar"),
            type: "POST",
            datatype: "json"
        },
        columns: [
            {
                data: "dataInicio",
                class: "text-nowrap text-center",
            },
            {
                data: "dataFim",
                class: "text-nowrap text-center",
            },
            {
                data: "potenciaAtivaTotal",
                class: "text-nowrap text-center",
            },
            {
                data: "potenciaReativaTotal",
                class: "text-nowrap text-center",
            },
            {
                data: "potenciaAparenteAritmetica",
                class: "text-nowrap text-center",
            },
            {
                data: "fpRealMedia",
                class: "text-nowrap text-center",
            },
            {
                data: "indMedia",
                class: "text-nowrap text-center",
            }
        ],

        rowCallback: function (row, data, index) {
            if (data.ere == true) {
                $(row).css('background-color', '#F9AEB3');
            }
        },

        "language": {
            "lengthMenu": "Apresentar _MENU_ linhas por página",
            "zeroRecords": "Não há registros",
            "search": "Pesquisar:",
            "emptyTable": "Sem dados disponíveis na tabela",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "infoEmpty": "Não há registros",
            "infoFiltered": "(Filtrando de _MAX_ registros)",
            "loadingRecords": "Carregando...",
            "processing": "Processando...",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "decimal": ".",
            "thousands": ".",
            buttons: {
                copyTitle: 'Copiar para área de transferência',
                copySuccess: {
                    _: '%d linhas copiadas',
                    1: '1 linha copiada'
                },
                colvisRestore: "Restaurar tudo"
            }
        },
        "lengthMenu": [[5, 12, 24, 48, 100, -1], [5, 12, 24, 48, 100, "Tudo"]],
        pageLength: 5,
        dom:
            "<'row'<'col-md-3'l><'col-md-9 d-flex justify-content-end'B>>" +
            "<'row'<'col-md-12'tr>>" +
            "<'row'<'col-md-5'i><'col-md-7 mt-2'p>>",
        buttons: [
            {
                extend: 'colvis',
                text: '<i class="fas fa-eye"></i> Colunas Visíveis',
                titleAttr: 'Colunas visíveis',
                className: 'btn btn-colvis btn-md',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    }
                },
                postfixButtons: ['colvisRestore']
            },
            {
                extend: 'copy',
                text: '<i class="fas fa-copy"></i>',
                titleAttr: 'Exportar para Cópia',
                className: 'btn btn-secondary btn-md',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    }
                }
            },
            {
                extend: 'excel',
                text: '<i class="fas fa-file-excel"></i>',
                titleAttr: 'Exportar para Excel',
                className: 'btn btn-success btn-md',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    }
                }
            },
            {
                extend: 'pdf',
                text: '<i class="fas fa-file-pdf"></i>',
                titleAttr: 'Exportar para PDF',
                className: 'btn btn-danger btn-md',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    }
                }
            },
        ],
    });

    $("#btn-filtrar").click(function () {
        recarregarTabelaEre();
    });

    $("#sModoCompensacao").select2({
        minimumResultsForSearch: Infinity,
        placeholder: " ",
        allowClear: false
    });

    var selecaoModoCompensacao = null;

    $('#sModoCompensacao').change(function () {

        if ($('#sModoCompensacao').val() != "" && $('#sModoCompensacao').val() != null) {
            selecaoModoCompensacao = $('#sModoCompensacao').val()
            recarregarTabelaEre();
        }
    });

    $("#sModoCompensacaoSemBancoCapacitor").select2({
        minimumResultsForSearch: Infinity,
        placeholder: " ",
        allowClear: false
    });

    $('#sModoCompensacaoSemBancoCapacitor').change(function () {

        if ($('#sModoCompensacaoSemBancoCapacitor').val() != "" && $('#sModoCompensacaoSemBancoCapacitor').val() != null) {
            selecaoModoCompensacao = $('#sModoCompensacaoSemBancoCapacitor').val()
            recarregarTabelaEre();
        }
    });

    $('#tblDadosResultado').DataTable({
        "processing": true,
        "serverSide": true,
        columnDefs: [{ width: '20%', targets: 0 }],
        scrollCollapse: true,
        scrollX: true,
        ajax: {
            url: ("/Dados/ListarModoCompensacao"),
            type: "POST",
            datatype: "json",
            data: function (data) {
                data.modoCompensacao = selecaoModoCompensacao,
                data.dataInicio = $("#iProcurarDataInicio").val();
                data.dataFim = $("#iProcurarDataFim").val();
                data.fpRealMedia = $("#iProcurarFP").val();
                data.indMedia = $("#sProcurarIndMedia").val();
                data.Acao = $("#sProcurarAcao").val();
                data.indMediaFPCorrigido = $("#sProcurarIndMediaFPCorrigido").val();
            }
        },

        columns: [
            {
                data: "dataInicio",
                class: "text-center",
            },
            {
                data: "dataFim",
                class: "text-center",
            },
            {
                data: "fpRealMedia",
                class: "text-center",
            },
            {
                data: "indMedia",
                class: "text-center",
            },
            {
                data: "acao",
                class: "text-center",
            },
            {
                data: "qcNecessario",
                class: "text-center",
            },
            {
                data: "estagiosUtilizados",
                orderable: false,
            },
            {
                data: "fatorPotenciaCorrigido",
                class: "text-center",
            },
            {
                data: "indFpCorrigido",
                class: "text-center",
            },
        ],

        rowCallback: function (row, data, index) {
            if (data.ere == true) {
                $(row).css('background-color', '#F9AEB3');
            }
            totalMedicoes = data.totalMedicoes;
            totalEre = data.totalEre;
            qtdFpNaoCorrigido = data.qtdFpNaoCorrigido;
        },

        "language": {
            "lengthMenu": "Apresentar _MENU_ linhas por página",
            "zeroRecords": "Não há registros",
            "search": "Pesquisar:",
            "emptyTable": "Sem dados disponíveis na tabela",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "infoEmpty": "Não há registros",
            "infoFiltered": "(Filtrando de _MAX_ registros)",
            "loadingRecords": "Carregando...",
            "processing": "Processando...",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "decimal": ".",
            "thousands": ".",
            buttons: {
                copyTitle: 'Copiar para área de transferência',
                copySuccess: {
                    _: '%d linhas copiadas',
                    1: '1 linha copiada'
                }
            }
        },
        "lengthMenu": [[5, 12, 24, 48, 100, -1], [5, 12, 24, 48, 100, "Tudo"]],
        pageLength: 5,
        dom:
            "<'row'<'col-md-3'l><'col-md-9 d-flex justify-content-end'<'btnRefresh'>B>>" +
            "<'row'<'col-md-12'tr>>" +
            "<'row'<'col-md-5'i><'col-md-7 mt-2'p>>",
        buttons: [
            {
                extend: 'colvis',
                text: '<i class="fas fa-eye"></i> Colunas Visíveis',
                titleAttr: 'Colunas visíveis',
                className: 'btn btn-colvis btn-md',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    }
                }
            },
            {
                extend: 'copy',
                text: '<i class="fas fa-copy"></i>',
                titleAttr: 'Exportar para Cópia',
                className: 'btn btn-secondary btn-md',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    }
                }
            },
            {
                extend: 'excel',
                text: '<i class="fas fa-file-excel"></i>',
                titleAttr: 'Exportar para Excel',
                className: 'btn btn-success btn-md',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    },
                    format: {
                        body: function (data, row, column, node) {
                            return data.replace(',', '.')
                                .replace(/Ativar<br><i.*<\/i>/, 'Ativar')
                                .replace(/Desativar<br><i.*<\/i>/, 'Desativar')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ')
                                .replace(/<i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"><\/i><br> /, '-> ');
                        }

                    }
                }
            },
            {
                extend: 'pdf',
                text: '<i class="fas fa-file-pdf"></i>',
                titleAttr: 'Exportar para PDF',
                className: 'btn btn-danger btn-md',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    }
                }
            },
        ],
    }).on('draw.dt', function () {
        $("#div-grid").show();
        $("#exemplo2").DataTable().columns.adjust();
        if (isNaN(totalMedicoes)) { $("#totalMedicoes").text("-"); } else { $("#totalMedicoes").text(totalMedicoes); }
        if (isNaN(totalEre)) { $("#totalEre").text("-"); } else { $("#totalEre").text(totalEre); }
        if (isNaN(qtdFpNaoCorrigido)) { $("#qtdFpNaoCorrigido").text("-"); } else { $("#qtdFpNaoCorrigido").text(qtdFpNaoCorrigido); }
        if (isNaN(totalEre - qtdFpNaoCorrigido)) { $("#qtdFpCorrigido").text("-"); } else { $("#qtdFpCorrigido").text(totalEre - qtdFpNaoCorrigido); }
        $("#tblDadosResultado").prepend("<tr><td class=\"col-12 \" colspan=\"12\">"
            + "<i id =\"infoIndMedia\" class=\"fa-solid fa-info-circle\" style=\"color:#6c757d\"></i>"
            + " Os cálculos serão sugreidos para a correção do fator de potência desejado igual a 0,92. </td></tr>");   
    });

    $("div.btnRefresh").html("<div class=\"mr-2\" style=\"border-radius: 0;\">" +
        "<button class=\"btn btn-md btn-success pb-2\" id=\"btn-refresh\">" +
        "<i class=\"fa fa-refresh mr-xs\"></i> <span class=\"\">Atualizar</span>" +
        "</button>" +
        "</div>");

    $("#btn-refresh").click(function () {
        var table = $("#tblDadosResultado").DataTable();
        table.page(1);
        table.ajax.reload();
    });

    $('#btn-limpar').click(function () {
        $("#iProcurarDataInicio").val("");
        $("#iProcurarDataFim").val("");
        $("#iProcurarFP").val("");
        $("#sProcurarIndMedia").val("");
        $("#select2-sProcurarIndMedia-container").text("Todos");
    });

    $('#infoDataInicio').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Data Início",
        content: "Data do início da medição."
    });

    $('#infoDataFim').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Data Fim",
        content: "Data do fim da medição."
    });

    $('#infoFP').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Fator de Potência",
        content: "Valor do Fator de Potência."
    });

    $('#infoIndMedia').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Tipo de FP",
        content: "Tipo de fator de potência."
    });

    $('#infoAcao').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Ação",
        content: "Ação de incluir ou desativar estágio do banco de capacitores."
    });

    $('#infoQcNecessario').popover({
        placement: 'bottom',
        trigger: 'click hover',
        html: true,
        title: "Qc Nessário (kVAr)",
        content: "Quantidade, em kVAr, necessário para adicionar ou subtrair a fim de atingr o fator de potência desejado."
    });

    $('#infoIndMediaFPCorrigido').popover({
        placement: 'bottom',
        trigger: 'click hover',
        html: true,
        title: "Qc Nessário (kVAr)",
        content: "Quantidade, em kVAr, necessário para adicionar ou subtrair a fim de atingr o fator de potência desejado."
    });

    $('#infoEstagios').popover({
        placement: 'bottom',
        trigger: 'click hover',
        html: true,
        title: "Estágios",
        content: "Valores de potências dos estágios utilizados para corrigir o fator de potência."
    });

    $('#infoFPCorrigido').popover({
        placement: 'bottom',
        trigger: 'click hover',
        html: true,
        title: "FP Corrigido",
        content: "Valor do fator de potência após ativação ou desativação de potências do banco de capacitores."
    });

    $('#infoERE').popover({
        placement: 'bottom',
        trigger: 'click hover',
        html: true,
        title: "ERE",
        content: "Energia Reativa Excedente (ERE) gerada em reais."
    });
    $('#info-Resultado-TotalMedicoes').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Total de Medições",
        content: "Quantidade de medições inseridas para análise."
    });

    $('#info-Resultado-CandidatosEre').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Candidatos a ERE",
        content: "Medições sujeitas a pagar multa por Energia Reativa Excedente (ERE), " +
            "que compreendem valores de fator de potência inferiores a 0, 92 capacitivo entre 00h00 e 06h00 " +
            "da manhã e inferiores a 0, 92 indutivo durantes as outras 18 horas do dia. "
    });

    $('#info-Resultado-FpCorrigidos').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "FP corrigidos",
        content: "Quantidade de medições que tiveram seus valores de fator de potência corrigidos, dadas as parametrizações configuradas, em relação ao total de medições analisadas. "
    });

    $('#info-Resultado-FpNaoCorrigidos').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "FP não corrigidos",
        content: "Quantidade de medições que não tiveram seus valores de fator de potência corrigidos, dadas as parametrizações configuradas, em relação ao total de medições analisadas. "
    });

    $('#info-Tabela-Resultado-DataInicio').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Data Início",
        content: "Data e hora do início da medição. "
    });

    $('#info-Tabela-Resultado-DataFim').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Data Fim",
        content: "Data e hora do fim da medição. "
    });

    $('#info-Tabela-Resultado-FpAtual').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "FP atual",
        content: "Valor do Fator de Potência inicialmente analisado. "
    });

    $('#info-Tabela-Resultado-TipoFpAtual').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Tipo de FP atual",
        content: "Tipo de Fator de Potência inicialmente analisado, sendo Ind para Indutivo e Cap para Capacitivo. "
    });

    $('#info-Tabela-Resultado-Acao').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Ação",
        content: "Operação ncessária de ativar ou desativar estágio(s) para corrigir o Fator de Potência desejado. "
    });

    $('#info-Tabela-Resultado-QcNecessario').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Qc Necessário (kVAr)",
        content: "Valor de potência, em kVAr, necessário para corrigido o Fator de Potência desejado. "
    });

    $('#info-Tabela-Resultado-Estagios').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Estáfios (kVAr)",
        content: "Estágios ativos para correção do Fator de Potência. Sejam [F] para Potência Reativa oriunda de Banco Fixo e [A] para Potência Reativa oriunda de Banco Automático. "
    });

    $('#info-Tabela-Resultado-FpCorrigido').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "FP Corrigido",
        content: "Fator de Potência corrigido após ativação/desativação de estágios. "
    });

    $('#info-Tabela-Resultado-TipoFpCorrigido').popover({
        placement: 'top',
        trigger: 'click hover',
        html: true,
        title: "Tipo de FP Corrigido",
        content: "Tipo do Fator de Potência corrigido após ativação/desativação de estágios; sendo Ind para indutivo e Cap para capacitivo. "
    });
});

function checkTemBancoCapacitor(checked) {
    var resultado = $('#checkTemBancoCapacitor:checked');
    if (resultado.length == checked) {
        $("#secaoSelectOptionsModoCompensacaoComBancoCapacitor").hide();
        $("#secaoSelectOptionsModoCompensacaoSemBancoCapacitor").show();
    } else {
        $("#secaoSelectOptionsModoCompensacaoSemBancoCapacitor").hide();
        $("#secaoSelectOptionsModoCompensacaoComBancoCapacitor").show();
    }
}

function isChecked() {
    const isCkecked = 1;
    var resultado = $('#checkTemBancoCapacitor:checked');
    return resultado.length == isCkecked ? true : false;
}

function recarregarTabelaEre() {
    var table = $("#tblDadosResultado").DataTable();
    table.page(1);
    table.ajax.reload();
}



