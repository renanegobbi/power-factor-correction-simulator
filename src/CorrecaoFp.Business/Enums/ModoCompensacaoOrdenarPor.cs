using System.ComponentModel;

namespace CorrecaoFp.Business.Enums
{
    public enum ModoCompensacaoOrdenarPor
    {
        [Description("Circular")]
        Circular = 5,

        [Description("Linear")]
        Linear = 4,

        [Description("Manual")]
        Manual = 6,

        [Description("PFW03-M12")]
        PFW03_M12 = 1,

        [Description("Sequencial Ascendente")]
        Sequencial_Ascendente = 2,

        [Description("Sequencial Descendente")]
        Sequencial_Descendente = 3,
    }
}