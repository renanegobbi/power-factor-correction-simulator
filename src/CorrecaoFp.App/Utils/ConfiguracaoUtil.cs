using CorrecaoFp.App.ViewModels;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Business.Utils;
using System;
using System.Collections.Generic;

namespace CorrecaoFp.App.Utils
{
    public static class ConfiguracaoUtil
    {
        public static void PreencherDadosConfiguracao(ConfiguracoesViewModel configuracoes, List<EstagioResumo> estagios, List<Capacitor> capacitores, List<Configuracao> dadosConfiguracao)
        {
            int quantidadeEstagiosFixos = (int)dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_FIXOS].Valor;
            int quantidadeEstagios = (int)dadosConfiguracao[Const.REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS].Valor;
            double tensaoLinha = dadosConfiguracao[Const.REGISTRO_TENSAO_LINHA].Valor;
            double relacaoTc = dadosConfiguracao[Const.REGISTRO_RELACAO_TC].Valor;

            configuracoes.EstagioResumo = estagios;
            configuracoes.Capacitores = capacitores;
            configuracoes.sQuantidadeEstagiosFixos = quantidadeEstagiosFixos;
            configuracoes.sQuantidadeEstagios = quantidadeEstagios;
            configuracoes.TensaoLinha = tensaoLinha;
            configuracoes.RelacaoTc = relacaoTc;
        }

        public static int DefinirQuantidadeColunasUltimaLinha(int quantidadeEstagios)
        {
            var colunas = (double)quantidadeEstagios % 4; // 0 == 4 colunas
            var quantidadeColunasUltimaLinha = 0;
            switch (colunas)
            {
                case 0.25:
                    quantidadeColunasUltimaLinha = 1;
                    break;
                case 0.50:
                    quantidadeColunasUltimaLinha = 2;
                    break;
                case 0.75:
                    quantidadeColunasUltimaLinha = 3;
                    break;
                case 0:
                    quantidadeColunasUltimaLinha = 4;
                    break;
                default:
                    quantidadeColunasUltimaLinha = (int)colunas;
                    break;
            }

            return quantidadeColunasUltimaLinha;
        }

        public static int DefinirQuantidadeLinhas(int quantidadeEstagios)
        {
            var linhas = (double)quantidadeEstagios / 4;
            var quantidadeLinhas = (int)Math.Ceiling(linhas);
            return quantidadeLinhas;
        }
    }
}
