using CorrecaoFp.Business.Comandos.Entrada;
using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Interfaces.Notificacoes;
using CorrecaoFp.Business.Interfaces.Services;
using CorrecaoFp.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Services
{
    public class MedicaoService : BaseService, IMedicaoService
    {
        private readonly IMedicaoRepository _medicaoRepository;
        public MedicaoService(IMedicaoRepository medicaoRepository,
                              INotificador notificador) : base(notificador)
        {
            _medicaoRepository = medicaoRepository;
        }

        public async Task<List<Medicao>> ObterTodasMedicoes()
        {
            return await _medicaoRepository.ObterTodos();
        }

        public async Task<Tuple<Medicao[], double>> ProcurarMedicaoReativoExcedente(ProcurarMedicaoEntrada entrada)
        {
            return await _medicaoRepository.ProcurarMedicaoReativoExcedente(entrada);
        }

        public async Task<Tuple<Medicao[], double>> ProcurarMedicao(ProcurarMedicaoEntrada entrada)
        {
            return await _medicaoRepository.ProcurarMedicao(entrada);
        }

        public async Task Adicionar(List<Medicao> model)
        {
            await _medicaoRepository.Adicionar(model);
        }

        public void Dispose()
        {
            _medicaoRepository?.Dispose();
        }
    }
}
