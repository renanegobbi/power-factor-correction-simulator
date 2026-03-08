using System.ComponentModel.DataAnnotations;

namespace CorrecaoFp.Business.Models
{
    public class TipoEstagio : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}