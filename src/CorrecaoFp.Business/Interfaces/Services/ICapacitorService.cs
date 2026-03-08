using CorrecaoFp.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Interfaces.Services
{
    public interface ICapacitorService: IDisposable
    {
        Task<List<Capacitor>> ObterTodosCapacitores();

    }
}
