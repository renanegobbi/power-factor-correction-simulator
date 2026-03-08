using CorrecaoFp.Business.Enums;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Business.Utils;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorrecaoFp.App.Helpers
{
    public static class RazorHelper
    {
        public static string SelectOptionsModoCompensacao(this RazorPage page, List<ModoCompensacao> lista, string alvo)
        {
            var sb = new StringBuilder();
            foreach (var item in lista)
            {
                var selected = "";
                if (item.Nome == alvo) selected = "selected";
                sb.Append($"<option {selected} value='{item.Id}'>{item.Nome}</option>");
            }

            return sb.ToString();
        }

        public static string SelectOptionsModoCompensacao(this RazorPage page, List<ModoCompensacao> lista, Enum alvo)
        {
            lista = lista.OrderBy(l => l.Nome).ToList();

            var sb = new StringBuilder();
            foreach (var item in lista)
            {
                var selected = "";
                if (item.Id == (int)ModoCompensacaoOrdenarPor.Sequencial_Ascendente) selected = "selected";
                if (item.Id != (int)ModoCompensacaoOrdenarPor.Manual)
                {
                    sb.Append($"<option {selected} value='{item.Id}'>{item.Nome}</option>");
                }
            }

            return sb.ToString();
        }

        public static string SelectOptionsModoCompensacaoComBancoCapacitor(this RazorPage page, List<ModoCompensacao> lista, Enum alvo)
        {
            lista = lista.OrderBy(l => l.Nome).ToList();
            var optionsDesabilitados = ObterOptionsModoCompesancaoDesabilitado();

            var sb = new StringBuilder();
            foreach (var item in lista)
            {
                var selected = "";

                if (optionsDesabilitados.Contains(item.Id))
                {
                    sb.Append($"<option {selected} disabled value='{item.Id}'>{item.Nome}</option>");
                    continue;
                }

                if (item.Id == (int)ModoCompensacaoOrdenarPor.Sequencial_Ascendente) selected = "selected";
                {
                    sb.Append($"<option {selected} value='{item.Id}'>{item.Nome}</option>");
                }
            }

            return sb.ToString();
        }

        private static List<int> ObterOptionsModoCompesancaoDesabilitado()
        {
            var selectOptionsDesabilitados = new List<int>() 
            { 
                (int)ModoCompensacaoOrdenarPor.Circular,
                (int)ModoCompensacaoOrdenarPor.Linear,
                (int)ModoCompensacaoOrdenarPor.Manual
            };

            return selectOptionsDesabilitados;
        }

        public static string SelectOptionsModoCompensacaoSemBancoCapacitor(this RazorPage page, List<ModoCompensacao> lista, Enum alvo)
        {
            lista = lista.OrderBy(l => l.Nome).ToList();

            var sb = new StringBuilder();
            foreach (var item in lista)
            {
                var selected = "";
                if (item.Id == (int)ModoCompensacaoOrdenarPor.Sequencial_Ascendente) selected = "selected";
                if (item.Id != (int)ModoCompensacaoOrdenarPor.Manual && item.Id != (int)ModoCompensacaoOrdenarPor.Linear && item.Id != (int)ModoCompensacaoOrdenarPor.Circular)
                {
                    sb.Append($"<option {selected} value='{item.Id}'>{item.Nome}</option>");
                }
            }

            return sb.ToString();
        }

        public static string SelectOptionsEstagioResumo(this RazorPage page, List<EstagioResumo> lista, int alvo)
        {
            lista = lista.OrderBy(l => l.Potencia).ToList();

            var sb = new StringBuilder();
            foreach (var item in lista)
            {
                var selected = "";
                if (item.Id == alvo) selected = "selected";
                sb.Append($"<option {selected} value='{item.CapacitorId}'>{item.Potencia}</option>");
            }

            return sb.ToString();
        }

        public static string SelectOptionsEstagioResumo(this RazorPage page, List<EstagioResumo> lista, int alvo, List<Capacitor> capacitores)
        {
            var estagio = lista.Where(l => l.Id == alvo).FirstOrDefault();

            var sb = new StringBuilder();
            foreach (var capacior in capacitores)
            {
                var selected = "";
                if (estagio != null) { if (estagio.CapacitorId == capacior.Id) selected = "selected"; }

                sb.Append($"<option {selected} value='{capacior.Id}'>{capacior.Potencia}</option>");
            }

            return sb.ToString();
        }

        public static string ListarEstagios(this RazorPage page, int quantidadeLinhas, int quantidadeColunasUltimaLinha, List<EstagioResumo> lista, List<Capacitor> capacitores)
        {
            var sb = new StringBuilder();

            string inicioDaLinha = "<div class=\"row pl-3 pr-3 pb-0 pt-0\">";
            string finalDaLinha = "</div>";

            var idSelect = 0;

            sb.Append(inicioDaLinha);

            for (int linha = 1; linha <= quantidadeLinhas; linha++)
            {
                if (linha == quantidadeLinhas)
                {
                    for (int coluna = 1; coluna <= quantidadeColunasUltimaLinha; coluna++)
                    {
                        idSelect = ObterIdSelect(linha, coluna, idSelect);

                        sb.Append(AdicionarSelectEstagio(page, idSelect, lista, capacitores));
                    }
                }
                else
                {
                    for (int coluna = 1; coluna <= 4; coluna++)
                    {
                        idSelect = ObterIdSelect(linha, coluna, idSelect);

                        sb.Append(AdicionarSelectEstagio(page, idSelect, lista, capacitores));
                    }
                }
            }

            sb.Append(finalDaLinha);

            return sb.ToString();
        }

        public static string ListarEstagiosFixos(this RazorPage page, int quantidadeLinhas, int quantidadeColunasUltimaLinha, List<EstagioResumo> lista, List<Capacitor> capacitores)
        {
            var sb = new StringBuilder();

            string inicioDaLinha = "<div class=\"row pl-3 pr-3 pb-0 pt-0\">";
            string finalDaLinha = "</div>";

            int idSelect = Const.ID_INICIO_ESTAGIO_FIXO;
            int idEstagioBD = Const.ID_INICIO_ESTAGIO_FIXO;

            sb.Append(inicioDaLinha);

            for (int linha = 1; linha <= quantidadeLinhas; linha++)
            {
                if (linha == quantidadeLinhas)
                {
                    for (int coluna = 1; coluna <= quantidadeColunasUltimaLinha; coluna++)
                    {
                        idSelect = ObterIdSelectEstagioFixo(linha, coluna, idSelect);

                        sb.Append(AdicionarSelectEstagioFixo(page, idSelect, lista, capacitores, idEstagioBD));

                        idEstagioBD++;
                    }
                }
                else
                {
                    for (int coluna = 1; coluna <= 4; coluna++)
                    {
                        idSelect = ObterIdSelectEstagioFixo(linha, coluna, idSelect);

                        sb.Append(AdicionarSelectEstagioFixo(page, idSelect, lista, capacitores, idEstagioBD));

                        idEstagioBD++;
                    }
                }
            }

            sb.Append(finalDaLinha);

            return sb.ToString();
        }

        private static int ObterIdSelect(int linha, int coluna, int idSelect)
        {
            if (linha == 1) { idSelect = coluna; }
            if (linha == 2) { idSelect = coluna + 4; }
            if (linha == 3) { idSelect = coluna + 8; }
            if (linha == 4) { idSelect = coluna + 12; }
            if (linha == 5) { idSelect = coluna + 16; }
            if (linha == 6) { idSelect = coluna + 20; }

            return idSelect;
        }

        private static int ObterIdSelectEstagioFixo(int linha, int coluna, int idSelect)
        {
            if (linha == 1) { idSelect = coluna; }
            if (linha == 2) { idSelect = idSelect + 4; }
            if (linha == 3) { idSelect = idSelect + 8; }
            if (linha == 4) { idSelect = idSelect + 12; }
            if (linha == 5) { idSelect = idSelect + 16; }
            if (linha == 6) { idSelect = idSelect + 20; }

            return idSelect;
        }

        public static string AdicionarSelectEstagio(RazorPage page, int id, List<EstagioResumo> estagioResumo, List<Capacitor> capacitores)
        {
            var sb = new StringBuilder();

            string inicioDaColuna = "<div class=\"col-sm-12 col-md-3\">";
            string inicioFormGroup = "<div class=\"form-group\">";
            string inicioLabel = $"<label class=\"control-label\">Estágio {id}";
            string inicioSelect = $"<select id=\"iSelectEstagio{id}\" name=\"IdCapacitorEstagio{id}\" class=\"form-control\" style=\"width:100%;\">";
            string finalSelect = "</select>";
            string finalLabel = "</label>";
            string finalFormGroup = "</div>";
            string finalDaColuna = "</div>";

            sb.Append(inicioDaColuna);
            sb.Append(inicioFormGroup);
            sb.Append(inicioLabel);
            sb.Append(finalLabel);
            sb.Append(inicioSelect);
            sb.Append(SelectOptionsEstagioResumo(page, estagioResumo, id, capacitores));
            sb.Append(finalSelect);
            sb.Append(finalFormGroup);
            sb.Append(finalDaColuna);

            return sb.ToString();
        }

        public static string AdicionarSelectEstagioFixo(RazorPage page, int id, List<EstagioResumo> estagioResumo, List<Capacitor> capacitores, int idEstagioBD)
        {
            var sb = new StringBuilder();

            string inicioDaColuna = "<div class=\"col-sm-12 col-md-3\">";
            string inicioFormGroup = "<div class=\"form-group\">";
            string inicioLabel = $"<label class=\"control-label\">Estágio {id}";
            string inicioSelect = $"<select id=\"iSelectEstagioFixo{id}\" name=\"IdCapacitorEstagioFixo{id}\" class=\"form-control\" style=\"width:100%;\">";
            string finalSelect = "</select>";
            string finalLabel = "</label>";
            string finalFormGroup = "</div>";
            string finalDaColuna = "</div>";

            sb.Append(inicioDaColuna);
            sb.Append(inicioFormGroup);
            sb.Append(inicioLabel);
            sb.Append(finalLabel);
            sb.Append(inicioSelect);
            sb.Append(SelectOptionsEstagioResumo(page, estagioResumo, idEstagioBD, capacitores));
            sb.Append(finalSelect);
            sb.Append(finalFormGroup);
            sb.Append(finalDaColuna);

            return sb.ToString();
        }
    }
}



