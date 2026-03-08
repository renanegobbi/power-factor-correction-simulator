using System;

namespace CorrecaoFp.Business.Utils
{
    public static class MathUtil
    {
        public static double ObterValorAbsoluto(this double valor)
        {
            return Math.Abs(valor);
        }

        public static double DividirPorMil(this double valor)
        {
            return (valor / 1000);
        }

        public static double ArredondarDuasCasasDecimais(this double valor)
        {
            return Math.Round(valor, 2);
        }

        public static double ArredondarTresCasasDecimais(this double valor)
        {
            return Math.Round(valor, 3);
        }
    }
}
