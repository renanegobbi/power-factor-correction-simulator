using CorrecaoFp.Business.Comandos.Entrada.Configuracao;
using CorrecaoFp.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Interfaces.Services
{
    public interface IConfiguracaoService: IDisposable
    {
        Task<List<Configuracao>> ObterTodasConfiguracoes();

        Task AtualizarConfiguracao(Configuracao configuracao);

        Task AtualizarConfiguracoes(List<Configuracao> configuracoes, AtualizarConfiguracaoEntrada entrada);
    }
}