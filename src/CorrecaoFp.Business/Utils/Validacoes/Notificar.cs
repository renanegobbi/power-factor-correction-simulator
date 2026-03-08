namespace CorrecaoFp.Business.Utils.Validacoes
{
    public static partial class Notificar
    {
        public static bool SeMaiorQue(this double numero, double numeroComparado)
        {
            return (numero > numeroComparado);
        }
    }
}
