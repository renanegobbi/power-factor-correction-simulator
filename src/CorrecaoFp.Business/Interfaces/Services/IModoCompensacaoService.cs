using CorrecaoFp.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Interfaces.Services
{
    public interface IModoCompensacaoService: IDisposable
    {
        Task<List<ModoCompensacao>> ObterTodosModoCompensacao();
    }
}