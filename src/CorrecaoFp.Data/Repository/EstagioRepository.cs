using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorrecaoFp.Data.Repository
{
    public class EstagioRepository : Repository<Estagio>, IEstagioRepository
    {
        public EstagioRepository(ControladorFpContext context) : base(context) { }

        public async Task<List<EstagioResumo>> ObterEstagios()
        {
            var registros = Db.Estagios
                .AsNoTracking()
                .Include(x => x.Capacitor);

            return await registros.Select(e => new EstagioResumo(e.Id, e.TipoPotenciaId, e.CapacitorId,
                e.Descricao, e.Capacitor.Fabricante, e.Capacitor.Potencia, e.Capacitor.Tensao, e.Capacitor.Unidade)).ToListAsync();
        }
    }
}