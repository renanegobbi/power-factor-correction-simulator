using CorrecaoFp.App.Utils;
using CorrecaoFp.App.ViewModels;
using CorrecaoFp.Business.Comandos.Entrada.Configuracao;
using CorrecaoFp.Business.Comandos.Entrada.Estagio;
using CorrecaoFp.Business.Enums;
using CorrecaoFp.Business.Interfaces.Services;
using CorrecaoFp.Business.Resources;
using CorrecaoFp.Business.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorrecaoFp.App.Controllers
{
    public class ConfiguracaoController : Controller
    {
        private readonly ILogger<ConfiguracaoController> _logger;
        private readonly IConfiguracaoService _configuracaoService;
        private readonly ICapacitorService _capacitorService;
        private readonly IEstagioService _estagioService;

        public ConfiguracaoController(ILogger<ConfiguracaoController> logger,
              ICapacitorService capacitorService,
              IConfiguracaoService configuracaoService,
              IEstagioService estagioService)
        {
            _logger = logger;
            _capacitorService = capacitorService;
            _configuracaoService = configuracaoService;
            _estagioService = estagioService;
        }

        public IActionResult Index()
        {
            var configuracoesViewModel = new ConfiguracoesViewModel();
            double ck = 0;

            var estagios = _estagioService.ObterTodosEstagiosResumo().Result.ToList();
            var capacitores = _capacitorService.ObterTodosCapacitores().Result.ToList();
            var configuracao = _configuracaoService.ObterTodasConfiguracoes().Result.ToList();

            ConfiguracaoUtil.PreencherDadosConfiguracao(configuracoesViewModel, estagios, capacitores, configuracao);

            List<double> estagiosOrdenados = BancoCapacitorUtil.OrdenarEstagiosPorPotencia(estagios, OrdenarPor.ModoAscendente);

            bool todosEstagiosZero = estagiosOrdenados.All(estagio => estagio == 0);

            if (!todosEstagiosZero &&
                configuracao[Const.REGISTRO_TENSAO_LINHA].Valor != 0.0 &&
                configuracao[Const.REGISTRO_RELACAO_TC].Valor != 0.0)
            {
                ck = BancoCapacitorUtil.CalcularCk(estagiosOrdenados[Const.PRIMEIRO_ELEMENTO_ARRAY],
                                                   configuracao[Const.REGISTRO_TENSAO_LINHA].Valor,
                                                   configuracao[Const.REGISTRO_RELACAO_TC].Valor);
            }

            configuracoesViewModel.RelacaoCk = ck;

            return View(configuracoesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ListarEstagiosFixos(string sQuantidadeEstagiosFixos, bool change)
        {
            var configuracoes = new ConfiguracoesViewModel();
            var dadosConfiguracao = _configuracaoService.ObterTodasConfiguracoes().Result.ToList();
            int quantidadeEstagiosFixos = Const.QUANTIDADE_ESTAGIO_FIXO_DEFAULT;

            if (change == true)
            {
                quantidadeEstagiosFixos = int.TryParse(sQuantidadeEstagiosFixos, out quantidadeEstagiosFixos) ? quantidadeEstagiosFixos : 0;
                configuracoes.sQuantidadeEstagiosFixos = quantidadeEstagiosFixos;

                dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_FIXOS].Valor = quantidadeEstagiosFixos;
                await _configuracaoService.AtualizarConfiguracao(dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_FIXOS]);
            }
            else // será igual ao valor do BD
            {
                quantidadeEstagiosFixos = (int)dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_FIXOS].Valor;
                configuracoes.sQuantidadeEstagiosFixos = quantidadeEstagiosFixos;
            }

            int quantidadeLinhas = ConfiguracaoUtil.DefinirQuantidadeLinhas(quantidadeEstagiosFixos);
            int quantidadeColunasUltimaLinha = ConfiguracaoUtil.DefinirQuantidadeColunasUltimaLinha(quantidadeEstagiosFixos);

            var estagios = _estagioService.ObterTodosEstagiosResumo().Result.ToList();
            var capacitores = _capacitorService.ObterTodosCapacitores().Result.ToList();

            configuracoes.QuantidadeLinhas = quantidadeLinhas;
            configuracoes.QuantidadeColunasUltimaLinha = quantidadeColunasUltimaLinha;
            configuracoes.EstagioResumo = estagios;
            configuracoes.Capacitores = capacitores;

            return PartialView("_ListarEstagiosFixos", configuracoes);
        }

        [HttpPost]
        public async Task<IActionResult> ListarEstagiosAutomaticos(string sQuantidadeEstagios, bool change)
        {
            var configuracoes = new ConfiguracoesViewModel();
            var dadosConfiguracao = _configuracaoService.ObterTodasConfiguracoes().Result.ToList();

            int quantidadeEstagios = Const.QUANTIDADE_ESTAGIO_AUTOMATICO_DEFAULT;
            int quantidadeEstagiosFixos = Const.QUANTIDADE_ESTAGIO_FIXO_DEFAULT;

            if (change == true)
            {
                quantidadeEstagios = int.TryParse(sQuantidadeEstagios, out quantidadeEstagios) ? quantidadeEstagios : 0;
                configuracoes.sQuantidadeEstagios = quantidadeEstagios;

                dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS].Valor = quantidadeEstagios;
                await _configuracaoService.AtualizarConfiguracao(dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS]);
            }
            else // será igual ao valor do BD
            {
                quantidadeEstagios = (int)dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS].Valor;
                configuracoes.sQuantidadeEstagios = quantidadeEstagios;
                configuracoes.sQuantidadeEstagiosFixos = quantidadeEstagiosFixos;
            }

            int quantidadeLinhas = ConfiguracaoUtil.DefinirQuantidadeLinhas(quantidadeEstagios);
            int quantidadeColunasUltimaLinha = ConfiguracaoUtil.DefinirQuantidadeColunasUltimaLinha(quantidadeEstagios);

            var estagios = _estagioService.ObterTodosEstagiosResumo().Result.ToList();
            var capacitores = _capacitorService.ObterTodosCapacitores().Result.ToList();

            configuracoes.QuantidadeLinhas = quantidadeLinhas;
            configuracoes.QuantidadeColunasUltimaLinha = quantidadeColunasUltimaLinha;
            configuracoes.EstagioResumo = estagios;
            configuracoes.Capacitores = capacitores;

            return PartialView("_ListarEstagiosAutomaticos", configuracoes);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEstagios(EstagioViewModel model)
        {
            var estagios = _estagioService.ObterTodosEstagios().Result.ToList();
            var configuracoes = _configuracaoService.ObterTodasConfiguracoes().Result.ToList();

            var atualizarEstagiosEntrada = new AtualizarEstagioEntrada(
                model.IdCapacitorEstagioFixo1, model.IdCapacitorEstagioFixo2, model.IdCapacitorEstagioFixo3, model.IdCapacitorEstagioFixo4,
                model.IdCapacitorEstagio1, model.IdCapacitorEstagio2, model.IdCapacitorEstagio3, model.IdCapacitorEstagio4,
                model.IdCapacitorEstagio5, model.IdCapacitorEstagio6, model.IdCapacitorEstagio7, model.IdCapacitorEstagio8,
                model.IdCapacitorEstagio9, model.IdCapacitorEstagio10, model.IdCapacitorEstagio11, model.IdCapacitorEstagio12,
                model.IdCapacitorEstagio13, model.IdCapacitorEstagio14, model.IdCapacitorEstagio15, model.IdCapacitorEstagio16,
                model.IdCapacitorEstagio17, model.IdCapacitorEstagio18, model.IdCapacitorEstagio19, model.IdCapacitorEstagio20,
                model.IdCapacitorEstagio21, model.IdCapacitorEstagio22, model.IdCapacitorEstagio23, model.IdCapacitorEstagio24);

            var atualizarConfiguracoesEntrada = new AtualizarConfiguracaoEntrada(model.TensaoLinha, model.RelacaoTc, model.sQuantidadeEstagios);

            await _estagioService.AtualizarEstagios(estagios, atualizarEstagiosEntrada, model.sQuantidadeEstagios);
            await _configuracaoService.AtualizarConfiguracoes(configuracoes, atualizarConfiguracoesEntrada);

            TempData["Sucesso"] = ConfiguracaoResource.Configuracoes_Salvos_Com_Sucesso;

            return RedirectToAction("Index");
        }
    }
}
