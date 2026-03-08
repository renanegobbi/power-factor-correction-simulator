namespace CorrecaoFp.Business.Comandos.Entrada.Configuracao
{
    public class AtualizarConfiguracaoEntrada
    {
        public double TensaoLinha { get; set; }
        public double RelacaoTc { get; set; }
        public int sQuantidadeEstagios { get; set; }

        public AtualizarConfiguracaoEntrada(double tensaoLinha, double relacaoTc, int sQuantidadeEstagios)
        {
            this.TensaoLinha = tensaoLinha;
            this.RelacaoTc = relacaoTc;
            this.sQuantidadeEstagios = sQuantidadeEstagios;
        }
    }
}
