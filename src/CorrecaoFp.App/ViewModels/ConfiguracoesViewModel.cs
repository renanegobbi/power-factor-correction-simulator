using CorrecaoFp.Business.Models;
using System.Collections.Generic;

namespace CorrecaoFp.App.ViewModels
{
    public class ConfiguracoesViewModel
    {
        public List<Estagio> Estagios = new List<Estagio>();

        public List<EstagioResumo> EstagioResumo = new List<EstagioResumo>();

        public List<Capacitor> Capacitores = new List<Capacitor>();

        public int sQuantidadeEstagios { get; set; }
        public int sQuantidadeEstagiosFixos { get; set; }
        public double TensaoLinha { get; set; }
        public double RelacaoTc { get; set; }
        public double RelacaoCk { get; set; }
        public int QuantidadeLinhas { get; set; }
        public int QuantidadeColunasUltimaLinha { get; set; }
    }
}
