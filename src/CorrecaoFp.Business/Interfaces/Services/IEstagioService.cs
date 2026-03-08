using CorrecaoFp.Business.Comandos.Entrada.Estagio;
using CorrecaoFp.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Interfaces.Services
{
    public interface IEstagioService : IDisposable
    {
        Task<List<Estagio>> ObterTodosEstagios();

        Task<List<EstagioResumo>> ObterTodosEstagiosResumo();

        Task AtualizarEstagios(List<Estagio> estagios, AtualizarEstagioEntrada entrada, int quantidadeEstagiosAutomaticosSelecionados);
    }
}
