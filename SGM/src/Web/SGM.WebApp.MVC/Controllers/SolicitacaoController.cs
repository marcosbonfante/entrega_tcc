using Microsoft.AspNetCore.Mvc;
using SGM.WebAPI.Core.Usuario;
using SGM.WebApp.MVC.Extensions;
using SGM.WebApp.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGM.WebApp.MVC.Controllers
{
    public class SolicitacaoController : MainController
    {
        private readonly ISolicitacaoService _solicitacaoService;
        private readonly IOrdemService _ordemService;
        private readonly IAspNetUser _user;

        public SolicitacaoController(ISolicitacaoService solicitacaoService, IOrdemService orderService, IAspNetUser user)
        {
            _solicitacaoService = solicitacaoService;
            _ordemService = orderService;
            _user = user;
        }

        [HttpGet]
        public IActionResult NovaSolicitacao()
        {
            //var model = new Models.SolicitacaoViewModel();
            //ViewData["Departamentos"] = new SGM.Core.DomainObjects.Departamento().GetDepartamentos();
            return View(new Models.SolicitacaoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> NovaSolicitacao(Models.SolicitacaoViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            model.IdSolicitante = _user.ObterUserId();
            model.CodDepartamento = "ATENDIMENTO";
            var response = await _solicitacaoService.Adicionar(model);

            if (ResponsePossuiErros(response.ResponseResult)) return View(response);

            return RedirectToAction("Solicitacoes", "Solicitacao");
        }

        [HttpGet]
        public async Task<IActionResult> Solicitacoes()
        {
            //var a = _user.ObterUserId();
            //var b = _user.ObterUserEmail();
            //var c = _user.ObterUserToken();
            //var d = _user.ObterUserRefreshToken();


            var solicitacoes = await _solicitacaoService.ObterTodosCidadao(_user.ObterUserId(), null);
            return View(solicitacoes);
        }

        [HttpGet]
        [Route("Solicitacao/Detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            ViewData["solicitacao"] = await _solicitacaoService.ObterPorId(Guid.Parse(id));
            ViewData["ordem"] = await _ordemService.ObterTodosSolicitacao(Guid.Parse(id));
            return View();
        }

    }
}
