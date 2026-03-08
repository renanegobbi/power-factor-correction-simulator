using CorrecaoFp.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Interfaces
{
    public interface IEstagioRepository : IRepository<Estagio>
    {
        Task<List<EstagioResumo>> ObterEstagios();
    }
}
