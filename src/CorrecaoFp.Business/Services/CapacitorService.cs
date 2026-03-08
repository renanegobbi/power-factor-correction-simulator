using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Interfaces.Notificacoes;
using CorrecaoFp.Business.Interfaces.Services;
using CorrecaoFp.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Services
{
    public class CapacitorService : BaseService, ICapacitorService
    {
        private readonly ICapacitorRepository _capacitorRepository;
        public CapacitorService(ICapacitorRepository capacitorRepository,
                                INotificador notificador) : base(notificador)
        {
            _capacitorRepository = capacitorRepository;
        }

        public async Task<List<Capacitor>> ObterTodosCapacitores()
        {
            return await _capacitorRepository.ObterTodos();
        }

        public void Dispose()
        {
            _capacitorRepository?.Dispose(); ;
        }
    }
}
