using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGM.Core.DomainObjects;
using SGM.Solicitacao.API.Models;
using SGM.Core.Mediator;
using SGM.WebAPI.Core.Controllers;
using SGM.Solicitacao.API.Application.Commands;

namespace SGM.Solicitacao.API.Controllers
{
    [Authorize]
    public class SolicitacaoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly ISolicitacaoRepository _solicitacaoRepository;

        public SolicitacaoController(
            ISolicitacaoRepository solicitacaoRepository,
            IMediatorHandler mediatorHandler)
        {
            _mediator = mediatorHandler;
            _solicitacaoRepository = solicitacaoRepository;
        }


        [HttpPost("/solicitacao/nova-solicitacao")]
        public async Task<ActionResult> NovaSolicitacao(Models.Solicitacao solicitacao)
        {
            if (!ModelState.IsValid) return CustomResponse();

            solicitacao.Id = Guid.NewGuid();
            solicitacao.DataCadastro = DateTime.Now;
            solicitacao.Status = StatusSolicitacao.Pendente;

            return CustomResponse(await _mediator.EnviarComando(
                new RegistrarSolicitacaoCommand(
                    solicitacao.Id, solicitacao.IdSolicitante, solicitacao.Descricao, solicitacao.CodDepartamento,
                    solicitacao.Status, solicitacao.DataCadastro)));
        }

        //[ClaimsAuthorize("Cidadao", "Ler")]
        [HttpGet("/solicitacao/solicitacao-user/{id}/{status}")]
        [HttpGet("/solicitacao/solicitacao-user/{id}/")]
        public async Task<IEnumerable<Models.Solicitacao>> SolicitacaoCidadao(Guid id, string status)
        {
            return await _solicitacaoRepository.ObterTodosCidadao(id, status);
        }

        [HttpGet("/solicitacao/solicitacao-geral/")]
        [HttpGet("/solicitacao/solicitacao-geral/{status}")]
        public async Task<IEnumerable<Models.Solicitacao>> SolicitacaoTodas(string status)
        {
            return await _solicitacaoRepository.ObterTodos(status);
        }

        [HttpGet("/solicitacao/solicitacao/{id}")]
        public async Task<Models.Solicitacao> SolicitacaoPorId(Guid id)
        {
            return await _solicitacaoRepository.ObterPorId(id);
        }


        [HttpPost("solicitacao/atualizar-solicitacao")]
        public async Task<ActionResult> UpdateSolicitacao(Models.Solicitacao solicitacao)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var solicitacaoExistente = await _solicitacaoRepository.ObterPorId(solicitacao.Id);

            if (solicitacaoExistente == null)
            {
                AdicionarErroProcessamento("Solicitação não localizada.");
                return CustomResponse();
            }

            var resultado = await _mediator.EnviarComando(
                new AtualizarSolicitacaoCommand(
                    solicitacao.Id, solicitacaoExistente.IdSolicitante, solicitacao.CodDepartamento,
                    solicitacaoExistente.Descricao, solicitacao.Status,
                    solicitacaoExistente.DataCadastro, solicitacao.DataEncerramento));

            return CustomResponse(resultado);

        }

    }
}