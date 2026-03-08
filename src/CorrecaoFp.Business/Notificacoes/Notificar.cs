namespace CorrecaoFp.Business.Notificacoes
{
    public static class Notificar
    {
        public static bool SeMaiorQue(this double numero, double numeroComparado)
        {
            return (numero > numeroComparado);
        }
    }
}
