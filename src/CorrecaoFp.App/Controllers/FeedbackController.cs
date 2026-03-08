using CorrecaoFp.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace CorrecaoFp.App.Controllers
{
    public class FeedbackController: Controller
    {
        [AllowAnonymous]
        [Route("feedback/{httpStatusCode:int}")]
        public IActionResult Feedback(HttpStatusCode httpStatusCode, Exception ex = null)

        {
            var urlLocal = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";

            var modelErro = new ErrorViewModel();
            modelErro.Excecao = ex;

            switch (httpStatusCode)
            {
                case HttpStatusCode.NotFound:
                    modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte"; ;
                    modelErro.Titulo = "Ops! Página não encontrada.";
                    modelErro.ErroCode = 404;
                    return View("Error", modelErro);
                case HttpStatusCode.Forbidden:
                    modelErro.Mensagem = "Você não tem permissão para acessar essa funcionalidade.";
                    modelErro.Titulo = "Acesso Negado";
                    modelErro.ErroCode = 403;
                    return View("Error", modelErro);
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.InternalServerError:
                    modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                    modelErro.Titulo = "Ocorreu um erro!";
                    modelErro.ErroCode = 500;
                    return View("Error", modelErro);
                case HttpStatusCode.Unauthorized:
                    modelErro.Mensagem = "Você precisa realizar seu login no Portal de Sistemas antes de acessar essa funcionalidade.";
                    modelErro.Titulo = "Acesso Negado";
                    modelErro.ErroCode = 401;
                    return View("Error", modelErro);
                default:
                    break;
            }

            return View("Error", modelErro);
        }
    }
}
