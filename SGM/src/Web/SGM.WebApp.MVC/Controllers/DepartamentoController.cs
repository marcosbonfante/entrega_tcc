using Microsoft.AspNetCore.Mvc;
using SGM.WebApp.MVC.Extensions;
using SGM.WebApp.MVC.Models;
using SGM.WebApp.MVC.Services;
using SGM.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SGM.WebAPI.Core.Usuario;

namespace SGM.WebApp.MVC.Controllers
{
    public class DepartamentoController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly IOrdemService _ordemService;
        private readonly ISolicitacaoService _solicitacaoService;
        private readonly ICidadaoService _cidadaoService;

        public DepartamentoController(
            ISolicitacaoService solicitacaoService,
            ICidadaoService cidadaoService,
            IOrdemService ordemService,
            IAspNetUser user)
        {
            _solicitacaoService = solicitacaoService;
            _cidadaoService = cidadaoService;
            _ordemService = ordemService;
            _user = user;
        }

        #region AtenderSolicitacao


        [HttpGet]
        public async Task<IActionResult> AtenderSolicitacao()
        {
            var solicitacoes = await _solicitacaoService.ObterTodos(StatusSolicitacao.Pendente);
            return View(solicitacoes);
        }

        //[HttpPost]
        //public async Task<IActionResult> AtenderSolicitacao(string SolicitacaoId)
        //{
        //    if (!string.IsNullOrEmpty(SolicitacaoId))
        //    {
        //        var solicitacao = await _solicitacaoService.ObterPorId(Guid.Parse(SolicitacaoId));

        //        var result = await _solicitacaoService.Atualizar(new Models.SolicitacaoViewModel
        //        {
        //            Id = solicitacao.Id,
        //            IdSolicitante = solicitacao.IdSolicitante,
        //            CodDepartamento = solicitacao.CodDepartamento,
        //            Status = StatusSolicitacao.Atendimento,
        //            Descricao = solicitacao.Descricao,
        //            DataCadastro = solicitacao.DataCadastro,
        //            DataEncerramento = solicitacao.DataEncerramento
        //        });

        //        return RedirectToAction("AtenderSolicitacao", "Departamento");
        //    }

        //    return RedirectToAction("AtenderSolicitacao", "Departamento");
        //}


        [HttpGet]
        [Route("Departamento/Detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var solicitacao = await _solicitacaoService.ObterPorId(Guid.Parse(id));
            ViewData["solicitacao"] = solicitacao;
            ViewData["ordem"] = await _ordemService.ObterTodosSolicitacao(solicitacao.Id);
            ViewData["Cidadao"] = await _cidadaoService.ObterPorId(solicitacao.IdSolicitante);
            return View();
        }


        #region ResponderSolicitacao

        [HttpGet]
        public async Task<IActionResult> ResponderSolicitacao(string SolicitacaoId)
        {
            if (!string.IsNullOrEmpty(SolicitacaoId))
            {
                var solicitacao = await _solicitacaoService.ObterPorId(Guid.Parse(SolicitacaoId));
                ViewData["Cidadao"] = await _cidadaoService.ObterPorId(solicitacao.IdSolicitante);
                return View(solicitacao);
            }
            return RedirectToAction("AtenderSolicitacao", "Departamento");
        }

        [HttpPost]
        public async Task<IActionResult> ResponderSolicitacao(SolicitacaoViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var solicitacao = await _solicitacaoService.ObterPorId(model.Id);

            if (solicitacao != null)
            {
                OrdemViewModel ordem = new OrdemViewModel
                {
                    IdSolicitacao = solicitacao.Id,
                    IdSolicitante = _user.ObterUserId(),
                    CodDepartamento = solicitacao.CodDepartamento,
                    DataSolucao = DateTime.Now,
                    Solucao = model.Solucao,
                    Descricao = "Resposta a solicitação do cidadão"
                };

                var response = await _ordemService.Adicionar(ordem);

                if (ResponsePossuiErros(response))
                {
                    TempData["Erros"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    ViewData["Cidadao"] = await _cidadaoService.ObterPorId(solicitacao.IdSolicitante);
                    return View(model);
                }
                return RedirectToAction("AtenderSolicitacao", "Departamento");
            }
            else
            {
                AdicionarErroValidacao("Solicitação não encontrada.");
            }
            return RedirectToAction("AtenderSolicitacao", "Departamento");
        }


        #endregion


        #region GerarOrdem

        [HttpGet]
        public async Task<IActionResult> GerarOrdemServico(string SolicitacaoId)
        {
            if (!string.IsNullOrEmpty(SolicitacaoId))
            {
                ViewData["Departamentos"] = new SGM.Core.DomainObjects.Departamento().GetDepartamentos();
                ViewData["Solicitacao"] = await _solicitacaoService.ObterPorId(Guid.Parse(SolicitacaoId));

                var model = new OrdemViewModel
                {
                    IdSolicitacao = Guid.Parse(SolicitacaoId)
                };
                return View(model);
            }
            return RedirectToAction("AtenderSolicitacao", "Departamento");
        }

        [HttpPost]
        public async Task<IActionResult> GerarOrdemServico(OrdemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Departamentos"] = new SGM.Core.DomainObjects.Departamento().GetDepartamentos();
                ViewData["Solicitacao"] = await _solicitacaoService.ObterPorId((Guid)model.IdSolicitacao);

                return View(model);
            }

            OrdemViewModel ordem = new OrdemViewModel
            {
                IdSolicitacao = model.IdSolicitacao,
                IdSolicitante = _user.ObterUserId(),
                CodDepartamento = model.CodDepartamento,
                Descricao = model.Descricao,
                Solucao = string.Empty
            };

            var response = await _ordemService.Adicionar(ordem);

            if (ResponsePossuiErros(response))
            {
                TempData["Erros"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                ViewData["Departamentos"] = new SGM.Core.DomainObjects.Departamento().GetDepartamentos();
                ViewData["Solicitacao"] = await _solicitacaoService.ObterPorId((Guid)model.IdSolicitacao);
                return View(model);
            }
            return RedirectToAction("AtenderSolicitacao", "Departamento");
        }

        #endregion


        #endregion



        [HttpGet]
        public async Task<IActionResult> Atendimento()
        {
            var solicitacoes = await _solicitacaoService.ObterTodos(StatusSolicitacao.Atendimento);
            return View(solicitacoes);
        }

        [HttpGet]
        public async Task<IActionResult> Encerrada()
        {
            var solicitacoes = await _solicitacaoService.ObterTodos(StatusSolicitacao.Encerrada);
            return View(solicitacoes);
        }


    }
}