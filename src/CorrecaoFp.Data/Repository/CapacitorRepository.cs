using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Data.Context;

namespace CorrecaoFp.Data.Repository
{
    public class CapacitorRepository : Repository<Capacitor>, ICapacitorRepository
    {
        public CapacitorRepository(ControladorFpContext context) : base(context) { }
    }
}