using CorrecaoFp.Business.Notificacoes;
using System.Collections.Generic;

namespace CorrecaoFp.Business.Interfaces.Notificacoes
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}