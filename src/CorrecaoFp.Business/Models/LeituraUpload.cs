using System.Collections.Generic;

namespace CorrecaoFp.Business.Models
{
    public class LeituraUpload
    {
        public List<Medicao> Medicoes { get; set; }
        public List<string> Erros { get; set; }

        public LeituraUpload()
        {
            Medicoes = new List<Medicao>();
            Erros = new List<string>();
        }
    }
}
