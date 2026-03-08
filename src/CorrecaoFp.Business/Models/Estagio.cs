using System.ComponentModel.DataAnnotations;

namespace CorrecaoFp.Business.Models
{
    public class Estagio : Entity
    {
        public int TipoPotenciaId { get; set; }
        public int CapacitorId { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public virtual Capacitor Capacitor { get; set; }

        public virtual TipoPotencia TipoPotencia { get; set; }
    }
}