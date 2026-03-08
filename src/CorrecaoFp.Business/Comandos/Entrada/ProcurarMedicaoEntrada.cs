using CorrecaoFp.Business.Enums;
using CorrecaoFp.Business.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace CorrecaoFp.Business.Comandos.Entrada
{
    public class ProcurarMedicaoEntrada
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public double? PotenciaAtivaTotal { get; set; }
        public double? PotenciaReativaTotal { get; set; }
        public double? PotenciaAparenteAritmetica { get; set; }
        public double? FpRealMedia { get; set; }
        public double? EstagiosNecessarios { get; set; }

        public List<Medicao> Medicoes = new List<Medicao>();
        public IndMedia IndMedia { get; set; }
        public IndMedia IndMediaFPCorrigido { get; set; }
        public OperacaoEstagio Acao { get; set; }
        public int? PaginaIndex { get; }
        public int? PaginaTamanho { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MedicaoOrdenarPor? OrdenarPor { get; }

        public string OrdenarSentido { get; }

        public ProcurarMedicaoEntrada(MedicaoOrdenarPor? ordenarPor = MedicaoOrdenarPor.DataInicio, string ordenarSentido = "ASC", int? paginaIndex = null, int? paginaTamanho = null)
        {
            this.OrdenarPor = ordenarPor;
            this.OrdenarSentido = ordenarSentido;
            this.PaginaIndex = paginaIndex;
            this.PaginaTamanho = paginaTamanho;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
