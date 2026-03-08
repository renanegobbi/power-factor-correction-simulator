using System.Collections.Generic;

namespace CorrecaoFp.Business.Models
{
    public class TipoPotencia: Entity
    {
        public string Sigla { get; set; }
        public string Descricao { get; set; }

        /* EF Relations */
        public virtual ICollection<Estagio> Estagios { get; set; }
    }
}