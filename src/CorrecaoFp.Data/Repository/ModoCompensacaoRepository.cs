using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Data.Context;

namespace CorrecaoFp.Data.Repository
{
    public class ModoCompensacaoRepository : Repository<ModoCompensacao>, IModoCompensacaoRepository
    {
        public ModoCompensacaoRepository(ControladorFpContext context) : base(context) { }
    }
}
