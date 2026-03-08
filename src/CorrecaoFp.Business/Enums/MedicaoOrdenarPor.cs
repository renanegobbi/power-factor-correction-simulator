using System.ComponentModel;

namespace CorrecaoFp.Business.Enums
{
    public enum MedicaoOrdenarPor
    {
        [Description("Data Início")]
        DataInicio,

        [Description("Data Fim")]
        DataFim,

        [Description("Potência Ativa Total")]
        PotenciaAtivaTotal,

        [Description("Potência Reativa Total")]
        PotenciaReativaTotal,

        [Description("Potência Aparente Soma Vetorial")]
        PotenciaAparenteSomaVetorial,

        [Description("Fator Potência")]
        FatorPotencia,

        [Description("Ind Média")]
        IndMedia,

        [Description("Qc Necessário")]
        QcNecessario,

        [Description("Fator de Potência corrigido")]
        FatorPotenciaCorrigido,

        [Description("Estágios utilizados")]
        EstagiosUtilizados,

        [Description("Fator Potência")]
        FpRealMedia,

        [Description("Potência Aparente Soma Aritmética")]
        PotenciaAparenteAritmetica,

        [Description("Ação de ativar ou desativa o reativo")]
        Acao,

        [Description("Tipo de fator de potência corrigido")]
        IndFpCorrigido
    }
}