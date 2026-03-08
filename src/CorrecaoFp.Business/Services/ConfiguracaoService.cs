using CorrecaoFp.Business.Comandos.Entrada.Configuracao;
using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Interfaces.Notificacoes;
using CorrecaoFp.Business.Interfaces.Services;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Business.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Services
{
    public class ConfiguracaoService : BaseService, IConfiguracaoService
    {
        private readonly IConfiguracaoRepository _configuracaoRepository;
        public ConfiguracaoService(IConfiguracaoRepository configuracaoRepository,
                              INotificador notificador) : base(notificador)
        {
            _configuracaoRepository = configuracaoRepository;
        }

        public async Task AtualizarConfiguracao(Configuracao configuracao)
        {
            await _configuracaoRepository.Atualizar(configuracao);
        }

        public async Task<List<Configuracao>> ObterTodasConfiguracoes()
        {
            return await _configuracaoRepository.ObterTodos();
        }

        public async Task AtualizarConfiguracoes(List<Configuracao> configuracoes, AtualizarConfiguracaoEntrada entrada)
        {
            if (configuracoes[Const.REGISTRO_TENSAO_LINHA].Valor != entrada.TensaoLinha)
            {
                configuracoes[Const.REGISTRO_TENSAO_LINHA].Valor = entrada.TensaoLinha;
                await _configuracaoRepository.Atualizar(configuracoes[Const.REGISTRO_TENSAO_LINHA]);
            }

            if (configuracoes[Const.REGISTRO_RELACAO_TC].Valor != entrada.RelacaoTc)
            {
                configuracoes[Const.REGISTRO_RELACAO_TC].Valor = entrada.RelacaoTc;
                await _configuracaoRepository.Atualizar(configuracoes[Const.REGISTRO_RELACAO_TC]);
            }
        }

        public void Dispose()
        {
            _configuracaoRepository?.Dispose();
        }
    }
}
