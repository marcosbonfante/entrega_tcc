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
    public class OrdemsController : MainController
    {

        private readonly IAspNetUser _user;
        private readonly IOrdemService _ordemService;

        public OrdemsController(IOrdemService ordemService, IAspNetUser user)
        {
            _ordemService = ordemService;
            _user = user;
        }

        public async Task<IActionResult> Atendimento()
        {
            var ordems = await _ordemService.ObterTodos();
            var ret = (from x in ordems where x.DataSolucao == null select x).ToList();
            return View(ret);
        }

        [HttpGet]
        public async Task<IActionResult> ResponderOrdem(string OrdemId)
        {
            if (!string.IsNullOrEmpty(OrdemId))
            {
                var ordem = await _ordemService.ObterPorId(Guid.Parse(OrdemId));
                return View(ordem);
            }
            return RedirectToAction("Atendimento", "Ordems");
        }

        [HttpPost]
        public async Task<IActionResult> ResponderOrdem(Models.OrdemViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            if (_ordemService != null)
            {
                model.DataSolucao = DateTime.Now;
                var response = await _ordemService.Atualizar(model);

                if (ResponsePossuiErros(response))
                {
                    TempData["Erros"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    return View(model);
                }
                return RedirectToAction("Atendimento", "Ordems");
            }
            else
            {
                AdicionarErroValidacao("Ordem de serviço não encontrada.");
            }
            return RedirectToAction("Atendimento", "Ordems");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var ordem = await _ordemService.ObterPorId(Guid.Parse(id));
            ViewData["ordem"] = ordem;
            return View();
        }


        public async Task<IActionResult> Encerrada()
        {
            var ordems = await _ordemService.ObterTodos();
            var ret = (from x in ordems where x.DataSolucao != null select x).ToList();
            return View(ret);
        }

    }
}
