using System;
using System.Collections.Generic;

namespace CorrecaoFp.Business.Utils
{
    public static class DisplayUtil
    {
        public const string DataEHora = "dd/MM/yyyy HH:mm:ss";

        public static string TrocarPontoPorVirgula(this string source)
        {
            return (string.IsNullOrEmpty(source))
                ? source : source.ToString().Replace(".", ",");
        }

        public static string TrocarPontoPorVirgula(this double source)
        {
            return source.ToString().Replace(".", ",");
        }

        public static string FormatarDataEHora(this DateTime source)
            => source.ToString(DataEHora);

        public static string FormatarEstagios(List<double> source)
        {
            var lista = new List<string>();

            foreach (var estagio in source)
                lista.Add(estagio + " [A]");

            return String.Join(" <i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"></i><br> ", lista);
        }


        public static string FormatarEstagios(List<KeyValuePair<double, string>> source)
        {
            var lista = new List<string>();

            foreach (var estagio in source)
                lista.Add(estagio.Value == Const.ESTAGIO_BANCO_FIXO ? estagio.Key + " [F]" : estagio.Key + " [A]");

            return String.Join(" <i class=\"fa fa-arrow-right fa-xs seta-direita\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"></i><br> ", lista);
        }

        public static string FormatarColunaAcao(string source)
            => (source == Const.Cap)
                ? Const.Destivar + "<br><i class=\"fa fa-angles-down fa-xs seta-baixo pl-0 pr-0\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"></i>"
                : Const.Ativar + "<br><i class=\"fa fa-angles-up fa-xs seta-cima pl-0 pr-0\" aria-hidden=\"true\" style=\"color:black; vertical-align: middle;\"></i>";
    }
}
