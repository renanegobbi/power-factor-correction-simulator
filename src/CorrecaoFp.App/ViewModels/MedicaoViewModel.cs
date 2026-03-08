using CorrecaoFp.Business.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CorrecaoFp.App.ViewModels
{
    public class MedicaoViewModel
    {
        public List<Medicao> Medicoes { get; set; }

        public List<SelectListItem> ModosCompensacao = new List<SelectListItem>();
        public List<ModoCompensacao> ModoCompensacao = new List<ModoCompensacao>();
    }
}
