using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CorrecaoFp.App.Controllers
{
    public class DocumentacaoController : Controller
    {
        private readonly ILogger<ConfiguracaoController> _logger;
        public DocumentacaoController(
            ILogger<ConfiguracaoController> logger
            )
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
