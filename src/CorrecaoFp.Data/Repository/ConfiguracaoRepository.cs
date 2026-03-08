using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Data.Repository
{
    public class ConfiguracaoRepository : Repository<Configuracao>, IConfiguracaoRepository
    {
        public ConfiguracaoRepository(ControladorFpContext context) : base(context) { }
    }
}