using System.Collections.Generic;

namespace CorrecaoFp.Business.Models
{
    public class Capacitor: Entity
    {
        public string Fabricante { get; set; }
        public double Potencia { get; set; }
        public double Tensao { get; set; }
        public string Unidade { get; set; }

        /* EF Relations */
        public virtual ICollection<Estagio> Estagios { get; set; }
    }
}
