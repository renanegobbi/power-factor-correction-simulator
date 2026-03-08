using CorrecaoFp.Business.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorrecaoFp.Business.Models
{
    public class Medicao : Entity
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public double PotenciaAtiva { get; set; }
        public double PotenciaReativa { get; set; }
        public double PotenciaAparente { get; set; }
        public double FatorPotencia { get; set; }
        public string TipoFatorPotencia { get; set; }

        [NotMapped]
        public double QcNecessario { get; set; } = 0;

        [NotMapped]
        public List<double> EstagiosNecessarios = new List<double>();

        [NotMapped]
        public double FpFinal { get; set; }

        [NotMapped]
        public bool FpCorrigido { get; set; }

        [NotMapped]
        public string TipoFatorPotenciaCorrigido { get; set; }
    }
}
