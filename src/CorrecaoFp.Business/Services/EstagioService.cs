using CorrecaoFp.Business.Comandos.Entrada.Estagio;
using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Interfaces.Notificacoes;
using CorrecaoFp.Business.Interfaces.Services;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Business.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Services
{
    public class EstagioService : BaseService, IEstagioService
    {
        private readonly IEstagioRepository _estagioRepository;
        public EstagioService(IEstagioRepository estagioRepository,
                              INotificador notificador) : base(notificador)
        {
            _estagioRepository = estagioRepository;
        }

        public async Task<List<Estagio>> ObterTodosEstagios()
        {
            return await _estagioRepository.ObterTodos();
        }

        public async Task<List<EstagioResumo>> ObterTodosEstagiosResumo()
        {
            return await _estagioRepository.ObterEstagios();
        }

        public async Task AtualizarEstagios(List<Estagio> estagios, AtualizarEstagioEntrada entrada, int quantidadeEstagiosAutomaticosSelecionados)
        {
            AtualizarStatusAtivoEstagioAutomatico(estagios, quantidadeEstagiosAutomaticosSelecionados);

            var estagio1 = estagios.Where(e => e.Id == 1).FirstOrDefault();
            var estagio2 = estagios.Where(e => e.Id == 2).FirstOrDefault();
            var estagio3 = estagios.Where(e => e.Id == 3).FirstOrDefault();
            var estagio4 = estagios.Where(e => e.Id == 4).FirstOrDefault();
            var estagio5 = estagios.Where(e => e.Id == 5).FirstOrDefault();
            var estagio6 = estagios.Where(e => e.Id == 6).FirstOrDefault();
            var estagio7 = estagios.Where(e => e.Id == 7).FirstOrDefault();
            var estagio8 = estagios.Where(e => e.Id == 8).FirstOrDefault();
            var estagio9 = estagios.Where(e => e.Id == 9).FirstOrDefault();
            var estagio10 = estagios.Where(e => e.Id == 10).FirstOrDefault();
            var estagio11 = estagios.Where(e => e.Id == 11).FirstOrDefault();
            var estagio12 = estagios.Where(e => e.Id == 12).FirstOrDefault();
            var estagio13 = estagios.Where(e => e.Id == 13).FirstOrDefault();
            var estagio14 = estagios.Where(e => e.Id == 14).FirstOrDefault();
            var estagio15 = estagios.Where(e => e.Id == 15).FirstOrDefault();
            var estagio16 = estagios.Where(e => e.Id == 16).FirstOrDefault();
            var estagio17 = estagios.Where(e => e.Id == 17).FirstOrDefault();
            var estagio18 = estagios.Where(e => e.Id == 18).FirstOrDefault();
            var estagio19 = estagios.Where(e => e.Id == 19).FirstOrDefault();
            var estagio20 = estagios.Where(e => e.Id == 20).FirstOrDefault();
            var estagio21 = estagios.Where(e => e.Id == 21).FirstOrDefault();
            var estagio22 = estagios.Where(e => e.Id == 22).FirstOrDefault();
            var estagio23 = estagios.Where(e => e.Id == 23).FirstOrDefault();
            var estagio24 = estagios.Where(e => e.Id == 24).FirstOrDefault();
            var estagio25 = estagios.Where(e => e.Id == 25).FirstOrDefault();
            var estagio26 = estagios.Where(e => e.Id == 26).FirstOrDefault();
            var estagio27 = estagios.Where(e => e.Id == 27).FirstOrDefault();
            var estagio28 = estagios.Where(e => e.Id == 28).FirstOrDefault();

            if (estagio1.CapacitorId != entrada.IdCapacitorEstagio1)
            {
                estagio1.CapacitorId = entrada.IdCapacitorEstagio1;
                await _estagioRepository.Atualizar(estagio1);
            }

            if (estagio2.CapacitorId != entrada.IdCapacitorEstagio2)
            {
                estagio2.CapacitorId = entrada.IdCapacitorEstagio2;
                await _estagioRepository.Atualizar(estagio2);
            }

            if (estagio3.CapacitorId != entrada.IdCapacitorEstagio3)
            {
                estagio3.CapacitorId = entrada.IdCapacitorEstagio3;
                await _estagioRepository.Atualizar(estagio3);
            }

            if (estagio4.CapacitorId != entrada.IdCapacitorEstagio4)
            {
                estagio4.CapacitorId = entrada.IdCapacitorEstagio4;
                await _estagioRepository.Atualizar(estagio4);
            }

            if (estagio5.CapacitorId != entrada.IdCapacitorEstagio5)
            {
                estagio5.CapacitorId = entrada.IdCapacitorEstagio5;
                await _estagioRepository.Atualizar(estagio5);
            }

            if (estagio6.CapacitorId != entrada.IdCapacitorEstagio6)
            {
                estagio6.CapacitorId = entrada.IdCapacitorEstagio6;
                await _estagioRepository.Atualizar(estagio6);
            }

            if (estagio7.CapacitorId != entrada.IdCapacitorEstagio7)
            {
                estagio7.CapacitorId = entrada.IdCapacitorEstagio7;
                await _estagioRepository.Atualizar(estagio7);
            }

            if (estagio8.CapacitorId != entrada.IdCapacitorEstagio8)
            {
                estagio8.CapacitorId = entrada.IdCapacitorEstagio8;
                await _estagioRepository.Atualizar(estagio8);
            }

            if (estagio9.CapacitorId != entrada.IdCapacitorEstagio9)
            {
                estagio9.CapacitorId = entrada.IdCapacitorEstagio9;
                await _estagioRepository.Atualizar(estagio9);
            }

            if (estagio10.CapacitorId != entrada.IdCapacitorEstagio10)
            {
                estagio10.CapacitorId = entrada.IdCapacitorEstagio10;
                await _estagioRepository.Atualizar(estagio10);
            }

            if (estagio11.CapacitorId != entrada.IdCapacitorEstagio11)
            {
                estagio11.CapacitorId = entrada.IdCapacitorEstagio11;
                await _estagioRepository.Atualizar(estagio11);
            }

            if (estagio12.CapacitorId != entrada.IdCapacitorEstagio12)
            {
                estagio12.CapacitorId = entrada.IdCapacitorEstagio12;
                await _estagioRepository.Atualizar(estagio12);
            }

            if (estagio13.CapacitorId != entrada.IdCapacitorEstagio13)
            {
                estagio13.CapacitorId = entrada.IdCapacitorEstagio13;
                await _estagioRepository.Atualizar(estagio13);
            }

            if (estagio14.CapacitorId != entrada.IdCapacitorEstagio14)
            {
                estagio14.CapacitorId = entrada.IdCapacitorEstagio14;
                await _estagioRepository.Atualizar(estagio14);
            }

            if (estagio15.CapacitorId != entrada.IdCapacitorEstagio15)
            {
                estagio15.CapacitorId = entrada.IdCapacitorEstagio15;
                await _estagioRepository.Atualizar(estagio15);
            }

            if (estagio16.CapacitorId != entrada.IdCapacitorEstagio16)
            {
                estagio16.CapacitorId = entrada.IdCapacitorEstagio16;
                await _estagioRepository.Atualizar(estagio16);
            }

            if (estagio17.CapacitorId != entrada.IdCapacitorEstagio17)
            {
                estagio17.CapacitorId = entrada.IdCapacitorEstagio17;
                await _estagioRepository.Atualizar(estagio17);
            }

            if (estagio18.CapacitorId != entrada.IdCapacitorEstagio18)
            {
                estagio18.CapacitorId = entrada.IdCapacitorEstagio18;
                await _estagioRepository.Atualizar(estagio18);
            }

            if (estagio19.CapacitorId != entrada.IdCapacitorEstagio19)
            {
                estagio19.CapacitorId = entrada.IdCapacitorEstagio19;
                await _estagioRepository.Atualizar(estagio19);
            }

            if (estagio20.CapacitorId != entrada.IdCapacitorEstagio20)
            {
                estagio20.CapacitorId = entrada.IdCapacitorEstagio20;
                await _estagioRepository.Atualizar(estagio20);
            }

            if (estagio21.CapacitorId != entrada.IdCapacitorEstagio21)
            {
                estagio21.CapacitorId = entrada.IdCapacitorEstagio21;
                await _estagioRepository.Atualizar(estagio21);
            }

            if (estagio22.CapacitorId != entrada.IdCapacitorEstagio22)
            {
                estagio22.CapacitorId = entrada.IdCapacitorEstagio22;
                await _estagioRepository.Atualizar(estagio22);
            }

            if (estagio23.CapacitorId != entrada.IdCapacitorEstagio23)
            {
                estagio23.CapacitorId = entrada.IdCapacitorEstagio23;
                await _estagioRepository.Atualizar(estagio23);
            }

            if (estagio24.CapacitorId != entrada.IdCapacitorEstagio24)
            {
                estagio24.CapacitorId = entrada.IdCapacitorEstagio24;
                await _estagioRepository.Atualizar(estagio24);
            }

            if (estagio25.CapacitorId != entrada.IdCapacitorEstagioFixo1)
            {
                estagio25.CapacitorId = entrada.IdCapacitorEstagioFixo1;
                await _estagioRepository.Atualizar(estagio25);
            }

            if (estagio26.CapacitorId != entrada.IdCapacitorEstagioFixo2)
            {
                estagio26.CapacitorId = entrada.IdCapacitorEstagioFixo2;
                await _estagioRepository.Atualizar(estagio26);
            }

            if (estagio27.CapacitorId != entrada.IdCapacitorEstagioFixo3)
            {
                estagio27.CapacitorId = entrada.IdCapacitorEstagioFixo3;
                await _estagioRepository.Atualizar(estagio27);
            }

            if (estagio28.CapacitorId != entrada.IdCapacitorEstagioFixo4)
            {
                estagio28.CapacitorId = entrada.IdCapacitorEstagioFixo4;
                await _estagioRepository.Atualizar(estagio28);
            }
        }

        private async Task AtualizarStatusAtivoEstagioAutomatico(List<Estagio> estagios, int quantidadeEstagiosAutomaticosSelecionados)
        {

            var quantidadeEstagioAutomatico = Const.QUANTIDADE_ESTAGIO_AUTOMATICO;

            foreach (var estagio in estagios)
            {
                if (estagio.Id <= quantidadeEstagiosAutomaticosSelecionados)
                {
                    estagio.Ativo = true;
                    await _estagioRepository.Atualizar(estagio);
                }
                if (estagio.Id > quantidadeEstagiosAutomaticosSelecionados && estagio.Id <= quantidadeEstagioAutomatico)
                {
                    estagio.Ativo = false;
                    await _estagioRepository.Atualizar(estagio);
                }
            }
        }

        public void Dispose()
        {
            _estagioRepository?.Dispose(); ;
        }
    }
}
