using CorrecaoFp.Business.Models;

namespace CorrecaoFp.Business.Utils
{
    public static class Const
    {
        public const string Ativar = "Ativar";
        public const string Cap = "Cap";
        public const string Destivar = "Desativar";
        public const double FP_0_92 = 0.92;
        public const int PRIMEIRO_ELEMENTO_ARRAY = 0;
        public const string Ind = "Ind";
        public const int QuiloWatt = 1000;

        public const string DELETE_FROM_Medicao = "DELETE FROM Medicao";

        public const string ORDEM_ASCENDENTE = "ASC";
        public const string ORDEM_DESCENDENTE = "DESC";

        public const int QUANTIDADE_ESTAGIO_AUTOMATICO = 24;
        public const int QUANTIDADE_ESTAGIO_AUTOMATICO_DEFAULT = 12;
        public const int QUANTIDADE_ESTAGIO_FIXO_DEFAULT = 4;
        public const int ID_INICIO_ESTAGIO_FIXO = 25;

        public const int REGISTRO_QUANTIDADE_ESTAGIOS_FIXOS = 0;
        public const int REGISTRO_QUANTIDADE_ESTAGIOS_AUTOMATICOS = 1;
        public const int REGISTRO_TENSAO_LINHA = 2;
        public const int REGISTRO_RELACAO_TC = 3;

        public const string ESTAGIO_BANCO_FIXO = "ESTAGIO_BANCO_FIXO";
        public const string ESTAGIO_BANCO_AUTOMATICO = "ESTAGIO_BANCO_AUTOMATICO";
    }
}