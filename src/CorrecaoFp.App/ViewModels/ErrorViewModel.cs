using System;

namespace CorrecaoFp.App.ViewModels
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public Exception Excecao { get; set; }
    }
}
