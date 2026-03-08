using CorrecaoFp.App.Helpers;
using CorrecaoFp.App.Results;
using CorrecaoFp.App.ViewModels;
using CorrecaoFp.Business.Comandos.Entrada;
using CorrecaoFp.Business.Enums;
using CorrecaoFp.Business.Interfaces.Notificacoes;
using CorrecaoFp.Business.Interfaces.Services;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Business.Models.Validations.Documentos;
using CorrecaoFp.Business.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrecaoFp.App.Controllers
{
    public class DadosController : BaseController
    {
        private readonly ILogger<DadosController> _logger;
        private readonly DatatablesHelper _datatablesHelper;
        private readonly IConfiguracaoService _configuracaoService;
        private readonly IEstagioService _estagioService;
        private readonly IMedicaoService _medicaoService;
        private readonly IModoCompensacaoService _modoCompensacaoService;

        public DadosController(ILogger<DadosController> logger,
            IConfiguracaoService configuracaoService,
            IEstagioService estagioService,
            IMedicaoService medicaoService,
            IModoCompensacaoService modoCompensacaoService,
            DatatablesHelper datatablesHelper,
            INotificador notificador) : base(notificador)
        {
            _logger = logger;
            _configuracaoService = configuracaoService;
            _estagioService = estagioService;
            _medicaoService = medicaoService;
            _modoCompensacaoService = modoCompensacaoService;
            _datatablesHelper = datatablesHelper;
        }

        public IActionResult Index()
        {
            var estagios = _estagioService.ObterTodosEstagiosResumo().Result.ToList();
            var medicoes = _medicaoService.ObterTodasMedicoes().Result.ToList();
            var modosCompensacao = _modoCompensacaoService.ObterTodosModoCompensacao().Result.ToList();

            var medicao = new MedicaoViewModel();
            medicao.ModosCompensacao = modosCompensacao.Select(x => new SelectListItem(x.Nome, x.Id.ToString()))?.ToList();
            medicao.ModoCompensacao = modosCompensacao;

            return View(medicao);
        }

        public async Task<IActionResult> Listar(
            DateTime? dataInicio, DateTime? dataFim, double? potenciaAtivaTotal,
            double? potenciaReativaTotal, double? potenciaAparenteAritmetica,
            double? fpRealMedia, string indMedia)
        {
            try
            {
                var ordenarPor = MedicaoOrdenarPor.DataInicio;
                IndMedia IndMedia;

                Enum.TryParse(_datatablesHelper.OrdenarPor, true, out ordenarPor);
                Enum.TryParse(indMedia, true, out IndMedia);

                var filtro = new ProcurarMedicaoEntrada(ordenarPor, _datatablesHelper.OrdenarSentido, _datatablesHelper.PaginaIndex, _datatablesHelper.PaginaTamanho)
                {
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    PotenciaAtivaTotal = potenciaAtivaTotal,
                    PotenciaReativaTotal = potenciaReativaTotal,
                    PotenciaAparenteAritmetica = potenciaAparenteAritmetica,
                    FpRealMedia = fpRealMedia,
                    IndMedia = IndMedia
                };

                var pesquisa = await _medicaoService.ProcurarMedicao(filtro);
                var registros = pesquisa.Item1;
                var totalRegistros = (int)pesquisa.Item2;

                return new DatatablesResult(_datatablesHelper.Draw, totalRegistros, registros.Select(x => new
                {
                    DataInicio = x.DataInicio.FormatarDataEHora(),
                    DataFim = x.DataFim.FormatarDataEHora(),
                    PotenciaAtivaTotal = x.PotenciaAtiva.ArredondarDuasCasasDecimais().TrocarPontoPorVirgula(),
                    PotenciaReativaTotal = x.PotenciaReativa.ArredondarDuasCasasDecimais().TrocarPontoPorVirgula(),
                    PotenciaAparenteAritmetica = x.PotenciaAparente.ArredondarDuasCasasDecimais().TrocarPontoPorVirgula(),
                    FpRealMedia = x.FatorPotencia.TrocarPontoPorVirgula(),
                    IndMedia = x.TipoFatorPotencia,
                    ERE = ((x.DataInicio.Hour < (int)HoraEnum.Seis && x.TipoFatorPotencia == Const.Cap && x.FatorPotencia < Const.FP_0_92) ||
                           (x.DataInicio.Hour >= (int)HoraEnum.Seis && x.TipoFatorPotencia == Const.Ind && x.FatorPotencia < Const.FP_0_92))
                           ? true : false
                }).ToArray());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> ListarModoCompensacao(
        string modoCompensacao, DateTime? dataInicio, DateTime? dataFim, double? potenciaAtivaTotal,
            double? potenciaReativaTotal, double? potenciaAparenteAritmetica,
            double? fpRealMedia, string indMedia, string acao, string indMediaFPCorrigido)
        {
            try
            {
                var ordenarPor = MedicaoOrdenarPor.DataInicio;
                var modoCompensacaoSelecionado = ModoCompensacaoEnum.SequencialAscendente;
                IndMedia IndMedia;
                IndMedia IndMediaFPCorrigido;
                OperacaoEstagio operacaoEstagio;
                double ck = 0;

                Enum.TryParse(_datatablesHelper.OrdenarPor, true, out ordenarPor);
                Enum.TryParse(modoCompensacao, true, out modoCompensacaoSelecionado);
                Enum.TryParse(indMedia, true, out IndMedia);
                Enum.TryParse(acao, true, out operacaoEstagio);
                Enum.TryParse(indMediaFPCorrigido, true, out IndMediaFPCorrigido);

                var estagios = _estagioService.ObterTodosEstagiosResumo().Result.ToList();
                var tamanhoBancoCapacitores = estagios?.Where(e => e.TipoPotenciaId == (int)TipoPotenciaEnum.Capacitivo)?.Select(e => e.Potencia).Sum();
                var medicoes = _medicaoService.ObterTodasMedicoes().Result.ToList();
                var medicao = new MedicaoViewModel();

                var dadosConfiguracao = _configuracaoService.ObterTodasConfiguracoes().Result.ToList();
                var listaEstagios = estagios.OrderBy(e => e.Potencia).Select(a => a.Potencia).ToList();
                var estagiosOrdenados = new List<double>();
                estagiosOrdenados.AddRange(listaEstagios.Where(estagio => estagio != 0));

                bool todosEstagiosZero = estagiosOrdenados.All(estagio => estagio == 0);

                if (!todosEstagiosZero &&
                dadosConfiguracao[Const.REGISTRO_TENSAO_LINHA].Valor != 0.0 &&
                dadosConfiguracao[Const.REGISTRO_RELACAO_TC].Valor != 0.0)
                {
                    ck = BancoCapacitorUtil.CalcularCk(estagiosOrdenados[Const.PRIMEIRO_ELEMENTO_ARRAY],
                                                       dadosConfiguracao[Const.REGISTRO_TENSAO_LINHA].Valor,
                                                       dadosConfiguracao[Const.REGISTRO_RELACAO_TC].Valor);
                }

                switch (modoCompensacaoSelecionado)
                {
                    case ModoCompensacaoEnum.ModoInteligente:
                        medicao.Medicoes = BancoCapacitorUtil.CompensacaoModoInteligente(
                            medicoes,
                            estagios,
                            ck,
                            Const.FP_0_92,
                            dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS].Valor);
                        break;
                    case ModoCompensacaoEnum.Circular:
                        medicao.Medicoes = BancoCapacitorUtil.CompensacaoCircular(
                            medicoes,
                            estagios,
                            ck,
                            Const.FP_0_92,
                            dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS].Valor);
                        break;
                    case ModoCompensacaoEnum.Linear:
                        medicao.Medicoes = BancoCapacitorUtil.CompensacaoLinear(
                            medicoes,
                            estagios,
                            ck,
                            Const.FP_0_92,
                            dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS].Valor);
                        break;
                    case ModoCompensacaoEnum.SequencialDescendente:
                        medicao.Medicoes = BancoCapacitorUtil.CompensacaoSequencialDescendente(
                            medicoes,
                            estagios,
                            ck,
                            Const.FP_0_92,
                            dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS].Valor);
                        break;
                    default:
                        medicao.Medicoes = BancoCapacitorUtil.CompensacaoSequencialAscendente(
                            medicoes,
                            estagios,
                            ck,
                            Const.FP_0_92,
                            dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS].Valor);
                        break;
                }

                var filtro = new ProcurarMedicaoEntrada(ordenarPor, _datatablesHelper.OrdenarSentido, _datatablesHelper.PaginaIndex, _datatablesHelper.PaginaTamanho)
                {
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    PotenciaAtivaTotal = potenciaAtivaTotal,
                    PotenciaReativaTotal = potenciaReativaTotal,
                    PotenciaAparenteAritmetica = potenciaAparenteAritmetica,
                    FpRealMedia = fpRealMedia,
                    IndMedia = IndMedia,
                    IndMediaFPCorrigido = IndMediaFPCorrigido,
                    Acao = operacaoEstagio,
                    Medicoes = medicao.Medicoes,
                };

                var pesquisa = await _medicaoService.ProcurarMedicaoReativoExcedente(filtro);

                int qtdFpNaoCorrigido = await CalcularFpNaoCorrigido(dataInicio, dataFim, potenciaAtivaTotal, potenciaReativaTotal, potenciaAparenteAritmetica,
                    fpRealMedia, ordenarPor, IndMedia, IndMediaFPCorrigido, operacaoEstagio, medicao, pesquisa);

                var registros = pesquisa.Item1;
                var totalRegistros = (int)pesquisa.Item2;

                return new DatatablesResult(_datatablesHelper.Draw, totalRegistros, registros.Select(x => new
                {
                    DataInicio = x.DataInicio.FormatarDataEHora(),
                    DataFim = x.DataFim.FormatarDataEHora(),
                    FpRealMedia = x.FatorPotencia.TrocarPontoPorVirgula(),
                    IndMedia = x.TipoFatorPotencia,
                    //Acao = (x.TipoFatorPotencia == Const.Cap) ? Const.Destivar : Const.Ativar,
                    Acao = DisplayUtil.FormatarColunaAcao(x.TipoFatorPotencia),//(x.TipoFatorPotencia == Const.Cap) ? Const.Destivar : Const.Ativar,
                    QcNecessario = x.QcNecessario.TrocarPontoPorVirgula(),
                    EstagiosUtilizados = DisplayUtil.FormatarEstagios(x.EstagiosNecessarios),
                    FatorPotenciaCorrigido = x.FpFinal.TrocarPontoPorVirgula(),
                    IndFpCorrigido = x.TipoFatorPotenciaCorrigido,
                    TotalMedicoes = medicoes.Count(),
                    TotalEre = totalRegistros,
                    QtdFpNaoCorrigido = qtdFpNaoCorrigido,
                    ERE = ((x.DataInicio.Hour < (int)HoraEnum.Seis && x.TipoFatorPotencia == Const.Cap && x.FpFinal < Const.FP_0_92) ||
                           (x.DataInicio.Hour >= (int)HoraEnum.Seis && x.TipoFatorPotencia == Const.Ind && x.FpFinal < Const.FP_0_92))
                           ? true : false
                }).ToArray());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<int> CalcularFpNaoCorrigido(DateTime? dataInicio, DateTime? dataFim, double? potenciaAtivaTotal,
            double? potenciaReativaTotal, double? potenciaAparenteAritmetica, double? fpRealMedia, MedicaoOrdenarPor ordenarPor,
            IndMedia IndMedia, IndMedia IndMediaFPCorrigido, OperacaoEstagio operacaoEstagio, MedicaoViewModel medicao, Tuple<Medicao[], double> pesquisa)
        {
            var filtro = new ProcurarMedicaoEntrada(ordenarPor, _datatablesHelper.OrdenarSentido, -1, (int)pesquisa.Item2)
            {
                DataInicio = dataInicio,
                DataFim = dataFim,
                PotenciaAtivaTotal = potenciaAtivaTotal,
                PotenciaReativaTotal = potenciaReativaTotal,
                PotenciaAparenteAritmetica = potenciaAparenteAritmetica,
                FpRealMedia = fpRealMedia,
                IndMedia = IndMedia,
                IndMediaFPCorrigido = IndMediaFPCorrigido,
                Acao = operacaoEstagio,
                Medicoes = medicao.Medicoes,
            };

            var pesquisaMedicaoReativoExcedente = await _medicaoService.ProcurarMedicaoReativoExcedente(filtro);
            var qtdFpNaoCorrigido = 0;
            var fatorPotenciaNaoCorrigido = false;
            foreach (var _medicao in pesquisaMedicaoReativoExcedente.Item1)
            {
                fatorPotenciaNaoCorrigido = ((_medicao.DataInicio.Hour < (int)HoraEnum.Seis && _medicao.TipoFatorPotencia == Const.Cap && _medicao.FpFinal < Const.FP_0_92) ||
                       (_medicao.DataInicio.Hour >= (int)HoraEnum.Seis && _medicao.TipoFatorPotencia == Const.Ind && _medicao.FpFinal < Const.FP_0_92))
                       ? true : false;

                if (fatorPotenciaNaoCorrigido == true)
                    qtdFpNaoCorrigido++;
            }

            return qtdFpNaoCorrigido;
        }

        [RequestSizeLimit(2147483648)] // 2 Gigabytes = 2147483648 bytes
        public async Task<IActionResult> UploadArquivo(IFormFile ArquivoUpload)
        {
            if (ArquivoUpload != null)
            {
                var extensoaValida = ArquivoValidacao.ExtensaoValida(ArquivoUpload);

                if (!extensoaValida)
                {
                    TempData["Falha"] = $"Ops! A extensão {ArquivoValidacao.ExtensaoArquivo(ArquivoUpload)} não é válida! Insrira um arquivo de extensão .xlsx.";
                    return RedirectToAction("Index");
                }

                if (!ArquivoValidacao.TamanhoValido(ArquivoUpload))
                {
                    TempData["Falha"] = $"Arquivo excedeu o limite permitido, por favor escolha arquivos com no máximo {ArquivoValidacao.TamanhoArquivoGigabytes}GB!";
                    return RedirectToAction("Index");
                }

                var quantidadeColunasValidas = ArquivoValidacao.QuantidadeColunasValidas(ArquivoUpload);
            }

            var imgPrefixo = Guid.NewGuid() + "_";

            var uploadAux = await UploadArquivoMethod(ArquivoUpload, imgPrefixo);

            if (!uploadAux.Verdadeiro) return RedirectToAction("Index");

            LeituraUpload dados = FileUtil.ReadXls(ArquivoUpload, uploadAux.Caminho);

            if (dados.Erros.Any())
            {
                FileUtil.DeletarArquivo(uploadAux.Caminho);

                StringBuilder sb = new StringBuilder();
                int numero = 1;
                sb.Append("Template inválido!").Append("<br>");
                var quantidadeErros = dados.Erros.Count;

                foreach (var erro in dados.Erros)
                {
                    if (quantidadeErros == 1)
                    {
                        sb.Append(erro).Append("<br>");
                    }
                    else
                    {
                        sb.Append($"{numero}) ").Append(erro).Append("<br>");
                    }

                    numero++;
                }

                TempData["Falha"] = sb.ToString();
                return RedirectToAction("Index");
            }


            await _medicaoService.Adicionar(dados.Medicoes);

            FileUtil.DeletarArquivo(uploadAux.Caminho);

            TempData["Sucesso"] = "Arquivo inserido com sucesso!";

            return RedirectToAction("Index");
        }

        private static readonly object uploadLock = new object();
        private const int MaxRetryAttempts = 3;
        private const int DelayBetweenRetries = 1000; // em milissegundos

        public async Task<UploadAux> UploadArquivoMethod(IFormFile arquivo, string imgPrefixo)
        {
            UploadAux uploadAux = new UploadAux();

            if (arquivo?.Length <= 0 || arquivo == null)
            {
                uploadAux.Verdadeiro = false;
                return uploadAux;
            }

            var basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? Directory.GetCurrentDirectory();
            uploadAux.Caminho = Path.Combine(basePath, "wwwroot/arquivos", imgPrefixo + arquivo.FileName);
            Console.WriteLine($"Valor de uploadAux.Caminho: {uploadAux.Caminho}");

            if (System.IO.File.Exists(uploadAux.Caminho))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com esse nome!");
                uploadAux.Verdadeiro = false;
                return uploadAux;
            }

            int attempt = 0;
            bool success = false;

            while (attempt < MaxRetryAttempts && !success)
            {
                attempt++;
                try
                {
                    lock (uploadLock)
                    {
                        using (var fileStream = new FileStream(uploadAux.Caminho, FileMode.Create))
                        {
                            arquivo.CopyTo(fileStream);
                        }
                    }

                    // Verifique se o arquivo foi salvo corretamente
                    if (new FileInfo(uploadAux.Caminho).Length == 0)
                    {
                        throw new Exception("O arquivo não foi salvo corretamente.");
                    }

                    success = true;
                    uploadAux.Verdadeiro = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao salvar o arquivo (tentativa {attempt}): {ex.Message}");
                    if (attempt < MaxRetryAttempts)
                    {
                        await Task.Delay(DelayBetweenRetries); // Aguarde antes de tentar novamente
                    }
                    else
                    {
                        uploadAux.Verdadeiro = false;
                    }
                }
            }

            return uploadAux;
        }

        public class UploadAux
        {
            public bool Verdadeiro { get; set; } = true;
            public string Caminho { get; set; } = "";
        }

        [HttpPost]
        public IActionResult DownloadTemplate()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/arquivos/template/Template.xlsx");

            string fileName = "Template.xlsx";

            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                return File(fileBytes, "application/force-download", fileName);
            }

            return NotFound();
        }
    }
}
