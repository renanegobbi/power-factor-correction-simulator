namespace CorrecaoFp.Business.Models
{
    public class EstagioResumo
    {
        public int Id { get; set; }
        public int TipoPotenciaId { get; set; }
        public int CapacitorId { get; set; }
        public string Descricao { get; set; }
        public string Fabricante { get; set; }
        public double Potencia { get; set; }
        public double Tensao { get; set; }
        public string Unidade { get; set; }

        public EstagioResumo()
        {
                
        }

        public EstagioResumo(int id, int tipoPotenciaId, int capacitorId, string descricao,
            string fabricante, double potencia, double tensao, string unidade)
        {
            this.Id = id;
            this.TipoPotenciaId = tipoPotenciaId;
            this.CapacitorId = capacitorId;
            this.Descricao = descricao;
            this.Fabricante = fabricante;
            this.Potencia = potencia;
            this.Tensao = tensao;
            this.Unidade = unidade;
        }
    }
}
