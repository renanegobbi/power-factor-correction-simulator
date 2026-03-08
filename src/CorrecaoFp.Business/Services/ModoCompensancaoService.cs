using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Interfaces.Notificacoes;
using CorrecaoFp.Business.Interfaces.Services;
using CorrecaoFp.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Services
{
    public class ModoCompensacaoService : BaseService, IModoCompensacaoService
    {
        private readonly IModoCompensacaoRepository _modoCompensacaoRepository;
        public ModoCompensacaoService(IModoCompensacaoRepository modoCompensacaoRepository,
                              INotificador notificador) : base(notificador)
        {
            _modoCompensacaoRepository = modoCompensacaoRepository;
        }

        public async Task<List<ModoCompensacao>> ObterTodosModoCompensacao()
        {
            return await _modoCompensacaoRepository.ObterTodos();
        }

        public void Dispose()
        {
            _modoCompensacaoRepository?.Dispose();
        }
    }
}
